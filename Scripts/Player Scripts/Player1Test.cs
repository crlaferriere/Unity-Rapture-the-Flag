// MyCharacter.cs - A simple example showing how to get input from Rewired.Player

using UnityEngine;
using System.Collections;
using Rewired;

//[RequireComponent(typeof(CharacterController))]
public class Player1Test : MonoBehaviour
{
    // Player settings
    public int playerId = 0; // The Rewired player id of this character
    public float moveSpeed;
    private Player player; // The Rewired Player
    //private CharacterController cc;
    private Vector3 moveVector;
    private float xSpeed, ySpeed;
    private bool fire;
    
    // Bullet settings
    public float bulletSpeed;
    public GameObject bulletPrefab, bulletSpawner;

    void Awake()
    {
        // Get the Rewired Player object for this player and keep it for the duration of the character's lifetime
        player = ReInput.players.GetPlayer(playerId);

        // Get the character controller
        //cc = GetComponent<CharacterController>();
    }

    void Update()
    {
        GetInput();
        ProcessInput();
    }

    private void GetInput()
    {
        // Get the input from the Rewired Player. All controllers that the Player owns will contribute, so it doesn't matter
        // whether the input is coming from a joystick, the keyboard, mouse, or a custom controller.

        moveVector.x = player.GetAxis("Move Horizontal"); // get input by name or action id
        moveVector.y = player.GetAxis("Move Vertical");
        xSpeed = player.GetAxisRaw("Rotate Horizontal");
        ySpeed = player.GetAxisRaw("Rotate Vertical");
        fire = player.GetButtonDown("Fire");
    }

    private void ProcessInput()
    {
        // Process movement
        GetComponent<Rigidbody2D>().velocity = new Vector2(moveVector.x, moveVector.y) * moveSpeed;

        // Process rotation
        if ((xSpeed != 0 || ySpeed != 0))
        {
            gameObject.transform.localRotation = Quaternion.Euler(0.0f, 0.0f, (Mathf.Atan2(xSpeed, ySpeed) * Mathf.Rad2Deg));
        }


        // Process fire
        if (fire)
        {
            GameObject bullet = (Instantiate(bulletPrefab, new Vector3(bulletSpawner.transform.position.x, bulletSpawner.transform.position.y, -1.5f), bulletSpawner.transform.rotation)) as GameObject;
            bullet.GetComponent<Rigidbody2D>().velocity = transform.forward * bulletSpeed;
        }
    }
}