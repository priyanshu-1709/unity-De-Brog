using System;
using UnityEngine;
using UnityEditor;

namespace Kineractive
{
    [CustomEditor(typeof(ButtonInput))]
    [CanEditMultipleObjects]
    public class CustomViewBinaryInput : UnityEditor.Editor
    {
        SerializedProperty BypassInput;

        SerializedProperty repeatingInput;

        
        SerializedProperty handSide;
        SerializedProperty position;
        SerializedProperty moveSpeed;
        SerializedProperty rotateSpeed;
        SerializedProperty returnPosition;

        SerializedProperty OnInput;

        SerializedProperty OnInputEnd;

        SerializedProperty buttonInputString;
        SerializedProperty inputAnimString;
        SerializedProperty inputEndAnimString;

        protected string[] inputChoices; 
        int inputChoiceIndex = 0;

        protected string[] inputAnimChoices;
        int inputAnimChoiceIndex = 0;

        protected string[] inputEndAnimChoices;
        int inputEndAnimChoiceIndex = 0;

        void OnEnable()
        {
            BypassInput = serializedObject.FindProperty("BypassInput");

            repeatingInput = serializedObject.FindProperty("repeatingInput");
        

            handSide = serializedObject.FindProperty("handSide");
            position = serializedObject.FindProperty("position");
            moveSpeed = serializedObject.FindProperty("moveSpeed");
            rotateSpeed = serializedObject.FindProperty("rotateSpeed");
            returnPosition = serializedObject.FindProperty("returnPosition");

            OnInput = serializedObject.FindProperty("OnInput");

            OnInputEnd = serializedObject.FindProperty("OnInputEnd");

            
            KineractiveManager iMan = FindObjectOfType<KineractiveManager>();
            if (iMan != null)
            {
                if (iMan.PlayerInputs != null)
                {
                    inputChoices = iMan.PlayerInputs.ButtonInputs;
                }
                else
                {
                    inputChoices = new string[] { "No 'Player Inputs' set in Interactive Manager" };
                    Debug.LogWarning("'Player Inputs' field in Interactive Manager is empty. Please insert a Player Inputs scriptable object into the empty field.");
                }

                if (iMan.PlayerAnims != null)
                {
                    inputAnimChoices = iMan.PlayerAnims.Anims;
                    inputEndAnimChoices = iMan.PlayerAnims.Anims;
                }
                else
                {
                    inputAnimChoices = new string[] { "No 'Player Anims' set in Interactive Manager" };
                    Debug.LogWarning("'Player Anims' field in Interactive Manager is empty. Please insert a Player Anims scriptable object into the empty field.");
                }
            }
            else
            {
                inputChoices = new string[] { "Not Found: Interactive Manager" };
                inputAnimChoices = new string[] { "Not Found: Interactive Manager" };
                inputEndAnimChoices = new string[] { "Not Found: Interactive Manager" };
                Debug.LogWarning("Interactive Manager not found - Please add the Interactive Manager component to this scene");
            }

                

            buttonInputString = serializedObject.FindProperty("buttonInputString");
            inputChoiceIndex = Array.IndexOf(inputChoices, buttonInputString.stringValue);

            inputAnimString = serializedObject.FindProperty("inputAnimString");
            inputAnimChoiceIndex = Array.IndexOf(inputAnimChoices, inputAnimString.stringValue);

            inputEndAnimString = serializedObject.FindProperty("inputEndAnimString");
            inputEndAnimChoiceIndex = Array.IndexOf(inputEndAnimChoices, inputEndAnimString.stringValue);


        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();



            CustomViewHelper.DisplayTitle("Button Input", CustomViewHelper.IconTypes.ButtonInput);
            EditorGUILayout.BeginVertical(CustomViewHelper.BodyBG);

            CustomViewHelper.DisplayHeader("Input Type");

            inputChoiceIndex = EditorGUILayout.Popup("Button Input", inputChoiceIndex, inputChoices);
            if (inputChoiceIndex < 0)
                inputChoiceIndex = 0;

            buttonInputString.stringValue = inputChoices[inputChoiceIndex];
            EditorGUILayout.PropertyField(repeatingInput, new GUIContent("Repeating Input", ""));

            EditorGUILayout.EndVertical();

            EditorGUILayout.BeginVertical(CustomViewHelper.BodyBG);
            EditorGUILayout.PropertyField(BypassInput, new GUIContent("Bypass", "Turn off this input? Manually here, or by script, or by event."));
            EditorGUILayout.EndVertical();

            EditorGUILayout.BeginVertical(CustomViewHelper.BodyBG);
            CustomViewHelper.DisplayHeader("Hands");
            EditorGUILayout.PropertyField(handSide);
            EditorGUILayout.PropertyField(moveSpeed);
            EditorGUILayout.PropertyField(rotateSpeed);
            EditorGUILayout.EndVertical();

            EditorGUILayout.BeginVertical(CustomViewHelper.BodyBG);
            CustomViewHelper.DisplayHeader("When Input Starts (Button Is Down / Pushed)");
            EditorGUILayout.PropertyField(position, new GUIContent("Move\\Rotate to:", "The hand will move and rotate to this position when key is pressed (and/or held) down"));



            inputAnimChoiceIndex = EditorGUILayout.Popup("Input Animation", inputAnimChoiceIndex, inputAnimChoices);
            if (inputAnimChoiceIndex < 0)
                inputAnimChoiceIndex = 0;

            inputAnimString.stringValue = inputAnimChoices[inputAnimChoiceIndex];


            EditorGUILayout.PropertyField(OnInput, new GUIContent("On Input Start"));
            EditorGUILayout.EndVertical();

            EditorGUILayout.BeginVertical(CustomViewHelper.BodyBG);
            CustomViewHelper.DisplayHeader("When Input Ends (Button Is Up / Released)");
            EditorGUILayout.PropertyField(returnPosition);


            inputEndAnimChoiceIndex = EditorGUILayout.Popup("Input End Animation", inputEndAnimChoiceIndex, inputEndAnimChoices);
            if (inputEndAnimChoiceIndex < 0)
                inputEndAnimChoiceIndex = 0;

            inputEndAnimString.stringValue = inputEndAnimChoices[inputEndAnimChoiceIndex];

            EditorGUILayout.PropertyField(OnInputEnd, new GUIContent("On Input End"));
            EditorGUILayout.EndVertical();

            serializedObject.ApplyModifiedProperties();
        }
    }
}