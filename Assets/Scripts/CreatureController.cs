using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureController : MonoBehaviour
{
    public int stamina = 1;
    public int staminaIncrease = 1;
    public int agility = 1;
    public int agilityIncrease = 1;
    public int strength = 1;
    public int strengthIncrease = 1;
    public int speed = 1;
    public int speedIncrease = 1;

    public void Increment(string statToTrain)
    {
        Debug.Log($"Incrementing {statToTrain}");
        if (statToTrain == "Stamina")
        {
            stamina += staminaIncrease;
        }
        else if (statToTrain == "Agility")
        {
            agility += agilityIncrease;
        }
        else if (statToTrain == "Strength")
        {
            strength += strengthIncrease;
        }
        else if (statToTrain == "Speed")
        {
            speed += speedIncrease;
        }
    }
}
