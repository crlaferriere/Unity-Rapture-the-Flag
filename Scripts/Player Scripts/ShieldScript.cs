using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ShieldScript : MonoBehaviour
{
    /* Manages the aspects of the shield for each character */
    // References for the shield's life and what shield it is
    public int shieldLife, shieldRecharge;
    public bool demonShield, angelShield;
    // References to colors, current position, and the stun icon for being stunned.
    public Vector3 curr;
    public GameObject stunIcon, parent;
    public Camera mainCamera;
    public bool cantBeStunned;
    private int curScene;
    // Sets the default parameters for the shield.
    void Awake()
    {
        shieldLife = 3;
        shieldRecharge = 0;
        curr = transform.localScale;
        cantBeStunned = false;
        // Create a temporary reference to the current scene.
        curScene = SceneManager.GetActiveScene().buildIndex;

        if (transform.parent.gameObject.CompareTag("Demon"))
        {
            demonShield = true;
        }
        else if (transform.parent.gameObject.CompareTag("Angel"))
        {
            angelShield = true;
        }
    }
    // If you lose your shield you get stunned and can't shield.
    void Update()
    {
        if (shieldLife <= 0)
        {
            GetComponentInParent<PlayerScript>().canShield = false;
        }
        else
        {
            GetComponentInParent<PlayerScript>().canShield = true;
        }

        rechargeShield();
        if (shieldLife == 3)
            transform.localScale = curr;
    }
    // Recharges the shield when over time
    public void rechargeShield()
    {
        shieldRecharge++;
        if (shieldRecharge >= 150)
        {
            if (shieldLife <= 2)
                shieldLife++;
        }
    }
    // Changes the state of the player to stunned depending on what it hits.
    void OnCollisionStay2D(Collision2D other)
    {
        //Demon Shield Behavior
        if (demonShield == true && GetComponentInParent<PlayerScript>().dead == false)
        {
            // If you are meleeing and hit an angel and they aren't meleeing, kill them.
            if ((other.collider.gameObject.tag == "Angel" && GetComponentInParent<PlayerScript>().meleeing && !other.gameObject.GetComponent<PlayerScript>().meleeing))
                other.gameObject.GetComponent<PlayerScript>().Die();
        }

        //Angel Shield Behavior
        if (angelShield == true && GetComponentInParent<PlayerScript>().dead == false)
        {
            // If you are meleeing and hit an angel and they aren't meleeing, kill them.
            if ((other.collider.gameObject.tag == "Demon" && GetComponentInParent<PlayerScript>().meleeing && !other.gameObject.GetComponent<PlayerScript>().meleeing) && !other.collider.gameObject.GetComponentInParent<PlayerScript>().shieldPowerUp)
                other.gameObject.GetComponent<PlayerScript>().Die();
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        //Demon Shield Behavior
        if (demonShield == true)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Angel Bullet") && !cantBeStunned)
            {
                shieldRecharge = 0;
                shieldLife--;
                rechargeShield();
                if (GetComponentInParent<PlayerScript>().meleeing == true)
                {
                    Destroy(other.gameObject);
                    GetComponentInParent<PlayerScript>().stunned = true;
                }
                if (shieldLife == 2)
                    transform.localScale = new Vector3(transform.localScale.x * 0.8f, transform.localScale.y, transform.localScale.z);
                else if (shieldLife == 1)
                    transform.localScale = new Vector3(transform.localScale.x * 0.6f, transform.localScale.y, transform.localScale.z);
                else if (shieldLife == 0)
                {
                    Destroy(other.gameObject);
                    GetComponentInParent<PlayerScript>().stunned = true;
                }
            }
            //Demon Shield Behavior
            if (demonShield == true && GetComponentInParent<PlayerScript>().dead == false)
            {
                // If you are meleeing and hit an enemy shield and they are not meleeing and they don't have the shield power up, stun them
                if ((other.collider.gameObject.tag == "Angel Shield" && GetComponentInParent<PlayerScript>().meleeing && !other.collider.gameObject.GetComponentInParent<PlayerScript>().meleeing) && !other.collider.gameObject.GetComponentInParent<PlayerScript>().shieldPowerUp)
                    other.gameObject.GetComponent<PlayerScript>().stunned = true;
                // If you are meleeing and you hit an angel shield and they are meleeing and don't have the shield power up, stun them.
                if ((other.collider.gameObject.tag == "Angel Shield" && GetComponentInParent<PlayerScript>().meleeing && other.collider.gameObject.GetComponentInParent<PlayerScript>().meleeing) && !other.collider.gameObject.GetComponentInParent<PlayerScript>().shieldPowerUp)
                    other.gameObject.GetComponent<PlayerScript>().stunned = true;
            }
        }
        else if (angelShield == true)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Demon Bullet"))
            {
                shieldRecharge = 0;
                shieldLife--;
                rechargeShield();
                if (GetComponentInParent<PlayerScript>().meleeing == true)
                {
                    Destroy(other.gameObject);
                    GetComponentInParent<PlayerScript>().stunned = true;
                }
                if (shieldLife == 2)
                    transform.localScale = new Vector3(transform.localScale.x * 0.8f, transform.localScale.y, transform.localScale.z);
                else if (shieldLife == 1)
                    transform.localScale = new Vector3(transform.localScale.x * 0.6f, transform.localScale.y, transform.localScale.z);
                else if (shieldLife == 0)
                {
                    Destroy(other.gameObject);
                    GetComponentInParent<PlayerScript>().stunned = true;
                }
            }         
            //Angel Shield Behavior
            if (angelShield == true && GetComponentInParent<PlayerScript>().dead == false)
            {
                // If you are meleeing and hit an enemy shield and they are not meleeing and they don't have the shield power up, stun them
                if ((other.collider.gameObject.tag == "Demon Shield" && GetComponentInParent<PlayerScript>().meleeing && !other.collider.gameObject.GetComponentInParent<PlayerScript>().meleeing) && !other.collider.gameObject.GetComponentInParent<PlayerScript>().shieldPowerUp)
                    other.gameObject.GetComponent<PlayerScript>().stunned = true;
                // If you are meleeing and you hit an angel shield and they are meleeing and don't have the shield power up, stun them.
                if ((other.collider.gameObject.tag == "Demon Shield" && GetComponentInParent<PlayerScript>().meleeing && other.collider.gameObject.GetComponentInParent<PlayerScript>().meleeing) && !other.collider.gameObject.GetComponentInParent<PlayerScript>().shieldPowerUp)
                    other.gameObject.GetComponent<PlayerScript>().stunned = true;
            }
        }

            //stop the player if they hit something while meleeing
            if (GetComponentInParent<PlayerScript>().meleeing)
            {
                if ((other.collider.gameObject.tag == "Angel") || (other.collider.gameObject.tag == "Demon"))
                    return;
                else
                    GetComponentInParent<PlayerScript>().meleeing = false;
            }
        }
    }



