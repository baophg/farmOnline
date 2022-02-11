using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Setting_menu_ctrl : MonoBehaviour
{
    private int setting_value;
    public TMP_Dropdown Graphic_dropdown;
    public Light light;
    static float x_axis_speed, y_axis_speed;
    // Start is called before the first frame update
    void Start()
    {
        QualitySettings.vSyncCount = 0;  // VSync must be disabled

        Application.targetFrameRate = 60;

        x_axis_speed = Third_ps_camera_ctrl.camera_rotation_speed;
        y_axis_speed = Third_ps_camera_ctrl.camera_y_axis_speed;
        if (PlayerPrefs.GetInt("Graphic_setting") != 0)
        {
            Graphic_setting(PlayerPrefs.GetInt("Graphic_setting")-1);
            Graphic_dropdown.value = PlayerPrefs.GetInt("Graphic_setting")-1;
        }
        else
        {
            Graphic_setting(2);
            Graphic_dropdown.value = 2;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Graphic_setting(int value)
    {
       
        PlayerPrefs.SetInt("Graphic_setting", value+1);
        if (value == 0)
        {
           
            QualitySettings.antiAliasing = 3;
            QualitySettings.resolutionScalingFixedDPIFactor = 1.5f;            
            QualitySettings.shadowResolution =ShadowResolution.VeryHigh;
            light.shadows = LightShadows.Soft;

        }
        if (value == 1)
        {
           
            QualitySettings.antiAliasing = 2;
            QualitySettings.resolutionScalingFixedDPIFactor = 1.5f;
            QualitySettings.shadowResolution = ShadowResolution.High;
            light.shadows = LightShadows.Soft;
        }
       
        if (value == 2)
        {
          
            QualitySettings.antiAliasing = 0;
            QualitySettings.resolutionScalingFixedDPIFactor = 1.5f;
            QualitySettings.shadowResolution = ShadowResolution.Medium;
          
            light.shadows = LightShadows.Hard;
        }
        if (value == 3)
        {
           
            QualitySettings.antiAliasing = 0;
            QualitySettings.resolutionScalingFixedDPIFactor = 1f;
            QualitySettings.shadowResolution = ShadowResolution.Low;
            Third_ps_camera_ctrl.camera_rotation_speed = x_axis_speed * 2f;
            Third_ps_camera_ctrl.camera_y_axis_speed = y_axis_speed * 2f;
            light.shadows = LightShadows.Hard;
        }
        if (value == 4)
        {

            QualitySettings.antiAliasing = 0;
            QualitySettings.resolutionScalingFixedDPIFactor = 0.7f;
            QualitySettings.shadowResolution = ShadowResolution.Low;
            Third_ps_camera_ctrl.camera_rotation_speed = x_axis_speed * 3f;
            Third_ps_camera_ctrl.camera_y_axis_speed = y_axis_speed *3f;
            light.shadows = LightShadows.Hard;
        }


    }
    public void time_pause()
    {

        Time.timeScale = 0;

    }
    public void time_continue()
    {

        Time.timeScale = 1;

    }
}
