using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine.UI;
using ScoreManager;
using System;
using System.Security.Cryptography;
using System.IO;

public class EndLevel : MonoBehaviour {

    public Canvas canvas;
    public GameObject storeFront;

    public Text Score_End;
    public Text Score_End_Best;
    public Image Medal_Image;

    public Sprite Medal_One;
    public Sprite Medal_Two;
    public Sprite Medal_Three;

    private int SceneNumber;

    GameManagerNew gameManger;

    void Awake()
    {

    }
	// Use this for initialization
	void Start () {

        // Get this level number
        SceneNumber = Convert.ToInt32(SceneManager.GetActiveScene().name.ToString().Replace("Level ", ""));
	    
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerController>().enabled = false;
            canvas.sortingLayerName = "UI";
            storeFront.SetActive(true);

            Score_End.text = PlayerController.currentLevelScore.ToString();

            Score_End_Best.text = GameManagerNew.playerData[SceneNumber].bestScore.ToString();

            if (PlayerController.currentLevelScore > 1)
            {
                Medal_Image.sprite = Medal_One;
            }

            if(PlayerController.currentLevelScore >= 500)
            {
                Medal_Image.sprite = Medal_Two;
            }

            if(PlayerController.currentLevelScore >= 1000)
            {
                Medal_Image.sprite = Medal_Three;
            }


            // Replace old score with new score by passing level name, and stripping "level" string to leave only number
            //GameManager.Instance.SaveLevelProgress(Convert.ToInt32(SceneManager.GetActiveScene().name.ToString().Replace("Level", "")), PlayerController.currentLevelScore);


            // First check to see if this level has already been unlocked and is just being replayed
            if(!GameManagerNew.playerData[SceneNumber].levelPlayedBefore)
            {

                GameManagerNew.playerData[SceneNumber + 1].levelEnabled = true;



            } else {
                Debug.Log("Message from " + GetType().Name + ": This level HAS been played");
            }

            if (GameManagerNew.playerData[SceneNumber].bestScore < Convert.ToInt32(Score_End.text))
            {
                Debug.Log("End of level reached. Current score is greater than previous");
                GameManagerNew.playerData[SceneNumber].bestScore = Convert.ToInt32(Score_End.text);
            }

            Debug.Log(Convert.ToInt32(Score_End));

            // Save the player prefs
            GameManagerNew.SaveGameData();         
        }
    }

    public void MoveLevel()
    {
        SceneManager.LoadScene("LevelSelect");
    }

    
}
