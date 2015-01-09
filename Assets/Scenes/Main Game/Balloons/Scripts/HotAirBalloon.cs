using UnityEngine;
using System.Collections;

public class HotAirBalloon : MonoBehaviour 
{
	Animator animator;
    public static float lastPercentageSpawned = 0.0f;
    public GameObject waterBalloon;
    int lastShot = 0;
    public static bool isSpawned = false;
    float speed = 2.0f;

    public AudioSource pop, travel;
	
	void Awake()
	{
		animator = GetComponent<Animator>();
        isSpawned = true;
        
        pop = GetComponent<AudioSource>();
	}
	
	void Update()
	{
		if(!animator.GetBool("isPopped"))
		{
            rigidbody2D.velocity = new Vector2(speed,0);
		}		

        if(GameObject.FindGameObjectWithTag("Player") != null && (int)this.transform.position.x % 4 == 0 && lastShot != (int)this.transform.position.x)
        {
            lastShot = (int)this.transform.position.x;
            Shoot();
        }

        float fractionUnpopped = (float)BalloonSpawner.currentBalloons/BalloonSpawner.totalBalloons;
        int percentUnpopped = (Mathf.CeilToInt(fractionUnpopped * 10))*10;
        
        if(percentUnpopped <= 20.0f && !animator.GetBool("isPopped"))
        {            
            speed = 3.0f;
        }

	}

	void OnCollisionEnter2D(Collision2D coll) 
	{
		if (coll.gameObject.tag == "PlayerDart")
		{			
			Player p = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
			p.updateScore(10);
			animator.SetBool("isPopped", true);
			Destroy (this.rigidbody2D);
			Destroy (this.collider2D);
			Destroy (coll.gameObject);
            pop.Play();
		}		
	}

    void Shoot()
    {        
        Player p = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        var wB = (GameObject)Instantiate(waterBalloon, this.transform.position, Quaternion.identity);
        wB.rigidbody2D.velocity = (p.transform.position - transform.position).normalized * speed * 1.5f;
    }
	
	void OnBecameInvisible()
	{
		Destroy(this.gameObject);
	}
	
	void Die()
	{		
		Destroy (this.gameObject);
	}

    void OnDestroy()
    {        
        isSpawned = false;
        travel.Stop();
    }
}
