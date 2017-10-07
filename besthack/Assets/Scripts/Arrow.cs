using UnityEngine;
using System.Collections;

public class Arrow : MonoBehaviour {

    private Vector3 from;
    private Vector3 to;
    private bool down;
    private float timer = 2f;
	// Use this for initialization
	void Start () {
        from = transform.position;
        to = transform.forward*2 + transform.position;
        down = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (down == false) timer -= Time.deltaTime;
        else timer += Time.deltaTime;
        if (timer < 0) down = true;
        else if (timer > 2) down = false;
        if (down == false)
        {
            transform.position = Vector3.MoveTowards(transform.position, to, Time.deltaTime);
        }
        if (down == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, from, Time.deltaTime);
        }
    }
}
