using UnityEditor;
using UnityEngine;

public class HikerPatches
{
    [MenuItem("Tools/HikersEditor/Extra/Patches")]
    public static void ShowPatches()
    {
        EditorUtility.DisplayDialog(
            "Hikers Editor Patch Notes",
            "Version 1.0.0\n- Release of Hikers Editor\n\nVersion 3\n- added new features and better things\n- Fixed bugs and issues",
            "Alright fuck you hiker"
        );
    }
}