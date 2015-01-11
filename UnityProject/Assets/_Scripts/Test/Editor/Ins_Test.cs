﻿using UnityEngine;
using UnityEditor;
using System.Collections;


[CustomEditor(typeof(Test))]
public class Ins_Test : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        Test test = target as Test;

        if (KMGUI.DrawHeader("DrawHeader"))
        {

            KMGUI.BeginContents();

            GUILayout.Label("in contents");

            KMGUI.EndContents();
            GUILayout.Label("DrawHeader");
        }

        GUIStyle style = new GUIStyle();
        style.name = "Float";
        Texture2D selectedBoxColour = new Texture2D(1, 1);
        selectedBoxColour.SetPixel(0, 0, test.color);
        selectedBoxColour.Apply();


        style.normal.background = selectedBoxColour;

        EditorGUILayout.BeginVertical(style, GUILayout.MinHeight(10f));

        GUILayout.Label("AS TextArea");
        GUILayout.Label("TextArea");

        EditorGUILayout.EndVertical();


        GUILayout.Label("DrawSeparator");

        KMGUI.DrawSeparator();

        if (KMGUI.Button("Add Button", Color.green))
        {
            Debug.Log("Add");
        }

        if (KMGUI.BtnDelete())
        {
            Debug.Log("del");
        }

        if (KMGUI.Button("按钮")) { }

    }
}
