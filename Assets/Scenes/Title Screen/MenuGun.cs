using UnityEngine;
using System.Collections;

public class MenuGun : MonoBehaviour 
{
	private Vector3 mousePosition;
	public float angle;
	public bool isFacingRight;
	private Vector3 mouseDirection;


	void Update () 
	{
		Vector3 position = Camera.main.WorldToScreenPoint(transform.position);							//Get the gun's position as a 2D screen position
		mouseDirection = Input.mousePosition - position;										//Get the direction the mouse is pointing relative to the gun
																										//Flip gun based on which direction the player is facing
		UpdateRotation();

	}

	void UpdateRotation()
	{  	
		angle = Mathf.Atan2(mouseDirection.y, mouseDirection.x) * Mathf.Rad2Deg;		//Get the angle of the cursor relative to the gun
		transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);						//Rotate the gun
	}

	float Orient(float value)
	{
		return isFacingRight ? -value : value;
	}
}
