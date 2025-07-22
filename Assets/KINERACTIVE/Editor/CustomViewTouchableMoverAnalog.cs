using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Kineractive
{
    [CustomEditor(typeof(MoverAnalog))]
    [CanEditMultipleObjects]
    public class CustomViewTouchableMoverAnalog : UnityEditor.Editor
    {
        SerializedProperty objectToMove;

        SerializedProperty dynamicFloatEvent;
        SerializedProperty minEvent;
        SerializedProperty maxEvent;
        SerializedProperty outOfMinEvent;
        SerializedProperty outOfMaxEvent;

        SerializedProperty increasePositionSound;
        SerializedProperty decreasePositionSound;
        SerializedProperty incVolume;
        SerializedProperty decVolume;

        SerializedProperty minPosition;
        SerializedProperty maxPosition;
        SerializedProperty moveSpeedIncrease;
        SerializedProperty moveSpeedDecrease;

        SerializedProperty startPosition;
        SerializedProperty moveAxis;



        void OnEnable()
        {
            objectToMove = serializedObject.FindProperty("objectToMove");
            dynamicFloatEvent = serializedObject.FindProperty("dynamicFloatEvent");
            minEvent = serializedObject.FindProperty("minEvent");
            maxEvent = serializedObject.FindProperty("maxEvent");
            outOfMinEvent = serializedObject.FindProperty("outOfMinEvent");
            outOfMaxEvent = serializedObject.FindProperty("outOfMaxEvent");
            increasePositionSound = serializedObject.FindProperty("increasePositionSound");
            decreasePositionSound = serializedObject.FindProperty("decreasePositionSound");

            incVolume = serializedObject.FindProperty("incVolume");
            decVolume = serializedObject.FindProperty("decVolume");

            minPosition = serializedObject.FindProperty("minPosition");
            maxPosition = serializedObject.FindProperty("maxPosition");
            moveSpeedDecrease = serializedObject.FindProperty("moveSpeedDecrease");
            moveSpeedIncrease = serializedObject.FindProperty("moveSpeedIncrease");
            startPosition = serializedObject.FindProperty("startPosition");
            moveAxis = serializedObject.FindProperty("moveAxis");


        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            CustomViewHelper.DisplayTitle("Mover Analog", CustomViewHelper.IconTypes.MoverAnalog);

            EditorGUILayout.BeginVertical(CustomViewHelper.BodyBG);
            EditorGUILayout.PropertyField(objectToMove);
            EditorGUILayout.PropertyField(moveAxis);

            EditorGUILayout.PropertyField(startPosition);
            EditorGUILayout.Space();
            EditorGUILayout.PropertyField(dynamicFloatEvent, new GUIContent("Send Normalized Float"));
            EditorGUILayout.EndVertical();

            EditorGUILayout.BeginVertical(CustomViewHelper.BodyBG);
            CustomViewHelper.DisplayHeader("Decrease Position");
            EditorGUILayout.PropertyField(minPosition);
            EditorGUILayout.PropertyField(moveSpeedDecrease);

            EditorGUILayout.Space();
            EditorGUILayout.Space();

            EditorGUILayout.PropertyField(decreasePositionSound, new GUIContent("Decrease Sound"));
            EditorGUILayout.PropertyField(decVolume);

            EditorGUILayout.Space();
            EditorGUILayout.Space();

            EditorGUILayout.PropertyField(minEvent, new GUIContent("When Position Minimum Is Reached"));

            EditorGUILayout.Space();
            EditorGUILayout.Space();
            EditorGUILayout.PropertyField(outOfMaxEvent, new GUIContent("When moving out of Max Position"));

            EditorGUILayout.EndVertical();

            EditorGUILayout.BeginVertical(CustomViewHelper.BodyBG);
            CustomViewHelper.DisplayHeader("Increase Position");
            EditorGUILayout.PropertyField(maxPosition);
            EditorGUILayout.PropertyField(moveSpeedIncrease);

            EditorGUILayout.Space();
            EditorGUILayout.Space();
            EditorGUILayout.PropertyField(increasePositionSound, new GUIContent("Increase Sound"));
            EditorGUILayout.PropertyField(incVolume);

            EditorGUILayout.Space();
            EditorGUILayout.Space();
            EditorGUILayout.PropertyField(maxEvent, new GUIContent("When Position Maximum Is Reached"));

            EditorGUILayout.Space();
            EditorGUILayout.Space();

            EditorGUILayout.PropertyField(outOfMinEvent, new GUIContent("When moving out of Min Position"));

            EditorGUILayout.EndVertical();

            serializedObject.ApplyModifiedProperties();
        }
    }
}
