using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuBackgroundScript : MonoBehaviour {

    public GameObject menuBackground;

    void Start()
    {
        if (this.gameObject.CompareTag("Menu1"))
            this.gameObject.name = "MenuBackground1";
        else if (this.gameObject.CompareTag("Menu2"))
            this.gameObject.name = "MenuBackground2";
        else if (this.gameObject.CompareTag("Menu3"))
            this.gameObject.name = "MenuBackground3";
    }

	void Update ()
    {
        transform.position = new Vector2(transform.position.x - 0.01f, transform.position.y - 0.01f);

        if (transform.position.y < -10)
        {
            if (this.gameObject.CompareTag("Menu1"))
                Instantiate(menuBackground, new Vector2(10, 10), Quaternion.identity);
            else if (this.gameObject.CompareTag("Menu2"))
                Instantiate(menuBackground, new Vector2(-7.8f, 10.3f), Quaternion.identity);
            else if (this.gameObject.CompareTag("Menu3"))
                Instantiate(menuBackground, new Vector2(27.8f, 9.7f), Quaternion.identity); 
            Destroy(gameObject);
        }
	}
}
