using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Task : MonoBehaviour {

    public int EnemyKills;
    public int ShipBox;
    public bool DefenseBox;
    public int defenseKill;

    public bool GoToAttack;
    public GameObject task1;
    public GameObject task2;
    public GameObject task3;
    public GameObject task4;
    public GameObject enemy;
    public GameObject Box;
    public GameObject train;
    public GameObject createBox;
    private GameObject holdBox;

    private bool holdingBox;
    public Text text;
    private float time = 3600F;

    private bool phase_4;
    private int countwaves =0 ;
    private float timer = 3f;

    // Use this for initialization
    void Start () {
        task1.GetComponent<Text>().text = "Дібратися до бази кенгуру.";
        phase_4 = false;
    }
	
	// Update is called once per frame
	void Update () {
        timer -= Time.deltaTime;
        if(Input.GetKeyDown(KeyCode.E))
        {
            if(task3.activeSelf)
            {
                if(Vector3.Distance(Box.transform.position, transform.position) < 4f && !holdingBox)
                {
                    holdingBox = true;
                    holdBox = Instantiate(createBox, transform.forward / 2 + transform.position, transform.rotation);
                    holdBox.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                    
                    holdBox.transform.parent = transform;
                }
                if (Vector3.Distance(train.transform.position, transform.position) < 4f && holdingBox)
                {
                    Destroy(holdBox);
                    holdingBox = false;
                    ShippedBox();
                }
            }
        }
        if(GetComponent<PlayerHandler>().healthpoints <= 0f)
        {
            PlayerPrefs.SetInt("winlast", 0);
            PlayerPrefs.SetInt("cookies_last", 0);
            time -= Time.deltaTime;
            int _n = (int)time;
            if (_n - _n / 60 * 60 > 9)
                PlayerPrefs.SetString("timelast", (_n / 60 + ":" + (_n - _n / 60 * 60)));
            else
                PlayerPrefs.SetString("timelast", (_n / 60 + ":0" + (_n - _n / 60 * 60)));
            SceneManager.LoadScene(4);
        }
	    if(EnemyKills == 0)
        {
            task3.SetActive(true);
            task3.GetComponent<Text>().text = "Забрати " + ShipBox + " ящиків з печивом і донести до метро.";
        }
        if(ShipBox == 0)
        {
            task4.SetActive(true);
            task4.GetComponent<Text>().text = "Захищатися від викликаної підмоги.";
            phase_4 = true;
        }
        if(phase_4 && timer < 0 && countwaves <=3)
        {
            timer = 15f;
            SpawnEnemies();
            countwaves++;
        }
        if(phase_4 && defenseKill == 0)
        {
            PlayerPrefs.SetInt("winlast", 1);
            PlayerPrefs.SetInt("cookies_last", 200);
            time -= Time.deltaTime;
            int _n = (int)time;
            if (_n - _n / 60 * 60 > 9)
                PlayerPrefs.SetString("timelast", (_n / 60 + ":" + (_n - _n / 60 * 60)));
            else
                PlayerPrefs.SetString("timelast", (_n / 60 + ":0" + (_n - _n / 60 * 60)));
            SceneManager.LoadScene(4);
        }
        time -= Time.deltaTime;
        int n = (int)time;
        if (n > 0)
        {
            if (n - n / 60 * 60 > 9)
                text.text =(n / 60 + ":" + (n - n / 60 * 60));
            else
                text.text = (n / 60 + ":0" + (n - n / 60 * 60));
        }
        else
        {
            text.text = ("0:00");
            PlayerPrefs.SetInt("winlast", 0);
            PlayerPrefs.SetInt("cookies_last", 0);
            PlayerPrefs.SetString("timelast", "0:00");
            SceneManager.LoadScene(4);
        }
            
    }
    private void SpawnEnemies()
    {
        Instantiate(enemy, new Vector3(transform.position.x + ( (Random.value * 2 -1 )*(Random.value * 5+5)), transform.position.y, transform.position.z + (Random.value * 2 - 1) * (Random.value * 5 + 5)), new Quaternion());
        Instantiate(enemy, new Vector3(transform.position.x + ((Random.value * 2 - 1) * (Random.value * 5 + 5)), transform.position.y, transform.position.z + (Random.value * 2 - 1) * (Random.value * 5 + 5)), new Quaternion());
    }
    private void OnTriggerEnter(Collider col)
    {
        if(col.tag == "task1")
        {
            task1.GetComponent<Text>().text = "Виконано";
            Destroy(GameObject.Find("arrow 1"));
            task2.SetActive(true);
            task2.GetComponent<Text>().text = "Вбити " + EnemyKills + " ворогів.";
        }
    }
    public void KilledEnemy()
    {
        if (task4.activeSelf == true)
        {
            defenseKill--;
        }
           else if (task2.activeSelf == true) EnemyKills--;
        task2.GetComponent<Text>().text = "Вбити " + EnemyKills + " ворогів.";
        
    }
    public void ShippedBox()
    {
        if (task3.activeSelf == true) ShipBox--;
        task3.GetComponent<Text>().text = "Забрати " + ShipBox + " ящиків і донести до метро.";
    }
}
