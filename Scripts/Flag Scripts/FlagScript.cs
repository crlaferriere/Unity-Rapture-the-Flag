using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagScript : MonoBehaviour {
    /* Managing all aspects of how the flags work */
    // Bools for tracking states of the flag
    public bool inBase, beingThrown, onPlayer, beingReturned; 
    // Reference to the flag's base
    public Transform flagHome;
    // References to throw speed and the players
    public float throwSpeed;
    public GameObject Lilith, Azazel, Urial, Barachial;
    // References for returning the flag
    public int returnTimer;
    //set audio clips and int for randomizing the sounds
    public AudioClip demonOne, demonTwo, demonThree, demonFour, angelOne, angelTwo, angelThree, angelFour;
    public AudioClip Bounce, Pickup, Return;
    private int whichOne;
    private bool canPlayDemon;
    private bool canPlayAngel;
    public GameObject rune1, rune2, rune3, rune4, rune5, rune6, rune7, rune8;
    [SerializeField] Sprite[] Sprites;
    SpriteRenderer spriteHandler; 
    // Set the default bools and speed and sprite of the flag
    void Awake ()
    {
        canPlayAngel = true;
        canPlayDemon = true;
        inBase = true;
        beingThrown = false;
        onPlayer = false;
        beingReturned = false; 
        throwSpeed = 10;
        InvokeRepeating("FlagThrowSpeed", 0.5f, 0.5f);
        spriteHandler = GetComponent<SpriteRenderer>();
    }
    // Updates how the flag interacts based on what state it is currently in.
    void Update()
    {  
        // if the flag is in the base, rotate it and make its runes are off
        if (inBase == true)
        {
            canPlayAngel = true;
            canPlayDemon = true;
            transform.position = flagHome.transform.position;
            transform.rotation = flagHome.transform.rotation; 
            FlagRunesOff();
            beingReturned = false; 
            spriteHandler.sprite = Sprites[0];                
            onPlayer = false;                     
        }
        //if the flag is being returned, and its not in the base, increase its return timer
        if (beingReturned == true)
        {
            if (inBase == false )
            {
                returnTimer++;    
            }
            else
            {
                returnTimer = 0;
            }
        }
      
        // if the flag return timer hits 150, return the flag to its base
        if (returnTimer >= 150)
        {
         
            inBase = true;
            GetComponent<AudioSource>().PlayOneShot(Return);
            returnTimer = 0;
            canPlayAngel = true;
            canPlayDemon = true;       
            onPlayer = false;         
        }

        if (throwSpeed < 0.19f)
        {
            throwSpeed = 0.2f; 
        }
        //switch statement for the runes around the flag
        switch (returnTimer)
        {
            case 1:
                rune1.GetComponent<SpriteRenderer>().enabled = true;
                break;
            case 18:
                rune2.GetComponent<SpriteRenderer>().enabled = true;
                break;
            case 37:
                rune3.GetComponent<SpriteRenderer>().enabled = true;
                break;
            case 55:
                rune4.GetComponent<SpriteRenderer>().enabled = true;
                break;
            case 74:
                rune5.GetComponent<SpriteRenderer>().enabled = true;
                break;
            case 92:
                rune6.GetComponent<SpriteRenderer>().enabled = true;
                break;
            case 110:
                rune7.GetComponent<SpriteRenderer>().enabled = true;
                break;
            case 135:
                rune8.GetComponent<SpriteRenderer>().enabled = true;
                break;
        }
        //if the flag isnt being throw, make sure its on trigger so it doesnt bounce
        if (!beingThrown)
        {
            GetComponent<CircleCollider2D>().isTrigger = true;
            GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 0f);
            if (!inBase)
                spriteHandler.sprite = Sprites[1];
        }
        //if the flag is thrown and its not stopped, make sure it reverts to a regular collider to it bounces
        if (beingThrown && GetComponent<CircleCollider2D>().isTrigger != false)
        {
            Invoke("TriggerOff", 0.1f);
            tossFlag();
        }
    }
    // Set the flag to not being a trigger anymore
    void TriggerOff()
    {
        GetComponent<CircleCollider2D>().isTrigger = false;
    }
    //if the flag is not being thrown, increase the distance it travels
    void FlagThrowSpeed()
    {
        if (!beingThrown)
        {
            if (throwSpeed < 1f)
                throwSpeed += 0.1f;
            else if (throwSpeed > 1)
                throwSpeed = 1;
        }
    }
    // Detections for how the flag interacts with other players
    void OnCollisionEnter2D (Collision2D other)
    {
        GetComponent<AudioSource>().PlayOneShot(Bounce);
        //if this is the Angel flag, and its collider is not set to "isTrigger"
        if (this.gameObject.CompareTag("Angel Flag") && GetComponent<CircleCollider2D>().isTrigger == false)
        {
            //Debug.Log("This is onCollisionEnter: " + other.gameObject.name);
            //if this character is interacting with it is an angel
            if (other.gameObject.CompareTag("Angel"))
            {             
                //turn its collider's isTrigger to true
                GetComponent<CircleCollider2D>().isTrigger = true;
                //set stopped bool to true so the flag can setActive its ring
                beingThrown = false; 
                //make sure it completely stops any velocity so it isnt floating around
                GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 0f);
            }
            //if this character is interacting with it is a demon
            else if (other.gameObject.CompareTag("Demon") && other.gameObject.GetComponent<PlayerScript>().meleeing == false)
            {        
                // turn off its rigidbody, and set it to being taken
                GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 0f);
                GetComponent<CircleCollider2D>().isTrigger = true;
                FlagTaken();
                FlagRunesOff();
                returnTimer = 0;
                whichOne = Random.Range(0, 4);
                if (whichOne == 0 && canPlayDemon == true)
                {
                    GetComponent<AudioSource>().Stop();
                    canPlayDemon = false;
                    GetComponent<AudioSource>().PlayOneShot(demonOne);
                    //Invoke("ResetAudioCD", 2f);
                }
                if (whichOne == 1 && canPlayDemon == true)
                {
                    GetComponent<AudioSource>().Stop();
                    canPlayDemon = false;
                    GetComponent<AudioSource>().PlayOneShot(demonTwo);
                 //   Invoke("ResetAudioCD", 2f);
                }
                if (whichOne == 2 && canPlayDemon == true)
                {
                    GetComponent<AudioSource>().Stop();
                    canPlayDemon = false;
                    GetComponent<AudioSource>().PlayOneShot(demonThree);
                  //  Invoke("ResetAudioCD", 2f);
                }
                if (whichOne == 3 && canPlayDemon == true)
                {
                    GetComponent<AudioSource>().Stop();
                    canPlayDemon = false;
                    GetComponent<AudioSource>().PlayOneShot(demonFour);
                    //Invoke("ResetAudioCD", 2f);
                }
            }
        }
        //if this is the Demon flag, and its collider is not set to "isTrigger"
        else if (this.gameObject.CompareTag("Demon Flag") && GetComponent<CircleCollider2D>().isTrigger == false)
        {
            //if this character is interacting with it is an demon
            if (other.gameObject.CompareTag("Demon"))
            {               
                //turn its collider's isTrigger to true
                GetComponent<CircleCollider2D>().isTrigger = true;
                //set stopped bool to true so the flag can setActive its ring
                beingThrown = false; 
                //make sure it completely stops any velocity so it isnt floating around
                GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 0f);
            }
            //if this character is interacting with it is an angel
            else if (other.gameObject.CompareTag("Angel") && other.gameObject.GetComponent<PlayerScript>().meleeing == false && !onPlayer)
            {
                // turn off its rigidbody, and set it to being taken
                GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 0f);
                GetComponent<CircleCollider2D>().isTrigger = true;
                FlagRunesOff();
                returnTimer = 0;
                FlagTaken();
                whichOne = Random.Range(0, 4);
                if (whichOne == 0 && canPlayDemon == true)
                {
                    GetComponent<AudioSource>().Stop();
                    canPlayDemon = false;
                    GetComponent<AudioSource>().PlayOneShot(angelOne);
                    //Invoke("ResetAudioCD", 2f);
                }
                if (whichOne == 1 && canPlayDemon == true)
                {
                    GetComponent<AudioSource>().Stop();
                    canPlayDemon = false;
                    GetComponent<AudioSource>().PlayOneShot(angelTwo);
                   // Invoke("ResetAudioCD", 2f);
                }
                if (whichOne == 2 && canPlayDemon == true)
                {
                    GetComponent<AudioSource>().Stop();
                    canPlayDemon = false;
                    GetComponent<AudioSource>().PlayOneShot(angelThree);
                    //Invoke("ResetAudioCD", 2f);
                }
                if (whichOne == 3 && canPlayDemon == true)
                {
                    GetComponent<AudioSource>().Stop();
                    canPlayDemon = false;
                    GetComponent<AudioSource>().PlayOneShot(angelFour);
                    //Invoke("ResetAudioCD", 2f);
                }
            }
        }
    }
    // When triggering with something, the flag interacts differently
    void OnTriggerEnter2D(Collider2D other)
    {
        // If this is an angel flag and a demon touches it, it gets taken.
        if (gameObject.CompareTag("Angel Flag"))
        {
            if (other.gameObject.CompareTag("Demon") && !other.gameObject.GetComponent<PlayerScript>().meleeing && !beingReturned && !onPlayer)
            { 
                FlagRunesOff();
                returnTimer = 0;
                FlagTaken();
                whichOne = Random.Range(0, 4);
                if (whichOne == 0 && canPlayDemon == true)
                {
                    GetComponent<AudioSource>().Stop();
                    canPlayDemon = false;
                    GetComponent<AudioSource>().PlayOneShot(demonOne);
                   // Invoke("ResetAudioCD", 2f);
                }
                if (whichOne == 1 && canPlayDemon == true)
                {
                    GetComponent<AudioSource>().Stop();
                    canPlayDemon = false;
                    GetComponent<AudioSource>().PlayOneShot(demonTwo);
                   // Invoke("ResetAudioCD", 2f);
                }
                if (whichOne == 2 && canPlayDemon == true)
                {
                    GetComponent<AudioSource>().Stop();
                    canPlayDemon = false;
                    GetComponent<AudioSource>().PlayOneShot(demonThree);
                  //  Invoke("ResetAudioCD", 2f);
                }
                if (whichOne == 3 && canPlayDemon == true)
                {
                    GetComponent<AudioSource>().Stop();
                    canPlayDemon = false;
                    GetComponent<AudioSource>().PlayOneShot(demonFour);
                 //   Invoke("ResetAudioCD", 2f);
                }             
            }
        }
        
        // If this is a demon flag and an angel touches it, it gets taken.
        else if (gameObject.CompareTag("Demon Flag"))
        {
            if (other.gameObject.CompareTag("Angel") && !other.gameObject.GetComponent<PlayerScript>().meleeing && !beingReturned && !onPlayer)
            {
                FlagRunesOff();
                returnTimer = 0;
                FlagTaken();              
                whichOne = Random.Range(0, 4);
                if (whichOne == 0 && canPlayDemon == true)
                {
                    GetComponent<AudioSource>().Stop();
                    canPlayDemon = false;
                    GetComponent<AudioSource>().PlayOneShot(angelOne);
                  //  Invoke("ResetAudioCD", 2f);
                }
                if (whichOne == 1 && canPlayDemon == true)
                {
                    GetComponent<AudioSource>().Stop();
                    canPlayDemon = false;
                    GetComponent<AudioSource>().PlayOneShot(angelTwo);
                  //  Invoke("ResetAudioCD", 2f);
                }
                if (whichOne == 2 && canPlayDemon == true)
                {
                    GetComponent<AudioSource>().Stop();
                    canPlayDemon = false;
                    GetComponent<AudioSource>().PlayOneShot(angelThree);
                   // Invoke("ResetAudioCD", 2f);
                }
                if (whichOne == 3 && canPlayDemon == true)
                {
                    GetComponent<AudioSource>().Stop();
                    canPlayDemon = false;
                    GetComponent<AudioSource>().PlayOneShot(angelFour);
                  //  Invoke("ResetAudioCD", 2f);
                }              
            }
        }            
    }
    
    // Changes the way the flag interacts when a character is staying on it
    void OnTriggerStay2D(Collider2D other)
    {      
        // If this is an angel flag and an angel touches it, start returning the flag
        if (gameObject.CompareTag("Angel Flag"))
        {
            if ((Azazel.GetComponent<PlayerScript>().hasFlag == false && Lilith.GetComponent<PlayerScript>().hasFlag == false))
            {
                if (other.gameObject.CompareTag("Angel"))
                {
                    if (other.gameObject.GetComponent<PlayerScript>().dead == false && !beingThrown)
                    {
                        beingReturned = true;
                    }
                    else
                    {
                        beingReturned = false;
                    }
                }
            }
        }
        // If this is a demon flag and a demon touches it, start returning the flag
        else if (gameObject.CompareTag("Demon Flag"))
        {
            if ((!Barachial.GetComponent<PlayerScript>().hasFlag && !Urial.GetComponent<PlayerScript>().hasFlag))
            {
                if (other.gameObject.CompareTag("Demon"))
                {
                    if (other.gameObject.GetComponent<PlayerScript>().dead == false && !beingThrown)
                    {
                        beingReturned = true;
                    }
                    else
                    {
                        beingReturned = false;
                    }
                }
            }
        }
    
    }
    // Change states of the flag based on what is leaving the flag area
    void OnTriggerExit2D(Collider2D other)
    {
        // If this is an angel flag and an angel stops touching it, stop returning the flag
        if (gameObject.CompareTag("Angel Flag") || gameObject.GetComponent<Collider2D>().name == "Blue Inner")
        {
            if (other.gameObject.CompareTag("Angel"))
            {
                beingReturned = false;
            }
        }
        // If this is a demon flag and a demon stops touching it, stop returning the flag
        else if (gameObject.CompareTag("Demon Flag") || gameObject.CompareTag("Demon Flag"))
        {
            if (other.gameObject.CompareTag("Demon"))
            {
                beingReturned = false;
            }
        }
    }
    // Turns all of the runes off
    public void FlagRunesOff()
        {
        rune1.GetComponent<SpriteRenderer>().enabled = false;
        rune2.GetComponent<SpriteRenderer>().enabled = false;
        rune3.GetComponent<SpriteRenderer>().enabled = false;
        rune4.GetComponent<SpriteRenderer>().enabled = false;
        rune5.GetComponent<SpriteRenderer>().enabled = false;
        rune6.GetComponent<SpriteRenderer>().enabled = false;
        rune7.GetComponent<SpriteRenderer>().enabled = false;
        rune8.GetComponent<SpriteRenderer>().enabled = false;
        returnTimer = 0;
    }
    // If the flag is taken and not being returned, change settings of the flag
    void FlagTaken()
    {
        if (!beingReturned)
        {
            GetComponent<AudioSource>().PlayOneShot(Pickup);
            beingReturned = false;
            FlagRunesOff();
            returnTimer = 0;
            inBase = false;
            onPlayer = true;
            beingThrown = false;          
        }
        else if (beingReturned)
        {
            onPlayer = false;
        }
    }
    // Throwing the flag settings
    void tossFlag()
    {
        GetComponent<Rigidbody2D>().AddForce(-transform.up * 5, ForceMode2D.Impulse);
        spriteHandler.sprite = Sprites[2];
        onPlayer = false;
    }
    void ResetAudioCD()
    {
        canPlayAngel = true;
        canPlayDemon = true;
    }
}
