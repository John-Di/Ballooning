using UnityEngine;
using System.Collections;

public class Cloud : MonoBehaviour 
{
    public Vector2 velocity;

	// Use this for initialization
	void Start () 
    {
        rigidbody2D.velocity = velocity;
	}
}
