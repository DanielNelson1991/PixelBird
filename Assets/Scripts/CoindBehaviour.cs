using UnityEngine;
using UnityEngine.UI;

public class CoindBehaviour : MonoBehaviour {

    public Text CointCounterText;
    public Text GiftCounterText;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

        if (gameObject.name == "Coin")
        {
            transform.Rotate(new Vector2(0, 360 * .5f * Time.deltaTime));
        }
        
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            if(gameObject.name == "GiftBox")
            {
                other.gameObject.GetComponent<PlayerController>().GiftsCollectedThisLevel++;
            } else
            {
                PlayerController.currentLevelScore += 130;
                other.gameObject.GetComponent<PlayerController>().CoinsCollectedThisLevel++;
            }

            
            Destroy(gameObject);

        }
    }
}
