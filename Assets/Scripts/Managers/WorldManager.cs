using UnityEngine.Tilemaps;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class WorldManager : MonoBehaviour
{
    public static WorldManager instance;

    [Range(0f, 1f)]
    public float battleChance;
    [SerializeField] float timeAfterBattle;
    bool battleing;
    public Tilemap map;

    public Animator blank;

    //Calls the event that a battle as initiated
    public delegate void BattleStarted();
    public static BattleStarted StartedBattle;
    public Animator pokeballAnimator;

    public List<PokemonData> pokemonInArea = new List<PokemonData>();

    [SerializeField] MoveDataObject obj;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        
    }

    

    void Update()
    {
        //If the player is not batteling then run this check
        if (!battleing)
        {
            //Get the player's position onto a tile grid
            Vector3Int gridPos = map.WorldToCell(Movement.instance.transform.position);

            TileBase tile = map.GetTile(gridPos);

            //For every step the player takes (on certain tiles) run this check
            if(tile != null)
            {
                if (Movement.instance.dir.magnitude > 1f && tile.name.Contains("Grass"))
                {
                    if(Random.value < battleChance)
                    {
                        // This stops the player from moving, sets it's velovity to 0 and calls the UI
                        battleing = true;
                        Movement.instance.dir = Vector2.zero;
                        Movement.instance.control = false;
                        StartCoroutine(InitiateBattle());
                        StartedBattle?.Invoke();
                    }

                }
            }

        }
        if (battleing)
        {
            //
        }
    }

    IEnumerator InitiateBattle()
    {
        pokeballAnimator.Play("Pokeball_Initate");

        yield return new WaitForSeconds(0.5f);

        ScreenTransition.instance.anim.Play("FadeSine");

        yield return new WaitForSeconds(1f);

        ScreenTransition.instance.anim.Play("FadeStatic");

        blank.gameObject.SetActive(true);

        yield return new WaitForSeconds(0.5f);

        //Switch Scenes

        SceneManager.LoadScene("BattleScene", LoadSceneMode.Additive);

        SceneManager.SetActiveScene(SceneManager.GetSceneByName("BattleScene"));
    }
}
