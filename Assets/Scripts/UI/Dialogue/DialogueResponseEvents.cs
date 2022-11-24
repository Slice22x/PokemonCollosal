using System;
using UnityEngine;

public class DialogueResponseEvents : MonoBehaviour
{
    [SerializeField] DialogueData data;
    [SerializeField] ResponseEvents[] events;

    public ResponseEvents[] Events => events;

    public void OnValidate()
    {
        if (data == null) return;
        if (data.responses == null) return;
        if (events != null && events.Length == data.responses.Count) return;

        if(events != null)
        {
            events = new ResponseEvents[data.responses.Count];
        }
        else
        {
            Array.Resize(ref events, data.responses.Count);
        }

        for (int i = 0; i < data.responses.Count; i++)
        {
            Response response = data.responses[i];

            if(events[i] != null)
            {
                events[i].name = response.ResponceText;
                continue;
            }
            events[i] = new ResponseEvents() { name = response.ResponceText };
        }
    }
}
