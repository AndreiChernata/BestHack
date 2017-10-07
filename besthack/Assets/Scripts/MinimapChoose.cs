using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityStandardAssets.ImageEffects;
using UnityEngine.SceneManagement;


public class MinimapChoose : MonoBehaviour {

    public GameObject from;
    public GameObject to;
    public GameObject showUI;
    public Text price;
    public Text _money;

    private int _price;
    private bool relsing;



    private bool loading;
    private Vector3 load_blur;



    public int money;
    private bool make_raid;

    public bool[,] relses = new bool[13, 13];

    private bool buy_rels;
    private bool raiding;

    private float timers = 1.7f;

    // Use this for initialization
    void Start () {
        relsing = false;
        buy_rels = false;
        make_raid = false;
        raiding = false;
        money = 300;
        _money.text = "Печива на складі: " + money;
        LoadRelses();
        loading = false;
        
        //PlayerPrefs.GetInt("money");
    }
	
	// Update is called once per frame
	void Update () {
        if (timers < 0f)
        {
            PlayerPrefs.SetInt("scenetoload", 3);
            SceneManager.LoadScene(0);
        }
        if (loading)
        {
            transform.position = Vector3.MoveTowards(transform.position, load_blur, Time.deltaTime * 3);
            timers -= Time.deltaTime;

            GetComponent<Blur>().enabled = true;
            return;
        }
        
        foreach (GameObject hit in GameObject.FindGameObjectsWithTag("area"))
        {
            if(hit.transform.gameObject == from || hit.transform.gameObject == to) hit.transform.position = new Vector3(hit.transform.position.x, 0.2f, hit.transform.position.z);
            else hit.transform.position = new Vector3(hit.transform.position.x, 0f, hit.transform.position.z);
        }
        if (buy_rels)
        {
            Ray _ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit _hit;
            if (Physics.Raycast(_ray, out _hit, Mathf.Infinity) && _hit.transform.tag == "area")
            {
                _hit.transform.position = new Vector3(_hit.transform.position.x, 0.2f, _hit.transform.position.z);
                if (Input.GetMouseButtonUp(0) && from == null && !relsing && _hit.transform.gameObject != from && _hit.transform.gameObject.GetComponent<AreaController>().koala == true)
                {
                    from = _hit.transform.gameObject;
                }
                else if (Input.GetMouseButtonUp(0) && from != null && _hit.transform.gameObject != from && !relsing && _hit.transform.gameObject.GetComponent<AreaController>().koala == false)
                {
                    if (relses[int.Parse(from.name), int.Parse(to.name)] == false)
                    {
                        to = _hit.transform.gameObject;
                        relsing = true;
                        ShowPrice();
                    }
                    else
                    {
                        from = null;
                        to = null;
                        buy_rels = false;
                        relsing = false;
                    }
                    
                }
            }
        }
        else if(make_raid)
        {
            Ray _ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit _hit;
            if (Physics.Raycast(_ray, out _hit, Mathf.Infinity) && _hit.transform.tag == "area")
            {
                _hit.transform.position = new Vector3(_hit.transform.position.x, 0.2f, _hit.transform.position.z);
                if (Input.GetMouseButtonUp(0) && from == null && !raiding && _hit.transform.gameObject != from && _hit.transform.gameObject.GetComponent<AreaController>().koala == true)
                {
                    from = _hit.transform.gameObject;
                }
                else if (Input.GetMouseButtonUp(0) && from != null && _hit.transform.gameObject != from && !raiding && _hit.transform.gameObject.GetComponent<AreaController>().koala == false)
                {
                    to = _hit.transform.gameObject;
                    if (relses[int.Parse(from.name), int.Parse(to.name)] == true)
                    {
                        raiding = true;
                        ShowPrice();
                    }
                    else
                    {
                        make_raid = false;
                        from = null;
                        to = null;
                    }
                }
            }
        }
    }
    void ShowPrice()
    {
        
        if (relsing)
        {
            
            _price = (int)(Vector3.Distance(from.transform.position, to.transform.position) * 100);
            price.text = "This will cost you: " + (int)(Vector3.Distance(from.transform.position, to.transform.position) * 100);
            showUI.SetActive(true);
        }
        else if(raiding)
        {

            price.text = "Відправлення з міста " + from.name + " до " + to.name + ".";
            showUI.SetActive(true);
        }
       // Debug.Log("This will cost you: " + Vector3.Distance(from.transform.position, to.transform.position));
    }
    public void Agree()
    {
        if (buy_rels && money >= _price)
        {
            money -= _price;
            relses[int.Parse(from.name), int.Parse(to.name)] = true;
            Vector3 temp_rels = (from.transform.position + to.transform.position) / 2;
            GameObject temp_obj = GameObject.CreatePrimitive(PrimitiveType.Cube);
            temp_obj.transform.position = temp_rels;
            if (from.transform.position.x > to.transform.position.x) temp_obj.transform.rotation = Quaternion.Euler(0, Vector3.Angle(temp_rels * 2, transform.right), 0);
            else temp_obj.transform.rotation = Quaternion.Euler(0, Vector3.Angle(temp_rels * 2, -transform.right), 0);
            temp_obj.transform.localScale = new Vector3(Vector3.Distance(from.transform.position, to.transform.position), 0.01f, 0.1f);
            to = null;
            from = null;
            showUI.SetActive(false);
            relsing = false;
            _money.text = "Печива на складі: " + money;
            _price = 0;
            //Instantiate(, temp_rels, Quaternion.Euler(0, Vector3.Angle(temp_rels * 2, transform.right),0));
            buy_rels = false;
            
            
            
        }
        else if(raiding)
        {
            if(relses[int.Parse(from.name), int.Parse(to.name)] == true)
            {
                load_blur = (from.transform.position + to.transform.position) / 2;
                loading = true;
                SaveRelses();
                from = null;
                to = null;
                Debug.Log("Start raid");
                showUI.SetActive(false);
                raiding = false;
                make_raid = false;
                
            }
        }

    }
    private void LoadRelses()
    {
        string temp = PlayerPrefs.GetString("relses");
        string[] rels = temp.Split(';');
        for(int i=0; i<rels.Length-1;i++)
        {
            string[] one_rels = rels[i].Split(',');
            Debug.Log(rels[i]);
            if (one_rels[2] == "1")
            {
                GameObject tempf, tempt;
                tempf = GameObject.Find(one_rels[0]);
                tempt = GameObject.Find(one_rels[1]);
                Vector3 temp_rels = (tempf.transform.position + tempt.transform.position) / 2 + new Vector3(0, 0.2f, 0);
                GameObject temp_obj = GameObject.CreatePrimitive(PrimitiveType.Cube);
                temp_obj.transform.position = temp_rels;
                if (tempf.transform.position.x > tempt.transform.position.x) temp_obj.transform.rotation = Quaternion.Euler(0, Vector3.Angle(temp_rels * 2, transform.right), 0);
                else temp_obj.transform.rotation = Quaternion.Euler(0, Vector3.Angle(temp_rels * 2, -transform.right), 0);
                temp_obj.transform.localScale = new Vector3(Vector3.Distance(tempf.transform.position, tempt.transform.position), 0.01f, 0.1f);
                relses[int.Parse(one_rels[0]), int.Parse(one_rels[1])] = true;
            }
        }
    }
    private void SaveRelses()
    {
        string temp = "";
        for(int i=1; i<13;i++)
            for(int j=1;j<13;j++)
            {
              
                if(relses[i,j]) temp += i + "," + j + ",1;";
                else temp += i + "," + j + ",0;";
            }
        PlayerPrefs.SetString("relses", temp);
    }
    public void Buy()
    {
        make_raid = false;
        raiding = false;
        relsing = false;
        from = null;
        to = null;
        buy_rels = true;
    }

    public void Raid()
    {
        
        buy_rels = false;
        relsing = false;
        raiding = false;
        from = null;
        to = null;
        make_raid = true;
    }

    public void DisAgree()
    {
        if (buy_rels)
        {
            from = null;
            to = null;
            relsing = false;
            showUI.SetActive(false);
            buy_rels = false;
            _price = 0;
        }
        if (make_raid)
        {
            from = null;
            to = null;
            make_raid = false;
        }
    }
}
