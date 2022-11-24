using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class FightButton : MonoBehaviour
{
    public MoveDataObject thisMove;

    [SerializeField] TMP_Text moveName;
    [SerializeField] TMP_Text pp;
    [SerializeField] TMP_Text pow;
    [SerializeField] Image imageType;

    void Update()
    {
        if (thisMove == null) return;

        GetComponentInChildren<Button>().interactable = true;
        moveName.text = thisMove.moveName;
        pp.text = thisMove.pp.ToString();
        pow.text = thisMove.power.ToString();
    }

    public void SetDescription()
    {
        FightUI ui = GetComponentInParent<FightUI>();

        if (thisMove == null) return;

        string moveDescription = thisMove.moveDescription;
        ui.description.text = moveDescription;
        ui.selectedMove = thisMove;
    }
}
