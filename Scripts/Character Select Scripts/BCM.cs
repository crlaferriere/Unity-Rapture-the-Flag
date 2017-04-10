using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class BCM : MonoBehaviour {
    /* Backup Character Manager for tracking everything */
    // Bools for keeping track of who selected what
    public bool P1Urial, P2Urial, P3Urial, P4Urial;
    public bool P1Barachial, P2Barachial, P3Barachial, P4Barachial;
    public bool P1Lilith, P2Lilith, P3Lilith, P4Lilith;
    public bool P1Azazel, P2Azazel, P3Azazel, P4Azazel;
    //Static instance of GameManager which allows it to be accessed by any other script.
    public static BCM instance = null;
    // References to the actual players, who was picked, and if everyone has been picked.
    //public GameObject Urial, Barachial, Lilith, Azazel;
    public bool urialPicked, barachialPicked, lilithPicked, azazelPicked;
    public bool donePicking;
    // Scene Checker
    public int curScene;
    // Set instance to this, and prevent other instaces to be created. Keeps this object through all scenes.
    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }
    // Get temp reference to the scene and change what you can do based on what scene it is.
    void Update()
    {
        // Create a temporary reference to the current scene.
        curScene = SceneManager.GetActiveScene().buildIndex;
        // Retrieve the name of this scene.
        if ((curScene == 8))
        {
            // Sets ID based on what character is selected and by what player
            // Urial
            if (P1Urial && !P1Barachial && !P1Lilith && !P1Azazel && !urialPicked)
            {
                urialPicked = true;
                GameObject.Find("Urial").GetComponent<PlayerScript>().playerId = 0;
                //GameObject newUrial = Instantiate(Urial);
                //newUrial.GetComponent<PlayerScript>().playerId = 0;
            }
            else if (P2Urial && !P2Barachial && !P2Lilith && !P2Azazel && !urialPicked)
            {
                urialPicked = true;
                GameObject.Find("Urial").GetComponent<PlayerScript>().playerId = 1;
                //GameObject newUrial = Instantiate(Urial);
                //newUrial.GetComponent<PlayerScript>().playerId = 1;
            }
            else if (P3Urial && !P3Barachial && !P3Lilith && !P3Azazel && !urialPicked)
            {
                urialPicked = true;
                GameObject.Find("Urial").GetComponent<PlayerScript>().playerId = 2;
                //                GameObject newUrial = Instantiate(Urial);
                //                newUrial.GetComponent<PlayerScript>().playerId = 2;
            }
            else if (P4Urial && !P4Barachial && !P4Lilith && !P4Azazel && !urialPicked)
            {
                Debug.Log("Testing 123");
                urialPicked = true;
                GameObject.Find("Urial").GetComponent<PlayerScript>().playerId = 3;
                //GameObject newUrial = Instantiate(Urial);
                //newUrial.GetComponent<PlayerScript>().playerId = 3;
            }
            // Barachial
            if (P1Barachial && !P1Urial && !P1Lilith && ! P1Azazel && !barachialPicked)
            {
                barachialPicked = true;
                GameObject.Find("Barachial").GetComponent<PlayerScript>().playerId = 0;
                //                GameObject newBarachial = Instantiate(Barachial);
                //                newBarachial.GetComponent<PlayerScript>().playerId = 0;
            }
            else if (P2Barachial && !P2Urial && !P2Lilith && !P2Azazel && !barachialPicked)
            {
                barachialPicked = true;

                GameObject.Find("Barachial").GetComponent<PlayerScript>().playerId = 1;
                //               GameObject newBarachial = Instantiate(Barachial);
                //                newBarachial.GetComponent<PlayerScript>().playerId = 1;
            }
            else if (P3Barachial && !P3Urial && !P3Lilith && !P3Azazel && !barachialPicked)
            {
                barachialPicked = true;

                GameObject.Find("Barachial").GetComponent<PlayerScript>().playerId = 2;
                //GameObject newBarachial = Instantiate(Barachial);
                //newBarachial.GetComponent<PlayerScript>().playerId = 2;
            }
            else if (P4Barachial && !P4Urial && !P4Lilith && !P4Azazel && !barachialPicked)
            {
                barachialPicked = true;

                GameObject.Find("Barachial").GetComponent<PlayerScript>().playerId = 3;
                //GameObject newBarachial = Instantiate(Barachial);
                //newBarachial.GetComponent<PlayerScript>().playerId = 3;
            }
            // Lilith
            if (P1Lilith && !P1Urial && !P1Barachial && !P1Azazel && !lilithPicked)
            {
                lilithPicked = true;

                GameObject.Find("Lilith").GetComponent<PlayerScript>().playerId = 0;
                //                GameObject newLilith = Instantiate(Lilith);
                //                newLilith.GetComponent<PlayerScript>().playerId = 0;
            }
            else if (P2Lilith && !P2Urial && !P2Barachial && !P2Azazel && !lilithPicked)
            {
                lilithPicked = true;
                GameObject.Find("Lilith").GetComponent<PlayerScript>().playerId = 1;
//                GameObject newLilith = Instantiate(Lilith);
//                newLilith.GetComponent<PlayerScript>().playerId = 1;
            }
            else if (P3Lilith && !P3Urial && !P3Barachial && !P3Azazel && !lilithPicked)
            {
                lilithPicked = true;
                GameObject.Find("Lilith").GetComponent<PlayerScript>().playerId = 2;
//                GameObject newLilith = Instantiate(Lilith);
 //               newLilith.GetComponent<PlayerScript>().playerId = 2;
            }
            else if (P4Lilith && !P4Urial && !P4Barachial && !P4Azazel && !lilithPicked)
            {
                lilithPicked = true;
                GameObject.Find("Lilith").GetComponent<PlayerScript>().playerId = 3;
//                Instantiate(Lilith);
//                Lilith.GetComponent<PlayerScript>().playerId = 3;
            }
            // Azazel
            if (P1Azazel && !P1Urial && !P1Barachial && !P1Lilith && !azazelPicked)
            {
                azazelPicked = true;
                GameObject.Find("Azazel").GetComponent<PlayerScript>().playerId = 0;
                //GameObject newAzazel = Instantiate(Azazel);
                //newAzazel.GetComponent<PlayerScript>().playerId = 0;
            }
            else if (P2Azazel && !P2Urial && !P2Barachial && !P2Lilith && !azazelPicked)
            {
                azazelPicked = true;
                GameObject.Find("Azazel").GetComponent<PlayerScript>().playerId = 1;
                //GameObject newAzazel = Instantiate(Azazel);
                //newAzazel.GetComponent<PlayerScript>().playerId = 1;
            }
            if (P3Azazel && !P3Urial && !P3Barachial && !P3Lilith && !azazelPicked)
            {
                azazelPicked = true;
                GameObject.Find("Azazel").GetComponent<PlayerScript>().playerId = 2;
//                GameObject newAzazel = Instantiate(Azazel);
//                newAzazel.GetComponent<PlayerScript>().playerId = 2;
            }
            if (P4Azazel && !P4Urial && !P4Barachial && !P4Lilith && !azazelPicked)
            {
                azazelPicked = true;
                GameObject.Find("Azazel").GetComponent<PlayerScript>().playerId = 3;
                //GameObject newAzazel = Instantiate(Azazel);
                //newAzazel.GetComponent<PlayerScript>().playerId = 3;
            }
        }
        // If all characters have been picked, you can then move onto the next scene.
        if (urialPicked && barachialPicked && lilithPicked && azazelPicked)
            donePicking = true;

        if (curScene == 14 || curScene == 15 || curScene == 16)
            resetBools();
    }
    // Resets all of the bools
    public void resetBools()
    {
        urialPicked = false; barachialPicked = false; lilithPicked = false; azazelPicked = false;
        donePicking = false;
        P1Urial = false; P2Urial = false; P3Urial = false; P4Urial = false;
        P1Barachial = false; P2Barachial = false; P3Barachial = false; P4Barachial = false;
        P1Lilith = false; P2Lilith = false; P3Lilith = false; P4Lilith = false;
        P1Azazel = false; P2Azazel = false; P3Azazel = false; P4Azazel = false;
    }
}
