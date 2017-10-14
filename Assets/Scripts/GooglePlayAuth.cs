using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Collections;
using GooglePlayGames.BasicApi.SavedGame;


/// <summary>
/// Google Play Authentication script.
/// </summary>
public class GooglePlayAuth : MonoBehaviour {

    public Text UsernameWelcomeSpeech;          // The text object welcoming the player
    public GameObject SpeechBubbleObject;       // The speech Bubble Image
    public string WebstoreURL;                  // The URL to the play store

    /// <summary>
    /// Awake Function, called before start.
    /// </summary>
    void Awake()
    {
        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder()
            .EnableSavedGames()
                .Build();

        PlayGamesPlatform.InitializeInstance(config);
        PlayGamesPlatform.DebugLogEnabled = false;
        PlayGamesPlatform.Activate();

        // If successful login to Google Play...
        Social.localUser.Authenticate((bool success) =>
        {
            if(success)
            {
                SpeechBubbleObject.SetActive(true);
                UsernameWelcomeSpeech.text = "Welcome back, " + Social.localUser.userName.ToString() +"!";
                Debug.Log("Successful");
            } else
            {
                //do nothing
            }
        });
    }

    /// <summary>
    /// Show the achievements to the player
    /// </summary>
    public void ShowAchievements()
    {
        if(Social.localUser.authenticated == true)
        {
            Social.ShowAchievementsUI();
        }
        
    }

    /// <summary>
    /// Level Select Function
    /// </summary>
    public void LevelSelect()
    {
        SceneManager.LoadScene("LevelSelect");
    }

    /// <summary>
    /// Exit Game Function
    /// </summary>
    public void ExitGame()
    {
        AndroidActivities t = new AndroidActivities();
        t.ExitApplication();
    }   

    public void RateGame()
    {
            AndroidJavaClass uriClass = new AndroidJavaClass("android.net.Uri");
            AndroidJavaObject uriObject = uriClass.CallStatic<AndroidJavaObject>("parse", WebstoreURL);

            AndroidJavaClass intentClass = new AndroidJavaClass("android.content.Intent");
            AndroidJavaObject intentObject = new AndroidJavaObject(
                            "android.content.Intent",
                            intentClass.GetStatic<string>("ACTION_VIEW"),
                            uriObject
            );

            AndroidJavaClass unity = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject currentActivity = unity.GetStatic<AndroidJavaObject>("currentActivity");

            currentActivity.Call("startActivity", intentObject);
    }

}
