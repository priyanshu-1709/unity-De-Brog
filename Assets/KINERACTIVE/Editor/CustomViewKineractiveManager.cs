using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Kineractive
{
    [CustomEditor(typeof(KineractiveManager))]
    [CanEditMultipleObjects]
    public class CustomViewKineractiveManager : UnityEditor.Editor
    {
        SerializedProperty interactChecksPerSec;
        SerializedProperty rayDistance;
        SerializedProperty rayOriginTransform;
        SerializedProperty layerMask;


        SerializedProperty returnToRestMoveSpeed;
        SerializedProperty returnToRestRotateSpeed;
        SerializedProperty idleMoveSpeed;
        SerializedProperty idleRotateSpeed;

        SerializedProperty leftHandHelper;
        SerializedProperty rightHandHelper;
        SerializedProperty leftHandRest;
        SerializedProperty rightHandRest;

        SerializedProperty handAnimator;


        SerializedProperty leftFootHelper;
        SerializedProperty rightFootHelper;


        SerializedProperty interactionText;
        SerializedProperty backgroundImage;
        SerializedProperty controlsIcon;
        SerializedProperty crosshair;
        SerializedProperty defaultCrosshairScale;

        SerializedProperty audioSourceObject;

        SerializedProperty playerInputs;
        SerializedProperty playerAnims;



        void OnEnable()
        {
            interactChecksPerSec = serializedObject.FindProperty("interactChecksPerSec");
            rayDistance = serializedObject.FindProperty("rayDistance");
            rayOriginTransform = serializedObject.FindProperty("rayOriginTransform");
            layerMask = serializedObject.FindProperty("layerMask");

            returnToRestMoveSpeed = serializedObject.FindProperty("returnToRestMoveSpeed");
            returnToRestRotateSpeed = serializedObject.FindProperty("returnToRestRotateSpeed");
            idleMoveSpeed = serializedObject.FindProperty("idleMoveSpeed");
            idleRotateSpeed = serializedObject.FindProperty("idleRotateSpeed");


            leftHandHelper = serializedObject.FindProperty("leftHandHelper");
            rightHandHelper = serializedObject.FindProperty("rightHandHelper");
            leftHandRest = serializedObject.FindProperty("leftHandRest");
            rightHandRest = serializedObject.FindProperty("rightHandRest");


            leftFootHelper = serializedObject.FindProperty("leftFootHelper");
            rightFootHelper = serializedObject.FindProperty("rightFootHelper");

            handAnimator = serializedObject.FindProperty("handAnimator");


            interactionText = serializedObject.FindProperty("interactionText");
            backgroundImage = serializedObject.FindProperty("backgroundImage");
            controlsIcon = serializedObject.FindProperty("controlsIcon");
            crosshair = serializedObject.FindProperty("crosshair");
            defaultCrosshairScale = serializedObject.FindProperty("defaultCrosshairScale");


            audioSourceObject = serializedObject.FindProperty("audioSourceObject");

            playerInputs = serializedObject.FindProperty("playerInputs");
            playerAnims = serializedObject.FindProperty("playerAnims");

        }

     

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            CustomViewHelper.DisplayTitle("Kineractive Manager", CustomViewHelper.IconTypes.Manager);

            EditorGUILayout.BeginVertical(CustomViewHelper.BodyBG);
                CustomViewHelper.DisplayHeader("Detection");
                EditorGUILayout.PropertyField(interactChecksPerSec, new GUIContent("Interact Checks Per/Sec", "How often the ray is fired to find \"Interactive Triggers\"."));
                EditorGUILayout.PropertyField(rayDistance, new GUIContent("Ray Distance", "How far the ray is fired."));
                EditorGUILayout.PropertyField(rayOriginTransform, new GUIContent("Ray Origin", "Start position of the ray."));
                EditorGUILayout.PropertyField(layerMask, new GUIContent("Layer Mask", "The ray will hit these layers only."));
            EditorGUILayout.EndVertical();

            EditorGUILayout.BeginVertical(CustomViewHelper.BodyBG);
                CustomViewHelper.DisplayHeader("Hands");

            EditorGUILayout.LabelField("Transforms", EditorStyles.boldLabel);
                EditorGUILayout.PropertyField(leftHandHelper, new GUIContent("Left Hand Helper", "The left hand IK always follows this transform. The \"Interaction_x\" scripts move this transform around."));
                EditorGUILayout.PropertyField(rightHandHelper, new GUIContent("Right Hand Helper", "The right hand IK always follows this transform. The \"Interaction_x\" scripts move this transform around."));
                EditorGUILayout.PropertyField(leftHandRest, new GUIContent("Left Hand Rest", "This is the default \"rest\" position\\rotation. The left hand will always return to here when no interactives are enabled - put this on your gun, steering wheel, joystick, etc."));
                EditorGUILayout.PropertyField(rightHandRest, new GUIContent("Right Hand Rest", "his is the default \"rest\" position\\rotation. The right hand will always return to here when no interactives are enabled - put this on your gun, steering wheel, joystick, etc."));


            EditorGUILayout.LabelField("", EditorStyles.boldLabel);
            EditorGUILayout.LabelField("Speeds", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(returnToRestMoveSpeed, new GUIContent("Return To Rest Move Speed", "How fast the hands return to the \"rest\" transform's position."));
            EditorGUILayout.PropertyField(returnToRestRotateSpeed, new GUIContent("Return To Rest Rotate Speed", "How fast the hands rotate to match \"rest\" transform rotation."));
            EditorGUILayout.PropertyField(idleMoveSpeed, new GUIContent("Idle Move Speed", "When the hands are at the \"rest\" position, the move speed is increased dramatically so that it can keep up with what they are attached to e.g. e.g. a moving joystick/wheel or a swaying gun etc."));
            EditorGUILayout.PropertyField(idleRotateSpeed,new GUIContent("Idle Rotate Speed", "When the hands are at the \"rest\" position, the rotation speed is increased dramatically so that it can keep up with what they are attached to e.g. a moving joystick/wheel or a swaying gun etc."));


            EditorGUILayout.LabelField("", EditorStyles.boldLabel);
            EditorGUILayout.LabelField("Animation", EditorStyles.boldLabel);
                EditorGUILayout.PropertyField(handAnimator, new GUIContent("Hand Animator", "The animator that controls what the fingers on the hand do (point, grip, thumbs up, etc). It is not required, as the IK does most of the work, but for games where the hands are visible up close, it looks better to have fingers moving to suit what they are touching."));
                EditorGUILayout.PropertyField(playerAnims, new GUIContent("Player Anims", "A list of animations which can be used by Kineractive"));
            EditorGUILayout.EndVertical();


            EditorGUILayout.BeginVertical(CustomViewHelper.BodyBG);
            CustomViewHelper.DisplayHeader("Feet");

            EditorGUILayout.PropertyField(leftFootHelper, new GUIContent("Left Foot Helper", "The left Foot IK always follows this transform. The \"Interaction_x\" scripts move this transform around."));
            EditorGUILayout.PropertyField(rightFootHelper, new GUIContent("Right Foot Helper", "The right Foot IK always follows this transform. The \"Interaction_x\" scripts move this transform around."));

            EditorGUILayout.EndVertical();


            EditorGUILayout.BeginVertical(CustomViewHelper.BodyBG);
                CustomViewHelper.DisplayHeader("UI");
                EditorGUILayout.PropertyField(interactionText, new GUIContent("Interaction Text", "This field helps other scripts find the Text box which they will use to put their interaction instructions on when the player looks at an interactive object"));      
                EditorGUILayout.PropertyField(backgroundImage, new GUIContent("Text Background", "This field helps other scripts find the  image which is used as a background for the text instructions")); 
                EditorGUILayout.PropertyField(controlsIcon, new GUIContent("Controls Icon", "This field helps other scripts find the icon field on the UI canvas - they will display their help icons here")); 
                EditorGUILayout.PropertyField(crosshair, new GUIContent("Crosshair", "This field helps other scripts find the crosshair on the UI canvas which changes to display various crosshairs or icons as set in the currently enabled Interactive Trigger object"));
                EditorGUILayout.PropertyField(defaultCrosshairScale, new GUIContent("Default Crosshair Scale", "The size the crosshair should return to after it has been modified by an Interactive Trigger"));
            EditorGUILayout.EndVertical();

            EditorGUILayout.BeginVertical(CustomViewHelper.BodyBG);
                CustomViewHelper.DisplayHeader("Audio");
                EditorGUILayout.PropertyField(audioSourceObject, new GUIContent("Audio Source", "An AudioSource moves to the current Interactive Trigger. This way we don't need to put an audiosource onto every single Interactive Trigger"));
            EditorGUILayout.EndVertical();

            serializedObject.ApplyModifiedProperties();

            EditorGUILayout.BeginVertical(CustomViewHelper.BodyBG);
                CustomViewHelper.DisplayHeader("Player Inputs");
                EditorGUILayout.PropertyField(playerInputs, new GUIContent("Player Inputs", "Select which Player Inputs scriptable object is used to configure the player inputs"));
                EditorGUILayout.EndVertical();

            serializedObject.ApplyModifiedProperties();
        }
    }
}