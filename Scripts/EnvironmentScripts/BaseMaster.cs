using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BaseMaster : MonoBehaviour {

    public GameObject urialBase, barachialBase, lilithBase, azazelBase;
    public SpriteRenderer urialBSprite, barachialBSprite, lilithBSprite, azazelBSprite;
    public Animator urialBAnim, barachialBAnim, lilithBAnim, azazelBAnim;
    public Vector3 pos1, pos2, pos3, pos4;
	void Start ()
    {
        StartCoroutine("BaseSwap");
	}

    IEnumerator BaseSwap()
    {
        // Start the fade for all four bases
        urialBAnim.SetBool("FadeStart", true);
        barachialBAnim.SetBool("FadeStart", true);
        lilithBAnim.SetBool("FadeStart", true);
        azazelBAnim.SetBool("FadeStart", true);
        // Wait until azazel's base is fully transparent.
        yield return new WaitUntil(() => azazelBSprite.color.a == 0);
        // Switch statement to teleport the bases to new positions
        int randCase = Random.Range(0, 23);
        switch (randCase)
        {
            case 0:
                urialBase.transform.position = pos1; barachialBase.transform.position = pos2; lilithBase.transform.position = pos3; azazelBase.transform.position = pos4;
                break;
            case 1:
                urialBase.transform.position = pos1; barachialBase.transform.position = pos2; lilithBase.transform.position = pos4; azazelBase.transform.position = pos3;
                break;
            case 2:
                urialBase.transform.position = pos1; barachialBase.transform.position = pos3; lilithBase.transform.position = pos2; azazelBase.transform.position = pos4;
                break;
            case 3:
                urialBase.transform.position = pos1; barachialBase.transform.position = pos3; lilithBase.transform.position = pos4; azazelBase.transform.position = pos2;
                break;
            case 4:
                urialBase.transform.position = pos1; barachialBase.transform.position = pos4; lilithBase.transform.position = pos2; azazelBase.transform.position = pos3;
                break;
            case 5:
                urialBase.transform.position = pos1; barachialBase.transform.position = pos4; lilithBase.transform.position = pos3; azazelBase.transform.position = pos2;
                break;
            case 6:
                urialBase.transform.position = pos2; barachialBase.transform.position = pos1; lilithBase.transform.position = pos3; azazelBase.transform.position = pos4;
                break;
            case 7:
                urialBase.transform.position = pos2; barachialBase.transform.position = pos1; lilithBase.transform.position = pos4; azazelBase.transform.position = pos3;
                break;
            case 8:
                urialBase.transform.position = pos2; barachialBase.transform.position = pos3; lilithBase.transform.position = pos1; azazelBase.transform.position = pos4;
                break;
            case 9:
                urialBase.transform.position = pos2; barachialBase.transform.position = pos3; lilithBase.transform.position = pos4; azazelBase.transform.position = pos1;
                break;
            case 10:
                urialBase.transform.position = pos2; barachialBase.transform.position = pos4; lilithBase.transform.position = pos1; azazelBase.transform.position = pos3;
                break;
            case 11:
                urialBase.transform.position = pos2; barachialBase.transform.position = pos4; lilithBase.transform.position = pos3; azazelBase.transform.position = pos1;
                break;
            case 12:
                urialBase.transform.position = pos3; barachialBase.transform.position = pos1; lilithBase.transform.position = pos2; azazelBase.transform.position = pos4;
                break;
            case 13:
                urialBase.transform.position = pos3; barachialBase.transform.position = pos1; lilithBase.transform.position = pos4; azazelBase.transform.position = pos2;
                break;
            case 14:
                urialBase.transform.position = pos3; barachialBase.transform.position = pos2; lilithBase.transform.position = pos1; azazelBase.transform.position = pos4;
                break;
            case 15:
                urialBase.transform.position = pos3; barachialBase.transform.position = pos2; lilithBase.transform.position = pos4; azazelBase.transform.position = pos1;
                break;
            case 16:
                urialBase.transform.position = pos3; barachialBase.transform.position = pos1; lilithBase.transform.position = pos4; azazelBase.transform.position = pos2;
                break;
            case 17:
                urialBase.transform.position = pos3; barachialBase.transform.position = pos4; lilithBase.transform.position = pos2; azazelBase.transform.position = pos1;
                break;
            case 18:
                urialBase.transform.position = pos4; barachialBase.transform.position = pos1; lilithBase.transform.position = pos2; azazelBase.transform.position = pos3;
                break;
            case 19:
                urialBase.transform.position = pos4; barachialBase.transform.position = pos1; lilithBase.transform.position = pos3; azazelBase.transform.position = pos2;
                break;
            case 20:
                urialBase.transform.position = pos4; barachialBase.transform.position = pos2; lilithBase.transform.position = pos1; azazelBase.transform.position = pos3;
                break;
            case 21:
                urialBase.transform.position = pos4; barachialBase.transform.position = pos2; lilithBase.transform.position = pos3; azazelBase.transform.position = pos1;
                break;
            case 22:
                urialBase.transform.position = pos4; barachialBase.transform.position = pos3; lilithBase.transform.position = pos1; azazelBase.transform.position = pos2;
                break;
            case 23:
                urialBase.transform.position = pos4; barachialBase.transform.position = pos3; lilithBase.transform.position = pos2; azazelBase.transform.position = pos1;
                break;
        }
        // Start the fade in for the four bases
        urialBAnim.SetBool("FadeStart", false);
        barachialBAnim.SetBool("FadeStart", false);
        lilithBAnim.SetBool("FadeStart", false);
        azazelBAnim.SetBool("FadeStart", false);
        // Wait until azazel's base is fully transparent.
        yield return new WaitForSeconds(30f);
        StartCoroutine("BaseSwap");
    }
}
