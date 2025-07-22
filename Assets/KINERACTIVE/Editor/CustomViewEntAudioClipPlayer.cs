using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Kineractive
{
    [CustomEditor(typeof(Ent_AudioClipPlayer))]
    [CanEditMultipleObjects]
    public class CustomViewEntAudioClipPlayer : UnityEditor.Editor
    {

        SerializedProperty audioClip;
        SerializedProperty playOnStart;
        SerializedProperty interruptCurrentClip;
        SerializedProperty volumeChangeAmount;
        SerializedProperty pitchChangeAmount;
        SerializedProperty minPitch;
        SerializedProperty maxPitch;


        void OnEnable()
        {
            audioClip = serializedObject.FindProperty("audioClip");
            playOnStart = serializedObject.FindProperty("playOnStart");
            interruptCurrentClip = serializedObject.FindProperty("interruptCurrentClip");
            volumeChangeAmount = serializedObject.FindProperty("volumeChangeAmount");
            pitchChangeAmount = serializedObject.FindProperty("pitchChangeAmount");
            minPitch = serializedObject.FindProperty("minPitch");
            maxPitch = serializedObject.FindProperty("maxPitch");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            CustomViewHelper.DisplayTitle("Audio Clip Player", CustomViewHelper.IconTypes.Audio);
            EditorGUILayout.BeginVertical(CustomViewHelper.BodyBG);
            
            EditorGUILayout.PropertyField(audioClip);
            EditorGUILayout.PropertyField(playOnStart);

            EditorGUILayout.PropertyField(interruptCurrentClip);
            EditorGUILayout.EndVertical();

            EditorGUILayout.BeginVertical(CustomViewHelper.BodyBG);
            EditorGUILayout.PropertyField(volumeChangeAmount);
            EditorGUILayout.EndVertical();

            EditorGUILayout.BeginVertical(CustomViewHelper.BodyBG);
            EditorGUILayout.PropertyField(pitchChangeAmount);
            EditorGUILayout.PropertyField(minPitch);
            EditorGUILayout.PropertyField(maxPitch);
            EditorGUILayout.EndVertical();

            serializedObject.ApplyModifiedProperties();
        }
    }
}
