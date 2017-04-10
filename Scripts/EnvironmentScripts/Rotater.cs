using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Rotater : MonoBehaviour
{
    private Vector3 RotationPerSecond;
    private int zRotation = 45;

    void Start()
    {
        int randNum = Random.Range(0, 2);
        if (randNum == 0)
            zRotation = 45;
        else if (randNum == 1)
            zRotation = -45;
        InvokeRepeating("SwapRotation", 30f, 30f);
    }

    void Update()
    {
        RotationPerSecond = new Vector3(0, 0, zRotation);
        transform.Rotate(RotationPerSecond * Time.deltaTime);
    }

    void SwapRotation()
    {
        zRotation = -zRotation;
    }
}