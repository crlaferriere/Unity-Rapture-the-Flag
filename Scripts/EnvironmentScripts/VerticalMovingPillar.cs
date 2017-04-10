using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalMovingPillar : MonoBehaviour {
    /* Script for the pillar on Arena 1 that moves up and down */
    // References to Rigidbody, bools, and ints managins the speed and how high or low it goes.
    Rigidbody2D rb;
    private bool goUp;
    public int range;
    public float moveSpeed;
    private int RandomizeMovement;
    // Set the rigidbody, and pick a direction to move the platform
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        RandomizeMovement = Random.Range(0, 2);
        if (RandomizeMovement == 0)
            goUp = true;
        else if (RandomizeMovement == 1)
            goUp = false;
    }
    // Move the object up or down based on its position
    void Update()
    {
        if (goUp)
            rb.velocity = new Vector2(0, 1) * moveSpeed;
        else if (!goUp)
            rb.velocity = new Vector2(0, 1) * -moveSpeed;

        if (transform.position.y <= -range)
            goUp = true;
        if (transform.position.y >= range)
            goUp = false;
    }
}
