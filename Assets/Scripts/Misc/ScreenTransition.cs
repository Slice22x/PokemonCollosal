using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreenTransition : MonoBehaviour
{
    public Animator anim;
    public static ScreenTransition instance;

    public delegate void DoneTrans();
    public static DoneTrans done;

    void Awake()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CallDone()
    {
        done?.Invoke();
    }

    public static void CallLevel(string SceneName)
    {
        instance.StartCoroutine(instance.LoadLevel(SceneName));
    }

    public static void CallLevel(int SceneIndex)
    {
        instance.StartCoroutine(instance.LoadLevel(SceneIndex));
    }

    public IEnumerator LoadLevel(string SceneName)
    {
        //SoundManager.StopAll();
        anim.SetTrigger("Start");

        yield return new WaitForSeconds(1);

        //SaveSystem.SaveData(Info);

        SceneManager.LoadScene(SceneName);
    }

    public IEnumerator LoadLevel(int SceneIndex)
    {
        //SoundManager.StopAll();
        anim.SetTrigger("Start");

        yield return new WaitForSeconds(1);

        //SaveSystem.SaveData(Info);

        SceneManager.LoadScene(SceneIndex);
    }
}
