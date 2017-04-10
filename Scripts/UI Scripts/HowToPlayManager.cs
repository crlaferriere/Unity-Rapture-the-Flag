using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HowToPlayManager : MonoBehaviour {
    /* Maps all of the buttons, images, and text based on what players select on the screen. */
    // References to all images, texts, and buttons.
    public GameObject ControlsImage;
    public GameObject ModesText;
    public GameObject MovementImage, MovementButton, MovementText;
    public GameObject ShootingImage, ShootingButton, ShootingText;
    public GameObject ShieldImage, ShieldButton, ShieldText;
    public GameObject MeleeImage, MeleeButton, MeleeText;
    public GameObject FlagImage, FlagButton, FlagText;
    public GameObject TeamAImage, TeamAButton, TeamAText;
    // Sets all buttons to false and defaults to controls screen.
    void Start()
    {
        ControlsImage.SetActive(false);
        MovementImage.GetComponent<MeshRenderer>().enabled = false;
        ShootingImage.GetComponent<MeshRenderer>().enabled = false;
        ShieldImage.GetComponent<MeshRenderer>().enabled = false;
        MeleeImage.GetComponent<MeshRenderer>().enabled = false;
        FlagImage.GetComponent<MeshRenderer>().enabled = false;
        TeamAImage.GetComponent<MeshRenderer>().enabled = false;

        MovementButton.SetActive(false);
        ShootingButton.SetActive(false);
        ShieldButton.SetActive(false);
        MeleeButton.SetActive(false);
        FlagButton.SetActive(false);
        TeamAButton.SetActive(false);

        ModesText.SetActive(false);
        MovementText.SetActive(false);
        ShootingText.SetActive(false);
        ShieldText.SetActive(false);
        MeleeText.SetActive(false);
        FlagText.SetActive(false);
        TeamAText.SetActive(false);

        ControlsButtonPress();
    }

    // Turns off/on every respective button's function.
    public void ControlsButtonPress()
    {
        ControlsImage.SetActive(true);
        ModesText.SetActive(false);
        MovementImage.GetComponent<MeshRenderer>().enabled = false;
        ShootingImage.GetComponent<MeshRenderer>().enabled = false;
        ShieldImage.GetComponent<MeshRenderer>().enabled = false;
        MeleeImage.GetComponent<MeshRenderer>().enabled = false;
        FlagImage.GetComponent<MeshRenderer>().enabled = false;
        TeamAImage.GetComponent<MeshRenderer>().enabled = false;

        MovementButton.SetActive(false);
        ShootingButton.SetActive(false);
        ShieldButton.SetActive(false);
        MeleeButton.SetActive(false);
        FlagButton.SetActive(false);
        TeamAButton.SetActive(false);

        ModesText.SetActive(false);
        MovementText.SetActive(false);
        ShootingText.SetActive(false);
        ShieldText.SetActive(false);
        MeleeText.SetActive(false);
        FlagText.SetActive(false);
        TeamAText.SetActive(false);
    }
    public void MechanicsButtonPress()
    {
        MovementImage.GetComponent<MeshRenderer>().enabled = true;
        MovementImage.GetComponent<MovieScript>().PlayMovie();
        ControlsImage.SetActive(false);
        ModesText.SetActive(false);
        ShootingImage.GetComponent<MeshRenderer>().enabled = false;
        ShieldImage.GetComponent<MeshRenderer>().enabled = false;
        MeleeImage.GetComponent<MeshRenderer>().enabled = false;
        FlagImage.GetComponent<MeshRenderer>().enabled = false;
        TeamAImage.GetComponent<MeshRenderer>().enabled = false;

        MovementButton.SetActive(true);
        ShootingButton.SetActive(true);
        ShieldButton.SetActive(true);
        MeleeButton.SetActive(true);
        FlagButton.SetActive(true);
        TeamAButton.SetActive(true);

        MovementText.SetActive(true);
        ModesText.SetActive(false);
        ShootingText.SetActive(false);
        ShieldText.SetActive(false);
        MeleeText.SetActive(false);
        FlagText.SetActive(false);
        TeamAText.SetActive(false);
    }
    public void ModesButtonPress()
    {
        ModesText.SetActive(true);
        ControlsImage.SetActive(false);
        MovementImage.GetComponent<MeshRenderer>().enabled = false;
        ShootingImage.GetComponent<MeshRenderer>().enabled = false;
        ShieldImage.GetComponent<MeshRenderer>().enabled = false;
        MeleeImage.GetComponent<MeshRenderer>().enabled = false;
        FlagImage.GetComponent<MeshRenderer>().enabled = false;
        TeamAImage.GetComponent<MeshRenderer>().enabled = false;

        MovementButton.SetActive(false);
        ShootingButton.SetActive(false);
        ShieldButton.SetActive(false);
        MeleeButton.SetActive(false);
        FlagButton.SetActive(false);
        TeamAButton.SetActive(false);

        ModesText.SetActive(true);
        MovementText.SetActive(false);
        ShootingText.SetActive(false);
        ShieldText.SetActive(false);
        MeleeText.SetActive(false);
        FlagText.SetActive(false);
        TeamAText.SetActive(false);
    }
    public void MovementButtonPress()
    {
        MovementImage.GetComponent<MeshRenderer>().enabled = true;
        ShootingImage.GetComponent<MeshRenderer>().enabled = false;
        ShieldImage.GetComponent<MeshRenderer>().enabled = false;
        MeleeImage.GetComponent<MeshRenderer>().enabled = false;
        FlagImage.GetComponent<MeshRenderer>().enabled = false;
        TeamAImage.GetComponent<MeshRenderer>().enabled = false;

        MovementText.SetActive(true);
        ShootingText.SetActive(false);
        ShieldText.SetActive(false);
        MeleeText.SetActive(false);
        FlagText.SetActive(false);
        TeamAText.SetActive(false);

        MovementImage.GetComponent<MovieScript>().PlayMovie();
        ShootingImage.GetComponent<MovieScript>().StopMovie();
        ShieldImage.GetComponent<MovieScript>().StopMovie();
        MeleeImage.GetComponent<MovieScript>().StopMovie();
        FlagImage.GetComponent<MovieScript>().StopMovie();
        TeamAImage.GetComponent<MovieScript>().StopMovie();
    }
    public void ShootingButtonPress()
    {
        ShootingImage.GetComponent<MeshRenderer>().enabled = true;
        MovementImage.GetComponent<MeshRenderer>().enabled = false;
        ShieldImage.GetComponent<MeshRenderer>().enabled = false;
        MeleeImage.GetComponent<MeshRenderer>().enabled = false;
        FlagImage.GetComponent<MeshRenderer>().enabled = false;
        TeamAImage.GetComponent<MeshRenderer>().enabled = false;

        ShootingText.SetActive(true);
        MovementText.SetActive(false);
        ShieldText.SetActive(false);
        MeleeText.SetActive(false);
        FlagText.SetActive(false);
        TeamAText.SetActive(false);

        ShootingImage.GetComponent<MovieScript>().PlayMovie();
        MovementImage.GetComponent<MovieScript>().StopMovie();
        ShieldImage.GetComponent<MovieScript>().StopMovie();
        MeleeImage.GetComponent<MovieScript>().StopMovie();
        FlagImage.GetComponent<MovieScript>().StopMovie();
        TeamAImage.GetComponent<MovieScript>().StopMovie();
    }
    public void ShieldButtonPress()
    {
        ShieldImage.GetComponent<MeshRenderer>().enabled = true;
        MovementImage.GetComponent<MeshRenderer>().enabled = false;
        ShootingImage.GetComponent<MeshRenderer>().enabled = false;
        MeleeImage.GetComponent<MeshRenderer>().enabled = false;
        FlagImage.GetComponent<MeshRenderer>().enabled = false;
        TeamAImage.GetComponent<MeshRenderer>().enabled = false;

        ShieldText.SetActive(true);
        MovementText.SetActive(false);
        ShootingText.SetActive(false);
        MeleeText.SetActive(false);
        FlagText.SetActive(false);
        TeamAText.SetActive(false);

        ShieldImage.GetComponent<MovieScript>().PlayMovie();
        MovementImage.GetComponent<MovieScript>().StopMovie();
        ShootingImage.GetComponent<MovieScript>().StopMovie();
        MeleeImage.GetComponent<MovieScript>().StopMovie();
        FlagImage.GetComponent<MovieScript>().StopMovie();
        TeamAImage.GetComponent<MovieScript>().StopMovie();
    }
    public void MeleeButtonPress()
    { 
        MeleeImage.GetComponent<MeshRenderer>().enabled = true;
        MovementImage.GetComponent<MeshRenderer>().enabled = false;
        ShootingImage.GetComponent<MeshRenderer>().enabled = false;
        ShieldImage.GetComponent<MeshRenderer>().enabled = false;
        FlagImage.GetComponent<MeshRenderer>().enabled = false;
        TeamAImage.GetComponent<MeshRenderer>().enabled = false;

        MeleeText.SetActive(true);
        MovementText.SetActive(false);
        ShootingText.SetActive(false);
        ShieldText.SetActive(false);
        FlagText.SetActive(false);
        TeamAText.SetActive(false);

        MeleeImage.GetComponent<MovieScript>().PlayMovie();
        ShootingImage.GetComponent<MovieScript>().StopMovie();
        MovementImage.GetComponent<MovieScript>().StopMovie();
        ShieldImage.GetComponent<MovieScript>().StopMovie();
        FlagImage.GetComponent<MovieScript>().StopMovie();
        TeamAImage.GetComponent<MovieScript>().StopMovie();
    }
    public void FlagButtonPress()
    {
        FlagImage.GetComponent<MeshRenderer>().enabled = true;
        MovementImage.GetComponent<MeshRenderer>().enabled = false;
        ShootingImage.GetComponent<MeshRenderer>().enabled = false;
        ShieldImage.GetComponent<MeshRenderer>().enabled = false;
        MeleeImage.GetComponent<MeshRenderer>().enabled = false;
        TeamAImage.GetComponent<MeshRenderer>().enabled = false;

        FlagText.SetActive(true);
        MovementText.SetActive(false);
        ShootingText.SetActive(false);
        ShieldText.SetActive(false);
        MeleeText.SetActive(false);
        TeamAText.SetActive(false);

        FlagImage.GetComponent<MovieScript>().PlayMovie();
        ShootingImage.GetComponent<MovieScript>().StopMovie();
        MovementImage.GetComponent<MovieScript>().StopMovie();
        ShieldImage.GetComponent<MovieScript>().StopMovie();
        MeleeImage.GetComponent<MovieScript>().StopMovie();
        TeamAImage.GetComponent<MovieScript>().StopMovie();
    }
    public void TeamAButtonPress()
    {
        TeamAImage.GetComponent<MeshRenderer>().enabled = true;
        MovementImage.GetComponent<MeshRenderer>().enabled = false;
        ShootingImage.GetComponent<MeshRenderer>().enabled = false;
        ShieldImage.GetComponent<MeshRenderer>().enabled = false;
        MeleeImage.GetComponent<MeshRenderer>().enabled = false;
        FlagImage.GetComponent<MeshRenderer>().enabled = false;

        TeamAText.SetActive(true);
        MovementText.SetActive(false);
        ShootingText.SetActive(false);
        ShieldText.SetActive(false);
        MeleeText.SetActive(false);
        FlagText.SetActive(false);

        TeamAImage.GetComponent<MovieScript>().PlayMovie();
        ShootingImage.GetComponent<MovieScript>().StopMovie();
        MovementImage.GetComponent<MovieScript>().StopMovie();
        ShieldImage.GetComponent<MovieScript>().StopMovie();
        MeleeImage.GetComponent<MovieScript>().StopMovie();
        FlagImage.GetComponent<MovieScript>().StopMovie();
    }
}
