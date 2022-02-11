using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class Third_ps_camera_ctrl : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler, IInitializePotentialDragHandler
{
    
    [SerializeField]private CinemachineFreeLook Third_ps_camera;
    
    public static float camera_rotation_speed = 5f;
    public static float camera_y_axis_speed = 0.03f;
    [SerializeField] private float donhaycamera = 5f;
    float camerarotate = 0, camerarotateb = 0;
    bool touchstart = false;
    bool touchstarton = false;
   
    float lastposx, newposx;
    float lastposy, newposy;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnPointerDown(PointerEventData eventData)
    {
       
        touchstarton = true;
        lastposx = eventData.position.x;
        lastposy = eventData.position.y;

    }

    public void OnPointerUp(PointerEventData eventData)
    {
        touchstarton = false;
        lastposx = 0;
        lastposy = 0;

    }
    
    public void OnDrag(PointerEventData eventData)
    {
        float camera_degx=0f;
        float camera_degy = 0f;



        if (touchstarton)
        {

            camera_degx = eventData.position.x;
            camera_degy = eventData.position.y;
           
            Third_ps_camera.m_XAxis.Value += (camera_degx - lastposx) * camera_rotation_speed * Time.deltaTime;
            lastposx = eventData.position.x;
            Third_ps_camera.m_YAxis.Value += (camera_degy - lastposy) * camera_y_axis_speed * Time.deltaTime * -1;
            lastposy = eventData.position.y;
           


        }



        // Debug.Log(eventData.position.x);
    }
    
    public void OnInitializePotentialDrag(PointerEventData eventData)
    {
        eventData.useDragThreshold = false;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("end_drag");
    }
}
