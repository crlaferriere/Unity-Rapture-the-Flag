using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MusicScript : MonoBehaviour {

    private static MusicScript instance = null;
    public static MusicScript Instance
    {
        get { return instance; }
    }
    // Scene Checker
    public int curScene;
    // Audio
    public AudioClip menuTheme;
    public AudioClip map1Theme;
    public AudioClip angelWin;
    public AudioClip demonWin;
    // Bool checker for music
    public bool menuPlaying, map1Playing, angelPlaying, demonPlaying;
    private AudioSource audioSource;
    void Awake()
    {
        // Don't destroy this object when loading through scenes
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else {
            instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
        // Audio Source
        audioSource = GetComponent<AudioSource>();
        // Default music settings
        menuPlaying = false;
        if ((curScene <= 8) && !menuPlaying)
        {
            map1Playing = false; angelPlaying = false; demonPlaying = false;
            audioSource.Stop();
            audioSource.clip = menuTheme;
            audioSource.volume = .6f;
            audioSource.Play();
            menuPlaying = true;
            audioSource.loop = true;
           
        }
        
        if ((curScene >= 9) && !angelPlaying)
        {
            menuPlaying = false; map1Playing = false; demonPlaying = false;
            audioSource.Stop();
            audioSource.clip = angelWin;
            audioSource.volume = .4f;
            audioSource.Play();
            angelPlaying = true;
            audioSource.loop = true;
        }
    }

    void Update()
    {
        curScene = SceneManager.GetActiveScene().buildIndex;
        // If on any menu or draw, play menu theme.
        if ((curScene <= 8 ) && !menuPlaying)
        {
            map1Playing = false;angelPlaying = false; demonPlaying = false;
            audioSource.Stop();
            audioSource.clip = menuTheme;
            audioSource.volume = .6f;
            audioSource.Play();
            menuPlaying = true;
            audioSource.loop = true;
        }
        // If on map 1, play map 1 song.
       
        // If map 2, play map 2 song.
   
        // If angel win screen, play angel song.
        if ((curScene >= 9) && !angelPlaying)
        {
            menuPlaying = false; map1Playing = false; demonPlaying = false;
            audioSource.Stop();
            audioSource.clip = angelWin;
            audioSource.volume = .4f;
            audioSource.Play();
            angelPlaying = true;
            audioSource.loop = true;
        }
        // If demon win screen, play demon song
        /*
        if ((curScene == 14 || curScene == 18 || curScene == 19) && !demonPlaying)
        {
            menuPlaying = false; map1Playing = false; map2Playing = false; angelPlaying = false;
            audioSource.Stop();
            audioSource.clip = demonWin;
            audioSource.Play();
            demonPlaying = true;
        }
        */
    }
}

