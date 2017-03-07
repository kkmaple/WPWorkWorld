﻿
using UnityEngine;
using UnityEditor;

namespace KMTool
{

    [CustomEditor(typeof(TweenRotation))]
    public class TweenRotationEditor : UITweenerEditor
    {
        public override void OnInspectorGUI()
        {
            GUILayout.Space(6f);

            EditorGUIUtility.labelWidth = 120f;

            TweenRotation tw = target as TweenRotation;
            GUI.changed = false;

            Vector3 from = EditorGUILayout.Vector3Field("From", tw.from);
            Vector3 to = EditorGUILayout.Vector3Field("To", tw.to);
            var quat = EditorGUILayout.Toggle("Quaternion", tw.quaternionLerp);

            if (GUI.changed)
            {
                KMEditorTools.RegisterUndo("Tween Change", tw);
                tw.from = from;
                tw.to = to;
                tw.quaternionLerp = quat;
                EditorUtility.SetDirty(tw);
            }

            DrawCommonProperties();
        }
    }
}