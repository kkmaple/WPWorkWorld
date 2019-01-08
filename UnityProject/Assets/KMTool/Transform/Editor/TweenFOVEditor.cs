﻿
using UnityEngine;
using UnityEditor;

namespace KMTool
{

    [CustomEditor(typeof(TweenFOV))]
    public class TweenFOVEditor : UITweenerEditor
    {
        public override void OnInspectorGUI()
        {
            GUILayout.Space(6f);
            EditorGUIUtility.labelWidth = 120f;

            TweenFOV tw = target as TweenFOV;
            GUI.changed = false;

            float from = EditorGUILayout.Slider("From", tw.from, 1f, 180f);
            float to = EditorGUILayout.Slider("To", tw.to, 1f, 180f);

            if (GUI.changed)
            {
                KMEditorTools.RegisterUndo("Tween Change", tw);
                tw.from = from;
                tw.to = to;
                EditorUtility.SetDirty(tw);
            }

            DrawCommonProperties();
        }
    }
}