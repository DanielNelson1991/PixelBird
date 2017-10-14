using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems; 
public class OptionsPanel : MonoBehaviour, IPointerClickHandler
{

    public AudioClip[] BackgroundAudio;
    public Dropdown dropdownList;

    public GameObject _go_SettingsMenu;
    public Slider volumeSlider;

    public GameObject[] SettingsButtons;
    public GameObject SettingsMenu;
    public GameObject StoreMenu;

    private bool SettingsButtonsBool;
    private bool SettingsMenuBool;

    private bool StoreButtonsBool;
    private bool StoreMenuBool;

    // Store Items
    public Text _store_SpinningBalls;

    // Use this for initialization
    void Start () {
        SettingsButtonsBool = false;
        //Camera.main.GetComponent<AudioSource>().volume = volumeSlider.GetComponent<Slider>().value;
    }
	
	// Update is called once per frame
	void Update () {
	    
	}

    public void OpenOptionsMenu()
    {

        SettingsButtonsBool = !SettingsButtonsBool;
        Time.timeScale = 0;
        
        for(int i = 0; i < SettingsButtons.Length; i++)
        {
            SettingsButtons[i].SetActive(SettingsButtonsBool);
        }
    }



    public void OnPointerClick(PointerEventData data)
    {
        GameObject.Find("Dropdown List").GetComponent<Canvas>().sortingLayerName = "UI";
    }

    public void IncreaseLevel()
    {
        GetComponent<AudioSource>().volume = GameObject.Find("Slider").GetComponent<Slider>().value;
    }

    public void OpenSettingsMenu()
    {
        SettingsMenuBool = !SettingsMenuBool;
        SettingsMenu.SetActive(SettingsMenuBool);        
    }

    public void OpenStoreMenu()
    {
        StoreMenuBool = !StoreMenuBool;
        StoreMenu.SetActive(StoreMenuBool);

        _store_SpinningBalls.text = "You Have: " + GameManagerNew._p_spinningBalls.ToString();
    }

    /// <summary>
    /// Change music from the settings menu
    /// </summary>
    public void ChangeMusic()
    {
        switch (GameObject.Find("Dropdown").GetComponent<Dropdown>().value)
        {
            case 0:
                GetComponent<AudioSource>().clip = BackgroundAudio[0];
                break;

            case 1:
                GetComponent<AudioSource>().clip = BackgroundAudio[1];
                break;
        }

        GetComponent<AudioSource>().Play();
    }
}
