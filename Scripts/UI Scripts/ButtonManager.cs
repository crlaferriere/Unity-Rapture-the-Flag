using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class ButtonManager : MonoBehaviour {
    // Scene 0 - Title Screen
    // Scene 1 - How To Play
    // Scene 2 - Credits
    // Scene 3 - Mode Select
    // Scene 4 - CTF Character Select
    // Scene 5 - FFA Character Select
    // Scene 6 - CTF Map Select
    // Scene 7 - FFA Map Select
    // Scene 8 - CTF Map 1
    // Scene 9 - FFA Map 1
    // Scene 10 - Angels Win
    // Scene 11 - Demons Win
    // Scene 12 - Draw
    // Scene 13 - Random gameplay videos
    // Scene 14 - Urial Wins
    // Scene 15 - Barachial Wins
    // Scene 16 - Lilith Wins
    // Scene 17 - Azazel Wins

    public Image black;
    public Animator anim;

    // Standard button presses
    public IEnumerator TitleScreenButtonPress()
    {
        anim.SetBool("Fade", true);
        yield return new WaitUntil(()=> black.color.a==1);
        SceneManager.LoadScene(0);
    }
    public void TitleScreenButtonWrapper()
    {
        StartCoroutine(TitleScreenButtonPress());
    }

    public IEnumerator HowToPlayButtonPress()
    {
        anim.SetBool("Fade", true);
        yield return new WaitUntil(() => black.color.a == 1);
        SceneManager.LoadScene(1);
    }
    public void HowToPlayButtonWrapper()
    {
        StartCoroutine(HowToPlayButtonPress());
    }

    public IEnumerator CreditsButtonPress()
    {
        anim.SetBool("Fade", true);
        yield return new WaitUntil(() => black.color.a == 1);
        SceneManager.LoadScene(2);
    }
    public void CreditsButtonWrapper()
    {
        StartCoroutine(CreditsButtonPress());
    }

    public IEnumerator PlayButtonPress()
    {
        anim.SetBool("Fade", true);
        yield return new WaitUntil(() => black.color.a == 1);
        SceneManager.LoadScene(3);
	}
    public void PlayButtonWrapper()
    {
        StartCoroutine(PlayButtonPress());
    }

    // Loading the CTF character select/maps
    public IEnumerator CTFButtonPress()
    {
        anim.SetBool("Fade", true);
        yield return new WaitUntil(() => black.color.a == 1);
        SceneManager.LoadScene(4);
    }
    public void CTFButtonWrapper()
    {
        StartCoroutine(CTFButtonPress());
    }

    public IEnumerator CTFMap1ButtonPress()
    {
        anim.SetBool("Fade", true);
        yield return new WaitUntil(() => black.color.a == 1);
        SceneManager.LoadScene(8);
    }
    public void CTFMap1ButtonWrapper()
    {
        StartCoroutine(CTFMap1ButtonPress());
    }

    public IEnumerator CTFMap2ButtonPress()
    {
        anim.SetBool("Fade", true);
        yield return new WaitUntil(() => black.color.a == 1);
        SceneManager.LoadScene(9);
    }
    public void CTFMap2ButtonWrapper()
    {
        StartCoroutine(CTFMap2ButtonPress());
    }

    public IEnumerator CTFMap3ButtonPress()
    {
        anim.SetBool("Fade", true);
        yield return new WaitUntil(() => black.color.a == 1);
        SceneManager.LoadScene(10);
    }
    public void CTFMap3ButtonWrapper()
    {
        StartCoroutine(CTFMap3ButtonPress());
    }

    // Loading the FFA character select/maps
    public IEnumerator FFAButtonPress()
    {
        anim.SetBool("Fade", true);
        yield return new WaitUntil(() => black.color.a == 1);
        SceneManager.LoadScene(5);
    }
    public void FFAButtonWrapper()
    {
        StartCoroutine(FFAButtonPress());
    }

    public IEnumerator FFAMap1ButtonPress()
    {
        anim.SetBool("Fade", true);
        yield return new WaitUntil(() => black.color.a == 1);
        SceneManager.LoadScene(9);
    }
    public void FFAMap1ButtonWrapper()
    {
        StartCoroutine(FFAMap1ButtonPress());
    }

    public IEnumerator FFAMap2ButtonPress()
    {
        anim.SetBool("Fade", true);
        yield return new WaitUntil(() => black.color.a == 1);
        SceneManager.LoadScene(12);
    }
    public void FFAMap2ButtonWrapper()
    {
        StartCoroutine(FFAMap2ButtonPress());
    }

    public IEnumerator FFAMap3ButtonPress()
    {
        anim.SetBool("Fade", true);
        yield return new WaitUntil(() => black.color.a == 1);
        SceneManager.LoadScene(13);
    }
    public void FFAMap3ButtonWrapper()
    {
        StartCoroutine(FFAMap3ButtonPress());
    }
}