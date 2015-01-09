using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour 
{
    Animator animator;
	private Vector3 mousePosition;
	public float moveSpeed = 0.1f;
	public float angle;
	public bool isFacingRight;	
	float shootingTime = 0.0f, backwardsShootTime = 5.0f;
	public Rigidbody2D dart;
    private Vector3 mouseDirection;
    bool specialMode = false, specialModeIsUsedUp = false;

	void Start () 
	{
        animator = GetComponent<Animator>(); 
	}

	void Update () 
	{
		Vector3 position = Camera.main.WorldToScreenPoint(transform.position);							//Get the gun's position as a 2D screen position
		mouseDirection = Input.mousePosition - position;										//Get the direction the mouse is pointing relative to the gun
																										//Flip gun based on which direction the player is facing
		UpdateRotation();

        Player p = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        if(!p.GetComponent<Animator>().GetBool("isHit") && p.extraLives >= 0)
        {
            UpdateShooting();
        }
	}

	void UpdateRotation()
	{  	
		Player p = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();		
		isFacingRight = p.isFacingRight;		
		angle = Mathf.Atan2(Orient(-mouseDirection.y), Orient(-mouseDirection.x)) * Mathf.Rad2Deg;		//Get the angle of the cursor relative to the gun
		transform.rotation = Quaternion.AngleAxis(Orient(-angle), Vector3.forward);						//Rotate the gun
        //Quaternion dartRotation = Quaternion.AngleAxis(angle, Vector3.forward);
	}

	void UpdateShooting()
    {		       
        if (Input.GetKeyDown(KeyCode.M) && !specialModeIsUsedUp)
        {
            specialModeIsUsedUp = !specialModeIsUsedUp;
            ToggleSpecialMode();
        }
        else if(backwardsShootTime <= 0)
        {
            ToggleSpecialMode();
            backwardsShootTime = 0.3f;
        }

        if(animator.GetBool("isSpecialMode") && backwardsShootTime > 0)
        {        
            backwardsShootTime = Mathf.Max(0, backwardsShootTime - Time.deltaTime);
        }

		shootingTime = Mathf.Max(0, shootingTime - Time.deltaTime);															//Prevent dart spammingbackwardsShootTime


		if (Input.GetButton("Fire1") && shootingTime == 0)
		{
			shootingTime = 0.35f;																											//Shoot Delay between darts
			//Vector2 offset = new Vector2(, transform.forward.y * mouseDirection.y);					//Dart position offset
			
			float dartSpeed = 500f;																											//Dart Speed
			Vector2 forward = mouseDirection.normalized * dartSpeed;																		//Forward force to propel dart
			Vector2 dartPosition = new Vector2(transform.position.x, transform.position.y);											//Dart position
            Rigidbody2D dartObject = (Rigidbody2D)Instantiate(dart, dartPosition, Quaternion.AngleAxis(reverseAngle(angle), Vector3.forward));	//Create dart
            dartObject.AddForce(forward);
			
            if(animator.GetBool("isSpecialMode") && backwardsShootTime > 0)
            {        
                Vector2 backward = new Vector2(-forward.x, -forward.y);                                                               
                Rigidbody2D reverseDartObject = (Rigidbody2D)Instantiate(dart, dartPosition, Quaternion.AngleAxis(reverseAngle(180.0f + angle), Vector3.forward));  //Create dart
                //reverseDartObject.GetComponent<Dart>().Flip();
                reverseDartObject.AddForce(backward);
            }
		}
	}

    void ToggleSpecialMode()
    {
        specialMode = !specialMode;
        animator.SetBool("isSpecialMode", specialMode);
        GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().GetComponent<Animator>().SetBool("isSpecialMode", specialMode);

    }

	float Orient(float value)
	{
		return isFacingRight ? -value : value;
	}

    float reverseAngle(float value)
    {
        return isFacingRight ? value : value + 180.0f;
    }
}
