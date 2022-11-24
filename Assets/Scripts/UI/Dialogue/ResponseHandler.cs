using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;

public class ResponseHandler : MonoBehaviour
{
    [SerializeField]
    RectTransform responseBox, responseButtonTemplate, responseContainer;

    List<GameObject> tempButtons = new List<GameObject>();

    public void ShowResponses(Response[] responses)
    {
        float responseHeight = 0;

        foreach (Response response in responses)
        {
            GameObject responseButton = Instantiate(responseButtonTemplate.gameObject, responseContainer);
            responseButton.gameObject.SetActive(true);
            responseButton.GetComponent<TMP_Text>().text = response.ResponceText;
            responseButton.GetComponent<Button>().onClick.AddListener(() => OnPickedResponse(response));

            tempButtons.Add(responseButton);

            responseHeight += responseButtonTemplate.sizeDelta.y;
        }

        responseBox.sizeDelta = new Vector2(responseBox.sizeDelta.x, responseHeight);
        responseBox.gameObject.SetActive(true);
    }

    void OnPickedResponse(Response response)
    {
        responseBox.gameObject.SetActive(false);

        foreach (GameObject g in tempButtons)
        {
            Destroy(g);
        }
        tempButtons.Clear();

        Dialogue.instance.CallDialogue(response.Data);
    }
}
