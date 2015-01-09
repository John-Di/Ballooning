using UnityEngine;
using System.Collections;

public class Wrappable : MonoBehaviour 
{
    float screenTop, screenRight;
    Vector3 zero;

	// Use this for initialization
	void Start () 
	{
        screenTop = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height, 0)).y;
        screenRight = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)).x;
        zero = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0));
	}

    void Update()
    {
        float x = this.transform.position.x,
              y = this.transform.position.y, 
              z = this.transform.position.z;

        if(transform.position.x > screenRight + renderer.bounds.size.x)
        {
            x = zero.x - renderer.bounds.size.x;

        }
        else if(transform.position.x < zero.x - renderer.bounds.size.x)
        {
            x = screenRight + renderer.bounds.size.x/2;
        }
        
        if(transform.position.y > screenTop + renderer.bounds.size.y)
        {
            y = zero.y - renderer.bounds.size.y/2;
        }
        else if(transform.position.y < zero.y - renderer.bounds.size.y)
        {
            y = screenTop + renderer.bounds.size.y/2;            
        }

        this.transform.position = new Vector3(x, y, z);
    }
    /*
    void OnBecameInvisible()
    {
        if(Mathf.Abs(this.transform.position.x) > screenRight)
        {
            this.transform.position = new Vector3(-(this.transform.position.x), this.transform.position.y, this.transform.position.z);
        }
        
        if(Mathf.Abs(this.transform.position.y) > screenTop)
        {
            this.transform.position = new Vector3(this.transform.position.x, -(this.transform.position.y), this.transform.position.z);
        }
	}*/
}
