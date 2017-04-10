using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class MasterSelector : MonoBehaviour
{
    /* Master Selector Manager for keeping track of every single selection */
    // Bools for keeping track of who selected what
    public bool P1Urial, P2Urial, P3Urial, P4Urial;
    public bool P1Barachial, P2Barachial, P3Barachial, P4Barachial;
    public bool P1Lilith, P2Lilith, P3Lilith, P4Lilith;
    public bool P1Azazel, P2Azazel, P3Azazel, P4Azazel;
    // Master bools for when a character has been selected
    public bool Urial, Barachial, Lilith, Azazel;
    // Game objects for visual indicators of who selected what
    public GameObject Urial1, Urial2, Urial3, Urial4;
    public GameObject Barachial1, Barachial2, Barachial3, Barachial4;
    public GameObject Lilith1, Lilith2, Lilith3, Lilith4;
    public GameObject Azazel1, Azazel2, Azazel3, Azazel4;
    // The actual in game characters for controller assigning
    // Game Objects for readying
    public GameObject ready;
    public int readyTransform;
    // Int for loading the right scene
    public int mapSelectScene;
    // Int for current scene
    public int curScene;
    public Image black;
    public Animator anim;
    private bool onCharSelect;
    private bool boolsSet;
    //Master Selector asset
    public static MasterSelector instance = null;
    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
        onCharSelect = false;
        boolsSet = false;
    }
    // Set all bools to false by default
    void Start()
    {
        P1Urial = false; P2Urial = false; P3Urial = false; P4Urial = false;
        P1Barachial = false; P2Barachial = false; P3Barachial = false; P4Barachial = false;
        P1Lilith = false; P2Lilith = false; P3Lilith = false; P4Lilith = false;
        P1Azazel = false; P2Azazel = false; P3Azazel = false; P4Azazel = false;
        Urial = false; Barachial = false; Lilith = false; Azazel = false;
        readyTransform = -900;
        ready.GetComponent<RectTransform>().localPosition = new Vector3(readyTransform, 0, 0);
    }
    // Auto manages all player colors, images, etc.
    void Update()
    {
        // Create a temporary reference to the current scene.
        curScene = SceneManager.GetActiveScene().buildIndex;
        if (curScene == 4 || curScene == 5)
        {
            if (onCharSelect)
            {
                onCharSelect = false;
                Urial1 = GameObject.Find("Urial1"); Urial2 = GameObject.Find("Urial2"); Urial3 = GameObject.Find("Urial3"); Urial4 = GameObject.Find("Urial4");
                Barachial1 = GameObject.Find("Barachial1"); Barachial2 = GameObject.Find("Barachial2"); Barachial3 = GameObject.Find("Barachial3"); Barachial4 = GameObject.Find("Barachial4");
                Lilith1 = GameObject.Find("Lilith1"); Lilith2 = GameObject.Find("Lilith2"); Lilith3 = GameObject.Find("Lilith3"); Lilith4 = GameObject.Find("Lilith3");
                Azazel1 = GameObject.Find("Azazel1"); Azazel2 = GameObject.Find("Azazel2"); Azazel3 = GameObject.Find("Azazel3"); Azazel4 = GameObject.Find("Azazel4");
                ready = GameObject.Find("Ready");
                black = GameObject.Find("Fader").GetComponent<Image>();
                anim = GameObject.Find("Fader").GetComponent<Animator>();
            }
            // If any Urial is selected, the others are greyed out.
            if (P1Urial)
            {
                Urial1.GetComponent<Image>().color = new Vector4(1, 1, 1, 1);
                Urial2.GetComponent<Image>().color = new Vector4(0.545f, 0.545f, 0.545f, 1);
                Urial3.GetComponent<Image>().color = new Vector4(0.545f, 0.545f, 0.545f, 1);
                Urial4.GetComponent<Image>().color = new Vector4(0.545f, 0.545f, 0.545f, 1);
                P2Urial = false; P3Urial = false; P4Urial = false; Urial = true;
            }
            if (P2Urial)
            {
                Urial2.GetComponent<Image>().color = new Vector4(1, 1, 1, 1);
                Urial1.GetComponent<Image>().color = new Vector4(0.545f, 0.545f, 0.545f, 1);
                Urial3.GetComponent<Image>().color = new Vector4(0.545f, 0.545f, 0.545f, 1);
                Urial4.GetComponent<Image>().color = new Vector4(0.545f, 0.545f, 0.545f, 1);
                P1Urial = false; P3Urial = false; P4Urial = false; Urial = true;
            }
            if (P3Urial)
            {
                Urial3.GetComponent<Image>().color = new Vector4(1, 1, 1, 1);
                Urial2.GetComponent<Image>().color = new Vector4(0.545f, 0.545f, 0.545f, 1);
                Urial1.GetComponent<Image>().color = new Vector4(0.545f, 0.545f, 0.545f, 1);
                Urial4.GetComponent<Image>().color = new Vector4(0.545f, 0.545f, 0.545f, 1);
                P2Urial = false; P1Urial = false; P4Urial = false; Urial = true;
            }
            if (P4Urial)
            {
                Urial4.GetComponent<Image>().color = new Vector4(1, 1, 1, 1);
                Urial2.GetComponent<Image>().color = new Vector4(0.545f, 0.545f, 0.545f, 1);
                Urial3.GetComponent<Image>().color = new Vector4(0.545f, 0.545f, 0.545f, 1);
                Urial1.GetComponent<Image>().color = new Vector4(0.545f, 0.545f, 0.545f, 1);
                P2Urial = false; P3Urial = false; P1Urial = false; Urial = true;
            }
            if (!P1Urial && !P2Urial && !P3Urial && !P4Urial)
            {
                Urial1.GetComponent<Image>().color = new Vector4(1, 1, 1, 1);
                Urial2.GetComponent<Image>().color = new Vector4(1, 1, 1, 1);
                Urial3.GetComponent<Image>().color = new Vector4(1, 1, 1, 1);
                Urial4.GetComponent<Image>().color = new Vector4(1, 1, 1, 1);
            }
            // If any Barachial is selected, the others are greyed out.
            if (P1Barachial)
            {
                Barachial1.GetComponent<Image>().color = new Vector4(1, 1, 1, 1);
                Barachial2.GetComponent<Image>().color = new Vector4(0.545f, 0.545f, 0.545f, 1);
                Barachial3.GetComponent<Image>().color = new Vector4(0.545f, 0.545f, 0.545f, 1);
                Barachial4.GetComponent<Image>().color = new Vector4(0.545f, 0.545f, 0.545f, 1);
                P2Barachial = false; P3Barachial = false; P4Barachial = false; Barachial = true;
            }
            if (P2Barachial)
            {
                Barachial2.GetComponent<Image>().color = new Vector4(1, 1, 1, 1);
                Barachial1.GetComponent<Image>().color = new Vector4(0.545f, 0.545f, 0.545f, 1);
                Barachial3.GetComponent<Image>().color = new Vector4(0.545f, 0.545f, 0.545f, 1);
                Barachial4.GetComponent<Image>().color = new Vector4(0.545f, 0.545f, 0.545f, 1);
                P1Barachial = false; P3Barachial = false; P4Barachial = false; Barachial = true;
            }
            if (P3Barachial)
            {
                Barachial3.GetComponent<Image>().color = new Vector4(1, 1, 1, 1);
                Barachial2.GetComponent<Image>().color = new Vector4(0.545f, 0.545f, 0.545f, 1);
                Barachial1.GetComponent<Image>().color = new Vector4(0.545f, 0.545f, 0.545f, 1);
                Barachial4.GetComponent<Image>().color = new Vector4(0.545f, 0.545f, 0.545f, 1);
                P2Barachial = false; P1Barachial = false; P4Barachial = false; Barachial = true;
            }
            if (P4Barachial)
            {
                Barachial4.GetComponent<Image>().color = new Vector4(1, 1, 1, 1);
                Barachial2.GetComponent<Image>().color = new Vector4(0.545f, 0.545f, 0.545f, 1);
                Barachial3.GetComponent<Image>().color = new Vector4(0.545f, 0.545f, 0.545f, 1);
                Barachial1.GetComponent<Image>().color = new Vector4(0.545f, 0.545f, 0.545f, 1);
                P2Barachial = false; P3Barachial = false; P1Barachial = false; Barachial = true;
            }
            if (!P1Barachial && !P2Barachial && !P3Barachial && !P4Barachial)
            {
                Barachial1.GetComponent<Image>().color = new Vector4(1, 1, 1, 1);
                Barachial2.GetComponent<Image>().color = new Vector4(1, 1, 1, 1);
                Barachial3.GetComponent<Image>().color = new Vector4(1, 1, 1, 1);
                Barachial4.GetComponent<Image>().color = new Vector4(1, 1, 1, 1);
            }
            // If any Lilith is selected, the others are greyed out.
            if (P1Lilith)
            {
                Lilith1.GetComponent<Image>().color = new Vector4(1, 1, 1, 1);
                Lilith2.GetComponent<Image>().color = new Vector4(0.545f, 0.545f, 0.545f, 1);
                Lilith3.GetComponent<Image>().color = new Vector4(0.545f, 0.545f, 0.545f, 1);
                Lilith4.GetComponent<Image>().color = new Vector4(0.545f, 0.545f, 0.545f, 1);
                P2Lilith = false; P3Lilith = false; P4Lilith = false; Lilith = true;
            }
            if (P2Lilith)
            {
                Lilith2.GetComponent<Image>().color = new Vector4(1, 1, 1, 1);
                Lilith1.GetComponent<Image>().color = new Vector4(0.545f, 0.545f, 0.545f, 1);
                Lilith3.GetComponent<Image>().color = new Vector4(0.545f, 0.545f, 0.545f, 1);
                Lilith4.GetComponent<Image>().color = new Vector4(0.545f, 0.545f, 0.545f, 1);
                P1Lilith = false; P3Lilith = false; P4Lilith = false; Lilith = true;
            }
            if (P3Lilith)
            {
                Lilith3.GetComponent<Image>().color = new Vector4(1, 1, 1, 1);
                Lilith2.GetComponent<Image>().color = new Vector4(0.545f, 0.545f, 0.545f, 1);
                Lilith1.GetComponent<Image>().color = new Vector4(0.545f, 0.545f, 0.545f, 1);
                Lilith4.GetComponent<Image>().color = new Vector4(0.545f, 0.545f, 0.545f, 1);
                P2Lilith = false; P1Lilith = false; P4Lilith = false; Lilith = true;
            }
            if (P4Lilith)
            {
                Lilith4.GetComponent<Image>().color = new Vector4(1, 1, 1, 1);
                Lilith2.GetComponent<Image>().color = new Vector4(0.545f, 0.545f, 0.545f, 1);
                Lilith3.GetComponent<Image>().color = new Vector4(0.545f, 0.545f, 0.545f, 1);
                Lilith1.GetComponent<Image>().color = new Vector4(0.545f, 0.545f, 0.545f, 1);
                P2Lilith = false; P3Lilith = false; P1Lilith = false; Lilith = true;
            }
            if (!P1Lilith && !P2Lilith && !P3Lilith && !P4Lilith)
            {
                Lilith1.GetComponent<Image>().color = new Vector4(1, 1, 1, 1);
                Lilith2.GetComponent<Image>().color = new Vector4(1, 1, 1, 1);
                Lilith3.GetComponent<Image>().color = new Vector4(1, 1, 1, 1);
                Lilith4.GetComponent<Image>().color = new Vector4(1, 1, 1, 1);
            }
            // If any Azazel is selected, the others are greyed out.
            if (P1Azazel)
            {
                Azazel1.GetComponent<Image>().color = new Vector4(1, 1, 1, 1);
                Azazel2.GetComponent<Image>().color = new Vector4(0.545f, 0.545f, 0.545f, 1);
                Azazel3.GetComponent<Image>().color = new Vector4(0.545f, 0.545f, 0.545f, 1);
                Azazel4.GetComponent<Image>().color = new Vector4(0.545f, 0.545f, 0.545f, 1);
                P2Azazel = false; P3Azazel = false; P4Azazel = false; Azazel = true;
            }
            if (P2Azazel)
            {
                Azazel2.GetComponent<Image>().color = new Vector4(1, 1, 1, 1);
                Azazel1.GetComponent<Image>().color = new Vector4(0.545f, 0.545f, 0.545f, 1);
                Azazel3.GetComponent<Image>().color = new Vector4(0.545f, 0.545f, 0.545f, 1);
                Azazel4.GetComponent<Image>().color = new Vector4(0.545f, 0.545f, 0.545f, 1);
                P1Azazel = false; P3Azazel = false; P4Azazel = false; Azazel = true;
            }
            if (P3Azazel)
            {
                Azazel3.GetComponent<Image>().color = new Vector4(1, 1, 1, 1);
                Azazel2.GetComponent<Image>().color = new Vector4(0.545f, 0.545f, 0.545f, 1);
                Azazel1.GetComponent<Image>().color = new Vector4(0.545f, 0.545f, 0.545f, 1);
                Azazel4.GetComponent<Image>().color = new Vector4(0.545f, 0.545f, 0.545f, 1);
                P2Azazel = false; P1Azazel = false; P4Azazel = false; Azazel = true;
            }
            if (P4Azazel)
            {
                Azazel4.GetComponent<Image>().color = new Vector4(1, 1, 1, 1);
                Azazel2.GetComponent<Image>().color = new Vector4(0.545f, 0.545f, 0.545f, 1);
                Azazel3.GetComponent<Image>().color = new Vector4(0.545f, 0.545f, 0.545f, 1);
                Azazel1.GetComponent<Image>().color = new Vector4(0.545f, 0.545f, 0.545f, 1);
                P2Azazel = false; P3Azazel = false; P1Azazel = false; Azazel = true;
            }
            if (!P1Azazel && !P2Azazel && !P3Azazel && !P4Azazel)
            {
                Azazel1.GetComponent<Image>().color = new Vector4(1, 1, 1, 1);
                Azazel2.GetComponent<Image>().color = new Vector4(1, 1, 1, 1);
                Azazel3.GetComponent<Image>().color = new Vector4(1, 1, 1, 1);
                Azazel4.GetComponent<Image>().color = new Vector4(1, 1, 1, 1);
            }
            // If all players are selected, you can press start to begin the game.
        
            if (Urial && Barachial && Lilith && Azazel)
            {
                ready.GetComponent<RectTransform>().localPosition = new Vector3(readyTransform, 0, 0);
                if (readyTransform < 0)
                    readyTransform += 200;
                else if (readyTransform >= 0)
                    readyTransform = 0;
                // Set the characters joysticks and then launch the game.
                if (Input.GetKeyDown("joystick button 7"))
                {
                    GameObject.Find("Player One Selection").GetComponent<Player1Select>().canSwap = false; GameObject.Find("Player One Selection").GetComponent<Player1Select>().canSelect = false;
                    GameObject.Find("Player Two Selection").GetComponent<Player2Select>().canSwap = false; GameObject.Find("Player Two Selection").GetComponent<Player2Select>().canSelect = false;
                    GameObject.Find("Player Three Selection").GetComponent<Player3Select>().canSwap = false; GameObject.Find("Player Three Selection").GetComponent<Player3Select>().canSelect = false;
                    GameObject.Find("Player Four Selection").GetComponent<Player4Select>().canSwap = false; GameObject.Find("Player Four Selection").GetComponent<Player4Select>().canSelect = false;
                    anim.SetBool("Fade", true);
                    StartCoroutine(LoadMapSelect());
                }
            }
            else {
                if (readyTransform > -900)
                    readyTransform -= 200;
                else if (readyTransform <= -900)
                    readyTransform = -900;
            }
        }
        if (curScene == 8 && boolsSet == false)
        {
            if (P1Urial)
            {
                GameObject.Find("Urial").GetComponent<PlayerScript>().playerId = 0;
                Debug.Log("Urial is Player ID 4, or Player 1");
            }
            else if (P2Urial)
            {
                GameObject.Find("Urial").GetComponent<PlayerScript>().playerId = 1;
                Debug.Log("Urial is Player ID 5, or Player 2");
            }
            else if (P3Urial)
            {
                GameObject.Find("Urial").GetComponent<PlayerScript>().playerId = 2;
                Debug.Log("Urial is Player ID 6, or Player 3");
            }
            else if (P4Urial)
            {
                GameObject.Find("Urial").GetComponent<PlayerScript>().playerId = 3;
                Debug.Log("Urial is Player ID 7, or Player 4");
            }

            if (P1Barachial)
            {
                GameObject.Find("Barachial").GetComponent<PlayerScript>().playerId = 0;
                Debug.Log("Barachial is Player ID 4, or Player 1");
            }
            else if (P2Barachial)
            {
                GameObject.Find("Barachial").GetComponent<PlayerScript>().playerId = 1;
                Debug.Log("Barachial is Player ID 5, or Player 2");
            }
            else if (P3Barachial)
            {
                GameObject.Find("Barachial").GetComponent<PlayerScript>().playerId = 2;
                Debug.Log("Barachial is Player ID 6, or Player 3");
            }
            else if (P4Barachial)
            {
                GameObject.Find("Barachial").GetComponent<PlayerScript>().playerId = 3;
                Debug.Log("Barachial is Player ID 7, or Player 4");
            }

            if (P1Lilith)
            {
                GameObject.Find("Lilith").GetComponent<PlayerScript>().playerId = 0;
                Debug.Log("Lilith is Player ID 4, or Player 1");
            }
            else if (P2Lilith)
            {
                GameObject.Find("Lilith").GetComponent<PlayerScript>().playerId = 1;
                Debug.Log("Lilith is Player ID 5, or Player 2");
            }
            else if (P3Lilith)
            {
                GameObject.Find("Lilith").GetComponent<PlayerScript>().playerId = 2;
                Debug.Log("Lilith is Player ID 6, or Player 3");
            }
            else if (P4Lilith)
            {
                GameObject.Find("Lilith").GetComponent<PlayerScript>().playerId = 3;
                Debug.Log("Lilith is Player ID 7, or Player 4");
            }

            if (P1Azazel)
            {
                GameObject.Find("Azazel").GetComponent<PlayerScript>().playerId = 0;
                Debug.Log("Azazel is Player ID 4, or Player 1");
            }
            else if (P2Azazel)
            {
                GameObject.Find("Azazel").GetComponent<PlayerScript>().playerId = 1;
                Debug.Log("Azazel is Player ID 5, or Player 2");
            }
            else if (P3Azazel)
            {
                GameObject.Find("Azazel").GetComponent<PlayerScript>().playerId = 2;
                Debug.Log("Azazel is Player ID 6, or Player 3");
            }
            else if (P4Azazel)
            {
                GameObject.Find("Azazel").GetComponent<PlayerScript>().playerId = 3;
                Debug.Log("Azazel is Player ID 7, or Player 4");
            }
            boolsSet = true;
        }
        else if (curScene == 9 && boolsSet == false)
        {
            if (P1Urial)
            {
                GameObject.Find("Urial").GetComponent<FFAPlayerScript>().playerId = 0;
                Debug.Log("Urial is Player ID 4, or Player 1");
            }
            else if (P2Urial)
            {
                GameObject.Find("Urial").GetComponent<FFAPlayerScript>().playerId = 1;
                Debug.Log("Urial is Player ID 5, or Player 2");
            }
            else if (P3Urial)
            {
                GameObject.Find("Urial").GetComponent<FFAPlayerScript>().playerId = 2;
                Debug.Log("Urial is Player ID 6, or Player 3");
            }
            else if (P4Urial)
            {
                GameObject.Find("Urial").GetComponent<FFAPlayerScript>().playerId = 3;
                Debug.Log("Urial is Player ID 7, or Player 4");
            }

            if (P1Barachial)
            {
                GameObject.Find("Barachial").GetComponent<FFAPlayerScript>().playerId = 0;
                Debug.Log("Barachial is Player ID 4, or Player 1");
            }
            else if (P2Barachial)
            {
                GameObject.Find("Barachial").GetComponent<FFAPlayerScript>().playerId = 1;
                Debug.Log("Barachial is Player ID 5, or Player 2");
            }
            else if (P3Barachial)
            {
                GameObject.Find("Barachial").GetComponent<FFAPlayerScript>().playerId = 2;
                Debug.Log("Barachial is Player ID 6, or Player 3");
            }
            else if (P4Barachial)
            {
                GameObject.Find("Barachial").GetComponent<FFAPlayerScript>().playerId = 3;
                Debug.Log("Barachial is Player ID 7, or Player 4");
            }

            if (P1Lilith)
            {
                GameObject.Find("Lilith").GetComponent<FFAPlayerScript>().playerId = 0;
                Debug.Log("Lilith is Player ID 4, or Player 1");
            }
            else if (P2Lilith)
            {
                GameObject.Find("Lilith").GetComponent<FFAPlayerScript>().playerId = 1;
                Debug.Log("Lilith is Player ID 5, or Player 2");
            }
            else if (P3Lilith)
            {
                GameObject.Find("Lilith").GetComponent<FFAPlayerScript>().playerId = 2;
                Debug.Log("Lilith is Player ID 6, or Player 3");
            }
            else if (P4Lilith)
            {
                GameObject.Find("Lilith").GetComponent<FFAPlayerScript>().playerId = 3;
                Debug.Log("Lilith is Player ID 7, or Player 4");
            }

            if (P1Azazel)
            {
                GameObject.Find("Azazel").GetComponent<FFAPlayerScript>().playerId = 0;
                Debug.Log("Azazel is Player ID 4, or Player 1");
            }
            else if (P2Azazel)
            {
                GameObject.Find("Azazel").GetComponent<FFAPlayerScript>().playerId = 1;
                Debug.Log("Azazel is Player ID 5, or Player 2");
            }
            else if (P3Azazel)
            {
                GameObject.Find("Azazel").GetComponent<FFAPlayerScript>().playerId = 2;
                Debug.Log("Azazel is Player ID 6, or Player 3");
            }
            else if (P4Azazel)
            {
                GameObject.Find("Azazel").GetComponent<FFAPlayerScript>().playerId = 3;
                Debug.Log("Azazel is Player ID 7, or Player 4");
            }

            boolsSet = true;
        }
        if (curScene > 9 && curScene != 13)
        {
            resetBools();
        }
    }

    // Resets all of the bools
    public void resetBools()
    {
        Urial = false; Barachial = false; Lilith = false; Azazel = false;
        P1Urial = false; P2Urial = false; P3Urial = false; P4Urial = false;
        P1Barachial = false; P2Barachial = false; P3Barachial = false; P4Barachial = false;
        P1Lilith = false; P2Lilith = false; P3Lilith = false; P4Lilith = false;
        P1Azazel = false; P2Azazel = false; P3Azazel = false; P4Azazel = false;
        onCharSelect = true;
        boolsSet = false;
    }

    IEnumerator LoadMapSelect()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(mapSelectScene);
    }
}
