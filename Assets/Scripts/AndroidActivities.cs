using UnityEngine;
using System.Collections;

public class AndroidActivities : MonoBehaviour {
    
    public void ExitApplication()
    {
        AndroidJavaObject activity = new AndroidJavaClass("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>("currentActivity");
        activity.Call<bool>("moveTaskToBack", true);
    }
}
