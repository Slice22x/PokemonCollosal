using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PokeInventory : MonoBehaviour
{
    public static PokeInventory instance;

    public PokemonBase currentPokemon;

    public List<PokemonBase> party = new List<PokemonBase>(6);

    private void Awake()
    {
        instance = this;
    }
}
