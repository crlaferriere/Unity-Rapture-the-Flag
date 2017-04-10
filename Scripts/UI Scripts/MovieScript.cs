using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(AudioSource))]
public class MovieScript : MonoBehaviour{
    /* Plays movies for our how to play screen. */
    // Reference to the movie texture and it's mesh renderer
    public MovieTexture movieclips;
    private MeshRenderer meshRenderer;
    // Set the mesh renderer and texture, and plays any audio that matches the movie clip. Begins playing the clip and loops it.
    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.material.mainTexture = movieclips;
        AudioSource audio = GetComponent<AudioSource>();
        audio.Play();
        //movieclips.Play();
        movieclips.loop = true;
    }
    // Stops the movie
    public void StopMovie()
    {
        movieclips.Stop();
    }
    // Begins playing the movie
    public void PlayMovie()
    {
        movieclips.Play();
    }
}
