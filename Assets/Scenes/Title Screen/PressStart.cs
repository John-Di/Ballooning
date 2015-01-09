using UnityEngine;
using System.Collections;

public class PressStart : MonoBehaviour 
{   
    
    float nativeWidth = 1582.0f;
    float nativeHeight = 890.0f;
    Vector3 screenScale;
    
    public GUIStyle style;

    
    void Start()
    {
        WaveText.waveNumber = 0;
        Score.score = 0;
    }

    
    void OnGUI()
    {
        screenScale.x = Screen.width / nativeWidth;
        screenScale.y = Screen.height / nativeHeight;
        screenScale.z = 1;
        
        var backupMat = GUI.matrix;
        
        GUI.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, screenScale);
        
        style.normal.textColor = new Color(Mathf.Cos(Time.timeSinceLevelLoad*3), -Mathf.Cos(Time.timeSinceLevelLoad*5), Mathf.Sin(Time.timeSinceLevelLoad*3), 1.0f);
        
        
        GUI.Label(new Rect(430,450,150,90), "Press Any Key to Start" , style);
        
        
        GUI.matrix = backupMat;
    }
}
