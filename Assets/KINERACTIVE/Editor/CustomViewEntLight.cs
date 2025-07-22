using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Kineractive
{
    [CustomEditor(typeof(Ent_Light))]
    [CanEditMultipleObjects]
    public class CustomViewEntLight : UnityEditor.Editor
    {

        SerializedProperty intensityStep;
        SerializedProperty rangeStep;

        SerializedProperty minIntensity;
        SerializedProperty maxIntensity;
        SerializedProperty minRange;
        SerializedProperty maxRange;


        void OnEnable()
        {
            intensityStep = serializedObject.FindProperty("intensityStep");
            rangeStep = serializedObject.FindProperty("rangeStep");

            minIntensity = serializedObject.FindProperty("minIntensity");
            maxIntensity = serializedObject.FindProperty("maxIntensity");
            minRange = serializedObject.FindProperty("minRange");
            maxRange = serializedObject.FindProperty("maxRange");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            CustomViewHelper.DisplayTitle("Light", CustomViewHelper.IconTypes.Light);
            EditorGUILayout.BeginVertical(CustomViewHelper.BodyBG);
            EditorGUILayout.PropertyField(intensityStep);
            EditorGUILayout.PropertyField(rangeStep);
            EditorGUILayout.EndVertical();

            EditorGUILayout.BeginVertical(CustomViewHelper.BodyBG);
            CustomViewHelper.DisplayHeader("Light Intensity");
            EditorGUILayout.PropertyField(minIntensity);
            EditorGUILayout.PropertyField(maxIntensity);
            EditorGUILayout.EndVertical();

            EditorGUILayout.BeginVertical(CustomViewHelper.BodyBG);
            CustomViewHelper.DisplayHeader("Light Range");
            EditorGUILayout.PropertyField(minRange);
            EditorGUILayout.PropertyField(maxRange);
            EditorGUILayout.EndVertical();

            serializedObject.ApplyModifiedProperties();
        }
    }
}
