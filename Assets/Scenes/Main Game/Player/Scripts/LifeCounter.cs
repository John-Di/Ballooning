using UnityEngine;
using System.Collections;

public class LifeCounter : MonoBehaviour 
{

    int STOCK = 2;
    int currentLife;
    GameObject[] lives;
    public GameObject initialLife;
    
    public GUIStyle style;

    float nativeWidth = 1582.0f;
    float nativeHeight = 890.0f;
    Vector3 screenScale;
    
    void Start()
    {
        lives = new GameObject[STOCK];
        currentLife = STOCK;
        Vector3 nextPosition = initialLife.transform.position;
        lives[0] = initialLife;

        for (int i = 1; i < STOCK; i++)
        {
            nextPosition.x += 0.5f;
            var life = (GameObject)Instantiate(initialLife);
            life.transform.parent = initialLife.transform.parent;
            life.transform.position = nextPosition;
            lives[i] = life;
        }
    }

    void OnGUI()
    {
        screenScale.x = Screen.width / nativeWidth;
        screenScale.y = Screen.height / nativeHeight;
        screenScale.z = 1;
        
        var backupMat = GUI.matrix;
        
        GUI.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, screenScale);
        
        GUI.color = Color.black;
        
        GUI.Label(new Rect(1200,820,100,90), "<size=25>Lives: </size>", style);
        
        GUI.matrix = backupMat;

        //GUI.matrix = backupMat;
    }

    
    public int DecrementLife()
    {
        currentLife = Mathf.Max(0, currentLife - 1);
        UpdateBars();
        return currentLife;
    }
    
    void UpdateBars()
    {
        for (int i = 0; i < STOCK; i++)
        {
            lives[i].SetActive(i < currentLife);
        }
    }
}
