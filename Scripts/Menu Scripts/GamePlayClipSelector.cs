using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GamePlayClipSelector : MonoBehaviour {

    int clipSelector;
    public GameObject clip1, clip2, clip3;

    public Image black;
    public Animator anim;
    void Start ()
    {
        clipSelector = Random.Range(0, 3);
        if (clipSelector == 0)
        {
            clip1.GetComponent<MovieScript>().PlayMovie();
            clip2.SetActive(false);
            clip3.SetActive(false);
            Invoke("TransitionWrapper", 30);
        }
        else if (clipSelector == 1)
        {
            clip2.GetComponent<MovieScript>().PlayMovie();
            clip1.SetActive(false);
            clip3.SetActive(false);
            Invoke("TransitionWrapper", 39);
        }
        else if (clipSelector == 2)
        {
            clip3.GetComponent<MovieScript>().PlayMovie();
            clip1.SetActive(false);
            clip2.SetActive(false);
            Invoke("TransitionWrapper", 32);
        }
    }

    void Update()
    {
        if (Input.anyKey)
            TransitionWrapper();
    }

    void TransitionWrapper()
    {
        StartCoroutine("TransitionChange");
    }

    IEnumerator TransitionChange()
    {
        anim.SetBool("Fade", true);
        yield return new WaitUntil(() => black.color.a == 1);
        SceneManager.LoadScene(0);
    }
}
