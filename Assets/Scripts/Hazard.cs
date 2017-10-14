using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
public class Hazard : MonoBehaviour {

    public float angle = 0;
    public float speed = (2 * Mathf.PI) / 5;
    public float radius = 5;
    public List<GameObject> HazardObjects;
    public GameObject _hitPointPrefab;
    public Sprite PlayerHitSprite;

    private Transform player;

    public class CloudHazardClass
    {
        GameObject MYGO;
        float WEIGHT;
    }

    public enum HazardTypes
    {
        Slider,
        Rotater,
        CloudCannon
    }

    public HazardTypes hazardTypes;

    // Use this for initialization
    void Start () {

        player = GameObject.FindGameObjectWithTag("Player").transform;

        if(hazardTypes == HazardTypes.CloudCannon)
        {
            InvokeRepeating("PushCannonBall", 0, 4);
        }
	}
	
	// Update is called once per frame
	void Update () {

        Vector2 playerTransform = transform.InverseTransformPoint(player.position);

        switch(hazardTypes)
        {
            case HazardTypes.Rotater:
                if (playerTransform.x < 0.0f)
                {
                    GetComponent<BoxCollider2D>().enabled = true;
                }

                if (playerTransform.x > 0.0f)
                {
                    GetComponent<BoxCollider2D>().enabled = false;
                }
            break;

            case HazardTypes.Slider:
                if (playerTransform.x < 0.0f)
                {
                    GetComponent<BoxCollider2D>().enabled = true;
                }

                if (playerTransform.x > 0.0f)
                {
                    GetComponent<BoxCollider2D>().enabled = false;
                }
            break;
        }

        if(hazardTypes == HazardTypes.Rotater)
        {
            angle += speed * Time.deltaTime;
            transform.rotation = new Quaternion(Mathf.Cos(angle) * radius, Mathf.Sin(angle) * radius, 0, 1);
        }

        if(hazardTypes == HazardTypes.Slider)
        {
            if (transform.localPosition.x <= -3.3)
            {
                GetComponent<SliderJoint2D>().angle = 180;
            }

            if (transform.localPosition.x >= 1)
            {
                GetComponent<SliderJoint2D>().angle = 0;
            }
        }


	}

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Vector2 direction = Vector2.left * 5f;
            collision.gameObject.GetComponent<AudioSource>().PlayOneShot(collision.gameObject.GetComponent<PlayerController>().audioClips[0]);
            //Debug.Break();
            collision.gameObject.GetComponent<Animator>().enabled = false;
            collision.gameObject.GetComponent<SpriteRenderer>().sprite = PlayerHitSprite;
            StartCoroutine(ChangeSpriteBack(collision.gameObject));
            ContactPoint2D contact = collision.contacts[0];
            Quaternion rot = Quaternion.FromToRotation(Vector2.up, contact.normal);
            Vector2 pos = contact.point;
            GameObject temp = Instantiate(_hitPointPrefab, pos, rot) as GameObject;
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(direction, ForceMode2D.Impulse);
            Destroy(temp, 0.5f);
        }
    }

    void PushCannonBall()
    {
        int random = Random.Range(0, 2);
        Debug.Log(random);
        GameObject temp;

        switch(random)
        {
            case 0:
                temp = Instantiate(HazardObjects[0], transform.position, transform.rotation) as GameObject;
            break;

            case 1:
                temp = Instantiate(HazardObjects[1], transform.position, transform.rotation) as GameObject;
            break;
        }
        
    }

    public IEnumerator ChangeSpriteBack(GameObject player)
    {
        yield return new WaitForSeconds(.5f);
        
        player.GetComponent<Animator>().enabled = true;
    }




}
