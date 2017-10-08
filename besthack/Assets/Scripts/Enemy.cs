using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    [SerializeField]
    public GameObject player;
    
    public GameObject bullet;

    public GameObject firstPoint;
    public GameObject secondPoint;

    private Vector3 first;
    private Vector3 second;
    private Vector3 moveVector;


    private float moveRate = 5F;
    private float speed = 1F;

    private float rate = 1F;
    private float nextFire = 0F;

    public new AudioClip[] audio = new AudioClip[4];

    private AudioSource a_player;
    private bool visible;
    private bool flag = true;
    void Start () {

        first = firstPoint.transform.position;
        second = secondPoint.transform.position;
        moveVector = first;
        player = GameObject.FindGameObjectWithTag("Player");
        a_player = Camera.main.GetComponent<AudioSource>();
    }


    private void FixedUpdate()
    {
        IsVisible();
    }

    void Update () {
        if (visible)
        {
            Do();
            Fire();
        }
        else Move();
	}

    private void Do()
    {
           transform.LookAt(player.transform);
          
     
    }

    private void Move()
    {
        if (flag)
        {
            if (moveRate > 0)
            {
                moveRate -= Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, moveVector, speed * Time.deltaTime);
            }
            else
            {
                transform.Rotate(0, 180, 0);
                moveVector = (moveVector == first) ? second : first;
                moveRate = 5F;
            }
        }
    }

    private void Fire()
    {
        flag = false;
        
        if (Time.time > nextFire)
        {
            nextFire = Time.time + rate;
            Vector3 position = transform.position;
            position.z -= 0.939F*0.3F;
            position.y += 0.3F;
            Vector3 fireposition = player.transform.position;
            Instantiate(bullet, transform.position, transform.rotation);
            Ray _ray = new Ray(transform.position,transform.forward);
            RaycastHit _hit;
            if(Physics.Raycast(_ray, out _hit, 20f))
            {
                if(_hit.transform.tag == "Player")
                {
                    _hit.transform.GetComponent<PlayerHandler>().healthpoints--;
                }
            }
        }
    }
    private void OnDestroy()
    {
        if (Random.value > 0.5f)
        {
            //if(a_player != null) if (!a_player.isPlaying) a_player.PlayOneShot(audio[1]);
            
        }
    }
    private void IsVisible()
    {

        Vector3 position = transform.position;
        position.y += 0.6F;
        Vector3 raydirection = player.transform.position;
        if (Multi(transform.forward, player.transform.position - transform.position))
        {

            Ray ray = new Ray(position, raydirection - position);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 30f))
            {

                if (hit.collider.gameObject.tag == "Player" ||
        Vector3.Distance(transform.position, player.transform.position) <= 4F)
                {
                    if (nextFire == 0) nextFire = Time.time + 1f;
                    if (Random.value > 0.5f && flag)
                    {
                        if(a_player.isPlaying) a_player.PlayOneShot(audio[0]);
                    }
                    visible = true;
                }
                else
                {

                    nextFire = 0;
                    visible = false;
                }
            }

        }
        else
        {
            nextFire = 0;
            visible = false;
        }
        
    }

    private bool Multi(Vector3 one, Vector3 two)
    {
      
        bool value = (one.magnitude * two.magnitude * Mathf.Cos((Vector3.Angle(one, two))/57) >= 0) ? true : false;
        return value;
    }
    /*private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "pulya")
        {
            Destroy(gameObject);
        }
    }*/
}
