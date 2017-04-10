using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReturnFiller : MonoBehaviour {
    /* Visual indicator for the flag being returned */
    // Reference to the flag, circle image, and the fill/speed ints of the return.
    public GameObject flag;
    public Image fillCircle;
    public float fill, speed; 
    // Fills the circle overtime based on how long you have been standing on it
	void Update ()
    {
        if (fill < 150)
            fill = flag.GetComponent<FlagScript>().returnTimer;
        GetComponent<Image>().fillAmount = fill / 150; 
	}
}
