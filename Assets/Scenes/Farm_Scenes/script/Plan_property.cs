using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New farm object",menuName ="farm object/plant")]
public class Plan_property : ScriptableObject
{
    // Start is called before the first frame update

    public string plant_name;
    public string display_name;
    public Sprite Plant_icon;
    public GameObject plant_s;
    public GameObject plant_m;
    public GameObject plant_l;
    public GameObject plant_xl;
    public int so_luong;
    public int plant_time;

    public Plan_property(string plant_name, string display_name, Sprite plant_icon, GameObject plant_s, GameObject plant_m, GameObject plant_l, GameObject plant_xl, int so_luong, int plant_time)
    {
        this.plant_name = plant_name;
        this.display_name = display_name;
        Plant_icon = plant_icon;
        this.plant_s = plant_s;
        this.plant_m = plant_m;
        this.plant_l = plant_l;
        this.plant_xl = plant_xl;
        this.so_luong = so_luong;
        this.plant_time = plant_time;
    }

    public Plan_property()
    {
    }

   

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
