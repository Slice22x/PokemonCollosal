using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Database", menuName = "Make ONE Database")]
public class PokemonDatabase : ScriptableObject
{
    public static PokemonDatabase database;
    public List<PokemonData> pokemonData;
    public List<Pokemon> currentPokemonDatabase;

    //Once the editor has comipled it checks if there is a static data base and if not then assigns it
    [System.Diagnostics.Conditional("UNITY_EDITOR")]
    private void OnValidate()
    {
        if (database == null)
            database = this;
        else
            if (database != this)
            Debug.LogError("Database already exists!");

        for (int i = 0; i < pokemonData.Count; i++)
        {
            if (pokemonData[i] == null)
            {
                pokemonData.RemoveAt(i);
            }
        }
    }
}
