using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class MenuSelectAudio : MonoBehaviour {
    public AudioClip Selection;
    public bool CanMakeSound;
    public AudioClip Click;
    public bool canClickSound;
    public bool isReset;
    
    // Use this for initialization
    void Start () {
        CanMakeSound = true;
        canClickSound = true;
        isReset = true;
        
    }
	
	// Update is called once per frame
	void Update () {




        if (SceneManager.GetActiveScene().buildIndex < 4 || SceneManager.GetActiveScene().buildIndex > 5)
        {

            if (Input.GetKeyDown("joystick button 0") && canClickSound == true)
            {
                GetComponent<AudioSource>().PlayOneShot(Click);
                canClickSound = false;
            }
            if (Input.GetAxis("Vertical") > 0 || Input.GetAxis("Vertical") < 0)
            {
                isReset = false;
                if (CanMakeSound == true)
                {
                    GetComponent<AudioSource>().PlayOneShot(Selection);

                    CanMakeSound = false;

                    Invoke("ResetAudio", .5f);


                }
            }
        }





       
        if (Input.GetAxis("Vertical") == 0)
        {
            isReset = true;
            CanMakeSound = true;
        }

        

    }
   public void ResetAudio()

    {

        if (isReset == false)
        {
            CanMakeSound = true;
            isReset = true;
        }

    }
}
