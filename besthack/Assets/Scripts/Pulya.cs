using UnityEngine;
using System.Collections;

public class Pulya : MonoBehaviour {
    private float lifetime = 2f;
	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
        if (lifetime <= 0) Destroy(gameObject);
        else lifetime -= Time.deltaTime;
        transform.position += transform.forward.normalized * Time.deltaTime * 200f;
	}
}
