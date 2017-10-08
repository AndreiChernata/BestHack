using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    public GameObject koala;
    private Vector3 destination;
    public Transform dest;
    private Vector3 from;
    public GameObject Option;

    private float progress = 0;
	// Use this for initialization
	void Start () {
        from = koala.transform.position;
        destination = dest.position;
	}
	
	// Update is called once per frame
	void Update () {
        Debug.Log(progress);
        if (progress < 1f) progress += 0.1f * Time.deltaTime;
        else
        {
            destination = from;
            from = koala.transform.position;
            Debug.Log("D: " + destination + " F: " + from);
            progress = 0;
        }
        Debug.Log("D: " + destination + " F: " + from);

        koala.transform.position = Vector3.Lerp(from, destination, progress);
	}
    public void loadGame()
    {
        PlayerPrefs.SetInt("scenetoload", 2);
        SceneManager.LoadScene(0);
    }
    public void quitGame()
    {
        Application.Quit();
    }
    public void ShowOption()
    {
        Option.SetActive(true);
    }
    public void HideOption(int glass_index)
    {
        PlayerPrefs.SetInt("glass_type", glass_index);
        Option.SetActive(false);
    }
}
