using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shop_warehouse_ctrl : MonoBehaviour
{
    [SerializeField]
    private GameObject shop_scroll;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("trigger: " + other.gameObject.tag);
        if (other.gameObject.tag.Equals("Player"))
        {
            shop_scroll.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
