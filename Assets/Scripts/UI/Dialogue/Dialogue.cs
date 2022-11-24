using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{
    public static Dialogue instance;

    public TMP_Text dialogueText;
    public DialogueData diaData;

    public GameObject labelHolder;

    string currentSpeaker;

    public bool isOpen { get; private set; }

    ResponseHandler handler;
    Typewriter writer;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        handler = GetComponent<ResponseHandler>();
        writer = GetComponent<Typewriter>();
    }

    public void CallDialogue(DialogueData data)
    {
        isOpen = true;
        labelHolder.SetActive(true);
        if(Movement.instance != null) Movement.instance.control = false;
        StartCoroutine(StepDialogue(data));
    }

    IEnumerator StepDialogue(DialogueData data)
    {
        for (int i = 0; i < data.data.Count; i++)
        {
            DialogueBase dialogue = data.data[i];

            yield return RunTypingEffect(dialogue);

            dialogueText.text = dialogue.dialogue;

            if (i == data.data.Count - 1 && data.HasResponses) break;

            yield return null;
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.E));
        }

        if (data.HasResponses)
        {
            handler.ShowResponses(data.responses.ToArray());
        }
        else
        {
            CloseDialogue();
        }

    }

    IEnumerator RunTypingEffect(DialogueBase dialogue)
    {
        writer.Run(dialogue, dialogueText);

        while (writer.isRunning)
        {
            yield return null;

            if (Input.GetKeyDown(KeyCode.E))
            {
                writer.Stop();
            }
        }
    }

    void CloseDialogue()
    {
        isOpen = false;
        labelHolder.SetActive(false);
        if (Movement.instance != null) Movement.instance.control = true;
        dialogueText.text = string.Empty;
    }
}
