using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Kineractive
{
    [CustomEditor(typeof(Mover))]
    [CanEditMultipleObjects]
    public class CustomViewTouchableMover : UnityEditor.Editor
    {


        SerializedProperty positionsArray;
        SerializedProperty eventsArray;
        SerializedProperty audioClips;
        SerializedProperty audioVolume;
        SerializedProperty positionToStartAt;
        SerializedProperty movableObject;
        SerializedProperty canLoop;
        SerializedProperty setsOfPositions;


        void OnEnable()
        {
            positionsArray = serializedObject.FindProperty("positionsArray");
            eventsArray = serializedObject.FindProperty("eventsArray");
            positionToStartAt = serializedObject.FindProperty("positionToStartAt");
            movableObject = serializedObject.FindProperty("movableObject");
            canLoop = serializedObject.FindProperty("canLoop");


            audioClips = serializedObject.FindProperty("audioClips");
            audioVolume = serializedObject.FindProperty("audioVolume");

            setsOfPositions = serializedObject.FindProperty("setsOfPositions");






        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            CustomViewHelper.DisplayTitle("Mover", CustomViewHelper.IconTypes.Mover);

            EditorGUILayout.BeginVertical(CustomViewHelper.BodyBG);

            EditorGUILayout.PropertyField(movableObject);
            EditorGUILayout.PropertyField(canLoop);

            EditorGUILayout.PropertyField(setsOfPositions);
            
    
            EditorGUILayout.PropertyField(positionToStartAt);
            positionToStartAt.intValue = Mathf.Clamp(positionToStartAt.intValue, (int)0, (int)setsOfPositions.intValue - 1);


            eventsArray.arraySize = setsOfPositions.intValue;
            positionsArray.arraySize = setsOfPositions.intValue;
            audioClips.arraySize = setsOfPositions.intValue;
            audioVolume.arraySize = setsOfPositions.intValue;



            EditorGUILayout.EndVertical();
  

            for (int i = 0; i < positionsArray.arraySize; i++)
            {
                EditorGUILayout.BeginVertical(CustomViewHelper.BodyBG);
                CustomViewHelper.DisplayHeader("Position " + i);

                EditorGUILayout.PropertyField(positionsArray.GetArrayElementAtIndex(i), new GUIContent("Move To:", "local coordinates " + i));

                EditorGUILayout.Space();
                EditorGUILayout.Space();

                EditorGUILayout.PropertyField(audioClips.GetArrayElementAtIndex(i), new GUIContent("Play Sound", "play sound at position " + i));
                EditorGUILayout.PropertyField(audioVolume.GetArrayElementAtIndex(i), new GUIContent("Volume", "play sound at angle " + i));

                EditorGUILayout.Space();
                EditorGUILayout.Space();

                EditorGUILayout.PropertyField(eventsArray.GetArrayElementAtIndex(i), new GUIContent("Events", "Do action at position " + i));
                EditorGUILayout.EndVertical();
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}