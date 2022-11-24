using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Move/Acid")]
public class Acid : MoveData
{
    public override IEnumerator Attack(PokemonBase attacker)
    {
        Debug.Log("Acid was used");
        yield return null;
    }
}
