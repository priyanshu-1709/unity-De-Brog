using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Kineractive
{
    [CustomEditor(typeof(Shortcut))]
    [CanEditMultipleObjects]
    public class CustomViewShortcut : UnityEditor.Editor
    {

        SerializedProperty Shortcuts;


        void OnEnable()
        {
            Shortcuts = serializedObject.FindProperty("Shortcuts");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            CustomViewHelper.DisplayTitle("ShortCut", CustomViewHelper.IconTypes.Shortcut);
            EditorGUILayout.BeginVertical(CustomViewHelper.BodyBG);

            EditorGUILayout.PropertyField(Shortcuts, true);
            EditorGUILayout.EndVertical();

            serializedObject.ApplyModifiedProperties();
        }
    }
}
