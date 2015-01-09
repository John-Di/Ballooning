using UnityEngine;
using System.Collections;

public class ParticleSystemAutoDestroy : MonoBehaviour {

    private ParticleSystem ps;
    public AudioSource pop;
    
    
    public void Start() 
    {
        ps = GetComponent<ParticleSystem>();
        //pop.Play();
    }
    
    public void Update() 
    {
        if(ps)
        {
            if(!ps.IsAlive())
            {
                Destroy(gameObject);
            }
        }
    }
}
