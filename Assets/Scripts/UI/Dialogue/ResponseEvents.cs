using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class ResponseEvents
{
    [HideInInspector] public string name;
    [SerializeField] UnityEvent onPickedResponse;

    public UnityEvent OnPickedResponse => onPickedResponse;
}
