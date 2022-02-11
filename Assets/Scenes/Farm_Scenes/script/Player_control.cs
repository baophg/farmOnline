using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_control : MonoBehaviour
{

    [SerializeField] private Transform cam_trans;
    [Range(0,9)]
    [SerializeField]private float smooth_cam_time = 6f;
    
    private Vector3 new_pos;
    private FloatingJoystick joy;
    private float do_xoay;
    [SerializeField] private float walk_speed=5;
    private Animator walk;
    [SerializeField]float velocity=0.01f;


    // Start is called before the first frame update
    void Start()
    {
        joy = FindObjectOfType<FloatingJoystick>();
        walk = this.GetComponent<Animator>();
       
    }

    // Update is called once per frame
    void Update()
    {
        
       
    }
    private void FixedUpdate()
    {
        Player_ctrl();
        //camera_follow();
        
    }

    private void Player_ctrl()
    {
     
        do_xoay = Mathf.Atan2(joy.Horizontal, joy.Vertical * 1) * Mathf.Rad2Deg + cam_trans.rotation.eulerAngles.y;
        float angle = Mathf.SmoothDampAngle(this.transform.rotation.eulerAngles.y, do_xoay, ref velocity,smooth_cam_time*Time.deltaTime);

        if (joy.Vertical != 0 && joy.Vertical <= 1 && joy.Vertical >= -1 || joy.Horizontal != 0 && joy.Horizontal <= 1 && joy.Horizontal >= -1)
        {
            this.transform.rotation = Quaternion.Euler(0, angle, 0);
            this.transform.Translate(Vector3.forward * walk_speed * Time.deltaTime);
            walk.SetBool("walk", true);
        }
        else
        {
            walk.SetBool("walk", false);
        };
    }

  
}
