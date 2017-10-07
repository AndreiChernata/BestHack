using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Task : MonoBehaviour {

    public int EnemyKills;
    public int ShipBox;
    public bool DefenseBox;

    public bool GoToAttack;
    public GameObject task1;
    public GameObject task2;
    public GameObject task3;
    public GameObject task4;
    public GameObject enemy;

    private bool phase_4;
    private float timer = 3f;

    // Use this for initialization
    void Start () {
        task1.GetComponent<Text>().text = "Дібратися до бази кенгуру.";
        phase_4 = false;
    }
	
	// Update is called once per frame
	void Update () {
        timer -= Time.deltaTime;
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
        if(phase_4 && timer < 0)
        {
            timer = 3f;
            SpawnEnemies();
        }
	}
    private void SpawnEnemies()
    {
        Instantiate(enemy, transform.position + new Vector3(transform.position.x + Random.value * 2 + 5, transform.position.y, transform.position.z + Random.value * 2 + 5), new Quaternion());
        Instantiate(enemy, transform.position + new Vector3(transform.position.x + Random.value * 2 + 5, transform.position.y, transform.position.z + Random.value * 2 + 5), new Quaternion());
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
        if (task2.activeSelf == true) EnemyKills--;
        task2.GetComponent<Text>().text = "Вбити " + EnemyKills + " ворогів.";
    }
    public void ShippedBox()
    {
        if (task3.activeSelf == true) ShipBox--;
        task3.GetComponent<Text>().text = "Забрати " + ShipBox + " ящиків і донести до метро.";
    }
}
