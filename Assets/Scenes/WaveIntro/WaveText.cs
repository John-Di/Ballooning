using UnityEngine;
using System.Collections;

public class WaveText : MonoBehaviour 
{
    float nativeWidth = 1582.0f;
    float nativeHeight = 890.0f;
    Vector3 screenScale;

    public static int waveNumber = 0;
    
    public GUIStyle style;

    public AudioSource bloop;

    void Start()
    {
        waveNumber++;
        gameObject.AddComponent<TimedLevelLoad>();
        gameObject.GetComponent<TimedLevelLoad>().waitTime = 8.0f;
        gameObject.GetComponent<TimedLevelLoad>().levelName = "Game";
    }

    void OnGUI()
    {
        screenScale.x = Screen.width / nativeWidth;
        screenScale.y = Screen.height / nativeHeight;
        screenScale.z = 1;
        
        var backupMat = GUI.matrix;
        
        GUI.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, screenScale);
        
        style.normal.textColor = Color.black;
        
        
        GUI.Label(new Rect(370,300,150,90), "Wave " + waveNumber, style);
        
        
        GUI.matrix = backupMat;
    }
}