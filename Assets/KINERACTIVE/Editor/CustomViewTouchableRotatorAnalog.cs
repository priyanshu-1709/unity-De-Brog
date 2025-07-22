using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Kineractive
{
    [CustomEditor(typeof(RotatorAnalog))]
    [CanEditMultipleObjects]
    public class CustomViewTouchableRotatorAnalog : UnityEditor.Editor
    {
        SerializedProperty hinge;

        SerializedProperty dynamicFloatEvent;
        SerializedProperty minEvent;
        SerializedProperty maxEvent;
        SerializedProperty outOfMinEvent;
        SerializedProperty outOfMaxEvent;

        SerializedProperty increaseRotationSound;
        SerializedProperty decreaseRotationSound;
        SerializedProperty incVolume;
        SerializedProperty decVolume;

        SerializedProperty minRotation;
        SerializedProperty maxRotation;
        SerializedProperty rotateSpeedIncrease;
        SerializedProperty rotateSpeedDecrease;

        SerializedProperty startRotation;
        SerializedProperty rotationAxis;
        SerializedProperty coordinateSystem;


        void OnEnable()
        {
            hinge = serializedObject.FindProperty("hinge");
            dynamicFloatEvent = serializedObject.FindProperty("dynamicFloatEvent");
            minEvent = serializedObject.FindProperty("minEvent");
            maxEvent = serializedObject.FindProperty("maxEvent");
            outOfMinEvent = serializedObject.FindProperty("outOfMinEvent");
            outOfMaxEvent = serializedObject.FindProperty("outOfMaxEvent");
            increaseRotationSound = serializedObject.FindProperty("increaseRotationSound");
            decreaseRotationSound = serializedObject.FindProperty("decreaseRotationSound");

            incVolume = serializedObject.FindProperty("incVolume");
            decVolume = serializedObject.FindProperty("decVolume");

            minRotation = serializedObject.FindProperty("minRotation");
            maxRotation = serializedObject.FindProperty("maxRotation");
            rotateSpeedDecrease = serializedObject.FindProperty("rotateSpeedDecrease");
            rotateSpeedIncrease = serializedObject.FindProperty("rotateSpeedIncrease");
            startRotation = serializedObject.FindProperty("startRotation");
            rotationAxis = serializedObject.FindProperty("rotationAxis");
            coordinateSystem = serializedObject.FindProperty("coordinateSystem");

        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            CustomViewHelper.DisplayTitle("Rotator Analog", CustomViewHelper.IconTypes.RotatorAnalog);

            EditorGUILayout.BeginVertical(CustomViewHelper.BodyBG);
            EditorGUILayout.PropertyField(hinge);
            EditorGUILayout.PropertyField(rotationAxis);
            EditorGUILayout.PropertyField(coordinateSystem);
            EditorGUILayout.PropertyField(startRotation);
            EditorGUILayout.PropertyField(dynamicFloatEvent, new GUIContent("Send Normalized Float"));
            EditorGUILayout.EndVertical();

            EditorGUILayout.BeginVertical(CustomViewHelper.BodyBG);
            CustomViewHelper.DisplayHeader("Decrease Rotation");
            EditorGUILayout.PropertyField(minRotation);
            EditorGUILayout.PropertyField(rotateSpeedDecrease);

            EditorGUILayout.Space();
            EditorGUILayout.Space();

            EditorGUILayout.PropertyField(decreaseRotationSound, new GUIContent("Decrease Sound"));
            EditorGUILayout.PropertyField(decVolume);

            EditorGUILayout.Space();
            EditorGUILayout.Space();

            EditorGUILayout.PropertyField(minEvent, new GUIContent("When Rotation Minimum Is Reached"));

            EditorGUILayout.Space();
            EditorGUILayout.Space();
            EditorGUILayout.PropertyField(outOfMaxEvent, new GUIContent("When rotating out of Max Rotation"));

            EditorGUILayout.EndVertical();

            EditorGUILayout.BeginVertical(CustomViewHelper.BodyBG);
            CustomViewHelper.DisplayHeader("Increase Rotation");
            EditorGUILayout.PropertyField(maxRotation);
            EditorGUILayout.PropertyField(rotateSpeedIncrease);
            
            EditorGUILayout.Space();
            EditorGUILayout.Space();

            EditorGUILayout.PropertyField(increaseRotationSound, new GUIContent("Increase Sound"));
            EditorGUILayout.PropertyField(incVolume);

            EditorGUILayout.Space();
            EditorGUILayout.Space();

            EditorGUILayout.PropertyField(maxEvent, new GUIContent("When Rotation Maximum Is Reached"));

            EditorGUILayout.Space();
            EditorGUILayout.Space();

            EditorGUILayout.PropertyField(outOfMinEvent, new GUIContent("When rotating out of Min Rotation"));
            EditorGUILayout.EndVertical();

     
            serializedObject.ApplyModifiedProperties();
        }
    }
}
