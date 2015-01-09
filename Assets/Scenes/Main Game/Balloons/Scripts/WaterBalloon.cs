using UnityEngine;
using System.Collections;

public class WaterBalloon : MonoBehaviour 
{
    public GameObject deathParticle;

    void OnCollisionEnter2D(Collision2D coll) 
    {       
        if (coll.gameObject.tag == "PlayerDart")
        {                 
            Destroy (coll.gameObject);
        }
        
        Instantiate(deathParticle, transform.position, Quaternion.identity);
        Destroy (this.gameObject); 
    }

    void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }
}
