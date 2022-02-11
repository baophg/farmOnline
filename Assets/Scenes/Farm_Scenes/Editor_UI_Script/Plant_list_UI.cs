using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
#if UNITY_EDITOR
[CustomEditor(typeof(UI_ctrl))]
public class Plant_list_UI : Editor
{

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        UI_ctrl ui = (UI_ctrl)target;
        GUILayout.BeginHorizontal();
       
        if (GUILayout.Button("Add 1 Plant object"))
        {
            ui.list_plant_object.Add(null);

        }
        if (GUILayout.Button("Remove 1 Plant object"))
        {
            ui.list_plant_object.RemoveAt(ui.list_plant_object.Count - 1);
        }
        GUILayout.EndHorizontal();
    }

}
#endif