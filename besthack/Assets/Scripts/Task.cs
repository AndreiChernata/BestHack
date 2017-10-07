using UnityEngine;
using System.Collections;
using UnityEngine.UI;

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
                if(Vector3.Distance(Box.transform.position, transform.position) < 2f && !holdingBox)
                {
                    holdingBox = true;
                    holdBox = Instantiate(createBox, transform.forward / 2 + transform.position, transform.rotation);
                    holdBox.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                    
                    holdBox.transform.parent = transform;
                }
                if (Vector3.Distance(train.transform.position, transform.position) < 2f && holdingBox)
                {
                    Destroy(holdBox);
                    holdingBox = false;
                    ShippedBox();
                }
            }
        }
	    if(EnemyKills == 0)
        {
            task3.SetActive(true);
            task3.GetComponent<Text>().text = "Забрати " + ShipBox + " ящиків і донести до метро.";
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
            Debug.Log("Finished");
        }
        //time -= Time.deltaTime;
        //int timeI = (int)time;
       // string s = string.Format("{0}", (time - (timeI * 60)));
        //text.text = (timeI % 60).ToString() + ":" + s;
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
