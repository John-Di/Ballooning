using UnityEngine;
using System.Collections;

public class TimedLevelLoad : MonoBehaviour 
{
    public float waitTime; 
    public string levelName;

    void Update()
    {      
         waitTime -= Time.deltaTime;

         if (waitTime <= 0)
         {
             Application.LoadLevel(levelName);
         }
    }
}
