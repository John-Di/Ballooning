using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Cluster : MonoBehaviour 
{
    public List<Balloon> balloons = new List<Balloon>();    
    public List<Vector2> balloonPositions = new List<Vector2>();
    GameObject clusterCenter;
    public List<GameObject> balloonColorPrefabs = new List<GameObject>();
    //static int totalBalloons = 100;
    public static int zPos = 0;
    public int count;

    void Start()
    {        
        clusterCenter = GameObject.Find("Balloon Spawner").GetComponent<BalloonSpawner>().clusterCenter;
    }



	// Use this for initialization
	public void PopulateCluster() 
    {
        int clusterSize = Random.Range(1, 10);

        for(int i = 0; i < clusterSize && BalloonSpawner.currentBalloons < BalloonSpawner.totalBalloons; i++)
        {
            GameObject balloonPrefab = balloonColorPrefabs[(int)(Random.Range(0,7))];
            
            var obj = (GameObject) Instantiate(balloonPrefab, 
                                               this.transform.position 
                                               + new Vector3(balloonPositions[i].x, balloonPositions[i].y, this.transform.position.z), 
                                               Quaternion.identity);
            
            obj.GetComponent<Balloon>().enabled = true;
            
            var balloon = obj.GetComponent<Balloon>();           
                 
            balloon.transform.parent = this.transform;
            balloon.SetCenter(this);
            balloons.Add (balloon);
            
            balloon.renderer.sortingOrder = zPos;
            BalloonSpawner.currentBalloons++;            

        }

        this.name = "Cluster " + zPos;
        zPos++;
	}
	
    public int getSize()
    {
        return balloons.Count;
    }

	// Update is called once per frame
	void Update () 
    {
        count = balloons.Count;

	    if(balloons.Count <= 0)
        {
            Destroy(gameObject);
        }
	}

    public void CutBalloonsFromCluster()
    {       
        GameObject subCluster = (GameObject) Instantiate(clusterCenter, new Vector2(this.transform.position.x, this.transform.position.y), Quaternion.identity);
        subCluster.gameObject.rigidbody2D.velocity = this.gameObject.rigidbody2D.velocity;

        int subClusterSize = Random.Range(1,balloons.Count);

        Player p = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        for(int i = 0; i < subClusterSize; i++)
        {
            int index = Random.Range(0,balloons.Count);

            Balloon b = balloons[index];

            AddToSubcluster(subCluster, b); 
            
            balloons.RemoveAt(index); 
            //b.rigidbody2D.velocity = gameObject.rigidbody2D.velocity * 1.5f;
        }

        p.updateScore(2);
    }

    void AddToSubcluster(GameObject cluster, Balloon b)
    {
        b.transform.parent = cluster.transform;        
        cluster.rigidbody2D.velocity = gameObject.rigidbody2D.velocity * 1.20f;
        b.SetCenter(cluster.GetComponent<Cluster>());
        cluster.GetComponent<Cluster>().balloons.Add (b);
    }
}
