using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine.UI;
using ScoreManager;
using System;
using System.Security.Cryptography;
using System.IO;
using System.Text;


public class LevelSelection : MonoBehaviour {

    // Create a nested class to assign properties towards the button. What we
    // need to know is the button object to set alpha and colour values, whether or not it is enabled to determine level
    // progression, and score to relay progress back to user


    // Instantiate the class within the inspector for developer feedback

    private int TrackingPosition = 0;
    public Sprite LockedSprite;
    public Sprite UnlockedSprite;

    void Awake()
    {
    }

    // Use this for initialization
    void Start()
    {
        // Go through each level in our list...
        for (int i = 0; i < GameManagerNew.amount_of_levels; i++)
        {
            if(GameManagerNew.playerData[i].levelEnabled)
            {
                GameObject.Find("Level " + i).GetComponent<Image>().sprite = UnlockedSprite;
                GameObject.Find("Level " + i).GetComponentInChildren<Text>().enabled = true;

                // If the level is unlocked, display medals
                if(GameManagerNew.playerData[i].bestScore >= 1)
                {
                    GameObject.Find("medal_" + i + "_01").GetComponent<Image>().enabled = true;
                }

                if(GameManagerNew.playerData[i].bestScore > 1000)
                {
                    GameObject.Find("medal_" + i + "_02").GetComponent<Image>().enabled = true;
                }

                if(GameManagerNew.playerData[i].bestScore > 2100)
                {
                    GameObject.Find("medal_" + i + "_03").GetComponent<Image>().enabled = true;
                }
            } else
            {
                GameObject.Find("Level " + i).GetComponent<Image>().sprite = LockedSprite;
                GameObject.Find("Level " + i).GetComponentInChildren<Text>().enabled = false;
            }
        }

    }

    public void LevelSelect(int LevelNumber)
    {

        if (LevelNumber == -1)
        {
            SceneManager.LoadScene("MainMenu");
        } else if(GameManagerNew.playerData[LevelNumber].levelEnabled)
        {
            DontDestroyOnLoad(GameObject.Find("GameManager"));
            SceneManager.LoadScene("Level " + LevelNumber);
        } else
        {
            Debug.Log("<color=red><b>Information From " + this.GetType().Name + ": Level " + LevelNumber + " is not yet enabled.</b></color>");
        }


        
    }

    public void GoLevelSelect()
    {
        SceneManager.LoadScene("LevelSelect");
    }

    public void MoveLevelSelectionGrid()
    {
        /*TrackingPosition++;
        Animator anim;
        anim = GameObject.Find("ButtonContainer").GetComponent<Animator>();
        anim.SetInteger("TrackingPosition", TrackingPosition);
        Debug.Log(anim.GetInteger("TrackingPosition"));*/
    }

    public void MoveLevelSelectionGridBack()
    {
        /*TrackingPosition--;
        Animator anim;
        anim = GameObject.Find("ButtonContainer").GetComponent<Animator>();
        anim.SetInteger("TrackingPosition", TrackingPosition);
        Debug.Log(anim.GetInteger("TrackingPosition"));*/
    }

}
