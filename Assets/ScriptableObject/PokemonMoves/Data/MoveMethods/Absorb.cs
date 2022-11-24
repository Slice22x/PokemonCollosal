using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Move/Absorb")]
public class Absorb : MoveData
{
    public override IEnumerator Attack(PokemonBase attacker)
    {
        Debug.Log("Absorb was used");
        yield return null;
    }
}
