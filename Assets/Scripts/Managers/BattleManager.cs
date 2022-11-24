using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum BattleState
{
    START, PLAYERTURN, ENEMYTURN, ATTACK, WON, LOSS
}

public enum BattleType
{
    Normal, Gym
}

public class BattleManager : MonoBehaviour
{
    public static BattleManager instance;

    public BattleType battleType;
    public BattleState state;

    public Animator platformAnim;
    [SerializeField] Transform playerPlatform, enemyPlatform;

    [SerializeField] MoveDataObject testMove;

    public PokemonData enemyPokemonData;
    public PokemonBase enemyPokemon { get; private set; }
    public PokemonBase playerPokemon { get; private set; }

    public MoveDataObject playerMove;
    MoveDataObject enemyMove;

    [Space()]
    [Header("Enemy UI")]
    public Image healthImage;
    public TMP_Text enemyName;

    [SerializeField] DialogueData data;


    private void Awake()
    {
        platformAnim.enabled = false;
        instance = this;

    }

    public void EnableAnim()
    {
        instance.platformAnim.enabled = true;
    }

    void Start()
    {
        state = BattleState.START;
        SetupBattle();
    }

    void Update()
    {
        
    }

    void SetupBattle()
    {
        switch (battleType)
        {
            case BattleType.Normal:
                GameObject m = Resources.Load("BasePokemon") as GameObject;
                // Spawns the enemy and sets its stats
                GameObject n = Instantiate(m, enemyPlatform);
                n.transform.localPosition = new Vector3(0, 50);
                enemyPokemon = n.AddComponent<Pokemon>();

                enemyPokemon.data = enemyPokemonData;

                n.GetComponentInChildren<Image>().sprite = enemyPokemon.data.frontSprite;

                PokemonEVIV v = new PokemonEVIV(30, 50, 34, 57, 23, 90, 34, 57, 12, 68);
                enemyPokemon.evIvStats = v;
                enemyPokemon.SetStats();
                enemyPokemon.moves.Add(testMove);

                enemyPokemon.currentHP = enemyPokemon.HP;

                // Spawns the player pokemon
                GameObject p = Instantiate(m, playerPlatform);
                p.transform.localPosition = new Vector3(0, 50);
                PokemonBase pb = CopyComponent<PokemonBase>(PokeInventory.instance.currentPokemon, p);
                playerPokemon = pb;

                p.GetComponentInChildren<Image>().sprite = PokeInventory.instance.currentPokemon.data.backSprite;

                pb.SetStats();
                UpdateEnemyStats();
                break;
        }
        state = BattleState.PLAYERTURN;
        PlayerTurn();
    }

    public void PlayerTurn()
    {
        if (!BattleUIAnimator.instance.active) BattleUIAnimator.instance.EnterUI();
    }

    public void Attack()
    {
        if (state != BattleState.PLAYERTURN) return;

        BattleUIAnimator.instance.EnterDesc(true);
    }

    public void ConfirmAttack(MoveDataObject move, bool player)
    {
        if (player)
        {
            playerMove = move;

            state = BattleState.ENEMYTURN;
            StartCoroutine(BattleUIAnimator.instance.ExitUIFast(EnemyTurn));
        }
        if (!player)
        {
            enemyMove = move;

            state = BattleState.ATTACK;
            StartCoroutine(StartAttack());
        }
    }

    IEnumerator StartAttack()
    {
        //Compare the speeds of both moves
        bool playerFirst = PokeInventory.instance.currentPokemon.speed > enemyPokemon.speed;
        bool attacking = true;

        while (attacking)
        {
            if (playerFirst)
            {
                // Player first
                yield return playerMove.attackData.Attack(enemyPokemon);

                yield return enemyMove.attackData.Attack(PokeInventory.instance.currentPokemon);
            }
            else if (!playerFirst)
            {
                yield return enemyMove.attackData.Attack(PokeInventory.instance.currentPokemon);

                yield return playerMove.attackData.Attack(enemyPokemon);
            }
            attacking = false;
        }

        state = BattleState.PLAYERTURN;
        PlayerTurn();
    }

    void EnemyTurn()
    {
        // Test logic to attack
        ConfirmAttack(enemyPokemon.moves[Random.Range(0, enemyPokemon.moves.Count)], false);
    }

    public void Pokemon()
    {
        if (state != BattleState.PLAYERTURN) return;

        BattleUIAnimator.instance.EnterDesc(false);
    }

    public void Item()
    {
        if (state != BattleState.PLAYERTURN) return;

        BattleUIAnimator.instance.EnterDesc(false);
    }

    void UpdateEnemyStats()
    {
        float n = (float)enemyPokemon.currentHP / (float)enemyPokemon.HP;
        healthImage.fillAmount = n;
        healthImage.color = HealthColour(n);
        healthImage.GetComponentInParent<TMP_Text>().color = HealthColour(n);
        enemyName.text = enemyPokemon.data.pokemonName;
    }

    Color HealthColour(float x)
    {
        return Color.Lerp(Color.red, Color.green, x);
    }

    T CopyComponent<T>(T original, GameObject destination) where T : Component
    {
        System.Type type = original.GetType();
        Component copy = destination.AddComponent(type);
        System.Reflection.FieldInfo[] fields = type.GetFields();
        foreach (System.Reflection.FieldInfo field in fields)
        {
            field.SetValue(copy, field.GetValue(original));
        }
        return copy as T;
    }
}
