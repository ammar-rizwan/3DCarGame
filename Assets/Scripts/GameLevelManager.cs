using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLevelManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void onMode1Selected()
    {
        PlayerPrefs.SetInt("level", 1);
        SceneManager.LoadScene(2);
    }
    public void onMode2Selected()
    {
        PlayerPrefs.SetInt("level", 2);
        SceneManager.LoadScene(2);
    }
}
