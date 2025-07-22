using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Kineractive
{
    [CustomEditor(typeof(InputHandler))]
    [CanEditMultipleObjects]
    public class CustomViewInputHandler : UnityEditor.Editor
    {
        SerializedProperty usageInstructions;
        SerializedProperty controlsIconTexture;
        SerializedProperty crosshairTexture;
        SerializedProperty crosshairScale;
        SerializedProperty maxInteractionRange;
        SerializedProperty KineractiveInputs;



        void OnEnable()
        {
            usageInstructions = serializedObject.FindProperty("usageInstructions");
            controlsIconTexture = serializedObject.FindProperty("controlsIconTexture");
            crosshairTexture = serializedObject.FindProperty("crosshairTexture");
            crosshairScale = serializedObject.FindProperty("crosshairScale");
            maxInteractionRange = serializedObject.FindProperty("maxInteractionRange");
            KineractiveInputs = serializedObject.FindProperty("KineractiveInputs");

        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            if (!EditorApplication.isPlaying)
            {
                GameObject go = Selection.activeGameObject;
                InputHandler ih = go.GetComponent<InputHandler>();
                ih.enabled = false;
            }


            CustomViewHelper.DisplayTitle("Input Handler", CustomViewHelper.IconTypes.Trigger);
            EditorGUILayout.BeginVertical(CustomViewHelper.BodyBG);
            EditorGUILayout.PropertyField(maxInteractionRange);
            EditorGUILayout.EndVertical();
            EditorGUILayout.BeginVertical(CustomViewHelper.BodyBG);
            CustomViewHelper.DisplayHeader("UI & Feedback");
            EditorGUILayout.PropertyField(usageInstructions);
            EditorGUILayout.PropertyField(controlsIconTexture);
            EditorGUILayout.PropertyField(crosshairTexture);
            EditorGUILayout.PropertyField(crosshairScale);
            EditorGUILayout.EndVertical();

            EditorGUILayout.BeginVertical(CustomViewHelper.BodyBG);
            CustomViewHelper.DisplayHeader("Inputs To Check");
            EditorGUILayout.PropertyField(KineractiveInputs, true);
            EditorGUILayout.EndVertical();

            serializedObject.ApplyModifiedProperties();
        }
    }
}
