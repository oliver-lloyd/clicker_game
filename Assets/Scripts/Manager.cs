using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;



public class Manager : MonoBehaviour
{
    public int coinCount = 1000;
    public Text wallet;
    public string[] statNames = { "Agility", "Speed", "Stamina", "Strength" };
    public string[] statValues = { "XP", "Level", "IncrementAmount", "UpgradeCost" };
    public Dictionary<string, Dictionary<string, int>> statDict = new Dictionary<string, Dictionary<string, int>>();

    private System.Random random = new System.Random();
    private int coinEventCount;


    private void Start()
    {
        //coinCount = GameObject.FindGameObjectWithTag("Wallet");
        UpdateCoins(0);
        foreach (string statName in statNames)
        {
            statDict[statName] = new Dictionary<string, int>();
            foreach(string statValue in statValues)
            {
                statDict[statName][statValue] = 1;
            }
        }
    }
    private void Update()
    {
        UpdateCoins(0);  // Display current amount of coins
        
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 100.0f))
            {
                if (hit.transform.name == "Creature")
                {
                    print("The creature is dancing!");
                    // Play animation
                }
            }
        }
    }

    private void FixedUpdate()
    {
        RandomPickupCoins(10, 100);
    }

    public void Increment(string statToTrain)
    {
        print($"Incrementing {statToTrain} by {statDict[statToTrain]["IncrementAmount"]}");
        
        statDict[statToTrain]["XP"] += statDict[statToTrain]["IncrementAmount"];
        System.Threading.Thread.Sleep(1000);
    }
    public void UpgradeStats(string statToUpgrade)
    {
        
        
        if (statDict[statToUpgrade]["UpgradeCost"] <= coinCount)
        {
            coinCount -= statDict[statToUpgrade]["UpgradeCost"];
            
            statDict[statToUpgrade]["Level"]++;
            statDict[statToUpgrade]["IncrementAmount"] = (int) Math.Pow(statDict[statToUpgrade]["Level"], 2);
            statDict[statToUpgrade]["UpgradeCost"] = (int) Math.Pow(statDict[statToUpgrade]["Level"], 3);

            Debug.Log($"Upgraded {statToUpgrade}, it will now give {statDict[statToUpgrade]["IncrementAmount"]} XP per click.");
        }
        else
        {
            print("Not enough coins.");
        }

    }

    public void UpdateCoins(int coinChange)
    {
        coinCount += coinChange;
        wallet.text = $"Coins: {coinCount}";
    }
    
    public void RandomPickupCoins(float meanPeriod, int maxPickup)
    {
        // 
        float frequency = meanPeriod / Time.fixedDeltaTime;

        int numberOfAttempts = (int)frequency;
        int randNum = random.Next(numberOfAttempts + 1);
        if (randNum == 69)
        {
            //These 2 lines were used for checking interval of pickups. The value logged should tend towards meanPeriod over time. If need to do again, initialise coinEventCount to 0 in start()
            //coinEventCount++;
            //print($"Mean interval of pickups = {Time.fixedTime / coinEventCount}");

            int amountFound = random.Next(2, maxPickup + 1);
            print($"Nice! The creature found {amountFound} coins.");
            coinCount += amountFound;
        }

    }
    
}
