using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GamePlaySceneLoader : MonoBehaviour {

    public Image black;
    public Animator anim;

    void Start ()
    {
        Invoke("Transition", 29f);
	}

    void Update ()
    {
		if (Input.GetKeyDown(KeyCode.RightShift))
        {
            StartCoroutine("Transition");
        }
	}

    IEnumerator Transition()
    {
        anim.SetBool("Fade", true);
        yield return new WaitUntil(() => black.color.a == 1);
        SceneManager.LoadScene(12);
    }

}
