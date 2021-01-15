using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(CAS_GridManager), true)]
public class GridManagerEditor : Editor
{

    GUISkin customSkin = null;
    CAS_GridManager grid = null;

    private void OnEnable()
    {
        grid = (CAS_GridManager)target;
        customSkin = Resources.Load<GUISkin>("EditorSkin");

    }


    private void OnSceneGUI()
    {
        for (int i = 0; i < grid.AllNodes.Count; i++)
        {
            Handles.Label(grid.AllNodes[i].Position + Vector3.up,
                $"F = {(grid.AllNodes[i].F == float.MaxValue ? "∞" : grid.AllNodes[i].F.ToString())} \n " +
                $"G = {(grid.AllNodes[i].G == float.MaxValue ? "∞" : grid.AllNodes[i].G.ToString())} \n " +
                $"H = {(grid.AllNodes[i].H == float.MaxValue ? "∞" : grid.AllNodes[i].H.ToString())} \n ",
            customSkin ? customSkin.GetStyle("gridLabel") : GetStyleBox());

        }
    }

    GUIStyle GetStyleBox()
    {
        GUIStyle _style = new GUIStyle(GUI.skin.box);
        _style.normal.textColor = Color.red;
        return _style;
    }

}
