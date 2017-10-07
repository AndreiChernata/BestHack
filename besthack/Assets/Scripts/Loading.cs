using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Loading : MonoBehaviour {
    
    public int sceneId;

    public Image process;
    public Text processText;
	// Use this for initialization
	void Start () {
        sceneId = PlayerPrefs.GetInt("scenetoload");
        if (sceneId == 0) sceneId = 1;
        PlayerPrefs.SetInt("scenetoload", 1);
        StartCoroutine(LoadScene());
	}

    private IEnumerator LoadScene()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneId);
        
        while (!operation.isDone)
        {
            
            float progress = operation.progress / 0.9F;
            process.fillAmount = progress;
            processText.text = string.Format("{0:0}%", progress*100);

            yield return null;
        }
        

    }
}
