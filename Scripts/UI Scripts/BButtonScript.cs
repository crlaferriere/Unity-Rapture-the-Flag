using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class BButtonScript : MonoBehaviour
{
    //int for checking the scene and checking the mode
    public int curScene;

    public float fillVal;
    public bool bPressed;
    public Image black;
    public Animator anim;

    void Start()
    {
        fillVal = 0;
        bPressed = false;
        curScene = SceneManager.GetActiveScene().buildIndex;
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.GetComponent<Image>().fillAmount = fillVal;

        if (Input.GetKey("joystick button 1") || Input.GetKey(KeyCode.RightShift))
            bPressed = true;
        else
            bPressed = false;

        if (bPressed)
        {
            if (fillVal < 1)
                fillVal += 0.02f;
        }
        else if (!bPressed)
        {
            if (fillVal > 0)
                fillVal -= 0.02f;
        }

        if (fillVal < 0)
            fillVal = 0;
        else if (fillVal > 1)
            fillVal = 1;

        if (fillVal == 1)
        {
            // Load title screen
            if (curScene == 3)
            {
                anim.SetBool("Fade", true);
                Invoke("LoadTitle", 1f);
            }
            // Load mode select screen
            if (curScene == 4)
            {
                anim.SetBool("Fade", true);
                Invoke("LoadModes", 1f);
            }
            // Load CTF character select
            if (curScene == 6)
            {
                anim.SetBool("Fade", true);
                GameObject.Find("CharacterMaster").GetComponent<MasterSelector>().resetBools();
                Invoke("LoadCharacterSelect", 1f);
            }
        }
    }

    void LoadTitle()
    {
        SceneManager.LoadScene(0);
    }
    void LoadModes()
    {
        SceneManager.LoadScene(3);
    }
    void LoadCharacterSelect()
    {
        SceneManager.LoadScene(4);
    }
}

