using UnityEngine;
using System.Collections;

public class LevelLoader : MonoBehaviour 
{
    public string level;

    void Update() 
    {
        if (Input.anyKey)
        {
            Application.LoadLevel(level);
        }        
    }
}
