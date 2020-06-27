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

        if (statToTrain == "Stamina")
        {
            Debug.Log($"Incrementing {statToTrain} by {staminaIncrease}");
            stamina += staminaIncrease;
        }
        else if (statToTrain == "Agility")
        {
            Debug.Log($"Incrementing {statToTrain} by {agilityIncrease}");
            agility += agilityIncrease;
        }
        else if (statToTrain == "Strength")
        {
            Debug.Log($"Incrementing {statToTrain} by {strengthIncrease}");
            strength += strengthIncrease;
        }
        else if (statToTrain == "Speed")
        {
            Debug.Log($"Incrementing {statToTrain} by {speedIncrease}");
            speed += speedIncrease;
        }
    }
}
