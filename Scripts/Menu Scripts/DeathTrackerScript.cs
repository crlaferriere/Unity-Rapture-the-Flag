using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathTrackerScript : MonoBehaviour {

    public bool urialAlive, barachialAlive, lilithAlive, azazelAlive;

	void Awake ()
    {
        urialAlive = true; barachialAlive = true; lilithAlive = true; azazelAlive = true;
	}
	
}
