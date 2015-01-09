using UnityEngine;
using System.Collections;

public class Score : MonoBehaviour 
{
    public static int score = 0;
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

        GUI.color = Color.black;

        GUI.Label(new Rect(40,45,150,90), "Score: " + score , style);

        GUI.matrix = backupMat;
    }
	
	// Update is called once per frame
    public void updateScore(int points)
    {
        score += points;
    }
}
