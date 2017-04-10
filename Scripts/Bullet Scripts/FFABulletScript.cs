using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FFABulletScript : MonoBehaviour {

    public bool urialB = false, lilithB = false, barachialB = false, azazelB = false;
    /* Manages how the bullet works */
    // References to the bounces, speed, color, particle effect, and rigidbody
    public int BounceNumber;
    public float bulletSpeed;
    public Rigidbody2D rb2D;
    public GameObject spark;
    // Trail renderer settings
    public TrailRenderer Trail;
    [SerializeField]
    Material Red;
    [SerializeField]
    Material Yellow;
    [SerializeField]
    Material Purple;
    [SerializeField]
    Material Orange;
    public AudioClip Bounce;

    void Start ()
    {
        rb2D = GetComponent<Rigidbody2D>();
        rb2D.velocity = -transform.up * bulletSpeed;
        Trail = GetComponent<TrailRenderer>();
    }
	
	void Update ()
    {
        if (BounceNumber <= 0)
            Destroy(this.gameObject);

        if (urialB)
        {
            this.gameObject.tag = "UrialB";
            Trail.material = Orange;
        }
        else if (barachialB)
        {
            this.gameObject.tag = "BarachialB";
            Trail.material = Yellow;
        }
        else if (azazelB)
        {
            this.gameObject.tag = "AzazelB";
            Trail.material = Red;
        }
        else if (lilithB)
        {
            this.gameObject.tag = "LilithB";
            Trail.material = Purple;
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        GetComponent<AudioSource>().PlayOneShot(Bounce);
        BounceNumber--;

        GameObject newSpark = (GameObject)Instantiate(spark, transform.position, transform.rotation);
        Destroy(newSpark, 1f);

        if (other.collider.gameObject.name == "UrialShield")
        {
            urialB = true; barachialB = false; lilithB = false; azazelB = false;
        }
        else if (other.collider.gameObject.name == "BarachialShield")
        {
            Debug.Log("Test");
            urialB = false; barachialB = true; lilithB = false; azazelB = false;
        }
        else if (other.collider.gameObject.name == "LilithShield")
        {
            urialB = false; barachialB = false; lilithB = true; azazelB = false;
        }
        else if (other.collider.gameObject.name == "AzazelShield")
        {
            urialB = false; barachialB = false; lilithB = false; azazelB = true;
        }

        if (this.gameObject.CompareTag("UrialB"))
        {
            if (other.collider.gameObject.name == "Barachial" && !other.gameObject.GetComponent<FFAPlayerScript>().invincible)
            {
                other.gameObject.GetComponent<FFAPlayerScript>().Die();
            }
            else if (other.collider.gameObject.name == "Lilith" && !other.gameObject.GetComponent<FFAPlayerScript>().invincible)
            {
                other.gameObject.GetComponent<FFAPlayerScript>().Die();
            }
            else if (other.collider.gameObject.name == "Azazel" && !other.gameObject.GetComponent<FFAPlayerScript>().invincible)
            {
                other.gameObject.GetComponent<FFAPlayerScript>().Die();
            }
        }
        if (this.gameObject.CompareTag("BarachialB"))
        {
            if (other.collider.gameObject.name == "Urial" && !other.gameObject.GetComponent<FFAPlayerScript>().invincible)
            {
                other.gameObject.GetComponent<FFAPlayerScript>().Die();
            }
            else if (other.collider.gameObject.name == "Lilith" && !other.gameObject.GetComponent<FFAPlayerScript>().invincible)
            {
                other.gameObject.GetComponent<FFAPlayerScript>().Die();
            }
            else if (other.collider.gameObject.name == "Azazel" && !other.gameObject.GetComponent<FFAPlayerScript>().invincible)
            {
                other.gameObject.GetComponent<FFAPlayerScript>().Die();
            }
        }
        if (this.gameObject.CompareTag("LilithB"))
        {
            if (other.collider.gameObject.name == "Urial" && !other.gameObject.GetComponent<FFAPlayerScript>().invincible)
            {
                other.gameObject.GetComponent<FFAPlayerScript>().Die();
            }
            else if (other.collider.gameObject.name == "Barachial" && !other.gameObject.GetComponent<FFAPlayerScript>().invincible)
            {
                other.gameObject.GetComponent<FFAPlayerScript>().Die();
            }
            if (other.collider.gameObject.name == "Azazel" && !other.gameObject.GetComponent<FFAPlayerScript>().invincible)
            {
                other.gameObject.GetComponent<FFAPlayerScript>().Die();
            }
        }
        if (this.gameObject.CompareTag("AzazelB"))
        {
            if (other.collider.gameObject.name == "Urial" && !other.gameObject.GetComponent<FFAPlayerScript>().invincible)
            {
                other.gameObject.GetComponent<FFAPlayerScript>().Die();
            }
            else if (other.collider.gameObject.name == "Barachial" && !other.gameObject.GetComponent<FFAPlayerScript>().invincible)
            {
                other.gameObject.GetComponent<FFAPlayerScript>().Die();
            }
            if (other.collider.gameObject.name == "Lilith" && !other.gameObject.GetComponent<FFAPlayerScript>().invincible)
            {
                other.gameObject.GetComponent<FFAPlayerScript>().Die();
            }
        }
    }
}