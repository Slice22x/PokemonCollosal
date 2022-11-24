using UnityEngine;

public class DialogueActivator : MonoBehaviour, IInteractable
{
    [SerializeField] DialogueData data;
    [SerializeField] Detection detect;
    Material mat;

    private void Start()
    {
        mat = GetComponent<SpriteRenderer>().material;
    }

    private void Update()
    {
        if (!Dialogue.instance.isOpen)
        {
            if (detect.Detected && detect.ObjectsInArea.tag == "Player")
            {
                Movement.instance.Interactable = this;
                GetComponent<SpriteRenderer>().material = Resources.Load("InreractOutline") as Material;
            }
            else if (Movement.instance.Interactable is DialogueActivator activator && activator == this)
            {
                Movement.instance.Interactable = null;
                GetComponent<SpriteRenderer>().material = mat;
            }
        }

    }

    public void Interact(Movement player)
    {
        Dialogue.instance.CallDialogue(data);
    }
}
