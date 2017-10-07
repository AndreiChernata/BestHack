using UnityEngine;
using System.Collections;

public class PlayerHandler : MonoBehaviour {


    public float speed;
    private float mousedeltaX;
    private float mousedeltaY;
   // private Vector2 prevmousepos;

    public Transform _camera;
    public float speedX;
    public float speedY;

	// Use this for initialization
	void Start () {
        //prevmousepos = Input.mousePosition;
        //Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
	
	// Update is called once per frame
	void Update () {
	    if(Input.GetKey(KeyCode.W))
        {
            transform.position += transform.forward.normalized * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += transform.right.normalized * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.position += -transform.right.normalized * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.position += -transform.forward.normalized * Time.deltaTime;
        }
        MouseLook();
    }
    private void MouseLook()
    {
        mousedeltaX = Input.GetAxis("Mouse X");
        mousedeltaY = Input.GetAxis("Mouse Y");
        float tempX = 0;
        transform.Rotate(new Vector3(0, mousedeltaX*speedX, 0));
        tempX = _camera.rotation.eulerAngles.x;
        if (_camera.rotation.eulerAngles.x > 180f) tempX = _camera.rotation.eulerAngles.x - 360f;
        if (_camera.rotation.eulerAngles.x < -360f) tempX = _camera.rotation.eulerAngles.x + 360f;
        if ((mousedeltaY < 0 && tempX > -20) || (mousedeltaY > 0 && tempX < 20)) _camera.Rotate(new Vector3(mousedeltaY * speedY, 0, 0));

       // prevmousepos = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
    }
    
}
