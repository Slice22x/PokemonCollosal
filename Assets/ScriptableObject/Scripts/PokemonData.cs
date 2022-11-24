using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEditor;

[System.Serializable]
public struct TypeEffectiveness
{
    Type type;
    float multiplier;
}

public enum Type
{
    Normal,
    Fire,
    Water,
    Grass,
    Ice,
    Electric,
    Fighting,
    Poison,
    Ground,
    Flying,
    Psychic,
    Bug,
    Rock,
    Ghost,
    Dark,
    Dragon,
    Steel,
    Fairy
}

[CreateAssetMenu(menuName = "Pokemon/Create PokemonData", fileName = "New Pokemon")]
public class PokemonData : ScriptableObject
{
    // This holds all the base data on the pokemon
    // The actual pokemon that uses this data is a seperate script
    // That script holds the current pokemon the player has 

    public enum ExperienceGroup
    {
        Erratic,
        Fast,
        MediumFast,
        MediumSlow,
        Slow,
        Fluctuating
    }

    [Header("Pokemon Info")]
    public string pokemonName;
    [TextArea(3, 10)]
    public string description;
    public int ID;

    public Sprite frontSprite, backSprite, front2, back2;

    [Header("Pokemon Stats")]
    public Type[] type = new Type[2];
    public List<MoveDataObject> baseMoves = new List<MoveDataObject>(4);
    public int baseHP;
    public int baseATK; 
    public int baseDEF; 
    public int baseSPAtk; 
    public int baseSPDef; 
    public int baseSPD;

    [Header("Pokemon Effectiveness")]
    public List<TypeEffectiveness> typeStats = new List<TypeEffectiveness>(18);

    //This automatically adds the new pokemon to the static database of pokemon
    [Conditional("UNITY_EDITOR")]
    private void OnValidate()
    {
        if (PokemonDatabase.database != null)
        {
            if (!PokemonDatabase.database.pokemonData.Contains(this))
            {
                PokemonDatabase.database.pokemonData.Add(this);
                UnityEngine.Debug.Log(pokemonName + " Has beed added to the database");
            }
        }

    }
}