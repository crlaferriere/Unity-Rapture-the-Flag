using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shotsPowerUp : MonoBehaviour {
    /* Manages the power up that spawns in the center of the map */
    // References to the players, life, and numbers
    public GameObject[] angels;
    public GameObject[] demons;
    [SerializeField] int life;
    [SerializeField] private GameObject child;
    public GameObject PowerDownAudioOb;
    public AudioClip PowerUp;
    public bool angelSpeedPowerUp, angelShotPowerUp, demonSpeedPowerUp, demonShotPowerUp;
    private bool masterPowerUpTracker;
    // Sets life, and picks what type of powerup it will be.
    void Awake()
    {
        PowerDownAudioOb = GameObject.Find("PowerDownAudio");
        life = 5;
        //angelSpeedPowerUp = false; angelShotPowerUp = false;
        //demonSpeedPowerUp = false; demonShotPowerUp = false;
        masterPowerUpTracker = false;

        if (PlayerPrefs.GetInt("moveSpeedModifier") == 1)
        {
            angelSpeedPowerUp = true;
            demonSpeedPowerUp = true;
        }
        else
        {
            angelSpeedPowerUp = false;
            demonSpeedPowerUp = false;
        }
        if (PlayerPrefs.GetInt("shotSpeedModifier") == 1)
        {
            angelShotPowerUp = true;
            demonShotPowerUp = true;
        }
        else
        {
            angelShotPowerUp = false;
            demonShotPowerUp = false;
        }
    }
    
    // When it is shot, decrease in size and if killed, give that team a power boost.
    void OnCollisionEnter2D (Collision2D other)
    {
        // If the power up is shot last by an angel, give the angel team power ups.
        if (other.gameObject.layer == LayerMask.NameToLayer("Angel Bullet"))
        {
            life--;
            transform.localScale -= new Vector3(0.1f, 0.1f, 0f); 

            if (life == 1)
            {
                GetComponent<AudioSource>().PlayOneShot(PowerUp);
                this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
                this.gameObject.GetComponent<CircleCollider2D>().enabled = false;
                angels = GameObject.FindGameObjectsWithTag("Angel");
                GetComponentInChildren<SpriteRenderer>().enabled = false;
                Destroy(child);
                if (gameObject.name == "Shots PowerUp(Clone)")
                {
                    angelShotPowerUp = true;             
                }
                else if (gameObject.name == "Shield PowerUp(Clone)")
                {
                    
                }
                else if (gameObject.name == "Speed PowerUp(Clone)")
                {
                    angelSpeedPowerUp = true;
                }
                Invoke("AngelPowerUpEnd", 10);
            }

        // If the power up is collected by a demon, give the demon team power ups.
        } else if (other.gameObject.layer == LayerMask.NameToLayer("Demon Bullet")) {
            life--;
            transform.localScale -= new Vector3(0.1f, 0.1f, 0f);

            if (life == 1)
            {

                GetComponent<AudioSource>().PlayOneShot(PowerUp);
                this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
                this.gameObject.GetComponent<CircleCollider2D>().enabled = false;
                demons = GameObject.FindGameObjectsWithTag("Demon");
                GetComponentInChildren<SpriteRenderer>().enabled = false;
                Destroy(child); 
                if (gameObject.name == "Shots PowerUp(Clone)")
                {
                    demonShotPowerUp = true;
                }
                else if (gameObject.name == "Shield PowerUp(Clone)")
                {

                }
                else if (gameObject.name == "Speed PowerUp(Clone)")
                {
                    demonSpeedPowerUp = true;
                }
                Invoke("DemonPowerUpEnd", 10);
            }
        }
    }
    // Turns off the power up for either team.
    void AngelPowerUpEnd()
    {
        if (PlayerPrefs.GetInt("shotSpeedModifier") == 0)
        {  
            angelShotPowerUp = false;
        }
        if (PlayerPrefs.GetInt("moveSpeedModifier") == 0)
        {
            angelSpeedPowerUp = false;
        }
        PowerDownAudioOb.GetComponent<AudioSource>().Play();
        Destroy(this.gameObject);
    }
    void DemonPowerUpEnd()
    {
        if (PlayerPrefs.GetInt("shotSpeedModifier") == 0)
        {  
            demonShotPowerUp = false;
        }
        if (PlayerPrefs.GetInt("moveSpeedModifier") == 0)
        {
            demonSpeedPowerUp = false;
        }
        PowerDownAudioOb.GetComponent<AudioSource>().Play();
        Destroy(this.gameObject);
    }
}
