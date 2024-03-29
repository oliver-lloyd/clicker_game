﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class CreatureController : MonoBehaviour
{
    public int coinCount = 10;
    public Text wallet;
    
    public Text agilityCounter;
    public Text speedCounter;
    public Text staminaCounter;
    public Text accelerationCounter;
    
    public UnityEngine.UI.Button agilityUpgrade;
    public UnityEngine.UI.Button speedUpgrade;
    public UnityEngine.UI.Button staminaUpgrade;
    public UnityEngine.UI.Button accelerationUpgrade;

    public UnityEngine.UI.Button agilityIncrement;
    public UnityEngine.UI.Button speedIncrement;
    public UnityEngine.UI.Button staminaIncrement;
    public UnityEngine.UI.Button accelerationIncrement;
    
    public string[] statNames = { "Acceleration" , "Agility", "Speed", "Stamina" };
    public string[] statValues = { "XP", "Level", "IncrementAmount", "UpgradeCost" , "Counter" };

    public Dictionary<string, Dictionary<string, int>> statDict = new Dictionary<string, Dictionary<string, int>>();
    public Dictionary<string, Dictionary<string, UnityEngine.UI.Button>> buttonDict = new Dictionary<string, Dictionary<string, UnityEngine.UI.Button>>();
    public Dictionary<string, Text> textDict = new Dictionary<string, Text>();

    private System.Random random = new System.Random();
    private int coinEventCount;
    private GameObject _Manager;


    private void Awake()
    {
        _Manager = GameObject.FindWithTag("_Manager");

        textDict["Agility"] = agilityCounter;
        textDict["Speed"] = speedCounter;
        textDict["Stamina"] = staminaCounter;
        textDict["Acceleration"] = accelerationCounter;

        //coinCount = GameObject.FindGameObjectWithTag("Wallet");
        UpdateCoins(0);
        foreach (string statName in statNames)
        {
            statDict[statName] = new Dictionary<string, int>();
            foreach(string statValue in statValues)
            {
                statDict[statName][statValue] = 1;
            }
            buttonDict[statName] = new Dictionary<string, UnityEngine.UI.Button>();
            print(statName);
            textDict[statName].text = statName + $": {statDict[statName]["XP"]}";  // Set counter text

            
        }


        buttonDict["Agility"]["Increment"] = agilityIncrement;
        buttonDict["Agility"]["Upgrade"] = agilityUpgrade;
        buttonDict["Agility"]["Increment"].GetComponentInChildren<Text>().text = $"Train Increment! (+{statDict["Agility"]["IncrementAmount"]})";
        buttonDict["Agility"]["Upgrade"].GetComponentInChildren<Text>().text = $"Buy {"Agility"} Upgrade (£{statDict["Agility"]["UpgradeCost"]})";

        buttonDict["Speed"]["Increment"] = speedIncrement;
        buttonDict["Speed"]["Upgrade"] = speedUpgrade;
        buttonDict["Speed"]["Increment"].GetComponentInChildren<Text>().text = $"Train Speed! (+{statDict["Speed"]["IncrementAmount"]})";
        buttonDict["Speed"]["Upgrade"].GetComponentInChildren<Text>().text = $"Buy {"Speed"} Upgrade (£{statDict["Speed"]["UpgradeCost"]})";
        
        buttonDict["Stamina"]["Increment"] = staminaIncrement;
        buttonDict["Stamina"]["Upgrade"] = staminaUpgrade;
        buttonDict["Stamina"]["Increment"].GetComponentInChildren<Text>().text = $"Train Stamina! (+{statDict["Stamina"]["IncrementAmount"]})";
        buttonDict["Stamina"]["Upgrade"].GetComponentInChildren<Text>().text = $"Buy {"Stamina"} Upgrade (£{statDict["Stamina"]["UpgradeCost"]})";
        
        buttonDict["Acceleration"]["Increment"] = accelerationIncrement;
        buttonDict["Acceleration"]["Upgrade"] = accelerationUpgrade;
        buttonDict["Acceleration"]["Increment"].GetComponentInChildren<Text>().text = $"Train Acceleration! (+{statDict["Acceleration"]["IncrementAmount"]})";
        buttonDict["Acceleration"]["Upgrade"].GetComponentInChildren<Text>().text = $"Buy {"Acceleration"} Upgrade (£{statDict["Stamina"]["UpgradeCost"]})";

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
                    // Play animation here
                }
            }
        }

    }

    private void FixedUpdate()
    {
        if (_Manager.GetComponent<SceneController>().currentSceneIndex == 0)
        {
            RandomPickupCoins(10, 100);
        }
    }

    public void Increment(string statToTrain)
    {
        print($"Incrementing {statToTrain} by {statDict[statToTrain]["IncrementAmount"]}");
        
        statDict[statToTrain]["XP"] += statDict[statToTrain]["IncrementAmount"];
        textDict[statToTrain].text = statToTrain + $": {statDict[statToTrain]["XP"]}";

        System.Threading.Thread.Sleep(500);
    }
    public void UpgradeStats(string statToUpgrade)
    {
        
        
        if (statDict[statToUpgrade]["UpgradeCost"] <= coinCount)
        {
            coinCount -= statDict[statToUpgrade]["UpgradeCost"];
            
            statDict[statToUpgrade]["Level"]++;
            statDict[statToUpgrade]["IncrementAmount"] = (int) Math.Pow(statDict[statToUpgrade]["Level"], 2);
            statDict[statToUpgrade]["UpgradeCost"] = (int) Math.Pow(statDict[statToUpgrade]["Level"], 3);

            buttonDict[statToUpgrade]["Increment"].GetComponentInChildren<Text>().text = $"Train {statToUpgrade}! (+{statDict[statToUpgrade]["IncrementAmount"]})";
            buttonDict[statToUpgrade]["Upgrade"].GetComponentInChildren<Text>().text = $"Buy {statToUpgrade} Upgrade (£{statDict[statToUpgrade]["UpgradeCost"]})";
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
