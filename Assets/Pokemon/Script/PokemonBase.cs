using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct PokemonEVIV
{
    public int HPIV;
    public int ATKIV;
    public int DEFIV;
    public int SPIV;
    public int SPDIV;

    //increase by the opponent's base stat
    public int HPEV;
    public int ATKEV;
    public int DEFEV;
    public int SPEV;
    public int SPDEV;

    public PokemonEVIV(int hPIV, int aTKIV, int dEFIV, int sPIV, int sPDIV, int hPEV, int aTKEV, int dEFEV, int sPEV, int sPDEV)
    {
        HPIV = hPIV;
        ATKIV = aTKIV;
        DEFIV = dEFIV;
        SPIV = sPIV;
        SPDIV = sPDIV;
        HPEV = hPEV;
        ATKEV = aTKEV;
        DEFEV = dEFEV;
        SPEV = sPEV;
        SPDEV = sPDEV;
    }
}

public abstract class PokemonBase : MonoBehaviour
{
    [Header("Data")]
    public PokemonData data;
    public PokemonData.ExperienceGroup group;

    [Header("Stats")]
    public int level;

    public List<MoveDataObject> moves = new List<MoveDataObject>(4);
    public int HP;
    public int currentHP;
    public int attack;
    public int defence;
    public int spAtk;
    public int spDef;
    public int speed;

    public bool player;

    public PokemonEVIV evIvStats;

    public virtual bool TakeDamage(int damage)
    {
        currentHP -= damage;

        if (currentHP <= 0) return true;
        else return false;
    }

    public void SetStats()
    {
        HP = this.Stat(Calculations.stat.HP);
        defence = this.Stat(Calculations.stat.DEF);
        attack = this.Stat( Calculations.stat.ATK);
        speed = this.Stat(Calculations.stat.SPD);
        spDef = this.Stat(Calculations.stat.SPDEF);
        spAtk = this.Stat(Calculations.stat.SPATK);
    }
}
