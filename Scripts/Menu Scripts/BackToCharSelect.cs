using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class BackToCharSelect : MonoBehaviour {
    /* Resets to the character select screen for testing purposes */
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject.Find("UI Manager").GetComponent<CTFUIScript>().minutes = 0;
            GameObject.Find("UI Manager").GetComponent<CTFUIScript>().seconds = 1;
        }
    }
}
