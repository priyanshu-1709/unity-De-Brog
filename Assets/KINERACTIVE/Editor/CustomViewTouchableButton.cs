using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Kineractive
{
    [CustomEditor(typeof(ButtonTouchable))]
    [CanEditMultipleObjects]
    public class CustomViewTouchableButton : UnityEditor.Editor
    {
       
        SerializedProperty inPosition;

        SerializedProperty outPosition;

        SerializedProperty pressedPosition;

        SerializedProperty theButton;
        SerializedProperty startPushedIn;

        SerializedProperty onInPosition;
        SerializedProperty onOutPosition;
        SerializedProperty onPress;

        SerializedProperty inClip;
        SerializedProperty outClip;
        SerializedProperty pressedClip;

        SerializedProperty inClipVolume;
        SerializedProperty outClipVolume;
        SerializedProperty pressedClipVolume;

        SerializedProperty buttonIsToggle;



        void OnEnable()
        {
            buttonIsToggle = serializedObject.FindProperty("buttonIsToggle");

            outClip = serializedObject.FindProperty("outClip");
            inClip = serializedObject.FindProperty("inClip");
            pressedClip = serializedObject.FindProperty("pressedClip");

            outClipVolume = serializedObject.FindProperty("outClipVolume");
            inClipVolume = serializedObject.FindProperty("inClipVolume");
            pressedClipVolume = serializedObject.FindProperty("pressedClipVolume");

            inPosition = serializedObject.FindProperty("inPosition");
            outPosition = serializedObject.FindProperty("outPosition");
            pressedPosition = serializedObject.FindProperty("pressedPosition");

            theButton = serializedObject.FindProperty("theButton");

            startPushedIn = serializedObject.FindProperty("startPushedIn");

            onInPosition = serializedObject.FindProperty("onInPosition");
            onOutPosition = serializedObject.FindProperty("onOutPosition");
            onPress = serializedObject.FindProperty("onPress");

            onInPosition = serializedObject.FindProperty("onInPosition");






        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            CustomViewHelper.DisplayTitle("Button (Touchable)", CustomViewHelper.IconTypes.Button);

            EditorGUILayout.BeginVertical(CustomViewHelper.BodyBG);
                EditorGUILayout.PropertyField(theButton);

                EditorGUILayout.PropertyField(buttonIsToggle);
                EditorGUILayout.PropertyField(startPushedIn);


            EditorGUILayout.EndVertical();

            EditorGUILayout.BeginVertical(CustomViewHelper.BodyBG);
                CustomViewHelper.DisplayHeader("Button Pressed");

            EditorGUILayout.PropertyField(pressedPosition);
            
                EditorGUILayout.Space();
                EditorGUILayout.Space();

                EditorGUILayout.PropertyField(pressedClip);
                EditorGUILayout.PropertyField(pressedClipVolume);


                EditorGUILayout.Space();
                EditorGUILayout.Space();

                EditorGUILayout.PropertyField(onPress);
            EditorGUILayout.EndVertical();

            EditorGUILayout.BeginVertical(CustomViewHelper.BodyBG);
                CustomViewHelper.DisplayHeader("Button In");

                EditorGUILayout.PropertyField(inPosition);

                EditorGUILayout.Space();
                EditorGUILayout.Space();

                EditorGUILayout.PropertyField(inClip);
                EditorGUILayout.PropertyField(inClipVolume);
            
                EditorGUILayout.Space();
                EditorGUILayout.Space();

                EditorGUILayout.PropertyField(onInPosition);
            EditorGUILayout.EndVertical();

            EditorGUILayout.BeginVertical(CustomViewHelper.BodyBG);
                CustomViewHelper.DisplayHeader("Button Out");
                EditorGUILayout.PropertyField(outPosition);

                EditorGUILayout.Space();
                EditorGUILayout.Space();
            
                EditorGUILayout.PropertyField(outClip);
                EditorGUILayout.PropertyField(outClipVolume);

                EditorGUILayout.Space();
                EditorGUILayout.Space();

                EditorGUILayout.PropertyField(onOutPosition);
            
            EditorGUILayout.EndVertical();

            serializedObject.ApplyModifiedProperties();
        }
    }
}
