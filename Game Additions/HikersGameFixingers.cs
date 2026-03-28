using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.XR.CoreUtils;
using UnityEditor;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.SpatialTracking;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

public class HikersGameFixingers : EditorWindow
{ // 
    GameObject mainbody;
    private string[] options = new string[] { "Do Fix", "Undo Fix"};
    private int selectedIndex = 0;
    bool options2;
    [MenuItem("Tools/HikersEditor/Additions/GameFixers")]
    public static void OpenWindow()
    {
        HikersGameFixingers window = GetWindow<HikersGameFixingers>();
        window.titleContent = new GUIContent("Hikers Editor <GameFixerThing>");
        window.Show();
    }

    private void OnGUI()
    {
        GUILayout.Label("game fixers tab", EditorStyles.boldLabel);
        GUILayout.Label("XR:", EditorStyles.boldLabel);
        if (GUILayout.Button("Fix XR Manager"))
        {
            fixxrmanager();
        }
        if (GUILayout.Button("Fix XR Controller Stuff"))
        {
            xrfixthing();

        }
        if (GUILayout.Button("XR origin fix"))
        {
            fixxrorgiin();
        }
        GUILayout.Label("GT:", EditorStyles.boldLabel);
        if (GUILayout.Button("Fix GorillaPlayer stuff"))
        {
            gpfix();
        }


        options2 = GUILayout.Toggle(options2, "text fix drop down");

        if (options2)
        {
            GUILayout.Label("FYI: open and close a tab to refresh it :)");
            selectedIndex = EditorGUILayout.Popup("Do - Undo : ", selectedIndex, options);
            if (GUILayout.Button("Finish"))
            {
                if (selectedIndex == 0)
                {
                    textfixbecauseimtoolazytodoitinunityedtior();
                    options2 = false;
                }
                if (selectedIndex == 1)
                {
                    undotextfix();
                    options2 = false;
                }
            }
        }

        GUILayout.Label("RIG:", EditorStyles.boldLabel);
        if (GUILayout.Button("fix rig stuff"))
        {
            rigfixandifitdoesntworkillkillmyself();
        }
        GUILayout.Label("CAMERA FIXES:", EditorStyles.boldLabel);
        if (GUILayout.Button("fix la tung tung"))
        {
            tungtungtungsahurfuckedmewhilstiscreamedsohardandidontknowhy();
        }
    }
    // if (working == false) kill roasty return;
    // camera fixes:
    void tungtungtungsahurfuckedmewhilstiscreamedsohardandidontknowhy()
    {
        Camera cm = Camera.main;
        cm.gameObject.AddComponent<TrackedPoseDriver>(); // Oh My God Rail Me
    }
    // rig:
    void rigfixandifitdoesntworkillkillmyself()
    {
        // basic refs things
        GameObject r0 = GameObject.Find("Local Gorilla Player");
        if (r0 == null)
        {
            r0 = GameObject.Find("Actual Gorilla");
            if (r0 == null)
            {
                Debug.Log("i lowkey dont know where else your local player is :(");
            }
        }
        GameObject r1 = GameObject.Find("GorillaPlayer");
        GameObject r2 = GameObject.Find("LeftHand Controller");
        GameObject r3 = GameObject.Find("RightHand Controller");
        GameObject r4 = GameObject.Find("VR Constraints");
        // rigging shit
        Transform f1h = r4.transform.GetChild(0);
        Transform f2lc = r4.transform.GetChild(1).GetChild(0); // thesse js mean like "left controller" and stuff for short
        Transform f3rc = r4.transform.GetChild(2).GetChild(0); // im js praying atp
        if (f1h == null || f2lc == null || f3rc == null)
        {
            Debug.Log("la rig shit is null");
            // bottom shit js tells u if it null or no :fearful:
            Debug.Log(f1h.name);
            Debug.Log(f2lc.name);
            Debug.Log(f3rc.name);
        }
        else
        {
            if (f1h.GetComponent<MultiParentConstraint>() != null)
            { // head con fix
                Debug.Log("thankfully the thing wasnt null :)");
                f1h.GetComponent<MultiParentConstraint>().data.constrainedObject = r0.GetComponent<VRRig>().headMesh.transform;
                f1h.GetComponent<MultiParentConstraint>().data.sourceObjects.Add( // dont fucking ask me what this does, i honestly dont fucking know even know im the one that made that code
                        new WeightedTransform(r0.GetComponent<VRRig>().headConstraint, 1f)
                );
            }
            else
            { // fix for head cons
                Debug.Log("Sorry to say that ur multi like doesnt exist soo ill do it for you i guses :(");
                f1h.gameObject.AddComponent<MultiParentConstraint>();
                f1h.GetComponent<MultiParentConstraint>().data.constrainedObject = r0.GetComponent<VRRig>().headMesh.transform;
                f1h.GetComponent<MultiParentConstraint>().data.sourceObjects.Add( // dont fucking ask me what this does, i honestly dont fucking know even know im the one that made that code
                        new WeightedTransform(r0.GetComponent<VRRig>().headConstraint, 1f)
                );

            }
            if (f2lc.GetComponent<TwoBoneIKConstraint>() != null || f3rc.GetComponent<TwoBoneIKConstraint>() != null)
            {
                Debug.Log("omg this user sexy :)))");
                TwoBoneIKConstraint bl = f2lc.GetComponent<TwoBoneIKConstraint>();
                TwoBoneIKConstraint br = f3rc.GetComponent<TwoBoneIKConstraint>();
                if (r0.name == "Actual Gorilla")
                {
                    mainbody = r0.transform.GetChild(2).transform.GetChild(1).gameObject;
                }
                else if (r0.name == "Local Gorilla Player")
                {
                    mainbody = r0.transform.GetChild(4).transform.GetChild(1).gameObject;
                }
                    // totally good way to find objects, some of this code makes me wanna commit
                    // also dont ask me what the fuck the syntaxs var names mean?? idfk
                    Transform shl = mainbody.transform.Find("shoulder.L");
                Transform shr = mainbody.transform.Find("shoulder.R");
                Transform fl = shl.GetChild(0);
                Transform fr = shr.GetChild(0);
                Transform hl = fl.Find("hand.L");
                Transform hr = fr.Find("hand.R");
                // left hand sahur
                bl.data.root = shl;
                bl.data.mid = fl;
                bl.data.tip = hl;
                bl.data.target = bl.transform.Find("TargetWrist");
                // right hand sahur
                br.data.root = shr;
                br.data.mid = fr;
                br.data.tip = hr;
                br.data.target = br.transform.Find("TargetWrist");
                // YES i copied the below code for this, fuh tha
                // float setup sahur
                bl.data.targetPositionWeight = 1f;
                bl.data.targetRotationWeight = 1f;
                bl.data.hintWeight = 1f;
                br.data.hintWeight = 1f;
                br.data.targetPositionWeight = 1f;
                br.data.targetRotationWeight = 1f;
            }
            else
            {
                Debug.Log("fuck the user using this tool, i have to add this manually again :(");
                f2lc.gameObject.AddComponent<TwoBoneIKConstraint>();
                f3rc.gameObject.AddComponent<TwoBoneIKConstraint>();
                TwoBoneIKConstraint bl = f2lc.GetComponent<TwoBoneIKConstraint>();
                TwoBoneIKConstraint br = f3rc.GetComponent<TwoBoneIKConstraint>();
                GameObject mainbody = r0.transform.GetChild(4).transform.GetChild(1).gameObject; 
                // totally good way to find objects, some of this code makes me wanna commit
                // also dont ask me what the fuck the syntaxs var names mean?? idfk
                Transform shl = mainbody.transform.Find("shoulder.L");
                Transform shr = mainbody.transform.Find("shoulder.R");
                Transform fl = shl.GetChild(0);
                Transform fr = shr.GetChild(0);
                Transform hl = fl.Find("hand.L");
                Transform hr = fr.Find("hand.R");
                // left hand sahur
                bl.data.root = shl;
                bl.data.mid = fl;
                bl.data.tip = hl;
                bl.data.target = bl.transform.Find("TargetWrist");
                // right hand sahur
                br.data.root = shr;
                br.data.mid = fr;
                br.data.tip = hr;
                br.data.target = br.transform.Find("TargetWrist");
                // float setup sahur
                bl.data.targetPositionWeight = 1f;
                bl.data.targetRotationWeight = 1f;
                bl.data.hintWeight = 1f;
                br.data.hintWeight = 1f;
                br.data.targetPositionWeight = 1f;
                br.data.targetRotationWeight = 1f;
            }
        }
            r4.AddComponent<Rig>();
        if (r0.GetComponent<BoneRenderer>() == null)
        {
            r0.AddComponent<BoneRenderer>();
        }
        if (r0.GetComponent<RigBuilder>() == null)
        {
            r0.AddComponent<RigBuilder>();
            r0.GetComponent<RigBuilder>().layers.Add(new RigLayer(r4.GetComponent<Rig>()));
            r0.GetComponent<RigBuilder>().Build();
        }
        
        
    }
    // gt:
    void gpfix()
    {
        GameObject gp = GameObject.Find("GorillaPlayer");
        if (gp == null)
        {
            Debug.Log("ur project is FUCKED bro");
        }
        else
        {
            gp.AddComponent<LocomotionSystem>();
            gp.GetComponent<GorillaSnapTurn>().system = gp.GetComponent<LocomotionSystem>();
            if (gp.GetComponent<GorillaSnapTurn>().controllers.Count > 0)
            {
                gp.GetComponent<GorillaSnapTurn>().controllers.Add(GorillaTagger.Instance.rightHandTransform.GetComponent<XRController>());
            }
        }
    }

    void textfixbecauseimtoolazytodoitinunityedtior()
    {
     Font utopium = AssetDatabase.LoadAssetAtPath<Font>("Assets/Font/Utopium.asset");
        if (utopium == null)
        {
            Debug.LogError("grrr");
            return;
        }
        Debug.Log("Font loaded: " + utopium.name);
        foreach (Text t in FindObjectsOfType<Text>())
        {
            if (t != null)
                t.font = utopium;
        }
    }
    void undotextfix()
    {
        foreach (Text t in FindObjectsOfType<Text>())
        {

            Undo.RecordObject(t, "Clear Font");
            t.font = null;
            EditorUtility.SetDirty(t);
        }
    }
    // xr:
    void fixxrorgiin()
    {
        bool allowedtodo = false;
        GameObject d = GameObject.Find("Player VR Controller");
        if (d == null)
        {
            d = GameObject.Find("Player");
            if (d == null)
            {
                Debug.Log("i cant find the XR origin");
            }
            else
            {
                allowedtodo = true;
            }
        }
        else
        {
            allowedtodo = true;
        }
        if (allowedtodo)
        {
            if (d.GetComponent<XROrigin>() != null)
            {
                XROrigin g = d.GetComponent<XROrigin>();
                Debug.Log("ok we goin to skip dat xrorigin cool emoji sunglasses"); // we hear you
                g.CameraFloorOffsetObject = GameObject.Find("GorillaPlayer");
                g.Camera = Camera.main;
            }
            else
            {
                d.AddComponent<XROrigin>(); // fearful emoji more of it
                XROrigin f = d.GetComponent<XROrigin>();
                f.CameraFloorOffsetObject = GameObject.Find("GorillaPlayer");
                f.Camera = Camera.main;
            }
        }
    }
    void fixxrmanager()
    {
        GameObject xrmanager = GameObject.Find("XR Interaction Manager");
        if (xrmanager.GetComponent<XRInteractionManager>() == null)
        {
            Debug.Log("zir ImOnWork");
            xrmanager.AddComponent<XRInteractionManager>();
        }
        else
        {
            Debug.Log("its already on");
        }
    }
    void xrfixthing()
    {
        GameObject controller_1 = GameObject.Find("LeftHand Controller");
        GameObject controller_2 = GameObject.Find("RightHand Controller");
        if (controller_1.GetComponent<XRController>() != null)
        {
            Debug.Log("uh sir, i think you already have it on LeftHand");
        }
        if (controller_2.GetComponent<XRController>() != null)
        {
            Debug.Log("uh sir, i think you already have it on RightHand");
        }
        if (controller_1.GetComponent<XRController>() == null || controller_2.GetComponent<XRController>() == null)
        {
            // 1:
            XRController ctrller_1 = controller_1.AddComponent<XRController>();
            XRController ctrller_2 = controller_2.AddComponent<XRController>();
            ctrller_1.controllerNode = UnityEngine.XR.XRNode.LeftHand;
            Debug.Log("set up 1 - success");
            // 2:
            if (controller_1.GetComponent<XRDirectInteractor>() != null || controller_2.GetComponent<XRDirectInteractor>() != null)
            {
                Debug.Log("i think u already have direct interactor so ill skip it :)");
            }
            else
            {
                XRDirectInteractor xr1 = controller_1.AddComponent<XRDirectInteractor>();
                XRDirectInteractor xr2 = controller_2.AddComponent<XRDirectInteractor>();
                GameObject xrmnger = GameObject.Find("XR Interaction Manager");
                if (GameObject.Find("XR Interaction Manager").GetComponent<XRInteractionManager>() == null)
                {
                    Debug.Log("please do xr manager fix C:");
                }
                else
                {
                    XRInteractionManager m = xrmnger.GetComponent<XRInteractionManager>();
                    xr1.interactionManager = m;
                    xr2.interactionManager = m;
                    Debug.Log("direct thing dobne :)");
                }
            }
        }
    }
}
