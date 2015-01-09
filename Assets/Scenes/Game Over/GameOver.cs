using UnityEngine;
using System.Collections;

public class GameOver : MonoBehaviour {

    float nativeWidth = 1582.0f;
    float nativeHeight = 890.0f;
    Vector3 screenScale;
    
    public GUIStyle style;


    void OnGUI()
    {
        screenScale.x = Screen.width / nativeWidth;
        screenScale.y = Screen.height / nativeHeight;
        screenScale.z = 1;
        
        var backupMat = GUI.matrix;
        
        GUI.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, screenScale);
        
        style.normal.textColor = new Color(1.0f, 0.66f, 0.1f, 1.0f);
        
        
        GUI.Label(new Rect(200,90,150,90), "GameOver" , style);
        
        
        GUI.matrix = backupMat;
    }
}
