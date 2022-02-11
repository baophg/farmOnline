using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

public class Dirt_plant_ctrl : MonoBehaviour
{

    [SerializeField]
    private GameObject Loading_screen;
    [SerializeField]
    private GameObject plant_prefab;
    private static GameObject plant;
    public GameObject Dirt_prefab;
    public GameObject Dirt_parent;
    public GameObject Fisrt_dirt_prefab;
    public GameObject Fisrt_dirt_prefab_zone2;
    private Transform first_dirt_in_row_transfm;
    GameObject New_dirt;
    private float pos_z_to_change;
    private float pos_x_to_change;
    private static int dirt_lenght = 1;
    private static int dirt_array_lenght = 1;
    private static int dirt_pos_in_row = 1;
    //---------------------------------------------------------------------
    private static List<dirt_property> Dirt_list = new List<dirt_property>();
    private static string dirt_trigger_name;
    private static string cay_muon_trong;
    private JArray dirt_array;
   //----------------------------UI CONTROL----------------------------
   [SerializeField]
    private GameObject UI_Ctrl_system;
    UI_ctrl UI;
    void Start()
    {
        //QualitySettings.resolutionScalingFixedDPIFactor = 3f;
        UI = UI_Ctrl_system.GetComponent<UI_ctrl>();
        first_dirt_in_row_transfm = Fisrt_dirt_prefab.GetComponent<Transform>();
        plant = plant_prefab;
        Dirt_list.Add(new dirt_property(Fisrt_dirt_prefab.name, Fisrt_dirt_prefab));
        Fisrt_dirt_prefab.GetComponent<dirt_property>().name = Fisrt_dirt_prefab.name;
        //dirt_zone2.position = new Vector3(58.5f, 0.33f,72f);
        spawn_dirt();

    }

    // Update is called once per frame
    void Update()
    {


    }
    private void FixedUpdate()
    {

    }
    private void get_dirt_array()
    {
        if (sever_config.dirt_array_str != null)
        {
            string dirt_array_str = sever_config.dirt_array_str;
            dirt_array = JArray.Parse(dirt_array_str);
            Debug.Log("dirt array lenght:" + dirt_array.Count);
        }


    }
    private void spawn_dirt()
    {
        get_dirt_array();
        //if (dirt_array != null)
        //{

            for (int i = 0; i <30; i++)
            {

                String name = "";

                name = "dirt" + dirt_lenght;

                if (dirt_lenght == 12)
                {
                    Fisrt_dirt_prefab_zone2.SetActive(true);
                    Fisrt_dirt_prefab = Fisrt_dirt_prefab_zone2;
                    first_dirt_in_row_transfm = Fisrt_dirt_prefab_zone2.transform;
                    New_dirt = Fisrt_dirt_prefab_zone2;
                    dirt_array_lenght = 2;
                    dirt_lenght += 1;
                    dirt_pos_in_row = 1;
                    name = New_dirt.name;
                }
                else
                {
                    if (dirt_lenght % 4 == 0)
                    {
                        pos_x_to_change = Fisrt_dirt_prefab.transform.position.x + 5.8f * (dirt_array_lenght / 4);
                        New_dirt = Instantiate(Dirt_prefab, new Vector3(pos_x_to_change, Fisrt_dirt_prefab.transform.position.y, Fisrt_dirt_prefab.transform.position.z), Quaternion.identity);
                        New_dirt.name = "dirt" + dirt_lenght;
                        first_dirt_in_row_transfm = New_dirt.transform;
                        dirt_array_lenght += 1;
                        dirt_lenght += 1;
                        dirt_pos_in_row = 1;
                    }
                    else
                    {
                        pos_z_to_change = first_dirt_in_row_transfm.position.z - 5.6f * dirt_pos_in_row;
                        New_dirt = Instantiate(Dirt_prefab, new Vector3(first_dirt_in_row_transfm.position.x, first_dirt_in_row_transfm.position.y, pos_z_to_change), Quaternion.identity);
                        New_dirt.name = "dirt" + dirt_lenght;
                        dirt_array_lenght += 1;
                        dirt_lenght += 1;
                        dirt_pos_in_row += 1;
                    }
                }
                New_dirt.transform.SetParent(Dirt_parent.transform);
                New_dirt.GetComponent<dirt_property>().name = name;
                Dirt_list.Add(new dirt_property(name, New_dirt));
                //Instantiate(plant, New_dirt.transform.position, Quaternion.identity);
                //}
                //if (Loading_screen.active == true)
                //{
                //    Loading_screen.SetActive(false);
            }

        //}
    }
    public void spawn_plant(string cay_name)
    {
        string name = dirt_trigger_name;

        if (!name.Equals(""))
        {

            for (int i = 0; i < Dirt_list.Count; i++)
            {
                if (Dirt_list[i].name.Equals(name) && Dirt_list[i].plant_name != null)
                {
                    Debug.Log("o dat nay da co cay");
                }
                else
                {

                    if (Dirt_list[i].name.Equals(name))
                    {

                        foreach (Plan_property plan in UI.get_hat_giong_List())
                        {
                            if (plan.display_name.Equals(cay_name))
                            {
                                plant = plan.plant_s;
                            }
                        }

                        GameObject new_plant = Instantiate(plant, Dirt_list[i].obj.transform.position, Quaternion.identity);
                        Dirt_list[i] = new dirt_property(Dirt_list[i].name, Dirt_list[i].obj, cay_name, System.DateTime.Now);
                        Debug.Log("da trong cay luc:" + Dirt_list[i].plant_time);
                        UI.show_button_gieo_giong(false);

                        //Plant_list.Add(new plant_property(Dirt_list[i].name + "_plant", new_plant));
                    }
                }
            }
        }
        else
        {
            Debug.Log("khong co o dat de trong");
        }
    }

    public void Dirt_get_trigger_name(String name)
    {

        dirt_trigger_name = name;

        if (!name.Equals(""))
        {
            UI.show_button_container(true);
            for (int i = 0; i < Dirt_list.Count; i++)
            {
                if (Dirt_list[i].name.Equals(name) && Dirt_list[i].plant_name == null)
                {
                    UI.show_button_gieo_giong(true);
                    break;
                }
                else if (Dirt_list[i].name.Equals(name) && Dirt_list[i].plant_name != null)
                {
                    UI.show_button_gieo_giong(false);
                    break;
                }

            }
        }
        else
        {

            UI.show_button_gieo_giong(false);
            UI.show_button_container(false);
        }


    }
    public void Dirt_get_cay_muon_trong(String name)
    {

        cay_muon_trong = name;


    }
    //public void destroy_plant(String name)
    //{
    //    plant_prefab = plant;
    //    for (int i = 0; i < Plant_list.Count; i++)
    //    {
    //        if (Plant_list[i].name.Equals(name+"_plant"))
    //        {
    //            Destroy(Plant_list[i].obj);
    //            Plant_list.Remove(Plant_list[i]);
    //            if (Plant_list.Count < 1)
    //            {
    //                Plant_list = null;
    //                //Plant_list = new List<plant_property>();
    //            }
    //        }
    //    }
    //}
}
