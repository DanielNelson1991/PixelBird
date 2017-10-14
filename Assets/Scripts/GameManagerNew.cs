using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Reflection;
public class GameManagerNew : ScriptableObject {

    public static GameManagerNew control;

    // The data in the current game
    public static int _g_Coins;
    public static int _g_Gifts;
    public static int _p_spinningBalls;
    public static int _p_rewinds;

    // Setup Game Data Variables to be Saved
    public static int amount_of_levels = 6;   // How many levels do we have?


    public static List<PlayerData> playerData = new List<PlayerData>();

    void Awake()
    {
       Debug.Log(Application.persistentDataPath);
    }

    void Start()
    {
        // Set initital data
        
        
        for(int i = 0; i < amount_of_levels; i++)
        {
            playerData.Add(new PlayerData { });
        }

        playerData[0].levelEnabled = true;

        //SaveGameData();
        LoadGameData();

    }


    public static void SaveGameData()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream playerDataFile = File.Create(Application.persistentDataPath + "/userData.dat");
        FileStream additionalDataFiler = File.Create(Application.persistentDataPath + "/additionalData.dat");

        // Save level data first
        List<PlayerData> data = new List<PlayerData>();

        AdditionalData additionalData = new AdditionalData();

        // Level data loop save
        for (int i = 0; i < amount_of_levels; i++)
        {
            if(data.Count < amount_of_levels)
            {
                data.Add(new PlayerData
                {
                    levelEnabled = playerData[i].levelEnabled,
                    levelPlayedBefore = playerData[i].levelPlayedBefore,
                    bestScore = playerData[i].bestScore
                });
            }           
        }

        // Save additional data
        additionalData.GlobalGifts = _g_Gifts;
        additionalData.GlobalCoins = _g_Coins;

        // Get name of method
        Debug.Log("*** Message from: " + MethodBase.GetCurrentMethod() + " additionalData.GlobalGifts = " + additionalData.GlobalGifts);

        bf.Serialize(playerDataFile, data);
        bf.Serialize(additionalDataFiler, additionalData);
        playerDataFile.Close();
        additionalDataFiler.Close();
    }

    public void LoadGameData()
    {
        if(File.Exists(Application.persistentDataPath + "/userData.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/userData.dat", FileMode.Open);
            List<PlayerData> data = (List<PlayerData>)bf.Deserialize(file);

            for (int i = 0; i < data.Count; i++)
            {
                playerData[i].levelEnabled = data[i].levelEnabled;
                playerData[i].levelPlayedBefore = data[i].levelPlayedBefore;
                playerData[i].bestScore = data[i].bestScore;
                Debug.Log("Level " + i + " Enabled: " + data[i].levelEnabled);
                Debug.Log("Level " + i + " Best Score: " + data[i].bestScore);
            }

            AdditionalData additionalData = new AdditionalData();

            Debug.Log(data.Count);
            file.Close();
        }

        if(File.Exists(Application.persistentDataPath + "/additionalData.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/additionalData.dat", FileMode.Open);
            AdditionalData data = (AdditionalData)bf.Deserialize(file);

            _g_Gifts = data.GlobalGifts;

            Debug.Log("****** First Time Playing? " + data.FirstTimePlaying);
            Debug.Log("****** Global Coinds: " + data.GlobalCoins);
        } else
        {
            Debug.Log("*** Message from: " + this.GetType().Name + " additionalData.dat file does not exists. Creating now...");
            // If the file does not exist, lets setup defaults
            _g_Gifts = 5;
            _g_Coins = 100;
            Debug.Log("*** Message from: " + this.GetType().Name + " _g_Gifts = " + _g_Gifts);
            SaveGameData();
        }
    }

}

[Serializable]
public class PlayerData
{
    // Variables relating to levels
    public bool levelEnabled;
    public bool levelPlayedBefore;
    public int bestScore;
}

[Serializable]
public class AdditionalData
{

    public bool FirstTimePlaying;

    // Data for counters
    public int GlobalCoins;        // Total amount of counds
    public int GlobalGifts;         // Total amount of gifts
    public int TotalRewinds;        // Total amount of in-game 'Rewinds'
    public int TotalSpinningBall;   // Total amount of in-game 'Spinning Balls'

    // Gift Timer Data
    public long GlobalTimeMin;      // Used for adding minor gifts
    public long GlobalTimeMed;      // Used for adding medium gifts
    public long GlobalTimeMax;      // Used for adding max gifts

}