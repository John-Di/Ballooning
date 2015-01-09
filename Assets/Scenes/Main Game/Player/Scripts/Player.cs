using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour 
{   
    Animator animator;

	private float PlayerSpeed = 1.75f;
    public bool isFacingRight, isInvincible;

    Gun gun;
    Animator gunAnimator;

    public float fallTime = 0.0f, respawnInvincibiltyTime = 0.0f;
    public int extraLives;

    Score score;

    LifeCounter lifebar;

    public AudioSource falling;


	// Use this for initialization
	void Start () 
    {        
        animator = GetComponent<Animator>(); 
        gun = GameObject.FindGameObjectWithTag("Gun").GetComponent<Gun>();
        gunAnimator = gun.GetComponent<Animator>();
		//score = 0;
		isFacingRight = true;
        lifebar = GameObject.FindGameObjectWithTag("LifeBar").GetComponent<LifeCounter>();
        score = GameObject.FindGameObjectWithTag("Score").GetComponent<Score>();
 
        EnterInvincibility();

        respawnInvincibiltyTime = 2.5f;
        animator.SetBool("isInvincible", true);
        gunAnimator.SetBool("isHit", true);

        extraLives = 2;
	}
	
	// Update is called once per frame
	void Update () 
    {

        UpdateInvincibility();

		float horizontal = Input.GetAxis("Horizontal") * PlayerSpeed;
		float vertical = Input.GetAxis("Vertical") * PlayerSpeed;

        if(fallTime <= 0.0f && extraLives >= 0)
        {
    		rigidbody2D.AddForce(new Vector2(horizontal, vertical));

    		if ((Input.GetAxis("Horizontal") > 0 && !isFacingRight) || (Input.GetAxis("Horizontal") < 0 && isFacingRight))
    		{
    			Flip();
    		}
        }
	}

    void UpdateInvincibility()
    {
        if (isInvincible && transform.position.y < -6)
        {
            fallTime = Mathf.Max(0, fallTime - Time.deltaTime);

            if(fallTime <= 0.0f)
            {
                gameObject.rigidbody2D.velocity = Vector2.zero;
                gameObject.transform.position = Vector2.zero;
                
                respawnInvincibiltyTime = 2.5f;
                fallTime = 0.0f;

                animator.SetBool("isHit", false);
                animator.SetBool("isInvincible", true);
                falling.Stop();
            } 

            if(extraLives < 0)
            {
                gameObject.renderer.sortingLayerID = 4;
                gun.gameObject.renderer.sortingLayerID = 4;
            }
        }

        if(isInvincible && respawnInvincibiltyTime > 0.0f)
        {
            respawnInvincibiltyTime = Mathf.Max(0, respawnInvincibiltyTime - Time.deltaTime);

            if(respawnInvincibiltyTime <= 0.0f && extraLives >= 0)
            {                
                this.collider2D.enabled = true;
                isInvincible = false;

                respawnInvincibiltyTime = 0.0f;
                
                gameObject.AddComponent<Wrappable>();
                animator.SetBool("isInvincible", false);
                gunAnimator.SetBool("isHit", false);
            }
        }

    }

    void EnterInvincibility()
    {        
        this.collider2D.enabled = false;
        isInvincible = true;
        Destroy(gameObject.GetComponent<Wrappable>());
    }

    void OnCollisionEnter2D(Collision2D coll) 
    {        
        if ((coll.gameObject.tag == "Balloon" || coll.gameObject.tag == "WaterBalloon" || coll.gameObject.tag == "Enemy") && !isInvincible)
        {   
            EnterInvincibility();
            animator.SetBool("isHit", true);
            gunAnimator.SetBool("isHit", true);
            fallTime = 1.5f;
            falling.Play();
            Vector2 fall = new Vector2(0.0f, -4.0f);
            gameObject.rigidbody2D.velocity = fall;
            lifebar.DecrementLife();
            extraLives--;            
            
            if(coll.gameObject.tag == "Balloon")
            {
                Balloon b = coll.gameObject.GetComponent<Balloon>();
                b.pop.Play();
                coll.gameObject.GetComponent<Balloon>().center.balloons.Remove(coll.gameObject.GetComponent<Balloon>());
            }

            if(coll.gameObject.tag == "WaterBalloon")
            {
                WaterBalloon b = coll.gameObject.GetComponent<WaterBalloon>();
                Instantiate(b.deathParticle, b.transform.position, Quaternion.identity);
            }

            if(coll.gameObject.tag != "Enemy")
            {
                Destroy(coll.gameObject);
            }
        }
    }

    
    public void updateScore(int points)
    {
        score.updateScore(points);
    }
    
    float Orient(float value)
    {
        return isFacingRight ? value : -value;
    }
    
    void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 nextScale = transform.localScale;
        nextScale.x *= -1f;
        transform.localScale = nextScale;
    }  
}
