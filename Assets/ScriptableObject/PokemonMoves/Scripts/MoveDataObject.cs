using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Move", menuName = "Create move")]
public class MoveDataObject : ScriptableObject
{
    public Type moevType;

    public string moveName;
    [TextArea(3, 10)]
    public string moveDescription;
    public int power, acc, pp;

    public MoveData attackData;
}


public abstract class MoveData : ScriptableObject
{
    public abstract IEnumerator Attack(PokemonBase attacker);
}
