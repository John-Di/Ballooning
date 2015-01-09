using UnityEngine;
using System.Collections;

public class MenuPlayer : MonoBehaviour 
{   
    Animator animator;

    bool specialMode = false;

    MenuGun gun;
    Animator gunAnimator;
    public int hoverSpeed;


	// Use this for initialization
	void Start () 
    {        
        animator = GetComponent<Animator>(); 
        gun = GameObject.FindGameObjectWithTag("Gun").GetComponent<MenuGun>();
        gunAnimator = gun.GetComponent<Animator>();
		
	}
	
	// Update is called once per frame
	void Update () 
    {       
        if (Input.GetKeyDown(KeyCode.M))
        {
            specialMode = !specialMode;
            animator.SetBool("isSpecialMode", specialMode);
            gunAnimator.SetBool("isSpecialMode", specialMode);
        }

        transform.position = new Vector2(transform.position.x, Mathf.Cos(5*Time.timeSinceLevelLoad)/hoverSpeed + transform.position.y);
	}

}
