using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.EditorTools;
using UnityEngine;

public class AddColliderToSelect : EditorWindow
{
    [MenuItem("Tools/HikersEditor/Additions/Surface Overrider")]
    public static void OpenWindow()
    {
        AddColliderToSelect window = GetWindow<AddColliderToSelect>();
        window.titleContent = new GUIContent("Hikers Editor <Selection Surface>");
        window.position = new Rect(100, 100, 600, 400);
        window.Show();
    }

    private void OnGUI()
    {
        GUILayout.Label("Information\nSelect a object in hierarchy and click the button and it will add a surface override to all", EditorStyles.boldLabel);
        if (GUILayout.Button("Add Collider To Selected"))
        {
            thing(Selection.activeObject);
        }
    }
    void thing(Object obj)
    {
        GameObject objConvert = obj as GameObject;
        Transform[] children = objConvert.GetComponentsInChildren<Transform>();
        foreach (Transform c in children)
        {
            MeshCollider c1 = c.gameObject.GetComponent<MeshCollider>();
            if (c1 != null)
            {
      
                if (c1.GetComponent<GorillaSurfaceOverride>() != null)
                {
                    Debug.Log("already has override");
                    continue; 
                }
                else
                {
                    Debug.Log("adding");
                    c.gameObject.AddComponent<GorillaSurfaceOverride>();
                }
            }
        }
    }
}

