using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired; 

public class FFAPlayerScript : MonoBehaviour {

    /* Manages all aspects of each character. */
    // Player settings
    public int playerId; // The Rewired player id of this character
    public float moveSpeed, slowMoveSpeed;
    private Player player; // The Rewired Player
    //private CharacterController cc;
    private Vector3 moveVector;
    private float xSpeed, ySpeed;
    private bool firing, blocking;
    public Rigidbody2D rb;
    public bool invincible;
    Animator invincAnim;
    // Sprite and trail settings
    public ParticleSystem LilithTrail, BarachialTrail, UrialTrail, AzazelTrail;
    // Death settings
    public bool dead;
    public Vector3 SpawnLocation1, SpawnLocation2, SpawnLocation3, SpawnLocation4;
    private int respawnRandomizer;
    public int respawnTimer;
    [SerializeField]
    GameObject deathParticleD;
    [SerializeField]
    GameObject invincibleShield;
    public GameObject azazelRespawnIndicator, lilithRespawnIndicator, urialRespawnIndicator, barachialRespawnIndicator;
    //Stun settings
    public bool stunned, stunIconToggle;
    // Shoot settings
    [SerializeField]
    private bool canFire; 
    public int reloadSpeed;
    // PowerUp Settings
    public bool shieldPowerUp;
    // Bullet settings
    public float bulletSpeed;
    public GameObject bulletPrefab, bulletSpawner;
    // Shield Settings
    public GameObject shield;
    public bool canShield;
    //Melee Settings
    public bool canMelee, meleeing, stunIconShow;
    public float meleeDuration, meleeSpeed, emission;
    public ParticleSystem meleeTrail, SpeedTrail, shotPowerTrail;
    public GameObject stunIcon, stunIconLoc;
    // Flag Settings
    public GameObject urialBase, lilithBase, azazelBase, barachialBase;
    [SerializeField] GameObject[] scoreParticles;
    //camera reference for stunned icon rotation
    public Camera mainCamera;
    // UI Game Object reference
    public GameObject UI;
    public AudioClip Dash;
    public int SoulCount;
    // Animations
    private Animator animator;
    public Transform deathPos;
    public GameObject soulPrefab;
    // Sets all references and assigns characters
    void Awake()
    {
        if (this.gameObject.name == "Urial(Clone)")
            this.gameObject.name = "Urial";
        else if (this.gameObject.name == "Barachial(Clone)")
            this.gameObject.name = "Barachial";
        else if (this.gameObject.name == "Lilith(Clone)")
            this.gameObject.name = "Lilith";
        else if (this.gameObject.name == "Azazel(Clone)")
            this.gameObject.name = "Azazel";
        // Grab the character master and sprite for assignment to the player.
        rb = GetComponent<Rigidbody2D>();
        // Set all bools to proper state
        dead = false;
        invincible = false;
        invincibleShield.SetActive(false);
        canFire = true;
        canShield = true;
        stunIconShow = true;
        stunned = false;
        stunIconToggle = true;
        canMelee = true;
        meleeing = false;
        shieldPowerUp = false;
        SoulCount = 1;
        invincAnim = invincibleShield.GetComponent<Animator>();
        animator = GetComponent<Animator>();
        deathPos = this.transform;
    }

    void Update()
    {

        // Get the Rewired Player object for this player and keep it for the duration of the character's lifetime
        player = ReInput.players.GetPlayer(playerId);
        // Get/Process input functions
        GetInput();
        ProcessInput();
        Debug.Log(SoulCount);
        //stunned mechanic...if player is stunned and still alive do this
        if (stunned == true && dead == false)
        {
            animator.enabled = false;
            //make ths stun icon appear
            if (stunIconShow)
            {
                GameObject stun = (Instantiate(stunIcon, new Vector3(stunIconLoc.transform.position.x, stunIconLoc.transform.position.y), mainCamera.transform.rotation)) as GameObject;
                Destroy(stun, 2f);
                stunIconShow = false;
            }
            //make sure both the shields collider and its renderer are off, however do setActive it to false.
            shield.gameObject.GetComponent<PolygonCollider2D>().enabled = false;
            shield.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            //player can no longer move while dead.
            moveSpeed = 0f;
            //if the player is stunned, and it currently has its stun icon, make a call to UnStun method. 
            if (stunIconToggle == true)
            {
                Invoke("UnStun", 2f);
                stunIconToggle = false;
            }
            rb.velocity = new Vector2(0f, 0f);
        }
        // If you are dead, turn off movement.
        if (dead == true)
        {
            rb.velocity = new Vector2(0f, 0f);
        }
        // Meleeing ability for the player
        if (meleeing)
        {
            GetComponent<Rigidbody2D>().velocity = -transform.up * moveSpeed * 3.5f;
            TrailPlay();
        }
        else
        {
            TrailStop();
        }
        // Shield power up...makes it very strong
        if (shieldPowerUp)
            shield.GetComponent<FFAShieldScript>().cantBeStunned = true;
        else if (!shieldPowerUp)
            shield.GetComponent<FFAShieldScript>().cantBeStunned = false;

        // Set the speed settings for the speed power up
        if (this.gameObject.CompareTag("Angel"))
        {
            if (GameObject.FindGameObjectWithTag("SpeedPowerUp") != null && GameObject.FindGameObjectWithTag("SpeedPowerUp").GetComponent<shotsPowerUp>().angelSpeedPowerUp)
            {
                if (!stunned && !dead)
                {
                    moveSpeed = 20;
                    slowMoveSpeed = 10;
                    SpeedTrail.Play();
                }
            }
            else if (GameObject.FindGameObjectWithTag("SpeedPowerUp") == null && PlayerPrefs.GetInt("moveSpeedModifier") == 0)
            {
                if (!stunned && !dead)
                {
                    moveSpeed = 10;
                    slowMoveSpeed = 5;
                    SpeedTrail.Stop();
                }
            }
            if (PlayerPrefs.GetInt("moveSpeedModifier") == 1)
            {
                moveSpeed = 20;
                slowMoveSpeed = 10;
                SpeedTrail.Play();
            }
        }
        else if (this.gameObject.CompareTag("Demon"))
        {
            if (GameObject.FindGameObjectWithTag("SpeedPowerUp") != null && GameObject.FindGameObjectWithTag("SpeedPowerUp").GetComponent<shotsPowerUp>().demonSpeedPowerUp)
            {
                if (!stunned && !dead)
                {
                    moveSpeed = 20;
                    SpeedTrail.Play();
                }
            }
            else if (GameObject.FindGameObjectWithTag("SpeedPowerUp") == null && PlayerPrefs.GetInt("moveSpeedModifier") == 0)
            {
                if (!stunned && !dead)
                {
                    moveSpeed = 10;
                    SpeedTrail.Stop();
                }
            }
            if (PlayerPrefs.GetInt("moveSpeedModifier") == 1)
            {
                moveSpeed = 20;
                SpeedTrail.Play();
            }
        }
        //If youre invincible aka just spawned, set the circle around you to active
        if (invincible)
        {
            invincibleShield.SetActive(true);

        }
        else
        {
            invincibleShield.SetActive(false);
        }
    }

    private void GetInput()
    {
        // Get the input from the Rewired Player. All controllers that the Player owns will contribute, so it doesn't matter
        // whether the input is coming from a joystick, the keyboard, mouse, or a custom controller.
        // If you aren't dead, you can input
        if (dead == false && stunned == false)
        {
            moveVector.x = player.GetAxis("Move Horizontal"); // get input by name or action id
            moveVector.y = player.GetAxis("Move Vertical");
            xSpeed = player.GetAxisRaw("Rotate Horizontal");
            ySpeed = player.GetAxisRaw("Rotate Vertical");
            firing = player.GetButtonDown("Fire");
            blocking = player.GetButton("Shield");
        }

        /** if (moveVector.x == 0 && moveVector.y == 0 && !stunned && !meleeing)
         {
             animator.SetBool("Moving", false);
         }
         else if (moveVector.x != 0  && moveVector.y != 0 && !stunned && !meleeing)
         {
             animator.SetBool("Moving", true);
         }
         else if (meleeing && !stunned)
         {
             animator.SetBool("Melee", true);
         }
         else if (stunned && !meleeing)
         {
             animator.SetBool("Stunned", true);
         }
     **/
    }

    private void ProcessInput()
    {
        // Process movement
        // If you don't have a flag and aren't shielding, move at normal speed
        if (!blocking)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(moveVector.x, moveVector.y) * moveSpeed;
        }
        // If you are blocking or have the flag, move at slow speed
        else if (blocking)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(moveVector.x, moveVector.y) * slowMoveSpeed;
        }
        // If you are meleeing, move at melee speed and turn on the shield's ability to kill things.

        // Process rotation
        if ((xSpeed != 0 || ySpeed != 0) && !meleeing)
        {
            gameObject.transform.localRotation = Quaternion.Euler(0.0f, 0.0f, (Mathf.Atan2(xSpeed, ySpeed) * Mathf.Rad2Deg));
        }
        // Process fire
        if (firing && canFire && !blocking)
        {
            canFire = false;
            GameObject bullet = (Instantiate(bulletPrefab, new Vector3(bulletSpawner.transform.position.x, bulletSpawner.transform.position.y, -1.5f), bulletSpawner.transform.rotation)) as GameObject;
            if (this.gameObject.name == "Urial")
                bullet.GetComponent<FFABulletScript>().urialB = true;
            else if (this.gameObject.name == "Barachial")
                bullet.GetComponent<FFABulletScript>().barachialB = true;
            else if (this.gameObject.name == "Lilith")
                bullet.GetComponent<FFABulletScript>().lilithB = true;
            else if (this.gameObject.name == "Azazel")
                bullet.GetComponent<FFABulletScript>().azazelB = true;
            Invoke("CanShootAgain", 0.5f);
            //shot indicator cooldown
            if (this.gameObject.name == "Urial")
            {
               // GameObject.Find("Urial Shot Indicator").GetComponent<ShotCooldownIndicator>().UrialShotCooldown();
            }
            else if (this.gameObject.name == "Barachial")
            {
                //GameObject.Find("Barachial Shot Indicator").GetComponent<ShotCooldownIndicator>().BarachialShotCooldown();
            }
            else if (this.gameObject.name == "Lilith")
            {
                //GameObject.Find("Lilith Shot Indicator").GetComponent<ShotCooldownIndicator>().LilithShotCooldown();
            }
            else if (this.gameObject.name == "Azazel")
            {
                //GameObject.Find("Azazel Shot Indicator").GetComponent<ShotCooldownIndicator>().AzazelShotCooldown();
            }
        }
        // Process melee
        if (blocking && canMelee)
        {
            if (firing)
            {
                GetComponent<AudioSource>().PlayOneShot(Dash);
                canMelee = false;
                meleeing = true;
                if (this.gameObject.name == "Urial")
                {
                   // GameObject.Find("Urial Melee Indicator").GetComponent<MeleeCooldownIndicator>().UrialMeleeCooldown();
                }
                else if (this.gameObject.name == "Barachial")
                {
                  //  GameObject.Find("Barachial Melee Indicator").GetComponent<MeleeCooldownIndicator>().BarachialMeleeCooldown();
                }
                else if (this.gameObject.name == "Lilith")
                {
                  //  GameObject.Find("Lilith Melee Indicator").GetComponent<MeleeCooldownIndicator>().LilithMeleeCooldown();
                }
                else if (this.gameObject.name == "Azazel")
                {
                  //  GameObject.Find("Azazel Melee Indicator").GetComponent<MeleeCooldownIndicator>().AzazelMeleeCooldown();
                }
                StartCoroutine(StopMelee());
            }
        }

        // Process shield
        if (blocking && canShield)
        {
            shield.gameObject.GetComponent<PolygonCollider2D>().enabled = true;
            shield.gameObject.GetComponent<SpriteRenderer>().enabled = true;
        }
        else if (!blocking || !canShield)
        {
            shield.gameObject.GetComponent<PolygonCollider2D>().enabled = false;
            shield.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Soul")
        {
            SoulCount += 1;
            Destroy(other.gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.name == "UrialBase" && this.name == "Urial")
        {
            if (SoulCount > 1)
            {
                UI.GetComponent<FFAUIScript>().urialScore += (SoulCount - 1);
                SoulCount = 1;
                GameObject.Find("Main Camera").GetComponent<CameraScript>().shakeDuration = .75f;
                Invoke("StopShake", 1f);
                GameObject score = (Instantiate(scoreParticles[0], new Vector3(other.transform.position.x, other.transform.position.y), mainCamera.transform.rotation)) as GameObject;
                Destroy(score, 2f);
            }
        }
        if (other.gameObject.name == "BarachialBase" && this.name == "Barachial")
        {
            if (SoulCount > 1)
            {
                UI.GetComponent<FFAUIScript>().barachialScore += (SoulCount - 1);
                SoulCount = 1;
                GameObject.Find("Main Camera").GetComponent<CameraScript>().shakeDuration = .75f;
                Invoke("StopShake", 1f);
                GameObject score = (Instantiate(scoreParticles[1], new Vector3(other.transform.position.x, other.transform.position.y), mainCamera.transform.rotation)) as GameObject;
                Destroy(score, 2f);
            }
        }
        if (other.gameObject.name == "AzazelBase" && this.name == "Azazel")
        {
            if (SoulCount > 1)
            {
                UI.GetComponent<FFAUIScript>().azazelScore += (SoulCount - 1);
                SoulCount = 1;
                GameObject.Find("Main Camera").GetComponent<CameraScript>().shakeDuration = .75f;
                Invoke("StopShake", 1f);
                GameObject score = (Instantiate(scoreParticles[3], new Vector3(other.transform.position.x, other.transform.position.y), mainCamera.transform.rotation)) as GameObject;
                Destroy(score, 2f);
            }
        }
        if (other.gameObject.name == "LilithBase" && this.name == "Lilith")
        {
            if (SoulCount > 1)
            {
                UI.GetComponent<FFAUIScript>().lilithScore += (SoulCount - 1);
                SoulCount = 1;
                GameObject.Find("Main Camera").GetComponent<CameraScript>().shakeDuration = .75f;
                Invoke("StopShake", 1f);
                GameObject score = (Instantiate(scoreParticles[2], new Vector3(other.transform.position.x, other.transform.position.y), mainCamera.transform.rotation)) as GameObject;
                Destroy(score, 2f);
            }
        }
    }

    // Reloading function
    private void CanShootAgain()
    {
        canFire = true;
    }

    // Death Function
    public void Die()
    {
        // if youre not invincible then die and do all this stuff
        if (!invincible)
        {
            if (this.gameObject.name == "Urial")
                GameObject.Find("DeathTracker").GetComponent<DeathTrackerScript>().urialAlive = false;
            else if (this.gameObject.name == "Barachial")
                GameObject.Find("DeathTracker").GetComponent<DeathTrackerScript>().barachialAlive = false;
            else if (this.gameObject.name == "Lilith")
                GameObject.Find("DeathTracker").GetComponent<DeathTrackerScript>().lilithAlive = false;
            else if (this.gameObject.name == "Azazel")
                GameObject.Find("DeathTracker").GetComponent<DeathTrackerScript>().azazelAlive = false;
            rb.velocity = new Vector2(0f, 0f);
            GameObject death = (Instantiate(deathParticleD, new Vector3(transform.position.x, transform.position.y), mainCamera.transform.rotation)) as GameObject;
            Destroy(death, 2f);
            deathPos.position = transform.position;
            while(SoulCount >= 1)
            {
                GameObject newSoul = Instantiate(soulPrefab) as GameObject;
                newSoul.transform.position = new Vector2(Random.Range(deathPos.position.x - .5f, deathPos.position.x + .5f), Random.Range(deathPos.position.y - .5f, deathPos.position.y + .5f));
                SoulCount -= 1;
            }
            gameObject.SetActive(false);
            respawnRandomizer = Random.Range(0, 4);
            if (respawnRandomizer == 0)
                transform.position = SpawnLocation1;
            else if (respawnRandomizer == 1)
                transform.position = SpawnLocation2;
            else if (respawnRandomizer == 2)
                transform.position = SpawnLocation3;
            else if (respawnRandomizer == 3)
                transform.position = SpawnLocation4;
            meleeing = false;
            // animator.SetBool("Melee", false);
            //animator.SetBool("Stunned", false);
            dead = true;
            stunned = false;
            if (this.gameObject.name == "Urial")
                Instantiate(urialRespawnIndicator, this.gameObject.transform.position, Quaternion.identity);
            else if (this.gameObject.name == "Barachial")
                Instantiate(barachialRespawnIndicator, this.gameObject.transform.position, Quaternion.identity);
            else if (this.gameObject.name == "Lilith")
                Instantiate(lilithRespawnIndicator, this.gameObject.transform.position, Quaternion.identity);
            else if (this.gameObject.name == "Azazel")
                Instantiate(azazelRespawnIndicator, this.gameObject.transform.position, Quaternion.identity);
        }
        shield.GetComponent<FFAShieldScript>().shieldLife = 3;
        Invoke("Respawn", respawnTimer);
        GameObject.Find("Main Camera").GetComponent<CameraScript>().shakeDuration = .2f;
        Invoke("StopShake", .2f);
    }

    // Respawn function
    private void Respawn()
    {
        dead = false;
        invincible = true;
        Invoke("InvincibleToggle", 1.5f);
        //invincAnim.SetTrigger("Shrink");
        canFire = true;

        canShield = true;
        canMelee = true;     
        gameObject.SetActive(true);
        SoulCount = 1;
        if (this.gameObject.name == "Lilith")
            GameObject.Find("DeathTracker").GetComponent<DeathTrackerScript>().lilithAlive = true;
        else if (this.gameObject.name == "Azazel")
            GameObject.Find("DeathTracker").GetComponent<DeathTrackerScript>().azazelAlive = true;
        else if (this.gameObject.name == "Urial")
            GameObject.Find("DeathTracker").GetComponent<DeathTrackerScript>().urialAlive = true;
        else if (this.gameObject.name == "Barachial")
            GameObject.Find("DeathTracker").GetComponent<DeathTrackerScript>().barachialAlive = true;
    }

    //function called to unstun the player
    public void UnStun()
    {
        stunned = false;
        animator.enabled = true;
        stunIconShow = true;
        stunIconToggle = true;
        if (this.gameObject.CompareTag("Angel"))
        {
            if (GameObject.FindGameObjectWithTag("SpeedPowerUp") != null && GameObject.FindGameObjectWithTag("SpeedPowerUp").GetComponent<shotsPowerUp>().angelSpeedPowerUp)
                moveSpeed = 20;
            else
                moveSpeed = 10;
        }
        else if (this.gameObject.CompareTag("Demon"))
        {
            if (GameObject.FindGameObjectWithTag("SpeedPowerUp") != null && GameObject.FindGameObjectWithTag("SpeedPowerUp").GetComponent<shotsPowerUp>().demonSpeedPowerUp)
                moveSpeed = 20;
            else
                moveSpeed = 10;
        }
    }

    //co routinue to stop the melee
    IEnumerator StopMelee()
    {
        yield return new WaitForSeconds(meleeDuration);
        meleeing = false;
        // animator.SetBool("Melee", false);
        yield return new WaitForSeconds(2);
        canMelee = true;
    }

    //function called to tell the melee trails to play
    void TrailPlay()
    {
        meleeTrail.Play();
        meleeTrail.enableEmission = true;
    }

    //function called to tell the melee trail to stop
    void TrailStop()
    {
        meleeTrail.enableEmission = false;
    }

    //function called to set invinicible to false after 1.5 seconds from spawning
    void InvincibleToggle()
    {
        invincible = false;
    }

    //function called to stop the camera from shaking anymore

    public void StopShake()
    {
        GameObject.Find("Main Camera").GetComponent<CameraScript>().shakeDuration = 0f;
    }

    void CanMeleeDelay()
    {
        canMelee = true;
    }

    void ResetFires()
    {
        canFire = true;
        canMelee = true;
    }
}
