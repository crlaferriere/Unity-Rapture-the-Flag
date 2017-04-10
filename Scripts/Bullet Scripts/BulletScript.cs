using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour {
    /* Manages how the bullet works */
    // References to the bounces, speed, color, particle effect, and rigidbody
    public int BounceNumber;
    public float bulletSpeed;
    public Rigidbody2D rb2D;
    public GameObject spark;
    // Trail renderer settings
    public TrailRenderer Trail;
    [SerializeField] Material Red;
    [SerializeField] Material Blue;
    public AudioClip Bounce;
    // Set initial movement for bullet and trail renderer
    void Start ()
    {
        rb2D = GetComponent<Rigidbody2D>();
        rb2D.velocity = -transform.up * bulletSpeed;
        Trail = GetComponent<TrailRenderer>();
    }
    // When the bounces number hits 0, kill the bullet.
    void Update ()
    {
        if (BounceNumber <= 0)
            Destroy(this.gameObject);
    }
    // When you hit something, decrease bounce number, play a spark, and change depending on what you hit.
    void OnCollisionEnter2D(Collision2D other)
    {
        GetComponent<AudioSource>().PlayOneShot(Bounce);
        BounceNumber--;
      
        GameObject newSpark = (GameObject)Instantiate(spark, transform.position, transform.rotation);
        Destroy(newSpark, 1f);
        // If you are an angel bullet
        if (this.gameObject.tag == "Angel Bullet")
        {
           // If you hit a demon shield, change bullet type.
           if (other.collider.gameObject.CompareTag("Demon Shield"))
            {
                this.gameObject.layer = 9;
                Trail.material = Red; 
                this.gameObject.tag = "Demon Bullet";
                this.gameObject.layer = 11;
            }
           // If you hit a demon, kill it.
           else if (other.collider.gameObject.CompareTag("Demon"))
                other.gameObject.GetComponent<PlayerScript>().Die();
        }
        // If you are a demon bullet
        else if (this.gameObject.tag == "Demon Bullet")
        {
            // If you hit an angel shield, change bullet type.
            if (other.collider.gameObject.CompareTag("Angel Shield"))
            {
                this.gameObject.layer = 8;
                Trail.material = Blue; 
                this.gameObject.tag = "Angel Bullet";
                this.gameObject.layer = 10;
            }
            // If you hit an angel, kill it.
            else if (other.collider.gameObject.CompareTag("Angel"))
                other.gameObject.GetComponent<PlayerScript>().Die();
        }
    }
}
