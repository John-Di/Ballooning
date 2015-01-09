using UnityEngine;
using System.Collections;

public class FadeOut : MonoBehaviour 
{
    float fadeOutAlpha = 0.0f;
    
    SpriteRenderer sr;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }
	
	// Update is called once per frame
	void Update () 
    {
        if(GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().extraLives < 0 &&
           GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().GetBool("isInvincible"))
        {
            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, fadeOutAlpha);
            fadeOutAlpha += 0.005f;

            if(fadeOutAlpha >= 1.0f)
            {
                Application.LoadLevel("Game Over");
            }
        }
	}
}
