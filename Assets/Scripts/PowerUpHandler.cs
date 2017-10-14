using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class PowerUpHandler : MonoBehaviour {

    // Text Callbacks
    public Text PowerUpBallTextCounter;
    public Text PowerUpRewindCounter;

    // Button Callbacks
    //public Button PowerUpBallUIButton;

    // Counters
    private int SpinningBallInteger;
    private int RewindTimeInteger;

    private static PowerUpHandler sInstance;

    public static bool PowerUpActive;
    public static bool PowerUpBallActivated;
    public static bool RewindTimeActiavated;

    // Properties for PowerUp (PowerBall)
    private float angle = 0;
    private float speed = (5 * Mathf.PI) / 5;
    private float radius = 5;

    private float RewindPositionTracker;

    private GameObject PowerUpClone;

    public GameObject[] PowerUpGameobjectArray;

    public static PowerUpHandler Instance
    {
        get
        {
            return sInstance;
        }
    } 

	// Use this for initialization
	void Start () {
        SpinningBallInteger = 2;
        RewindTimeInteger = 2;

        
    }
	
	// Update is called once per frame
	void Update () {
        
        // Set Text Information
        //PowerUpBallTextCounter.text = GameManager.GlobalSpinngBall.ToString();
        //PowerUpRewindCounter.text = GameManager.GlobalRewinds.ToString();
        if (PowerUpBallActivated && PowerUpActive)
        {
            angle += speed * Time.deltaTime;
            PowerUpClone.transform.rotation = new Quaternion(Mathf.Cos(angle) * radius, Mathf.Sin(angle) * radius, 0, 1);
        }

        if(Input.GetKeyDown(KeyCode.P))
        {
            TestCall();
        }

        if(RewindTimeActiavated)
        {
            
            gameObject.transform.localPosition = new Vector2(Mathf.Lerp(transform.localPosition.x, RewindPositionTracker, 0.05f), transform.localPosition.y);
        }
	}

    void TestCall()
    {
        PowerUpCallerMethod("RewindTime");
    }

    // The function called for different powerups. I.e. What they do when colliding
    // with another game object.
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.name == "scene_03_pandulum")
        {
            Destroy(other.gameObject);
        }
    }

    public void PowerUpCallerMethod(string PowerUpName)
    {
        GameObject GameObjectReference;

        switch (PowerUpName)
        {
            case "SpinngBall":

                if (GameManagerNew._p_spinningBalls >= 1)
                {
                    GameObjectReference = PowerUpGameobjectArray[0].gameObject;
                    PowerUpClone = Instantiate(GameObjectReference, transform.position, transform.rotation) as GameObject;
                    PowerUpClone.transform.SetParent(this.gameObject.transform);
                    PowerUpBallActivated = true;
                    PowerUpActive = true;
                    GameManagerNew._p_spinningBalls--;

                    // Start the timer for destroying the clone
                    StartCoroutine(PowerUpTimer(5));
                } 

                break;

            case "RewindTime":

                if(GameManagerNew._p_spinningBalls >= 1)
                {
                    GetComponent<Rigidbody2D>().isKinematic = true;
                    RewindPositionTracker = gameObject.transform.localPosition.x - 5;
                    RewindTimeActiavated = true;
                    StartCoroutine(NonObjectTimerCounter(1.5f));
                    GameManagerNew._p_spinningBalls--;
                }

                break;
        }
        

    }

    IEnumerator PowerUpTimer(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        PowerUpBallActivated = false;
        PowerUpActive = false;
        Destroy(PowerUpClone.gameObject);
    }

    IEnumerator NonObjectTimerCounter(float seconds)
    {
        PlayerController.Instance.canJump = false;
        yield return new WaitForSeconds(seconds);
        Debug.Log("Message: NonObjectTimerCounter has finished.");
        RewindTimeActiavated = false;
        GetComponent<Rigidbody2D>().isKinematic = false;

    }
}
