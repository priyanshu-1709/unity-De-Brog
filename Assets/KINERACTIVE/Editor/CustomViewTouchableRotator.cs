using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Kineractive
{
    [CustomEditor(typeof(Rotator))]
    [CanEditMultipleObjects]
    public class CustomViewTouchableRotator : UnityEditor.Editor
    {


        SerializedProperty rotationsArray;
        SerializedProperty eventsArray;
        SerializedProperty audioClips;
        SerializedProperty audioVolume;
        SerializedProperty rotationToStartAt;
        SerializedProperty hinge;
        SerializedProperty canLoop;
        SerializedProperty setsOfRotations;


        void OnEnable()
        {
            rotationsArray = serializedObject.FindProperty("rotationsArray");
            eventsArray = serializedObject.FindProperty("eventsArray");
            rotationToStartAt = serializedObject.FindProperty("rotationToStartAt");
            hinge = serializedObject.FindProperty("hinge");
            canLoop = serializedObject.FindProperty("canLoop");


            audioClips = serializedObject.FindProperty("audioClips");
            audioVolume = serializedObject.FindProperty("audioVolume");

            setsOfRotations = serializedObject.FindProperty("setsOfRotations");






        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            CustomViewHelper.DisplayTitle("Rotator", CustomViewHelper.IconTypes.Rotator);

            EditorGUILayout.BeginVertical(CustomViewHelper.BodyBG);

            EditorGUILayout.PropertyField(hinge);
            EditorGUILayout.PropertyField(canLoop);

            EditorGUILayout.PropertyField(setsOfRotations);
            
    
            EditorGUILayout.PropertyField(rotationToStartAt);
            rotationToStartAt.intValue = Mathf.Clamp(rotationToStartAt.intValue, (int)0, (int)setsOfRotations.intValue - 1);


            eventsArray.arraySize = setsOfRotations.intValue;
            rotationsArray.arraySize = setsOfRotations.intValue;
            audioClips.arraySize = setsOfRotations.intValue;
            audioVolume.arraySize = setsOfRotations.intValue;

    

            EditorGUILayout.EndVertical();
  

            for (int i = 0; i < rotationsArray.arraySize; i++)
            {
                EditorGUILayout.BeginVertical(CustomViewHelper.BodyBG);
                CustomViewHelper.DisplayHeader("Rotation " + i);

                EditorGUILayout.PropertyField(rotationsArray.GetArrayElementAtIndex(i), new GUIContent("Rotate To:", "angles for rotation " + i));
                EditorGUILayout.Space();
                EditorGUILayout.Space();
                
                EditorGUILayout.PropertyField(audioClips.GetArrayElementAtIndex(i), new GUIContent("Play Sound", "play sound at angle " + i));
                EditorGUILayout.PropertyField(audioVolume.GetArrayElementAtIndex(i), new GUIContent("Volume", "volume of sound " + i));

                EditorGUILayout.Space();
                EditorGUILayout.Space();

                EditorGUILayout.PropertyField(eventsArray.GetArrayElementAtIndex(i), new GUIContent("Events", "Do action at angle " + i));
                EditorGUILayout.EndVertical();
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}