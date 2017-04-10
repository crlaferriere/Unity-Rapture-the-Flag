using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class CameraScript : MonoBehaviour{
    /* All fuctionality that our camera has, including vibration and round intros */
    // References to the characters, their positions, pausing, and positions
    GameObject Urial, Barachial, Lilith, Azazel;
    Transform UrialT, BarachialT, LilithT, AzazelT;
    Vector3 CameraSmall;
    Vector3 Oldpos;
    Vector3 Targetpos;
    public bool isPaused;
    bool starting;
    public GameObject BG;
    public Transform camTransform;
    // How long the object should shake for.
    public float shakeDuration = 0f;
    // Amplitude of the shake. A larger value shakes the camera harder.
    public float shakeAmount = 0.7f;
    public float decreaseFactor = 0f;
    private bool canPause;
    public bool isShaking;
    Vector3 originalPos;
    public GameObject MusicMan;
    public AudioClip Pause;
    public AudioClip Unpause;
    public AudioClip Three, Two, One, Go;
    public bool canPlay3, canPlay2, canPlay1, canPlayGo;
    // Int for current scene
    public int curScene;
    void Start()
    {
        canPlay1 = true;
        canPlay2 = true;
        canPlay3 = true;
        canPlayGo = true;
        Invoke("Play3", 1);
        MusicMan = GameObject.Find("MusicManager");
        isShaking = false;
        canPause = false;
        isPaused = false;
        starting = false;
        //setting variables to access players and shortcut transforms 
        Urial = GameObject.Find("Urial");
        UrialT = Urial.GetComponent<Transform>();
        Barachial = GameObject.Find("Barachial");
        BarachialT = Barachial.GetComponent<Transform>();
        Lilith = GameObject.Find("Lilith");
        LilithT = Lilith.GetComponent<Transform>();
        Azazel = GameObject.Find("Azazel");
        AzazelT = Azazel.GetComponent<Transform>();

        if (camTransform == null)
            camTransform = GetComponent(typeof(Transform)) as Transform;

        // Create a temporary reference to the current scene.
        curScene = SceneManager.GetActiveScene().buildIndex;
        
        //sets them to dead to prevent movement
        if (curScene == 8)
        {
        Urial.GetComponent<PlayerScript>().dead = true;
        Barachial.GetComponent<PlayerScript>().dead = true;
        Azazel.GetComponent<PlayerScript>().dead = true;
        Lilith.GetComponent<PlayerScript>().dead = true;
        }
        else if (curScene == 9)
        {
            Urial.GetComponent<FFAPlayerScript>().dead = true;
            Barachial.GetComponent<FFAPlayerScript>().dead = true;
            Azazel.GetComponent<FFAPlayerScript>().dead = true;
            Lilith.GetComponent<FFAPlayerScript>().dead = true;
        }

        //starts zoomed in on Azazel
        Oldpos = new Vector3(0, 0, -10);
        Targetpos = new Vector3 (AzazelT.position.x, AzazelT.position.y, -10);

        //start switching
        Invoke("ChangetoLilith", 1);

    }

    void Update()
    {
        //pause logic
        if (Input.GetKeyDown("joystick button 7") && isPaused == false && canPause == true)
        {
            GetComponent<AudioSource>().PlayOneShot(Pause);
            MusicMan.GetComponent<AudioSource>().Pause();
            isPaused = true;
            if (curScene == 8)
            {
                Urial.GetComponent<PlayerScript>().dead = true;
                Barachial.GetComponent<PlayerScript>().dead = true;
                Azazel.GetComponent<PlayerScript>().dead = true;
                Lilith.GetComponent<PlayerScript>().dead = true;
            }
            else if (curScene == 9)
            {
                Urial.GetComponent<FFAPlayerScript>().dead = true;
                Barachial.GetComponent<FFAPlayerScript>().dead = true;
                Azazel.GetComponent<FFAPlayerScript>().dead = true;
                Lilith.GetComponent<FFAPlayerScript>().dead = true;
            }
            shakeDuration = 0;
        }
        else if (Input.GetKeyDown("joystick button 7") && isPaused == true && canPause == true)
        {
            GetComponent<AudioSource>().PlayOneShot(Unpause);
            MusicMan.GetComponent<AudioSource>().Play();
            isPaused = false;
            if (curScene == 8)
            {
                Urial.GetComponent<PlayerScript>().dead = false;
                Barachial.GetComponent<PlayerScript>().dead = false;
                Azazel.GetComponent<PlayerScript>().dead = false;
                Lilith.GetComponent<PlayerScript>().dead = false;
            }
            if (curScene == 9)
            {
                Urial.GetComponent<FFAPlayerScript>().dead = false;
                Barachial.GetComponent<FFAPlayerScript>().dead = false;
                Azazel.GetComponent<FFAPlayerScript>().dead = false;
                Lilith.GetComponent<FFAPlayerScript>().dead = false;
            }

            if (isShaking == true)
            {
                shakeDuration = .5f;
                Invoke("StopShake", .5f);
            }
        }
        //CameraMovement
        if (Targetpos.x == AzazelT.position.x)
            GetComponent<Camera>().orthographicSize = Mathf.Lerp(GetComponent<Camera>().orthographicSize, 2.5f, 5 * Time.deltaTime);
        transform.position = Vector3.MoveTowards(transform.position, Targetpos, 1f);
        if (Targetpos.x == LilithT.position.x)
            // GetComponent<Camera>().orthographicSize = Mathf.Lerp(10.5f, 2.5f, 1);
         

        transform.position = Vector3.MoveTowards(transform.position, Targetpos, 1f);
        if (Targetpos.x == UrialT.position.x)
        {
           
                //   GetComponent<Camera>().orthographicSize = Mathf.Lerp(10.5f, 2.5f, 1);
                transform.position = Vector3.MoveTowards(transform.position, Targetpos, 1f);
        }
        if (Targetpos.x == BarachialT.position.x)
        {
            
                //  GetComponent<Camera>().orthographicSize = Mathf.Lerp(10.5f, 2.5f, 1);
                transform.position = Vector3.MoveTowards(transform.position, Targetpos, 1f);
           
        }
        if (Targetpos.x == 0)
        {
            
            if (curScene == 8)
                GetComponent<Camera>().orthographicSize = Mathf.Lerp(GetComponent<Camera>().orthographicSize, 12.5f, 5 * Time.deltaTime);
            else
                GetComponent<Camera>().orthographicSize = Mathf.Lerp(GetComponent<Camera>().orthographicSize, 14, 5 * Time.deltaTime);
            transform.position = Vector3.MoveTowards(transform.position, Targetpos, 1f);
        }
        //resets camera size
    
           
        //pausing logic pt2
        if (isPaused)
        {
            BG.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, .5f);
            Time.timeScale = 0;
        }
        else
        {
            if (curScene == 8)
                BG.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
            else if (curScene == 9)
                BG.GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.5f, 0.5f, 1);
            Time.timeScale = 1;
        }
        // Camera Shaking
        if (shakeDuration > 0)
        {
            isShaking = true;
            camTransform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;
            shakeDuration -= Time.deltaTime * decreaseFactor;

        }
        else
            shakeDuration = 0f;
    }
    //functions that switch the camera to new characters
    void ChangetoLilith()
    {
        Oldpos = new Vector3(AzazelT.position.x, AzazelT.position.y, -10);
        Targetpos = new Vector3(LilithT.position.x, LilithT.position.y, -10);
        Invoke("ChangetoUrial", 1);
    }
    void ChangetoUrial()
    {
        Oldpos = new Vector3(LilithT.position.x, LilithT.position.y, -10);
        Targetpos = new Vector3(UrialT.position.x, UrialT.position.y, -10);
        Invoke("ChangetoBarachial", 1);
    }
    void ChangetoBarachial()
    {
        Oldpos = new Vector3(UrialT.position.x, UrialT.position.y, -10);
        Targetpos = new Vector3(BarachialT.position.x, BarachialT.position.y, -10);
        Invoke("Reset", 1);
    }
    //resets to start the level
    void Reset()
    {
        Oldpos = new Vector3(BarachialT.position.x, BarachialT.position.y, -10);
        Targetpos = new Vector3(0,0, -10);
        Invoke("StartVar", .3f);   
       // GetComponent<Camera>().orthographicSize = 12.5f;
       // transform.position = new Vector3(0, 0, -10);    
    }
    //slight delay to let the camera become big
    void StartVar()
    {
      
        starting = true;
        if (curScene == 8)
        {
            Urial.GetComponent<PlayerScript>().dead = false;
            Barachial.GetComponent<PlayerScript>().dead = false;
            Azazel.GetComponent<PlayerScript>().dead = false;
            Lilith.GetComponent<PlayerScript>().dead = false;
        }
        else if (curScene == 9)
        {
            Urial.GetComponent<FFAPlayerScript>().dead = false;
            Barachial.GetComponent<FFAPlayerScript>().dead = false;
            Azazel.GetComponent<FFAPlayerScript>().dead = false;
            Lilith.GetComponent<FFAPlayerScript>().dead = false;
        }
        canPause = true;
        
    }
    // Full reset of camera position
    void OnEnable()
    {
        originalPos = camTransform.localPosition;
    }

    void StopShake()
    {
        shakeDuration = 0;
        isShaking = false;
    }
    void Play3()
    {
        if (canPlay3)
        {
            GetComponent<AudioSource>().PlayOneShot(Three);
            canPlay3 = false;
            Invoke("Play2", 1);
        }
    }
    void Play2()
    {
        if (canPlay2)
        {
            GetComponent<AudioSource>().PlayOneShot(Two);
            canPlay2 = false;
            Invoke("Play1", 1);
        }
    }
    void Play1()
    {
        if (canPlay1)
        {
            GetComponent<AudioSource>().PlayOneShot(One);
            canPlay1 = false;
            Invoke("PlayGo", 1);
        }
    }
    void PlayGo()
    {
        if (canPlayGo)
        {
            GetComponent<AudioSource>().PlayOneShot(Go);
            canPlayGo = false;
        
        }
    }

}
