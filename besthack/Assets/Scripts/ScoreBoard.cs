using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class ScoreBoard : MonoBehaviour {
    private int win;
    private int cookies_earned;
    private string time;


    public Text stats;


	// Use this for initialization
	void Start () {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        win = PlayerPrefs.GetInt("winlast");
        cookies_earned = PlayerPrefs.GetInt("cookies_last");
        time = PlayerPrefs.GetString("timelast");
        if (win == 1)
        {
            stats.text = "ВИКОНАНО\nЗароблено печива " + cookies_earned + "\nЧасу залишилось: " + time;
        }
        else stats.text = "ПРОВАЛ\nЗароблено печива: 0\nЧасу залишилось: " + time;

	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void _continue()
    {
        PlayerPrefs.SetInt("money", PlayerPrefs.GetInt("money") + cookies_earned);
        PlayerPrefs.SetInt("scenetoload", 2);
        SceneManager.LoadScene(0);
    }
}
