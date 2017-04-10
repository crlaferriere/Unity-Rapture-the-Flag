using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class RespawnVisualizer : MonoBehaviour {
    /* Circle that spawns telling all players where this dead character will respawn */
    // Reference to text, as well as timer for respawn.
    public Text respawnText;
    private int timer;
    // Sets timer and text reference, and begins the timer decrease function
	void Start ()
    {
        timer = 2;
        if (this.gameObject.name == "AzazelRespawnCanvas(Clone)")
            respawnText = GameObject.Find("Azazel Respawn Text").GetComponent<Text>();
        else if (this.gameObject.name == "LilithRespawnCanvas(Clone)")
            respawnText = GameObject.Find("Lilith Respawn Text").GetComponent<Text>();
        else if (this.gameObject.name == "UrialRespawnCanvas(Clone)")
            respawnText = GameObject.Find("Urial Respawn Text").GetComponent<Text>();
        else if (this.gameObject.name == "BarachialRespawnCanvas(Clone)")
            respawnText = GameObject.Find("Barachial Respawn Text").GetComponent<Text>();
        InvokeRepeating("TimerDecrease", 1, 1f);
	}
	// Auto updates the timer number
	void Update ()
    {
        respawnText.text = timer.ToString();
	}
    // Decreases the timer and destroys itself after the timer reaches 0.
    void TimerDecrease()
    {
        if (timer > 0)
            timer--;
        else if (timer == 0)
            Destroy(this.gameObject);
    }
}
