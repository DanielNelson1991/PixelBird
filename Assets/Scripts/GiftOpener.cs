using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
public class GiftOpener : MonoBehaviour {


    [System.Serializable]
    public class GiftProperties
    {
        public Sprite _GiftSprite;
        public string _GiftDesc;
    }

    public Text GiftCounterText;
    public GameObject GiftPanel;
    public List<GiftProperties> GiftPropertyList = new List<GiftProperties>();
    public Text GiftSpriteTextHolder;
    public Image GiftSpriteHolder;
    
    
    // Use this for initialization
    void Start ()
    {
        // Set the gift box count at the start
        GiftCounterText.text = GameManagerNew._g_Gifts.ToString();
        Debug.Log(GameManagerNew._g_Gifts.ToString());
    }

    public void OpenGift()
    {
        Debug.Log(GameManagerNew._g_Gifts);

        if (GameManagerNew._g_Gifts > 0)
        {
            GiftPanel.SetActive(true);

            int itemDrop = Random.Range(0, GiftPropertyList.Count);

            for (int i = 0; i < GiftPropertyList.Count; i++)
            {
                GiftSpriteHolder.sprite = GiftPropertyList[itemDrop]._GiftSprite;
                GiftSpriteTextHolder.text = GiftPropertyList[itemDrop]._GiftDesc;
            }

            GameManagerNew._g_Gifts--;
            GiftCounterText.text = GameManagerNew._g_Gifts.ToString();

        }

        

    }

    public void CloseGift()
    {
        GameManagerNew.SaveGameData();
        GiftPanel.SetActive(false);
    }
}
