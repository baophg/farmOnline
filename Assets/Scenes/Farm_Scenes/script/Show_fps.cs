using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Show_fps : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI Textz;

    [SerializeField] private float _hudRefreshRate = 1f;

    private float _timer;

    private void Update()
    {
        if (Time.unscaledTime > _timer)
        {
            int fps = (int)(1f / Time.deltaTime);
            Textz.text = "FPS: " + fps;
            _timer = Time.unscaledTime + _hudRefreshRate;
        }
    }
}
