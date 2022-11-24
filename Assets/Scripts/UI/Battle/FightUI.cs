using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FightUI : MonoBehaviour
{
    public List<MoveDataObject> moves = new List<MoveDataObject>(4);
    public List<GameObject> buttons = new List<GameObject>(4);

    public TMP_Text description;

    [HideInInspector] public MoveDataObject selectedMove;

    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        moves = BattleManager.instance.playerPokemon.moves;
        if (moves.Count != 0) for (int i = 0; i < buttons.Count; i++)
            {
                if (moves[i] == null) return;
                buttons[i].GetComponent<FightButton>().thisMove = moves[i];
            }
    }

    public void Attack()
    {
        BattleManager.instance.ConfirmAttack(selectedMove, true);
    }
}
