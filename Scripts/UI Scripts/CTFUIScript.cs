using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class CTFUIScript : MonoBehaviour {
    /* Manages the score and timer when in game */
    // References for all text elements and the timer
    public Text angelScoreText, demonScoreText, timerText;
    public int minutes, seconds, angelScore, demonScore, angelLives1, angelLives2, demonLives1, demonLives2;
    // bools for spawning the power up
    private bool powerUpSpawned, startShowingUI;
    public GameObject shotPU, speedPU, shieldPU, powerUpText;
    //audio stuff
    public AudioClip oneMinuteOne, oneMinuteTwo, oneMinuteThree, oneMinuteFour, PowerUpSpawn;
    int whichOne;
    bool canPlay;
    bool canPlayPowerUpSpawn;
    // Set the default time and score numbers and start timer
    private int randNum; 
	void Start ()
    {
        canPlay = true;
        canPlayPowerUpSpawn = true;
        //minutes = 3;
        //seconds = 0;
        angelScore = 0;
        demonScore = 0;
        InvokeRepeating("TimeReduce", 4.4f, 1);
        powerUpSpawned = false;
        startShowingUI = false;

        if (PlayerPrefs.GetFloat("timeAmount") == 0) 
        {
            minutes = 1;
        }
        if (PlayerPrefs.GetFloat("timeAmount") == 1) 
        {
            minutes = 1;
            seconds = 30;
        }
        if (PlayerPrefs.GetFloat("timeAmount") == 2) 
        {
            minutes = 2;
        }
        if (PlayerPrefs.GetFloat("timeAmount") == 3) 
        {
            minutes = 2;
            seconds = 30;
        }
        if (PlayerPrefs.GetFloat("timeAmount") == 4) 
        {
           minutes = 3;
        }
        
    }
    // Set up the text elements and load results at the end of the timer
    void Update()
    {
        if (startShowingUI)
        {
            if (seconds < 10)
                timerText.text = minutes + ":0" + seconds;
            else
                timerText.text = minutes + ":" + seconds;
            angelScoreText.text = "Angels: " + angelScore;
            demonScoreText.text = "Demons: " + demonScore;
        }
        else if (!startShowingUI)
        {
            timerText.text = "";
            angelScoreText.text = "";
            demonScoreText.text = "";
        }
        if (minutes == 2 && seconds == 0 && !powerUpSpawned)
        {
            if (canPlayPowerUpSpawn == true)
            {
                GetComponent<AudioSource>().PlayOneShot(PowerUpSpawn);
                canPlayPowerUpSpawn = false;
            }

            int powerUpNumber = Random.Range(1, 3);
            
            if (powerUpNumber == 1)
            {
                Instantiate(speedPU);
                GameObject powerText = Instantiate(powerUpText);
                Destroy(powerText, 2f);
                powerUpSpawned = true;

            } else if (powerUpNumber == 2) {

                Instantiate(shotPU);
                GameObject powerText = Instantiate(powerUpText);
                Destroy(powerText, 2f);
                powerUpSpawned = true;
            }
        }
        if (minutes == 1 && seconds == 0)
        {
            whichOne = Random.Range(0, 4);
            if (whichOne == 0 && canPlay == true)
            {
                canPlay = false;
                GetComponent<AudioSource>().PlayOneShot(oneMinuteOne);
               
            }
            if (whichOne == 1 && canPlay == true)
            {
                canPlay = false;
                GetComponent<AudioSource>().PlayOneShot(oneMinuteTwo);
            }
            if (whichOne == 2 && canPlay == true)
            {
                canPlay = false;
                GetComponent<AudioSource>().PlayOneShot(oneMinuteThree);
            }
            if (whichOne == 3 && canPlay == true)
            {
                canPlay = false;
                GetComponent<AudioSource>().PlayOneShot(oneMinuteFour);
            }
        }
        if (minutes == 0 && seconds == 0)
        {
            if (angelScore > demonScore)
            {
                SceneManager.LoadScene(10);
            }
            else if (demonScore > angelScore)
            {
                SceneManager.LoadScene(11);
            }
            else if (demonScore == angelScore)
            {
                SceneManager.LoadScene(12);
            }
        }
    }
    // Reduce the timer
    void TimeReduce()
    {
        startShowingUI = true;
        if (seconds == 0 && minutes != 0)
        {
            seconds = 59;
            minutes--;
        }
        else if (seconds > 0)
        {
            seconds--;
        }
    }
}