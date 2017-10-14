using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour {

    public Canvas canvas;
    public GameObject StoreFront;
    public float velocity = 1.2f;
    private GameObject mainCam;
    public bool canJump;
    public Text CoinCounter;
    public int CoinsCollectedThisLevel;
    public Text GiftCounter;
    public int GiftsCollectedThisLevel;
    public GameObject currentPowerUp;

    public AudioClip[] audioClips;

    private int livesLeft;

    public static int levelNumber;
    public static int currentLevelScore;


    private bool SpinningBallPowerUpCalled;
    GameObject Clone;
    // Achievement Setup

    private static PlayerController sInstance = new PlayerController();

    public static PlayerController Instance
    {
        get
        {
            return sInstance;
        }
    }


    // Use this for initialization
    void Start () {

        currentLevelScore = 0;
        canJump = true;
        //GameManager.levelManagement[levelN].score = 0;
        //Debug.Log("Level " + levelN + " fomr player " + GameManager.levelManagement[levelN].score);
        mainCam = GameObject.Find("Main Camera");
        this.gameObject.SetActive(true);
        InvokeRepeating("InvokeAddScore", 0, 2);
	}
	
	// Update is called once per frame
	void Update () {

        if(SpinningBallPowerUpCalled)
        {
            Debug.Log("Spinng");

        }

        if(CoinsCollectedThisLevel == 5)
        {
            Debug.Log("COMPLETE");
            if(Social.localUser.authenticated == true)
            {
                Social.ReportProgress("CgkI-_6QuqEPEAIQAQ", 100.0f, (bool success) => {
                    if (success)
                    {
                        Debug.Log("ACHIEVE");
                    }
                    else
                    {
                        //do nothing
                    }
                });
            }

        }

        float currentVelocity = GetComponent<Rigidbody2D>().velocity.magnitude;

        CoinCounter.text = CoinsCollectedThisLevel.ToString();
        GiftCounter.text = GiftsCollectedThisLevel.ToString();


        transform.Translate(Vector2.right * 2f * Time.deltaTime);
        
        if(Input.GetMouseButtonDown(0))
        {
            if (transform.position.y < 2.84f)
            {
                if (canJump)
                {
                    GetComponent<Rigidbody2D>().velocity = new Vector2(0, 2);
                }
                StartCoroutine(JumpBird());
                Debug.Log(velocity);
                //StoreFront.SetActive(true);
            }
        }
    }

    void InvokeAddScore()
    {
        currentLevelScore += 50;
    }

    IEnumerator JumpBird()
    {
        canJump = false;
        yield return new WaitForSeconds(.2f);
        canJump = true;
    }

    public void BuyMenu()
    {
        canvas.sortingLayerName = "UI";
        StoreFront.SetActive(true);
        Time.timeScale = 0.0f;
    }

    public void CloseBuyMenu()
    {
        canvas.sortingLayerName = "UI";
        StoreFront.SetActive(false);
        Time.timeScale = 1.0f;
    }
}
