using UnityEngine;
using System.Collections;

public class Dart : MonoBehaviour {

	private int wrapCounter = 0;
    bool isFacingRight;


	// Update is called once per frame
	void Update () 
	{/*
        isFacingRight = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().isFacingRight;

        if(!isFacingRight)
        {
            Flip();
        }*/

		if(wrapCounter == 2)
		{
			Destroy(gameObject);
		}
    }	    

    public void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 nextScale = transform.localScale;
        nextScale.x *= -1f;
        transform.localScale = nextScale;
    }  


	void OnBecameInvisible()
	{
		wrapCounter++;
	}
}
