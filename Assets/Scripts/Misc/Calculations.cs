using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Mathf;

public static class Calculations
{

    /// <summary>
    /// Calculates the damage dealt to a pokemon
    /// </summary>
    /// <param name="level">Level of the pokemon (gotten from pokemon data)</param>
    /// <param name="power">The power of the move (gotten from move data)</param>
    /// <param name="A">Attack of the pokemon (ATK for physical attacks spATK for other moves) (gotten from pokemon data)</param>
    /// <param name="D">Defence of the pokemon (DEF for physical attacks spDEF for other moves) (gotten from pokemon data)</param>
    /// <param name="targets">How many pokemon the move affects (gotten from move data)</param>
    /// <param name="weather">The current weather (gotten from battle state)</param>
    /// <param name="type">The move type (gotten from move data)</param>
    /// <param name="crit">If the attack crits or not (RNG)</param>
    /// <param name="burn">If the Pokemon is burnt (gotten from battle state)</param>
    /// <param name="same">If the attack is the same type as the victim pokemon (checked when attacking)</param>
    /// <returns>the fucking damage what else</returns>
    public static int Damage(int level, int power, int A, int D, int targets, float weather,float type, bool crit, bool burn, bool same)
    {
        // Calculates the first part
        int a = ((2 * level) / 5) + 2;
        int b = ((a * power * (A / D)) / 50) + 2;

        //All the multiplication variables
        float tar = targets == 2 ? 0.75f : 1f;
        float c = crit ? 2f : 1f;
        float brn = burn ? 0.5f : 1f;
        float STAB = same ? 1.5f : 1f;

        //muliplys everything
        float m = tar * weather * c * brn * STAB;

        //the actual damage calculated
        int damage = RoundToInt(a * b * m);

        //returns the damage
        return damage;
    }

    /// <summary>
    /// Calculates how much XP is needed for the next level
    /// </summary>
    /// <param name="n">The current level of the pokemon</param>
    /// <param name="group">The XP group it's in</param>
    /// <returns>The XP needed</returns>
    public static int NextXP(int n, PokemonData.ExperienceGroup group)
    {
        //Basically an advanced if statement that is more efficient and easier to manage
        int XP = 0;
        int m = n + 1;
        float a = 0;
        float b = 0;
        int c = 0;
        switch (group)
        {
            case PokemonData.ExperienceGroup.Erratic:

                if(n < 50)
                {
                    a = (Pow(n, 3) * (100f - n)) / 50f;

                    b = (Pow(m, 3) * (100f - m)) / 50f;
                }

                if(50 <= n && n < 68)
                {
                    a = (Pow(n, 3) * (150f - n)) / 100f;

                    b = (Pow(m, 3) * (150f - m)) / 100f;
                }

                if (68 <= n && n < 98)
                {
                    a = (Pow(n, 3) * ((1911f - (10f * n)) / 3f)) / 500f;

                    b = (Pow(m, 3) * ((1911f - (10f * m)) / 3f)) / 500f;
                }

                if (98 <= n && n < 100)
                {
                    a = (Pow(n, 3) * (160f - n)) / 100f; 

                    b = (Pow(m, 3) * (160f - m)) / 100f;
                }

                break;
            case PokemonData.ExperienceGroup.Fast:

                a = (4f * Pow(n, 3)) / 5f;

                b = (4f * Pow(m, 3)) / 5f;

                break;
            case PokemonData.ExperienceGroup.MediumFast:

                a = Pow(n, 3);

                b = Pow(m, 3);

                break;
                //Fix
            case PokemonData.ExperienceGroup.MediumSlow:
                
                a = (6f / 5f) * Pow(n, 3) - (15 * Pow(n, 2)) + (100f * n) - 140f;

                b = (6f / 5f) * Pow(m, 3) - (15 * Pow(m, 2)) + (100f * m) - 140f;

                break;
            case PokemonData.ExperienceGroup.Slow:

                a = (5 * Pow(n, 3)) / 4f;

                b = (5 * Pow(m, 3)) / 4f;

                break;
            case PokemonData.ExperienceGroup.Fluctuating:

                if(n < 15)
                {
                    int a1 = n + 1;
                    float b1 = (float)a1 / 3f;
                    float c1 = b1 + 24;
                    float d1 = c1 / 50f;
                    a = Pow(n, 3) * d1;

                    int a2 = m + 1;
                    float b2 = (float)a2 / 3f;
                    float c2 = b2 + 24;
                    float d2 = c2 / 50f;
                    b = Pow(m, 3) * d2;
                }

                if(15 <= n && n < 36)
                {
                    a = Pow(n, 3) * ((n + 14) / 50f);

                    b = Pow(m, 3) * ((m + 14) / 50f);
                }

                if (36 <= n && n < 100)
                {
                    float a1 = n / 2;
                    float b1 = a1 + 32;
                    float c1 = b1 / 50;
                    a = Pow(n, 3) * c1;

                    float a2 = m / 2;
                    float b2 = a2 + 32;
                    float c2 = b2 / 50;
                    b = Pow(m, 3) * c2;
                }
                break;
        }

        //Truncartes (Removes the decimals without rounding) the XP and returns it

        int ai = System.Convert.ToInt32(System.Math.Truncate(a));
        int bi = System.Convert.ToInt32(System.Math.Truncate(b));

        Debug.Log("a = " + ai);
        Debug.Log("b = " + bi);

        c = bi - ai;
        XP = c;

        return XP;
    }

    /// <summary>
    /// Calculates the Max XP to the next level
    /// </summary>
    /// <param name="n">The current level of the pokemon</param>
    /// <param name="group">The XP group it's in</param>
    /// <returns>The XP needed</returns>
    public static int NextMaxXP(int n, PokemonData.ExperienceGroup group)
    {
        //Basically an advanced if statement that is more efficient and easier to manage
        int m = n + 1;
        float b = 0;
        switch (group)
        {
            case PokemonData.ExperienceGroup.Erratic:

                if (n < 50)
                {
                    b = (Pow(m, 3) * (100f - m)) / 50f;
                }

                if (50 <= n && n < 68)
                {
                    b = (Pow(m, 3) * (150f - m)) / 100f;
                }

                if (68 <= n && n < 98)
                {
                    b = (Pow(m, 3) * ((1911f - (10f * m)) / 3f)) / 500f;
                }

                if (98 <= n && n < 100)
                {
                    b = (Pow(m, 3) * (160f - m)) / 100f;
                }

                break;
            case PokemonData.ExperienceGroup.Fast:

                b = (4f * Pow(m, 3)) / 5f;

                break;
            case PokemonData.ExperienceGroup.MediumFast:
                b = Pow(m, 3);

                break;
            //Fix
            case PokemonData.ExperienceGroup.MediumSlow:
                b = (6f / 5f) * Pow(m, 3) - (15 * Pow(m, 2)) + (100f * m) - 140f;

                break;
            case PokemonData.ExperienceGroup.Slow:
                b = (5 * Pow(m, 3)) / 4f;

                break;
            case PokemonData.ExperienceGroup.Fluctuating:

                if (n < 15)
                {
                    int a2 = m + 1;
                    float b2 = (float)a2 / 3f;
                    float c2 = b2 + 24;
                    float d2 = c2 / 50f;
                    b = Pow(m, 3) * d2;
                }

                if (15 <= n && n < 36)
                {
                   b = Pow(m, 3) * ((m + 14) / 50f);
                }

                if (36 <= n && n < 100)
                {
                    float a2 = m / 2;
                    float b2 = a2 + 32;
                    float c2 = b2 / 50;
                    b = Pow(m, 3) * c2;
                }
                break;
        }

        //Truncartes (Removes the decimals without rounding) the XP and returns it

        int bi = System.Convert.ToInt32(System.Math.Truncate(b));

        Debug.Log("b = " + bi);

        return bi;
    }

    public enum stat
    {
        HP,
        ATK,
        DEF,
        SPATK,
        SPDEF,
        SPD
    }

    /// <summary>
    /// Gets the stat and knows how much to increase it by
    /// </summary>
    /// <param name="pokemon">the pokemon to upgrade</param>
    /// <param name="st">the stat to upgrade</param>
    /// <returns>The number to increment it by</returns>
    public static int Stat(this PokemonBase pokemon, stat st)
    {
        float s = 0f;
        switch (st)
        {
            case stat.HP:
                s = (((pokemon.data.baseHP + pokemon.evIvStats.HPIV) * 2 + (Sqrt(pokemon.evIvStats.HPEV) / 4) * pokemon.level) / 100) + (pokemon.level + 10);
                break;
            case stat.ATK:
                s = (((pokemon.data.baseHP + pokemon.evIvStats.ATKIV) * 2 + (Sqrt(pokemon.evIvStats.ATKEV) / 4) * pokemon.level) / 100) + 5;
                break;
            case stat.DEF:
                s = (((pokemon.data.baseHP + pokemon.evIvStats.DEFIV) * 2 + (Sqrt(pokemon.evIvStats.DEFEV) / 4) * pokemon.level) / 100) + 5;
                break;
            case stat.SPATK:
                s = (((pokemon.data.baseHP + pokemon.evIvStats.SPIV) * 2 + (Sqrt(pokemon.evIvStats.SPEV) / 4) * pokemon.level) / 100) + 5;
                break;
            case stat.SPDEF:
                s = (((pokemon.data.baseHP + pokemon.evIvStats.SPIV) * 2 + (Sqrt(pokemon.evIvStats.SPEV) / 4) * pokemon.level) / 100) + 5;
                break;
            case stat.SPD:
                s = (((pokemon.data.baseHP + pokemon.evIvStats.SPDIV) * 2 + (Sqrt(pokemon.evIvStats.SPDEV) / 4) * pokemon.level) / 100) + 5;
                break;
        }

        return System.Convert.ToInt32(System.Math.Truncate(s));
    }
}
