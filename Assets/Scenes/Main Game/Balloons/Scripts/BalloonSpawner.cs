using UnityEngine;
using System.Collections;

public class BalloonSpawner : MonoBehaviour 
{   
    public GameObject clusterCenter;
    public static int totalBalloons, currentBalloons = 0;
    public Vector2 balloonVelocity;
    public HotAirBalloon hotAirBalloon;
    public Pumpkin pumpkin;
    public float waitTime; 
    public static bool bossWin;

	void Awake()
    {
        waitTime = 2.0f;
        totalBalloons = WaveText.waveNumber * 25;
        bossWin = true;

        while(currentBalloons < totalBalloons)
        {
            GameObject cluster = (GameObject) Instantiate(clusterCenter, new Vector2(Random.Range(-7, 8), Random.Range(-3, 4)), Quaternion.identity);
            cluster.gameObject.rigidbody2D.velocity = new Vector2(0.5f * ((Random.value >= 0.5) ? 1 : -1) , 0.5f * ((Random.value >= 0.5) ? 1 : -1));

            cluster.GetComponent<Cluster>().PopulateCluster();
        }

        HotAirBalloon.lastPercentageSpawned = 0;
    }


	// Update is called once per frame
	void Update () 
    {
        float fractionUnpopped = (float)currentBalloons/totalBalloons;
        int percentUnpopped = (Mathf.CeilToInt(fractionUnpopped * 10))*10;

        if(percentUnpopped != 100.0f && percentUnpopped % 30 == 0.0f && HotAirBalloon.lastPercentageSpawned != percentUnpopped && currentBalloons > 0.0f)
        {
            Instantiate(hotAirBalloon, new Vector2(-11.18851f,2.579201f), Quaternion.identity);
            HotAirBalloon.lastPercentageSpawned = percentUnpopped;
        }	

        if(currentBalloons <= 0.0f && !HotAirBalloon.isSpawned)
        { 
            
            if(bossWin)
            {
                waitTime -= Time.deltaTime;
                
                if (waitTime <= 0)
                {                
                    Application.LoadLevel("WaveIntro");
                }
            }
            else if(!Pumpkin.isSpawned && !bossWin)
            {
                Instantiate(pumpkin, new Vector2(-11.18851f,2.579201f), Quaternion.identity);
                Pumpkin.lastPercentageSpawned = percentUnpopped;

            }

        }
	}
}
