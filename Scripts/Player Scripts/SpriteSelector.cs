using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteSelector : MonoBehaviour {

    // Angel One = Urial
    // Angel Two = Barachial
    // Demon One = Lilith
    // Demon Two = Azazel;
    public bool UrialPicked, BarachialPicked, LilithPicked, AzazelPicked;

    void Awake ()
    {
        UrialPicked = false; BarachialPicked = false; LilithPicked = false; AzazelPicked = false;
    }
}
