using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class FFAUIScript : MonoBehaviour {
    /* Manages the score and timer when in game */
    // References for all text elements and the timer
    public Text urialScoreText, barachialScoreText, lilithScoreText, azazelScoreText, timerText;
    public int minutes, seconds, urialScore, barachialScore, lilithScore, azazelScore;
    // bools for spawning the power up
    private bool /*powerUpSpawned,*/ startShowingUI;
    //public GameObject shotPU, shotSP, shotSH, powerUpText;
    //audio stuff
    public AudioClip oneMinuteOne, oneMinuteTwo, oneMinuteThree, oneMinuteFour/*, PowerUpSpawn*/;
    int whichOne;
    bool canPlay;
    bool canPlayPowerUpSpawn;
    // Set the default time and score numbers and start timer
    private int randNum;
    void Start()
    {
        canPlay = true;
        canPlayPowerUpSpawn = true;
        //minutes = 3;
        //seconds = 0;
        urialScore = 0; barachialScore = 0; lilithScore = 0; azazelScore = 0;
        InvokeRepeating("TimeReduce", 4.4f, 1);
        //powerUpSpawned = false;
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
        Debug.Log(lilithScore);
        urialScoreText.text = "Urial: " + urialScore;
        barachialScoreText.text = "Barachial: " + barachialScore;
        lilithScoreText.text = "Lilith: " + lilithScore;
        azazelScoreText.text = "Azazel: " + azazelScore;
        if (startShowingUI)
        {
            if (seconds < 10)
                timerText.text = minutes + ":0" + seconds;
            else
            {
                timerText.text = minutes + ":" + seconds;
                
                Debug.Log(urialScoreText.text);
           
            }
        }
        else if (!startShowingUI)
        {
            timerText.text = " ";
            urialScoreText.text = " ";
            barachialScoreText.text = " ";
            lilithScoreText.text = " ";
            azazelScoreText.text = " ";
        }
        /*if (minutes == 2 && seconds == 0 && !powerUpSpawned)
        {
            if (canPlayPowerUpSpawn == true)
            {
                GetComponent<AudioSource>().PlayOneShot(PowerUpSpawn);
                canPlayPowerUpSpawn = false;
            }

            //when we have time, this is where the power ups will be determined (for now, just speed)
            Instantiate(shotSP);
            GameObject powerText = Instantiate(powerUpText);
            Destroy(powerText, 2f);
            powerUpSpawned = true;
        }*/
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
            if (urialScore > lilithScore && urialScore > barachialScore && urialScore > azazelScore)
                SceneManager.LoadScene(14);
            else if (barachialScore > urialScore && barachialScore > lilithScore && barachialScore > azazelScore)
                SceneManager.LoadScene(15);
            else if (lilithScore > urialScore && lilithScore > barachialScore && lilithScore > azazelScore)
                SceneManager.LoadScene(16);
            else if (azazelScore > urialScore && azazelScore > barachialScore && azazelScore > lilithScore)
                SceneManager.LoadScene(17);
            else
                SceneManager.LoadScene(12);
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
