using UnityEngine;
using System.Collections;

public class Pumpkin : MonoBehaviour 
{
    public static float lastPercentageSpawned = 0.0f;
    public GameObject candy;
    int lastShot = 0;
    public static bool isSpawned = false;
    float speed = 2.0f;

    int extralives = 5;
    
    public AudioSource pop, travel;
    
    void Awake()
    {
    
        isSpawned = true;
        
        pop = GetComponent<AudioSource>();
        rigidbody2D.velocity = new Vector2(speed,0);
    }
    
    void Update()
    {
        if(GameObject.FindGameObjectWithTag("Player") != null && (int)this.transform.position.x % 4 == 0 && lastShot != (int)this.transform.position.x)
        {
            lastShot = (int)this.transform.position.x;
            Shoot();
        }
        
        float fractionUnpopped = (float)BalloonSpawner.currentBalloons/BalloonSpawner.totalBalloons;
        int percentUnpopped = (Mathf.CeilToInt(fractionUnpopped * 10))*10;

        
    }
    
    void OnCollisionEnter2D(Collision2D coll) 
    {
        if (coll.gameObject.tag == "PlayerDart")
        {           
            if(extralives == 0)
            {
                Player p = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
                p.updateScore(50);
                Destroy (this.rigidbody2D);
                Destroy (this.collider2D);
                Destroy (this.gameObject);
                pop.Play();
            }
            else if(extralives > 0)
            {
                extralives--;
            }

            Destroy (coll.gameObject);
        }   
    }
    
    void Shoot()
    {        
        Player p = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        for(int i = 0; i < 3; i++)
        {
            var wB = (GameObject)Instantiate(candy, this.transform.position, Quaternion.identity);
            wB.rigidbody2D.velocity = (p.transform.position - transform.position).normalized * speed * 1.5f * (i + 1);
        }
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
        BalloonSpawner.bossWin = true;
    }
}
