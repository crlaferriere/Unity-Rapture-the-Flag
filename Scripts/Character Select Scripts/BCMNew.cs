using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class BCMNew : MonoBehaviour
{
    public List<bool> characters = new List<bool>();
    public bool Urial, Barachial, Lilith, Azazel;
    void Start()
    {
        Urial = false; Barachial = false; Lilith = false; Azazel = false;
        characters.Add(Urial);
        characters.Add(Barachial);
        characters.Add(Lilith);
        characters.Add(Azazel);
    }

}