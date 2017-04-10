using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MeleeCooldownIndicator : MonoBehaviour{
    /* Indicator for the melee reloading */
    // Bools for melee reloading reloading states and UI reference
    private bool urialReloading, barachialReloading, lilithReloading, azazelReloading;
    public GameObject meleeUI, character;
    // Set the bools, and set the color for the background and UI Icon
    void Start()
    {
        urialReloading = false; barachialReloading = false; lilithReloading = false; azazelReloading = false;
        GetComponent<Image>().color = new Color(1, 1, 1, 1f);
        meleeUI.GetComponent<Image>().color = new Color(1, 1, 1, 1f);
    }
    // Sets the reloading bool and reset the image colors and slider
    public void UrialMeleeCooldown()
    {
        urialReloading = true;
        GetComponent<Image>().color = new Color(1, 1, 1, 0.5f);
        meleeUI.GetComponent<Image>().color = new Color(1, 1, 1, 0.5f);
        GetComponent<Image>().fillAmount = 0;
    }
    public void BarachialMeleeCooldown()
    {
        barachialReloading = true;
        GetComponent<Image>().color = new Color(1, 1, 1, 0.5f);
        meleeUI.GetComponent<Image>().color = new Color(1, 1, 1, 0.5f);
        GetComponent<Image>().fillAmount = 0;
    }
    public void LilithMeleeCooldown()
    {
        lilithReloading = true;
        GetComponent<Image>().color = new Color(1, 1, 1, 0.5f);
        meleeUI.GetComponent<Image>().color = new Color(1, 1, 1, 0.5f);
        GetComponent<Image>().fillAmount = 0;
    }
    public void AzazelMeleeCooldown()
    {
        azazelReloading = true;
        GetComponent<Image>().color = new Color(1, 1, 1, 0.5f);
        meleeUI.GetComponent<Image>().color = new Color(1, 1, 1, 0.5f);
        GetComponent<Image>().fillAmount = 0;
    }
    // Lerps the fill amount to the same reload speed for the characters.
    void Update()
    {
        if (urialReloading)
        {
            if (GetComponent<Image>().fillAmount < 1)
                GetComponent<Image>().fillAmount += Time.deltaTime/2.25f;
            else if (GetComponent<Image>().fillAmount >= 1)
            {
                GetComponent<Image>().fillAmount = 1;
                UrialReloaded();
            }
        }
        if (barachialReloading)
        {
            if (GetComponent<Image>().fillAmount < 1)
                GetComponent<Image>().fillAmount += Time.deltaTime/ 2.25f;
            else if (GetComponent<Image>().fillAmount >= 1)
            {
                GetComponent<Image>().fillAmount = 1;
                BarachialReloaded();
            }
        }
        if (lilithReloading)
        {
            if (GetComponent<Image>().fillAmount < 1)
                GetComponent<Image>().fillAmount += Time.deltaTime/ 2.25f;
            else if (GetComponent<Image>().fillAmount >= 1)
            {
                GetComponent<Image>().fillAmount = 1;
                LilithReloaded();
            }
        }
        if (azazelReloading)
        {
            if (GetComponent<Image>().fillAmount < 1)
                GetComponent<Image>().fillAmount += Time.deltaTime/ 2.25f;
            else if (GetComponent<Image>().fillAmount >= 1)
            {
                GetComponent<Image>().fillAmount = 1;
                AzazelReloaded();
            }
        }
    }
    // Sets the final color for the images and resets the bool to false.
    void UrialReloaded()
    {
        GetComponent<Image>().color = new Color(1, 1, 1, 1);
        meleeUI.GetComponent<Image>().color = new Color(1, 1, 1, 1f);
        urialReloading = false;
    }
    void BarachialReloaded()
    {
        GetComponent<Image>().color = new Color(1, 1, 1, 1);
        meleeUI.GetComponent<Image>().color = new Color(1, 1, 1, 1f);
        barachialReloading = false;
    }
    void LilithReloaded()
    {
        GetComponent<Image>().color = new Color(1, 1, 1, 1);
        meleeUI.GetComponent<Image>().color = new Color(1, 1, 1, 1f);
        lilithReloading = false;
    }
    void AzazelReloaded()
    {
        GetComponent<Image>().color = new Color(1, 1, 1, 1);
        meleeUI.GetComponent<Image>().color = new Color(1, 1, 1, 1f);
        azazelReloading = false;
    }
}
