using UnityEngine;
using System.Collections;

public class MinimapChoose : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        foreach (GameObject hit in GameObject.FindGameObjectsWithTag("area"))
        {
           hit.transform.position = new Vector3(hit.transform.position.x, 0f, hit.transform.position.z);
        }
        Ray _ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit _hit;
        if (Physics.Raycast(_ray, out _hit, Mathf.Infinity) && _hit.transform.tag == "area")
        {
            _hit.transform.position = new Vector3(_hit.transform.position.x, 0.2f, _hit.transform.position.z);
            Debug.Log(_hit.transform.name);
        }
       
    }
}
