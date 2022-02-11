using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UI_ctrl : MonoBehaviour
{

    [SerializeField]
    private GameObject button_chongiong;
    [SerializeField]
    private GameObject button_trong_cay;
    [SerializeField]
    private GameObject button_container;
    [SerializeField]
    private GameObject list_chon_giong;
    [SerializeField]
    private GameObject UI_Plant_item;
    [SerializeField]
    private GameObject UI_Plant_item_list;
    [SerializeField]
    private ScrollRect ScrollRect_list_chon_giong;
    
    [SerializeField]
    private Image image_gieogiong;
    
    private string giong_dang_chon = "";
    [SerializeField]
    private GameObject Dirt_ctrl_system;
    
    public List<Plan_property> list_plant_object = new List<Plan_property>();

    private List<Plan_property> list_plant_dang_co = new List<Plan_property>();
    // Start is called before the first frame update
    void Start()
    {
        list_plant_dang_co.Add(new Plan_property(null, "Bap", null, null, null, null, null, 15, 60));
        list_plant_dang_co.Add(new Plan_property(null, "Dua_hau", null, null, null, null, null, 10, 60));
        list_plant_dang_co.Add(new Plan_property(null, "Nho", null, null, null, null, null, 25, 60));
        list_plant_dang_co.Add(new Plan_property(null, "Ca_chua", null, null, null, null, null, 30, 60));
        add_plant_item_to_UI();



    }
    //--------------------method for get list_plant_dang_co call by Dirt_ctrl
    public List<Plan_property> get_hat_giong_List()
    {
        return list_plant_dang_co;
      
    }

    // this method call by chon_gion Button for chose plant
    public void chong_giong(Button button)
    {
        string ten_giong=button.name;
        foreach (Plan_property item in list_plant_dang_co)
        {
            if (item.display_name.Equals(ten_giong))
            {
                image_gieogiong.sprite = item.Plant_icon;
                giong_dang_chon = ten_giong;
            }
        }
    }
    /// <summary>
    /// this method use for add plant you have to UI for chose plant
    /// </summary>
    /// <param name="add plant item to ui">add plant item to ui</param>
    private void add_plant_item_to_UI()
    {
        foreach (Plan_property item in list_plant_dang_co)
        {
           
            foreach (Plan_property item1 in list_plant_object)
            {
               
                if (item1.display_name.Equals(item.display_name))
                {
                    
                    item.Plant_icon = item1.Plant_icon;
                    item.plant_name = item1.plant_name;
                    item.display_name = item1.display_name;
                    item.plant_s = item1.plant_s;
                    item.plant_m = item1.plant_m;
                    item.plant_l = item1.plant_l;
                    item.plant_xl = item1.plant_xl;
                    var New_plant_item = Instantiate(UI_Plant_item, new Vector3(0, 0, 0), Quaternion.identity, UI_Plant_item_list.transform);
                    New_plant_item.transform.GetChild(3).GetComponent<Image>().sprite = item.Plant_icon;
                    New_plant_item.transform.GetChild(2).GetComponent<TextMeshProUGUI>().SetText(item.plant_name);
                    New_plant_item.transform.GetChild(1).GetComponent<TextMeshProUGUI>().SetText(item.so_luong + "");
                    New_plant_item.name = item.display_name;
                    New_plant_item.SetActive(true);
                }
            }


        }
        button_container.SetActive(true);
        list_chon_giong.SetActive(true);
        ScrollRect_list_chon_giong.content = UI_Plant_item_list.GetComponent<RectTransform>();
        ScrollRect_list_chon_giong.horizontalNormalizedPosition = 0f;
        list_chon_giong.SetActive(false);
        button_container.SetActive(false);

    }
    //this method use for show and hide gieo_giong button
    public void show_button_gieo_giong(bool show)
    {


        if (show)
        {
            button_chongiong.SetActive(true);
            button_trong_cay.SetActive(true);


        }
        else
        {

            button_chongiong.SetActive(false);
            button_trong_cay.SetActive(false);
            list_chon_giong.SetActive(false);
        }
    }
    public void show_button_container(bool show)
    {
        if (show)
        {
            button_container.SetActive(true);

        }
        else
        {

            button_container.SetActive(false);
            button_chongiong.transform.GetChild(0).gameObject.SetActive(true);
            button_chongiong.transform.GetChild(1).gameObject.SetActive(false);
            list_chon_giong.SetActive(false);

        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    //call by chon_giong_btn for select item to trong_cay
   
    public void trongcay()
    {
        Dirt_plant_ctrl dirt = Dirt_ctrl_system.GetComponent<Dirt_plant_ctrl>();
        dirt.spawn_plant(giong_dang_chon);
    }
   
}
