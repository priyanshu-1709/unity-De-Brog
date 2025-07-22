using System;
using UnityEngine;
using UnityEditor;

/// <summary>
/// This script changes the Inpsector view for AxisInput to make the serialized Input field appear at the top of the script in the inspector
/// </summary>

namespace Kineractive
{
    [CustomEditor(typeof(AnalogInput))]
    [CanEditMultipleObjects]
    public class CustomViewAnalogInput : UnityEditor.Editor
    {
        SerializedProperty BypassInput;
        SerializedProperty axisInput;
        SerializedProperty repeatingInput;
        SerializedProperty handSide;
        SerializedProperty position;
        SerializedProperty moveSpeed;
        SerializedProperty rotateSpeed;
        SerializedProperty returnPosition;
        SerializedProperty inputAnim;
        SerializedProperty OnInput;
        SerializedProperty inputEndAnim;
        SerializedProperty OnInputEnd;


        SerializedProperty axisInputString;
        SerializedProperty inputAnimString;
        SerializedProperty inputEndAnimString;

        SerializedProperty sendFloat;
        SerializedProperty invertInput;

        protected string[] inputChoices;
        int inputChoiceIndex = 0;

        protected string[] inputAnimChoices;
        int inputAnimChoiceIndex = 0;

        protected string[] inputEndAnimChoices;
        int inputEndAnimChoiceIndex = 0;


        void OnEnable()
        {
            BypassInput = serializedObject.FindProperty("BypassInput");
            axisInput = serializedObject.FindProperty("axisInput");
            repeatingInput = serializedObject.FindProperty("repeatingInput");
            sendFloat = serializedObject.FindProperty("SendFloat");
            handSide = serializedObject.FindProperty("handSide");
            position = serializedObject.FindProperty("position");
            moveSpeed = serializedObject.FindProperty("moveSpeed");
            rotateSpeed = serializedObject.FindProperty("rotateSpeed");
            returnPosition = serializedObject.FindProperty("returnPosition");
            inputAnim = serializedObject.FindProperty("inputAnim");
            OnInput= serializedObject.FindProperty("OnInput");
            inputEndAnim = serializedObject.FindProperty("inputEndAnim");
            OnInputEnd = serializedObject.FindProperty("OnInputEnd");
            invertInput = serializedObject.FindProperty("invertInput");

            KineractiveManager iMan = FindObjectOfType<KineractiveManager>();
            if (iMan != null)
            {
                if (iMan.PlayerInputs != null)
                {
                    inputChoices = iMan.PlayerInputs.AxisInputs;
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



            axisInputString = serializedObject.FindProperty("axisInputString");
            inputChoiceIndex = Array.IndexOf(inputChoices, axisInputString.stringValue);

            inputAnimString = serializedObject.FindProperty("inputAnimString");
            inputAnimChoiceIndex = Array.IndexOf(inputAnimChoices, inputAnimString.stringValue);

            inputEndAnimString = serializedObject.FindProperty("inputEndAnimString");
            inputEndAnimChoiceIndex = Array.IndexOf(inputEndAnimChoices, inputEndAnimString.stringValue);

        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            CustomViewHelper.DisplayTitle("Analog Input", CustomViewHelper.IconTypes.AnalogInput);
            EditorGUILayout.BeginVertical(CustomViewHelper.BodyBG);
            CustomViewHelper.DisplayHeader("Input Type");


            inputChoiceIndex = EditorGUILayout.Popup("Axis Input", inputChoiceIndex, inputChoices);
            if (inputChoiceIndex < 0)
                inputChoiceIndex = 0;

            axisInputString.stringValue = inputChoices[inputChoiceIndex];

            EditorGUILayout.PropertyField(invertInput);

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
            CustomViewHelper.DisplayHeader("Analog Event");
            EditorGUILayout.PropertyField(sendFloat);
            EditorGUILayout.EndVertical();

            EditorGUILayout.BeginVertical(CustomViewHelper.BodyBG);
            CustomViewHelper.DisplayHeader("When Input Starts (Axis is not 0)");
            EditorGUILayout.PropertyField(position, new GUIContent("Move\\Rotate to:", "The hand will move and rotate to this position when key is pressed (and/or held) down"));


            inputAnimChoiceIndex = EditorGUILayout.Popup("Input Animation", inputAnimChoiceIndex, inputAnimChoices);
            if (inputAnimChoiceIndex < 0)
                inputAnimChoiceIndex = 0;

            inputAnimString.stringValue = inputAnimChoices[inputAnimChoiceIndex];

            EditorGUILayout.PropertyField(OnInput, new GUIContent("On Input Start"));
            EditorGUILayout.EndVertical();

            EditorGUILayout.BeginVertical(CustomViewHelper.BodyBG);
            CustomViewHelper.DisplayHeader("When Input Ends (Axis is 0)");
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