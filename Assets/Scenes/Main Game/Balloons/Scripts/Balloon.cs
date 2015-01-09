using UnityEngine;
using System.Collections;

public class Balloon : MonoBehaviour 
{		
	Animator animator;
	//private int wrapCounter = 0;
	public Cluster center;
	float velocityMultiplier = 1.0f;
    public Vector2 velocity;
    public int count;
    public int indexInCluster;

    public AudioSource pop;

    void Start()
    {
        count = center.getSize();
        indexInCluster = center.balloons.IndexOf(gameObject.GetComponent<Balloon>());

        animator = GetComponent<Animator>(); 
        
        velocity = new Vector2(center.rigidbody2D.velocity.x * velocityMultiplier, center.rigidbody2D.velocity.y * velocityMultiplier);

        this.rigidbody2D.velocity = new Vector2(velocity.x, velocity.y);

        pop = GetComponent<AudioSource>();
    }

	// Update is called once per frame
	void Update () 
    {      
        count = center.getSize();

        float fractionUnpopped = (float)BalloonSpawner.currentBalloons/BalloonSpawner.totalBalloons;
        int percentUnpopped = (Mathf.CeilToInt(fractionUnpopped * 10))*10;

        if(percentUnpopped <= 20.0f)
        {            
            velocityMultiplier = 3.0f;
        }
        else if(count == 1)
        {
            velocityMultiplier = 1.5f;
        }  

        if(!animator.GetBool("isPopped"))
        {
            rigidbody2D.velocity = new Vector2(velocity.x * velocityMultiplier, velocity.y * velocityMultiplier);	
            center.rigidbody2D.velocity = rigidbody2D.velocity;
        }
        else 
        {
            center.rigidbody2D.velocity = new Vector2(0,0);
        }
    }
	
	public void SetCenter(Cluster c)
	{
        this.center = c;       
	}

    public void cutFromCluster()
    {
        transform.parent = null;
    }

	void OnCollisionEnter2D(Collision2D coll) 
	{
		if (coll.gameObject.tag == "PlayerDart")
        {		
            Player p = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

           // if()
			//{		
                center.CutBalloonsFromCluster();
                //cutFromCluster();
			//}
            //else 
            if(count <= 1)
            {   
                Cluster.zPos++;
                renderer.sortingOrder = Cluster.zPos++;
                animator.SetBool("isPopped", true);
                center.rigidbody2D.velocity = new Vector2(0,0);
                Destroy (this.rigidbody2D);
                Destroy (this.GetComponent<BoxCollider2D>());
                Destroy (this.GetComponent<CircleCollider2D>());
                p.updateScore(1);
                pop.Play();
			}				

			Destroy (coll.gameObject);
		}  
	}
	
	void Die()
	{		
        Destroy (gameObject);
        Destroy (center.gameObject);
	}

    void OnDestroy()
    {        
        BalloonSpawner.currentBalloons--;
    }

}
