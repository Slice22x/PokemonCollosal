using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct DialogueBase
{
    public string speakerName;

    [TextArea(3, 10)]
    public string dialogue;
}

[CreateAssetMenu(fileName = "New Text", menuName = "Create Dialogue")]
public class DialogueData : ScriptableObject
{
    public List<DialogueBase> data = new List<DialogueBase>();
    public List<Response> responses;

    public bool HasResponses => responses != null && responses.Count > 0;
}
