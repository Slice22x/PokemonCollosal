using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Typewriter : MonoBehaviour
{
    [SerializeField] float typeSpeed;

    public bool isRunning { get; private set; }

    readonly List<Punctuation> punctuations = new List<Punctuation>()
    {
        new Punctuation(new HashSet<char>(){'.', '!', '?'}, 0.6f),
        new Punctuation(new HashSet<char>(){',', ';', ':'}, 0.3f)
    };

    Coroutine typingCoroutine;

    public void Run(DialogueBase data, TMP_Text textLabel)
    {
        typingCoroutine =  StartCoroutine(TypeText(data, textLabel));
    }

    public void Stop()
    {
        StopCoroutine(typingCoroutine);
        isRunning = false;
    }

    IEnumerator TypeText(DialogueBase data, TMP_Text textLabel)
    {
        isRunning = true;

        textLabel.text = string.Empty;

        float t = 0;
        int charIndex = 0;
        string currentText = data.dialogue;

        while (charIndex < currentText.Length)
        {
            int lastCharIndex = charIndex;

            t += Time.deltaTime * typeSpeed;

            charIndex = Mathf.FloorToInt(t);
            charIndex = Mathf.Clamp(charIndex, 0, currentText.Length);

            for (int i = lastCharIndex; i < charIndex; i++)
            {
                bool isLast = i >= currentText.Length - 1;

                textLabel.text = currentText.Substring(0, i + 1);

                if(IsPunctuation(currentText[i], out float waitTime) && !isLast && !IsPunctuation(currentText[i + 1], out _))
                {
                    yield return new WaitForSeconds(waitTime);
                }
            }


            yield return null;
        }

        isRunning = false;
    }

    bool IsPunctuation(char c, out float waitTime)
    {
        foreach (Punctuation p in punctuations)
        {
            if (p.Punctuations.Contains(c))
            {
                waitTime = p.WaitTime;
                return true;
            }
        }

        waitTime = default;
        return false;
    }

    readonly struct Punctuation
    {
        public readonly HashSet<char> Punctuations;
        public readonly float WaitTime;

        public Punctuation(HashSet<char> punctuations, float waitTime)
        {
            Punctuations = punctuations;
            WaitTime = waitTime;
        }
    }
}
