using UnityEngine;
using System.Collections;
using Rewired;

public class PlayerScript : MonoBehaviour
{
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
    [SerializeField] GameObject deathParticleA, deathParticleD;
    [SerializeField] GameObject invincibleShield;
    public GameObject azazelRespawnIndicator, lilithRespawnIndicator, urialRespawnIndicator, barachialRespawnIndicator;
    //Stun settings
    public bool stunned, stunIconToggle;
    // Shoot settings
    [SerializeField]
    private bool canFire, canThrowFlag;
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
    public GameObject AngelFlag, DemonFlag;
    public GameObject AngelBase, DemonBase;
    public bool hasFlag, canCapture;
    public FlagScript FlagReference;
    [SerializeField] GameObject[] scoreParticles; 
    //camera reference for stunned icon rotation
    public Camera mainCamera;
    // UI Game Object reference
    public GameObject UI;
    public AudioClip Dash;
    public AudioClip Toss;
    // Animations
    private Animator animator;
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
        FlagReference = DemonFlag.GetComponent<FlagScript>();
        invincibleShield.SetActive(false);
        canFire = true;
        canShield = true;
        stunIconShow = true;
        hasFlag = false;
        stunned = false;
        stunIconToggle = true;
        canMelee = true;
        meleeing = false;
        shieldPowerUp = false;
        canThrowFlag = true;
        canCapture = true; 
        invincAnim = invincibleShield.GetComponent<Animator>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Get the Rewired Player object for this player and keep it for the duration of the character's lifetime
        player = ReInput.players.GetPlayer(playerId);
        // Get/Process input functions
        GetInput();
        ProcessInput(); 

        // If you have the flag, you can't fire or shield, and make the flag follow player.
        if (hasFlag == true)
        {
            canFire = false;
            canShield = false;
            canMelee = false;
            if (this.gameObject.CompareTag("Angel"))
            {
                DemonFlag.transform.position = this.gameObject.transform.position;
                DemonFlag.transform.rotation = this.gameObject.transform.rotation;
            }
            else if (this.gameObject.CompareTag("Demon"))
            {
                AngelFlag.transform.position = this.gameObject.transform.position;
                AngelFlag.transform.rotation = this.gameObject.transform.rotation;
            }
        }
        //stunned mechanic...if player is stunned and still alive do this
        if (stunned == true && dead == false)
        {
            animator.SetBool("Idle", false);
            animator.SetBool("Stunned", true);
            //animator.SetBool("Meleeing", false); 
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
            //animator.SetBool("Meleeing", true);
            //animator.SetBool("Idle", false);
        }
        else
        {
            TrailStop();
            //animator.SetBool("Meleeing", false);
            //animator.SetBool("Idle", true);
        }
        // Shield power up...makes it very strong
        if (shieldPowerUp)
            shield.GetComponent<ShieldScript>().cantBeStunned = true;
        else if (!shieldPowerUp)
            shield.GetComponent<ShieldScript>().cantBeStunned = false;

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
            if (GameObject.FindGameObjectWithTag("SpeedPowerUp") == null && PlayerPrefs.GetInt("moveSpeedModifier") == 1)
            {
                if (!stunned && !dead)
                {
                    moveSpeed = 15;
                    slowMoveSpeed = 8;
                    //SpeedTrail.Play();
                }
            }

          if (GameObject.FindGameObjectWithTag("ShotPowerUp") != null && GameObject.FindGameObjectWithTag("ShotPowerUp").GetComponent<shotsPowerUp>().angelShotPowerUp)
            {
                shotPowerTrail.Play();
            }

            if (GameObject.FindGameObjectWithTag("ShotPowerUp") == null)
            {
                shotPowerTrail.Stop();
            } 
        }
        else if (this.gameObject.CompareTag("Demon"))
        {
            if (GameObject.FindGameObjectWithTag("SpeedPowerUp") != null && GameObject.FindGameObjectWithTag("SpeedPowerUp").GetComponent<shotsPowerUp>().demonSpeedPowerUp)
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
            if (GameObject.FindGameObjectWithTag("SpeedPowerUp") == null && PlayerPrefs.GetInt("moveSpeedModifier") == 1)
            {
                if (!stunned && !dead) 
                {
                    moveSpeed = 15;
                    slowMoveSpeed = 8; 
                    //SpeedTrail.Play();
                }
            }

            if (GameObject.FindGameObjectWithTag("ShotPowerUp") != null && GameObject.FindGameObjectWithTag("ShotPowerUp").GetComponent<shotsPowerUp>().demonShotPowerUp)
            {
                shotPowerTrail.Play();
            }

            if (GameObject.FindGameObjectWithTag("ShotPowerUp") == null)
            {
                shotPowerTrail.Stop();
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
    }

    private void ProcessInput()
    {
        // Process movement
        // If you don't have a flag and aren't shielding, move at normal speed
        if (!blocking && !hasFlag)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(moveVector.x, moveVector.y) * moveSpeed;
        }
        // If you are blocking or have the flag, move at slow speed
        else if (blocking || hasFlag)
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
        if (firing && canFire && !blocking && !hasFlag)
        {
            canFire = false;
            GameObject bullet = (Instantiate(bulletPrefab, new Vector3(bulletSpawner.transform.position.x, bulletSpawner.transform.position.y, -1.5f), bulletSpawner.transform.rotation)) as GameObject;
            if (this.gameObject.CompareTag("Angel"))
            {
                if ((GameObject.FindGameObjectWithTag("ShotPowerUp") != null && GameObject.FindGameObjectWithTag("ShotPowerUp").GetComponent<shotsPowerUp>().angelShotPowerUp) || PlayerPrefs.GetInt("shotSpeedModifier") == 1)
                    Invoke("CanShootAgain", 0.5f);
                else
                    Invoke("CanShootAgain", reloadSpeed);
            }
            if (this.gameObject.CompareTag("Demon"))
            {
                if ((GameObject.FindGameObjectWithTag("ShotPowerUp") != null && GameObject.FindGameObjectWithTag("ShotPowerUp").GetComponent<shotsPowerUp>().demonShotPowerUp) || PlayerPrefs.GetInt("shotSpeedModifier") == 1)
                    Invoke("CanShootAgain", 0.5f);
                else
                    Invoke("CanShootAgain", reloadSpeed);
            }
            //shot indicator cooldown
            if (this.gameObject.name == "Urial")
            {
                GameObject.Find("Urial Shot Indicator").GetComponent<ShotCooldownIndicator>().UrialShotCooldown();
            }
            else if (this.gameObject.name == "Barachial")
            {
                GameObject.Find("Barachial Shot Indicator").GetComponent<ShotCooldownIndicator>().BarachialShotCooldown();
            }
            else if (this.gameObject.name == "Lilith")
            {
                GameObject.Find("Lilith Shot Indicator").GetComponent<ShotCooldownIndicator>().LilithShotCooldown();
            }
            else if (this.gameObject.name == "Azazel")
            {
                GameObject.Find("Azazel Shot Indicator").GetComponent<ShotCooldownIndicator>().AzazelShotCooldown();
            }
        }
        // Process flag throw
        if (firing && hasFlag && canThrowFlag)
        {
            //if this object is an Angel
            if (this.gameObject.CompareTag("Angel"))
            {
                canThrowFlag = false;
                GetComponent<AudioSource>().PlayOneShot(Toss);
                //remove the flag from it
                hasFlag = false;
                Debug.Log(this.gameObject.name + "has thrown the demon flag!");
                //let the demonflag know that it is now being thrown and is no longer on the player
                DemonFlag.GetComponent<FlagScript>().beingThrown = true;
                //after a one second delay, call a function that tells the demon flag to stop moving
                Invoke("EndFlagThrow", DemonFlag.GetComponent<FlagScript>().throwSpeed);
                //re allow the player to fire regular projectiles
                Invoke("CanShootAgain", 0.5f);
                Invoke("CanThrowAgain", 0.5f);
                //re allow the player to shield
                canShield = true;
                Invoke("CanMeleeDelay", 0.5f);
            }

            if (this.gameObject.CompareTag("Demon"))
            {
                canThrowFlag = false;
                GetComponent<AudioSource>().PlayOneShot(Toss);
                //
                hasFlag = false;
                Debug.Log(this.gameObject.name + "has thrown the angel flag!");
                //
                AngelFlag.GetComponent<FlagScript>().beingThrown = true;
                //
                Invoke("EndFlagThrow", AngelFlag.GetComponent<FlagScript>().throwSpeed);
                //
                Invoke("CanShootAgain", 0.5f);
                Invoke("CanThrowAgain", 0.5f);
                //
                canShield = true;
                Invoke("CanMeleeDelay", 0.5f);
            }
        }

        // Process melee
        if (blocking && canMelee && !hasFlag)
        {
            if (firing)
            {
                GetComponent<AudioSource>().PlayOneShot(Dash);
                canMelee = false;
                meleeing = true;
                if (this.gameObject.name == "Urial")
                {
                    GameObject.Find("Urial Melee Indicator").GetComponent<MeleeCooldownIndicator>().UrialMeleeCooldown();
                }
                else if (this.gameObject.name == "Barachial")
                {
                    GameObject.Find("Barachial Melee Indicator").GetComponent<MeleeCooldownIndicator>().BarachialMeleeCooldown();
                }
                else if (this.gameObject.name == "Lilith")
                {
                    GameObject.Find("Lilith Melee Indicator").GetComponent<MeleeCooldownIndicator>().LilithMeleeCooldown();
                }
                else if (this.gameObject.name == "Azazel")
                {
                    GameObject.Find("Azazel Melee Indicator").GetComponent<MeleeCooldownIndicator>().AzazelMeleeCooldown();
                }
                StartCoroutine(StopMelee());
            }
        }

        // Process shield
        if (blocking && canShield && !hasFlag)
        {
            shield.gameObject.GetComponent<PolygonCollider2D>().enabled = true;
            shield.gameObject.GetComponent<SpriteRenderer>().enabled = true;
        }
        else if (!blocking || !canShield || hasFlag)
        {
            shield.gameObject.GetComponent<PolygonCollider2D>().enabled = false;
            shield.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // How you interact with flags will differ depending on your player type.
        if (this.gameObject.CompareTag("Angel"))
        {
            // if an angel hits a demon flag, pick it up if its not being thrown, youre not meleeing, and its not being returned
            if (other.gameObject.CompareTag("Demon Flag"))
            {
                if (!other.gameObject.GetComponent<FlagScript>().beingThrown && !meleeing && !other.gameObject.GetComponent<FlagScript>().beingReturned)
                {
                    if (this.gameObject.name == "Urial" && GameObject.Find("DeathTracker").GetComponent<DeathTrackerScript>().barachialAlive)
                    {
                        if (GameObject.Find("Barachial").GetComponent<PlayerScript>().hasFlag == false)
                        {
                            DemonFlag.GetComponent<FlagScript>().FlagRunesOff();
                            hasFlag = true;
                            CancelInvoke("EndFlagThrow"); 
                        }
                    }
                    else if (this.gameObject.name == "Urial" && !GameObject.Find("DeathTracker").GetComponent<DeathTrackerScript>().barachialAlive)
                    {
                        DemonFlag.GetComponent<FlagScript>().FlagRunesOff();
                        hasFlag = true;
                        CancelInvoke("EndFlagThrow");
                    }
                    else if (this.gameObject.name == "Barachial" && GameObject.Find("DeathTracker").GetComponent<DeathTrackerScript>().urialAlive)
                    {
                        if (GameObject.Find("Urial").GetComponent<PlayerScript>().hasFlag == false)
                        {
                            DemonFlag.GetComponent<FlagScript>().FlagRunesOff();
                            hasFlag = true;
                            CancelInvoke("EndFlagThrow");
                        }
                    }
                    else if (this.gameObject.name == "Barachial" && !GameObject.Find("DeathTracker").GetComponent<DeathTrackerScript>().urialAlive)
                    {
                        DemonFlag.GetComponent<FlagScript>().FlagRunesOff();
                        hasFlag = true;
                        CancelInvoke("EndFlagThrow");
                    }
                }
            }
        }
        else if (this.gameObject.CompareTag("Demon"))
        {
            //else if youre a demon and hit the angel flag while its not moving, youre not meleeing, and its not being returned
            if (other.gameObject.CompareTag("Angel Flag"))
            {
                if (!other.gameObject.GetComponent<FlagScript>().beingThrown && !meleeing && !other.gameObject.GetComponent<FlagScript>().beingReturned)
                {
                    if (this.gameObject.name == "Azazel" && GameObject.Find("DeathTracker").GetComponent<DeathTrackerScript>().lilithAlive)
                    {
                        if (!GameObject.Find("Lilith").GetComponent<PlayerScript>().hasFlag)
                        {
                            AngelFlag.GetComponent<FlagScript>().FlagRunesOff();
                            hasFlag = true;
                            CancelInvoke("EndFlagThrow");
                        }
                    }
                    else if (this.gameObject.name == "Azazel" && !GameObject.Find("DeathTracker").GetComponent<DeathTrackerScript>().lilithAlive)
                    {
                        AngelFlag.GetComponent<FlagScript>().FlagRunesOff();
                        hasFlag = true;
                        CancelInvoke("EndFlagThrow");
                    }
                    else if (this.gameObject.name == "Lilith" && GameObject.Find("DeathTracker").GetComponent<DeathTrackerScript>().azazelAlive)
                    {
                        if (!GameObject.Find("Azazel").GetComponent<PlayerScript>().hasFlag)
                        {
                            AngelFlag.GetComponent<FlagScript>().FlagRunesOff();
                            hasFlag = true;
                            CancelInvoke("EndFlagThrow");
                        }
                    }
                    else if (this.gameObject.name == "Lilith" && !GameObject.Find("DeathTracker").GetComponent<DeathTrackerScript>().azazelAlive)
                    {
                        AngelFlag.GetComponent<FlagScript>().FlagRunesOff();
                        hasFlag = true;
                        CancelInvoke("EndFlagThrow");
                    }
                }
            }
        }
    }

    void OnTriggerStay2D (Collider2D other) {

        // Interacting with your base
        if (other.gameObject.CompareTag("Demon Base") && gameObject.tag == "Demon")
        {
            if (hasFlag && canCapture)
            {
                canFire = true;
                canMelee = true;
                Invoke("ResetFires", 0.5f);
                AngelFlag.transform.position = AngelBase.transform.position;
                AngelFlag.GetComponent<FlagScript>().inBase = true;
                hasFlag = false;
                canFire = true;
                canShield = true;
                canMelee = true;
                UI.GetComponent<CTFUIScript>().demonScore++;
                canCapture = false;
                Invoke("CanCapture", 1f);
                GameObject.Find("Main Camera").GetComponent<CameraScript>().shakeDuration = .75f;
                Invoke("StopShake", 1f);
                GameObject score = (Instantiate(scoreParticles[1], new Vector3(DemonBase.transform.position.x, DemonBase.transform.position.y), mainCamera.transform.rotation)) as GameObject;
                Destroy(score, 2f);
            }
        }

        // If you bring the enemy team's flag to your base, do this vvv
        if (other.gameObject.CompareTag("Angel Base") && gameObject.tag == "Angel")
        {
            if (hasFlag && canCapture)
            {
                canFire = true;
                canMelee = true;
                Invoke("ResetFires", 0.5f);
                Invoke("CanCapture", 1f);
                DemonFlag.transform.position = DemonBase.transform.position;
                DemonFlag.GetComponent<FlagScript>().inBase = true;
                hasFlag = false;
                canFire = true;
                canShield = true;
                canMelee = true;
                UI.GetComponent<CTFUIScript>().angelScore++;
                canCapture = false; 
                GameObject.Find("Main Camera").GetComponent<CameraScript>().shakeDuration = .75f;
                Invoke("StopShake", 1f);
                GameObject score = (Instantiate(scoreParticles[0], new Vector3(AngelBase.transform.position.x, AngelBase.transform.position.y), mainCamera.transform.rotation)) as GameObject;
                Destroy(score, 2f);
            }
        }
    }


    void OnCollisionEnter2D(Collision2D other) {

           if (this.gameObject.CompareTag("Angel")) {
            
            //if you hit a moving demon flag pick it up and stop meleeing
            if (other.gameObject.CompareTag("Demon Flag") && !meleeing)
            {
                //Debug.Log(this.gameObject.name + " has the demon flag!");
                hasFlag = true;
                CancelInvoke("EndFlagThrow");
            }
            else if (other.gameObject.CompareTag("Angel Flag"))
            {
                other.gameObject.GetComponent<FlagScript>().beingThrown = false;
            }
        }
        else if (this.gameObject.CompareTag("Demon"))
        {
                if (other.gameObject.CompareTag("Angel Flag") && !meleeing)
                {
                    //Debug.Log(this.gameObject.name + " has the angel flag!");
                    hasFlag = true;
                CancelInvoke("EndFlagThrow");
            }
                else if (other.gameObject.CompareTag("Demon Flag"))
                {
                    other.gameObject.GetComponent<FlagScript>().beingThrown = false;
                }
            }
        }
       
    // Trigger exit function
    private void OnTriggerExit2D(Collider2D other)
    {
        // How you interact with flags will differe depending on your player type.
        if (this.gameObject.CompareTag("Angel"))
        {
            if (other.gameObject.CompareTag("Angel Flag"))
            {
                if (AngelFlag.GetComponent<FlagScript>().inBase == true)
                {
                    // Flag is in base, nothing needs to happen.
                }
                else if (AngelFlag.GetComponent<FlagScript>().inBase == false)
                {
                    // Flag is not in base, begin returning.
                }
            }
            else if (other.gameObject.CompareTag("Demon Flag"))
            {
                // Don't have the enemy team's flag anymore.
                hasFlag = false;
            }
        }
        else if (this.gameObject.CompareTag("Demon"))
        {
            if (other.gameObject.CompareTag("Angel Flag"))
            {
                // Don't have the enemy team's flag anymore.
                hasFlag = false;      
            }
            else if (other.gameObject.CompareTag("Demon Flag"))
            {
                if (DemonFlag.GetComponent<FlagScript>().inBase == true)
                {
                    // Flag is in base, nothing needs to happen.
                }
                else if (DemonFlag.GetComponent<FlagScript>().inBase == false)
                {
                    // Flag is not in base, begin returning.
                }
            }
        }
    }

    // Reloading function
    private void CanShootAgain()
    {
        canFire = true;
    }
    private void CanThrowAgain()
    {
        canThrowFlag = true;
    }

    // Ending the flag throw
    public void EndFlagThrow()
    {
        if (this.gameObject.CompareTag("Angel"))
        {
            DemonFlag.GetComponent<FlagScript>().beingThrown = false;
            DemonFlag.GetComponent<FlagScript>().throwSpeed /= 2;
            DemonFlag.GetComponent<FlagScript>().onPlayer = false;
        }
        else if (this.gameObject.CompareTag("Demon"))
        {
            AngelFlag.GetComponent<FlagScript>().beingThrown = false;
            AngelFlag.GetComponent<FlagScript>().throwSpeed /= 2;
            DemonFlag.GetComponent<FlagScript>().onPlayer = false;
        }
    }

    // Death Function
    public void Die()
    {
        // if youre not invincible then die and do all this stuff
        if (!invincible)
        {
            hasFlag = false;
            if (this.gameObject.name == "Urial")
                GameObject.Find("DeathTracker").GetComponent<DeathTrackerScript>().urialAlive = false;
            else if (this.gameObject.name == "Barachial")
                GameObject.Find("DeathTracker").GetComponent<DeathTrackerScript>().barachialAlive = false;
            else if (this.gameObject.name == "Lilith")
                GameObject.Find("DeathTracker").GetComponent<DeathTrackerScript>().lilithAlive = false;
            else if (this.gameObject.name == "Azazel")
                GameObject.Find("DeathTracker").GetComponent<DeathTrackerScript>().azazelAlive = false;
            rb.velocity = new Vector2(0f, 0f);
            gameObject.SetActive(false);
            meleeing = false;
            dead = true;
            stunned = false;
            if (gameObject.CompareTag("Demon"))
            {
                GameObject death = (Instantiate(deathParticleD, new Vector3(transform.position.x, transform.position.y), mainCamera.transform.rotation)) as GameObject;
                Destroy(death, 2f);
                DemonFlag.GetComponent<FlagScript>().beingReturned = false;
            }
            else if (gameObject.CompareTag("Angel"))
            {
                GameObject death = (Instantiate(deathParticleA, new Vector3(transform.position.x, transform.position.y), mainCamera.transform.rotation)) as GameObject;
                Destroy(death, 2f);
                AngelFlag.GetComponent<FlagScript>().beingReturned = false;
            }
            // new respawning
            if (this.gameObject.CompareTag("Demon"))
            {
                if (!DemonFlag.GetComponent<FlagScript>().inBase)
                {
                    if (DemonFlag.transform.position.y > 0)
                    {
                        respawnRandomizer = Random.Range(0, 2);
                        if (this.gameObject.name == "Azazel")
                        {
                            if (respawnRandomizer == 0)
                                transform.position = SpawnLocation1;
                            else if (respawnRandomizer == 1)
                                transform.position = SpawnLocation3;
                        }
                        else if (this.gameObject.name == "Lilith")
                        {
                            if (respawnRandomizer == 0)
                                transform.position = SpawnLocation2;
                            else if (respawnRandomizer == 1)
                                transform.position = SpawnLocation4;
                        }
                    }
                    else if (DemonFlag.transform.position.y <= 0)
                    {
                        respawnRandomizer = Random.Range(0, 2);
                        if (this.gameObject.name == "Azazel")
                        {
                            if (respawnRandomizer == 0)
                                transform.position = SpawnLocation2;
                            else if (respawnRandomizer == 1)
                                transform.position = SpawnLocation4;
                        }
                        else if (this.gameObject.name == "Lilith")
                        {
                            if (respawnRandomizer == 0)
                                transform.position = SpawnLocation1;
                            else if (respawnRandomizer == 1)
                                transform.position = SpawnLocation3;
                        }
                    }
                }
                else if (DemonFlag.GetComponent<FlagScript>().inBase)
                {
                    respawnRandomizer = Random.Range(0, 4);
                    if (respawnRandomizer == 0)
                        transform.position = SpawnLocation1;
                    else if (respawnRandomizer == 1)
                        transform.position = SpawnLocation2;
                    else if (respawnRandomizer == 2)
                        transform.position = SpawnLocation3;
                    else if (respawnRandomizer == 3)
                        transform.position = SpawnLocation4;
                }
                if (this.gameObject.name == "Azazel")
                    Instantiate(azazelRespawnIndicator, this.gameObject.transform.position, Quaternion.identity);
                else if (this.gameObject.name == "Lilith")
                    Instantiate(lilithRespawnIndicator, this.gameObject.transform.position, Quaternion.identity);
            }
            else if (this.gameObject.CompareTag("Angel"))
            {
                if (!AngelFlag.GetComponent<FlagScript>().inBase)
                {
                    if (AngelFlag.transform.position.y > 0)
                    {
                        respawnRandomizer = Random.Range(0, 2);
                        if (this.gameObject.name == "Urial")
                        {
                            if (respawnRandomizer == 0)
                                transform.position = SpawnLocation2;
                            else if (respawnRandomizer == 1)
                                transform.position = SpawnLocation4;
                        }
                        else if (this.gameObject.name == "Barachial")
                        {
                            if (respawnRandomizer == 0)
                                transform.position = SpawnLocation1;
                            else if (respawnRandomizer == 1)
                                transform.position = SpawnLocation3;
                        }
                    }
                    else if (AngelFlag.transform.position.y <= 0)
                    {
                        respawnRandomizer = Random.Range(0, 2);
                        if (this.gameObject.name == "Urial")
                        {
                            if (respawnRandomizer == 0)
                                transform.position = SpawnLocation1;
                            else if (respawnRandomizer == 1)
                                transform.position = SpawnLocation3;
                        }
                        else if (this.gameObject.name == "Barachial")
                        {
                            if (respawnRandomizer == 0)
                                transform.position = SpawnLocation2;
                            else if (respawnRandomizer == 1)
                                transform.position = SpawnLocation4;
                        }
                    }
                }
                else if (AngelFlag.GetComponent<FlagScript>().inBase)
                {
                    respawnRandomizer = Random.Range(0, 4);
                    if (respawnRandomizer == 0)
                        transform.position = SpawnLocation1;
                    else if (respawnRandomizer == 1)
                        transform.position = SpawnLocation2;
                    else if (respawnRandomizer == 2)
                        transform.position = SpawnLocation3;
                    else if (respawnRandomizer == 3)
                        transform.position = SpawnLocation4;
                }
                if (this.gameObject.name == "Urial")
                    Instantiate(urialRespawnIndicator, this.gameObject.transform.position, Quaternion.identity);
                else if (this.gameObject.name == "Barachial")
                    Instantiate(barachialRespawnIndicator, this.gameObject.transform.position, Quaternion.identity);
            }
            shield.GetComponent<ShieldScript>().shieldLife = 3;
            Invoke("Respawn", respawnTimer);
            GameObject.Find("Main Camera").GetComponent<CameraScript>().shakeDuration = .2f;
            Invoke("StopShake", .2f);
        }
    }
    // Respawn function
    private void Respawn()
    {
        animator.SetBool("Idle", true);
        animator.SetBool("Stunned", false);
        dead = false;
        invincible = true;
        Invoke("InvincibleToggle", 1.5f);
        //invincAnim.SetTrigger("Shrink");
        canFire = true;
        canShield = true;
        canMelee = true;
        canCapture = true;
        hasFlag = false;
        gameObject.SetActive(true);
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
        animator.SetBool("Idle", true);
        animator.SetBool("Stunned", false);       
        stunned = false;
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

    void CanCapture()
    {
        canCapture = true; 
    }

    void ResetFires()
    {
        canFire = true;
        canMelee = true;
    }
}