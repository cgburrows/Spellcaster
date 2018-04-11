using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTrigger : MonoBehaviour
{

    [Header("Damage Trackers")]
    public int totalDPS;
    public int lastHitDPS;
    public float damagePerSecond;

    [Header("Magic Resistances")]
    public int arcaneResist;
    public int fireResist;
    public int frostResist;
    public int lightResist;
    public int natureResist;
    public int stormResist;

    [Header("Physical Resistances")]
    public int slashResist;
    public int impactResist;
    public int punctureResist;

    // =========
    // ==NOTES==
    // =========

    // Formula for magical resistances:
    // If resistance >=0 (100/(100 + value))
    // If resistance < 0 (2 - 100/(100 - value))
    // Reason for this: Makes damage calculation easy.
    // Example: A dummy with 60 fire resistance must take 60% more damage in order to take the same damage as a dummy with 
    //          0 fire resistance, so it will take 1600 premitigated fire damage in order to deal 1000 damage to the dummy.

    // % pen applies *before* flat pen
    // Physical resistances apply after magic resistances

    // give the dummy random resistances
    void RandomizeResist(int complexity)
    {
        arcaneResist = 0;
        fireResist = 0;
        frostResist = 0;
        lightResist = 0;
        natureResist = 0;
        stormResist = 0;

        //int resistList[];

        for (int i = 0; i < complexity; i++)
        {
            int temp1 = Random.Range(1, 6);
        }
        arcaneResist = Random.Range(-10, 10);
        fireResist = 0;
        frostResist = 0;
        lightResist = 0;
        natureResist = 0;
        stormResist = 0;
    }

    // apply damage reduction/amplification formula
    int ResistDamage(int damage, int resistance)
    {
        int tempDamage;

        if (resistance >= 0)
        {
            tempDamage = damage * (100 / (100 + resistance));
        }
        else
        {
            tempDamage = damage * (2 - (100 / (100 - resistance)));
        }
        return tempDamage;
    }

    void Start()
    {
        totalDPS = 0;
        lastHitDPS = 0;
    }

    public void passDamage(int damage)
    {

    }

    public void applyCurse(int strength, int resistance)
    {

    }

    public void breakCure(int resistance)
    {

    }

    void calcDamage()
    {
        totalDPS = lastHitDPS + totalDPS;
        //damagePerSecond = totalDPS / elapsedTime;
        // if no damage has been taken for X seconds, reset damagePerSecond
    }

    void Update()
    {
        totalDPS = lastHitDPS + totalDPS;

    }
}
