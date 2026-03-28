using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class HikerUnmesher : EditorWindow
{
    [MenuItem("Tools/HikersEditor/Additions/Unmeshing")]
    public static void OpenWindow()
    {
        HikerUnmesher window = GetWindow<HikerUnmesher>();
        window.titleContent = new GUIContent("Hikers Editor <UnmesherWindow>");
        window.Show();
    }
    private void OnGUI()
    {
        GUILayout.Label("Thanks for usin my editor window, i remade crackers \n unmesher to make it less of a setup C:", EditorStyles.boldLabel);

        if (GUILayout.Button("Unmesh Combine V2"))
        {
            unmesh1();
        }
    }
    void unmesh1()
    { // upgraded ver - original ver by cracker
        GameObject[] thing = Object.FindObjectsOfType<GameObject>();
        foreach (GameObject stuff in thing)
        {
            if (stuff.name.Contains("HeadModelOff") || stuff.name.Contains("Head Model Off"))
            {
                stuff.SetActive(false);
                Debug.Log("tung tung sahur model ");
            }
        }
        HeadModel[] heads = Object.FindObjectsOfType<HeadModel>(true);

        foreach (HeadModel headModel in heads)
        {

            Transform[] childTransforms = headModel.GetComponentsInChildren<Transform>(true);
            GameObject stuff = headModel.gameObject;
 
            foreach (Transform t in childTransforms)
            {
                GameObject stuff2 = t.gameObject;
                MeshRenderer mr = stuff2.GetComponent<MeshRenderer>();
                if (mr != null)
                {
                    mr.enabled = true;
                }
                if (t.name == "LMADY." || t.name == "LMAEG." || t.name == "LMAEA." || t.name == "LMAEF." || t.name == "LMAEJ." || t.name == "LMAEI." || t.name == "LMADZ." || t.name == "LMAEB." || t.name == "LMADW." || t.name == "LMAED." || t.name == "LMAEH." || t.name == "LMAEC." || t.name == "LMAEE.")
                {
                    if (t.parent.GetComponent<Renderer>() == null) return;
                    Debug.Log("i wanna disable mr " + t.parent.name);
                    t.parent.GetComponent<Renderer>().enabled = false;
                }
       
            }
        }

        CosmeticStand[] stands = Object.FindObjectsOfType<CosmeticStand>(true);

        foreach (CosmeticStand stand in stands)
        {
            if (stand.GetComponent<Renderer>() != null)
            {
                stand.GetComponent<Renderer>().enabled = false;
            }
            Transform[] childTransforms = stand.GetComponentsInChildren<Transform>(true);

            foreach (Transform t in childTransforms)
            {
                GameObject stuff = t.gameObject;
                MeshRenderer mr = stuff.GetComponent<MeshRenderer>();
                if (mr != null)
                {
                    mr.enabled = true;
                    Debug.Log("triple t ALSO disabled a mesh renderer.");
                }
            }
        }

        GameObject[] allObjects = Object.FindObjectsOfType<GameObject>(true);

        foreach (GameObject obj in allObjects)
        {
            MeshCollider meshCollider = obj.GetComponent<MeshCollider>();
            MeshFilter meshFilter = obj.GetComponent<MeshFilter>();
            MeshRenderer meshRenderer = obj.GetComponent<MeshRenderer>();
            var colidrs = obj.GetComponents<MeshCollider>();
            var collidr = obj.GetComponent<MeshCollider>();
            var filtr = obj.GetComponent<MeshFilter>();
            var rendr = obj.GetComponent<MeshRenderer>();

            if (colidrs != null && obj.name.ToLower().Contains("tombstone") && colidrs.Length > 1)
            {
                Mesh maybethetomb = findmeshPLEASEEEE(obj.name);
                if (maybethetomb)
                {
                    if (filtr == null) filtr = obj.AddComponent<MeshFilter>();
                    if (rendr == null) rendr = obj.AddComponent<MeshRenderer>();
                    filtr.sharedMesh = maybethetomb;
                    rendr.enabled = true;
                    UnityEngine.Debug.Log("fixed tombstone thingy on " + obj.name);
                }
                else
                {
                    UnityEngine.Debug.LogError("couldnt find replacement for tombstone");
                }
                continue;
            }


            if (colidrs != null && obj.name.ToLower().Contains("pumpkin") && colidrs.Length > 1)
            {
                Mesh maybethetomb = findmeshPLEASEEEE(obj.name);
                if (maybethetomb)
                {
                    if (filtr == null) filtr = obj.AddComponent<MeshFilter>();
                    if (rendr == null) rendr = obj.AddComponent<MeshRenderer>();
                    filtr.sharedMesh = maybethetomb;
                    rendr.enabled = true;
                    UnityEngine.Debug.Log("fixed pump kine thingy on " + obj.name);
                }
                else
                {
                    UnityEngine.Debug.LogError("couldnt find replacement for pump kine");
                }
                continue;
            }

            if (meshCollider != null && meshRenderer != null && meshFilter == null)
            {
                meshFilter = obj.AddComponent<MeshFilter>();
                meshFilter.sharedMesh = meshCollider.sharedMesh;
            
            }

            if (meshCollider != null && meshRenderer != null && meshCollider.enabled && !meshRenderer.enabled)
            {
                meshRenderer.enabled = true;
            }

            if (meshCollider != null && meshFilter != null)
            {
                meshFilter.sharedMesh = meshCollider.sharedMesh;
                meshFilter.gameObject.isStatic = true;
                UnityEngine.Debug.Log($"Mesh from MeshCollider applied to MeshFilter on {obj.name}");
            }

            else if (meshFilter != null && meshCollider == null)
            {
                GameObjectUtility.SetStaticEditorFlags(obj, 0);
                UnityEngine.Debug.Log($"Static flag turned off for {obj.name} because it only has a MeshFilter.");
            }
        }

        foreach (GameObject obj in GameObject.FindObjectsOfType<GameObject>())
        {
            if (obj.name.Contains("Uncover"))
            {
                obj.SetActive(false);
                obj.isStatic = false;
            }
        }
    }
    static Dictionary<string, Mesh> tufcache = new Dictionary<string, Mesh>();

    static Mesh findmeshPLEASEEEE(string name)
    {
        if (string.IsNullOrEmpty(name)) return null;
        if (tufcache.TryGetValue(name, out var thestuffweneed)) return thestuffweneed;

        var gooeyd = AssetDatabase.FindAssets($"t:Mesh {name}", new[] { "Assets" });
        foreach (var goo in gooeyd)
        {
            var wee = AssetDatabase.LoadAssetAtPath<Mesh>(AssetDatabase.GUIDToAssetPath(goo));
            if (wee && wee.name == name)
                return tufcache[name] = wee;
        }

        foreach (var goo in gooeyd)
            foreach (var oh in AssetDatabase.LoadAllAssetsAtPath(AssetDatabase.GUIDToAssetPath(goo)))
                if (oh is Mesh meshguy && meshguy.name == name)
                    return tufcache[name] = meshguy;

        return tufcache[name] = null;
    }
}
