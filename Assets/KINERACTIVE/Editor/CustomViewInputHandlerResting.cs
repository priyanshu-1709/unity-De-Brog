using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Kineractive
{
    [CustomEditor(typeof(InputHandlerResting))]
    [CanEditMultipleObjects]
    public class CustomViewRestingInteractive : UnityEditor.Editor
    {

        SerializedProperty KineractiveInputs;


        void OnEnable()
        {
            KineractiveInputs = serializedObject.FindProperty("KineractiveInputs");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            CustomViewHelper.DisplayTitle("Input Handler Resting", CustomViewHelper.IconTypes.Resting);
            EditorGUILayout.BeginVertical(CustomViewHelper.BodyBG);
            CustomViewHelper.DisplayHeader("Inputs To Check");
            EditorGUILayout.PropertyField(KineractiveInputs, true);
            EditorGUILayout.EndVertical();

            serializedObject.ApplyModifiedProperties();
        }
    }
}
