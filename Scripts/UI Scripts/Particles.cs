using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particles : MonoBehaviour {
    /* Automatically sets the sorting layer for any particle system */
	void Start ()
    {
       GetComponent<Renderer>().sortingLayerName = "Particle";
    }
}
