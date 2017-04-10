using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Rewired;
public class Player4Select : MonoBehaviour {
    /* Manager for the player 1 selection spot on the character select screen */
    // References to the characters on the panel, the panel itself, and the text
    public GameObject Urial, Barachial, Lilith, Azazel;
    public GameObject Player4Panel;
    public Text Player4Text, Player4Character;
    // 1 = Urial, 2 = Barachial, 3 = Lilith, 4 = Azazel
    public int curCharacter;
    public bool canSwap;
    public bool selected;
    public bool deselected;
    public bool canSelect;
    public GameObject AzazelVO;
    public GameObject UrialVO;
    public GameObject MasterSelector;
    public MasterSelector masterReference;
    // Colors for selection
    private Color Blue = new Vector4(0, 0.545f, 1, 0.392f);
    private Color Red = new Vector4(1, 0, 0, 0.392f);
    // The Rewired player id of this character and player
    public int playerId;
    private Player player;
    private Vector3 moveVector;
    public AudioClip selAudio;
    public AudioClip deSelAudio;
    public AudioClip MenuMoveAudio;
    // Setup for the character images, text, as well as panel color.
    void Start ()
    {
        Urial.GetComponent<Image>().enabled = false; Barachial.GetComponent<Image>().enabled = false; Lilith.GetComponent<Image>().enabled = false; Azazel.GetComponent<Image>().enabled = true;
        Player4Panel.GetComponent<Image>().color = new Vector4(124, 124, 124, 100);
        Player4Text.text = "Player Four";
        Player4Character.text = "Azazel";
        curCharacter = 4;
        canSwap = true;
        selected = false; deselected = true;
        // Get the Rewired Player object for this player and keep it for the duration of the character's lifetime
        player = ReInput.players.GetPlayer(playerId);
        canSelect = true;
        MasterSelector = GameObject.Find("CharacterMaster");
        masterReference = MasterSelector.GetComponent<MasterSelector>();
    }
    // Checking all inputs, and updates visual indicators based on what you are doing
    void Update ()
    {
        GetInput();
        ProcessInput();
        if (curCharacter == 1)
        {
            Urial.GetComponent<Image>().enabled = true; Barachial.GetComponent<Image>().enabled = false; Lilith.GetComponent<Image>().enabled = false; Azazel.GetComponent<Image>().enabled = false;
            Player4Character.text = "Urial";
        }
        else if (curCharacter == 2)
        {
            Urial.GetComponent<Image>().enabled = false; Barachial.GetComponent<Image>().enabled = true; Lilith.GetComponent<Image>().enabled = false; Azazel.GetComponent<Image>().enabled = false;
            Player4Character.text = "Barachial";
        }
        else if (curCharacter == 3)
        {
            Urial.GetComponent<Image>().enabled = false; Barachial.GetComponent<Image>().enabled = false; Lilith.GetComponent<Image>().enabled = true; Azazel.GetComponent<Image>().enabled = false;
            Player4Character.text = "Lilith";
        }
        else if (curCharacter == 4)
        {
            Urial.GetComponent<Image>().enabled = false; Barachial.GetComponent<Image>().enabled = false; Lilith.GetComponent<Image>().enabled = false; Azazel.GetComponent<Image>().enabled = true;
            Player4Character.text = "Azazel";
        }
    }
    // Get the input from the Rewired Player.
    private void GetInput()
    {
        // get input by name or action id
        moveVector.x = player.GetAxis("Move Horizontal");
        if (player.GetButtonDown("Select") && canSelect)
        {
            canSelect = false;
            selected = true;
            deselected = false;
            canSwap = false;
            Invoke("CanSelectAgain", 0.5f);
            GetComponent<AudioSource>().clip = selAudio;
            GetComponent<AudioSource>().PlayOneShot(selAudio);
        }
        if (player.GetButtonDown("Deselect") && canSelect)
        {
            canSelect = false;
            selected = false;
            deselected = true;
            canSwap = true;
            Invoke("CanSelectAgain", 0.5f);
            GetComponent<AudioSource>().clip = deSelAudio;
            GetComponent<AudioSource>().PlayOneShot(deSelAudio);
        }
    }
    // Process input from the Rewired Player
    private void ProcessInput()
    {
        // Change character for when they move right on the joystick.
        if (moveVector.x > 0 && canSwap)
        {
            if (curCharacter == 1)
            {
                curCharacter = 2;
                canSwap = false;
                Invoke("SwapReset", 0.25f);
                GetComponent<AudioSource>().PlayOneShot(MenuMoveAudio);
            }
            else if (curCharacter == 2)
            {
                curCharacter = 3;
                canSwap = false;
                Invoke("SwapReset", 0.25f);
                GetComponent<AudioSource>().PlayOneShot(MenuMoveAudio);
            }
            else if (curCharacter == 3)
            {
                curCharacter = 4;
                canSwap = false;
                Invoke("SwapReset", 0.25f);
                GetComponent<AudioSource>().PlayOneShot(MenuMoveAudio);
            }
            else if (curCharacter == 4)
            {
                curCharacter = 1;
                canSwap = false;
                Invoke("SwapReset", 0.25f);
                GetComponent<AudioSource>().PlayOneShot(MenuMoveAudio);
            }
        }
        // Change character for when they press left on the joystick.
        else if (moveVector.x < 0 && canSwap)
        {
            if (curCharacter == 1)
            {
                curCharacter = 4;
                canSwap = false;
                Invoke("SwapReset", 0.25f);
                GetComponent<AudioSource>().PlayOneShot(MenuMoveAudio);
            }
            else if (curCharacter == 2)
            {
                curCharacter = 1;
                canSwap = false;
                Invoke("SwapReset", 0.25f);
                GetComponent<AudioSource>().PlayOneShot(MenuMoveAudio);
            }
            else if (curCharacter == 3)
            {
                curCharacter = 2;
                canSwap = false;
                Invoke("SwapReset", 0.25f);
                GetComponent<AudioSource>().PlayOneShot(MenuMoveAudio);
            }
            else if (curCharacter == 4)
            {
                curCharacter = 3;
                canSwap = false;
                Invoke("SwapReset", 0.25f);
                GetComponent<AudioSource>().PlayOneShot(MenuMoveAudio);
            }
        }
        // When you select a character, indicate it visually and codewise.
        if (selected)
        {
            if (curCharacter == 1 && !masterReference.Urial && !masterReference.P1Urial && !masterReference.P2Urial && !masterReference.P3Urial && !masterReference.P4Urial)
            {
                MasterSelector.GetComponent<MasterSelector>().P4Urial = true;
                MasterSelector.GetComponent<MasterSelector>().Urial = true;
                Player4Panel.GetComponent<Image>().color = Blue;
                UrialVO.GetComponent<AudioSource>().Play();

                Player4Text.text = "Ready!";
            }
            else if (curCharacter == 2 && !masterReference.Barachial && !masterReference.P1Barachial && !masterReference.P2Barachial && !masterReference.P3Barachial && !masterReference.P4Barachial)
            {
                MasterSelector.GetComponent<MasterSelector>().Barachial = true;
                MasterSelector.GetComponent<MasterSelector>().P4Barachial = true;
                Player4Panel.GetComponent<Image>().color = Blue;
                Player4Text.text = "Ready!";
            }
            else if (curCharacter == 3 && !masterReference.Lilith && !masterReference.P1Lilith && !masterReference.P2Lilith && !masterReference.P3Lilith && !masterReference.P4Lilith)
            {
                MasterSelector.GetComponent<MasterSelector>().Lilith = true;
                MasterSelector.GetComponent<MasterSelector>().P4Lilith = true;
                Player4Panel.GetComponent<Image>().color = Red;
                Player4Text.text = "Ready!";
            }
            else if (curCharacter == 4 && !masterReference.Azazel && !masterReference.P1Azazel && !masterReference.P2Azazel && !masterReference.P3Azazel && !masterReference.P4Azazel)
            {
                MasterSelector.GetComponent<MasterSelector>().Azazel = true;
                MasterSelector.GetComponent<MasterSelector>().P4Azazel = true;
                Player4Panel.GetComponent<Image>().color = Red;
                AzazelVO.GetComponent<AudioSource>().Play();
                Player4Text.text = "Ready!";
            }
            else if (!MasterSelector.GetComponent<MasterSelector>().P4Urial && !MasterSelector.GetComponent<MasterSelector>().P4Barachial && !MasterSelector.GetComponent<MasterSelector>().P4Lilith && !MasterSelector.GetComponent<MasterSelector>().P4Azazel)
            {
                selected = false;
                deselected = true;
                canSwap = true;
            }
        }
        // When you deselect a character, indicate it visually and codewise.
        if (deselected)
        {
            Player4Panel.GetComponent<Image>().color = new Vector4(124, 124, 124, 100);
            Player4Text.text = "Player Four";
            if (curCharacter == 1)
            {
                MasterSelector.GetComponent<MasterSelector>().P4Urial = false;
                MasterSelector.GetComponent<MasterSelector>().Urial = false;
            }
            if (curCharacter == 2)
            {
                MasterSelector.GetComponent<MasterSelector>().P4Barachial = false;
                MasterSelector.GetComponent<MasterSelector>().Barachial = false;
            }
            if (curCharacter == 3)
            {
                MasterSelector.GetComponent<MasterSelector>().P4Lilith = false;
                MasterSelector.GetComponent<MasterSelector>().Lilith = false;
            }
            if (curCharacter == 4)
            {
                MasterSelector.GetComponent<MasterSelector>().P4Azazel = false;
                MasterSelector.GetComponent<MasterSelector>().Azazel = false;
            }
        }
    }
    // Resetting the swap for selections
    private void SwapReset()
    {
        if (deselected == true)
            canSwap = true;
    }
    // Resetting the select function
    void CanSelectAgain()
    {
        canSelect = true;
    }
}
