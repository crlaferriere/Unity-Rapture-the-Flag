using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class TitleScreenAudio : MonoBehaviour {

    public AudioClip startOne;
    public AudioClip startTwo;
    public AudioClip startThree;
    public AudioClip startFour;
    private int whichOne;
    // Use this for initialization
    void Awake () {
        whichOne = Random.Range(0, 4);
        if (whichOne == 0)
        {

            GetComponent<AudioSource>().PlayOneShot(startOne);
        }
        if (whichOne == 1)
        {

            GetComponent<AudioSource>().PlayOneShot(startTwo);
        }
        if (whichOne == 2)
        {

            GetComponent<AudioSource>().PlayOneShot(startThree);
        }
        if (whichOne == 3)
        {

            GetComponent<AudioSource>().PlayOneShot(startFour);
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
