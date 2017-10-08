using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHandler : MonoBehaviour {

    public float healthpoints;
    public float speed;
    private float mousedeltaX;
    private float mousedeltaY;
    // private Vector2 prevmousepos;
    private float boost = 1f;
    public Transform _camera;
    public float speedX;
    public float speedY;
    private float reload = 1 / 5f;
    public Text Ammo;
    private int ammo;

    private float stay_y;
    private float sit_y;
    public AudioClip rel;

    private Text hpBar;
    public GameObject pulya;
    private int glass_type;

    // Use this for initialization
    void Start () {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        healthpoints = 10f;
        hpBar = GameObject.Find("hpBar").GetComponent<Text>();
        ammo = 12;
        glass_type = PlayerPrefs.GetInt("glass_type");
        if (glass_type == 1) healthpoints += 3f;
        else if (glass_type == 2) speed += 2f;

        stay_y = transform.position.y;
        sit_y = (-transform.up * 0.7f + transform.position).y;

    }
   
    // Update is called once per frame
    void Update () {
        Ammo.text = "" + ammo;
        hpBar.text = "" + (int)healthpoints;
        
        if (reload > 0) reload -= Time.deltaTime;
        else if (ammo == 0) ammo = 12;        
        boost = (Input.GetKey(KeyCode.LeftShift)) ?
            1.5f : 1f;
	    if(Input.GetKey(KeyCode.W))
        {
            
            transform.position += transform.forward.normalized * Time.deltaTime * speed * boost;
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += transform.right.normalized * Time.deltaTime * speed *0.6f;
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.position += -transform.right.normalized * Time.deltaTime* speed * 0.6f;
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.position += -transform.forward.normalized * Time.deltaTime *speed * 0.8f;
        }
        if(Input.GetKey(KeyCode.LeftControl))
        {
            transform.position = new Vector3(transform.position.x, sit_y, transform.position.z);
            speed = 3.5f;
            if (boost > 0f) speed = 2f;
        }
        else if(Input.GetKeyUp(KeyCode.LeftControl))
        {
            transform.position = new Vector3(transform.position.x, stay_y, transform.position.z);
            speed = 7f;
            if (glass_type == 2) speed += 2f;
        }
        if (Input.GetMouseButton(0))
        {
            if (GetComponent<AudioSource>().isPlaying == false && reload < 0f) GetComponent<AudioSource>().PlayOneShot(GetComponent<AudioSource>().clip);
            Shoot();
        }
        else if(reload < 0) GetComponent<AudioSource>().Stop();
        MouseLook();
    }

    private void Shoot()
    {
        if(reload <= 0 && ammo > 0)
        {
            
            Ray _ray = new Ray(_camera.position, _camera.forward);
            RaycastHit _hit;
            ammo--;
            
            if (Physics.Raycast(_ray, out _hit, 200f))
            {
                
                if (_hit.transform.tag == "enemy" && GetComponent<Task>().task2.activeSelf == true)
                {
                    Destroy(_hit.transform.gameObject);
                    GetComponent<Task>().KilledEnemy();
                }
                else if (_hit.transform.tag == "enemy" && GetComponent<Task>().task4.activeSelf == true)
                {
                    Destroy(_hit.transform.gameObject);
                    GetComponent<Task>().KilledEnemy();
                }
            }
            Instantiate(pulya, _camera.position, _camera.rotation);
            reload = 1 / 5f;
            if (ammo == 0)
            {
                GetComponent<AudioSource>().Stop();
                GetComponent<AudioSource>().PlayOneShot(rel);
                reload = 7f;
            }
        }
    }

    private void MouseLook()
    {
        mousedeltaX = Input.GetAxis("Mouse X");
        mousedeltaY = -Input.GetAxis("Mouse Y");
        float tempX = 0;
        transform.Rotate(new Vector3(0, mousedeltaX*speedX, 0));
        tempX = _camera.rotation.eulerAngles.x;
        if (_camera.rotation.eulerAngles.x > 180f) tempX = _camera.rotation.eulerAngles.x - 360f;
        if (_camera.rotation.eulerAngles.x < -360f) tempX = _camera.rotation.eulerAngles.x + 360f;
        if ((mousedeltaY < 0 && tempX > -20) || (mousedeltaY > 0 && tempX < 20)) _camera.Rotate(new Vector3(mousedeltaY * speedY, 0, 0));

       // prevmousepos = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
    }
    
}
