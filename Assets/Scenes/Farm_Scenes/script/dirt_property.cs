using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dirt_property : MonoBehaviour
{

    public string name;
    public GameObject obj;
    private bool trigger_ = false;
    public string plant_name;
    public System.DateTime plant_time;
    [SerializeField]
    private GameObject game_ctrl_system;
    private Dirt_plant_ctrl dirt;
    //public string Name { get => name; set => name = value; }
    //public GameObject Obj { get => obj; set => obj = value; }

    public dirt_property(string name, GameObject obj)
    {
        this.name = name;
        this.obj = obj;
    }
   

    public dirt_property()
    {
    }

    public dirt_property(string name, GameObject obj, string plant_name, System.DateTime plant_time) : this(name, obj)
    {
        this.plant_name = plant_name;
        this.plant_time = plant_time;
    }

    // Start is called before the first frame update
    void Start()
    {
         dirt = game_ctrl_system.GetComponent<Dirt_plant_ctrl>();
    }
  
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 8)
        {
            trigger_ = true;
            this.GetComponent<Outline>().enabled = true;
            dirt.Dirt_get_trigger_name(this.name);
            //dirt.spawn_plant(this.name);

        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 8)
        {
            trigger_ = false;
            this.GetComponent<Outline>().enabled = false;
            dirt.Dirt_get_trigger_name("");
            //dirt.destroy_plant(this.name);

        }
    }
   

    // Update is called once per frame
    void Update()
    {
        
    }

   

  
}
