using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Response
{
    [SerializeField] string responceText;
    [SerializeField] DialogueData data;

    public string ResponceText => responceText;
    public DialogueData Data => data;
}
