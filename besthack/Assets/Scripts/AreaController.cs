using UnityEngine;
using System.Collections;

public class AreaController : MonoBehaviour {
    public bool koala;
	// Use this for initialization
	void Start () {
        //if (koala) Instantiate(Resources.Load("koala"), new Vector3(transform.position.x, 0.4f, transform.position.z), Quaternion.Euler(0, -90, 0));
        //else Instantiate(Resources.Load("kangaroo"), new Vector3(transform.position.x, 0.4f, transform.position.z), new Quaternion());
	}
	public void afterStart()
    {
        if (koala) Instantiate(Resources.Load("koala"), new Vector3(transform.position.x, 0.4f, transform.position.z), Quaternion.Euler(0, -90, 0));
        else Instantiate(Resources.Load("kangaroo"), new Vector3(transform.position.x, 0.4f, transform.position.z), new Quaternion());

    }
    // Update is called once per frame
    void Update () {
	
	}
}
