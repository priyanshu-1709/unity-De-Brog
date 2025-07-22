using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Kineractive
{
    public class CustomViewHelper : UnityEditor.Editor
    {
        public enum IconTypes
        {
            Manager,
            Trigger,
            Resting,
            KeyCodeInput,
            ButtonInput,
            AxisInput,
            SelfInput,
            Button,
            Rotator,
            RotatorAnalog,
            MoverAnalog,
            Light,
            Shortcut,
            Audio,
            Swapper,
            Mover,
            AnalogInput
        }


        private static GUIStyle GetIcon(IconTypes iconType)
        {

                GUIStyle managerIcon;
                Texture2D titleTexture = Resources.Load<Texture2D>(iconType.ToString());
                managerIcon = new GUIStyle(GUI.skin.box);
                managerIcon.normal.background = titleTexture;


                return managerIcon;
        }


        private static GUIStyle TitleText
        {
            get
            {
                GUIStyle titleText;

                Font titleFont = (Font)Resources.GetBuiltinResource(typeof(Font), "Arial.ttf");
                if (titleFont == null) { titleFont = EditorStyles.standardFont; }

                titleText = new GUIStyle(GUI.skin.label);
                titleText.font = titleFont;
                titleText.fontSize = 18;
                titleText.fontStyle = FontStyle.Bold;
                titleText.normal.textColor = (new Color(.2f,.2f,.2f));
                titleText.fixedHeight = 33f;
                titleText.alignment = TextAnchor.MiddleLeft;

                return titleText;
            }
        }

        private static GUIStyle HeadingText
        {
            get
            {
                GUIStyle headingText;
                Font heading1Font = (Font)Resources.GetBuiltinResource(typeof(Font), "Arial.ttf");

                headingText = new GUIStyle(GUI.skin.label);
                headingText.font = heading1Font;
                headingText.fontSize = 12;

                headingText.normal.textColor = (new Color(.0f, .66f, 1f));
                headingText.fixedHeight = 18f;
                headingText.fixedWidth = 300f;



                return headingText;
            }
        }

        private static GUIStyle TitleBG
        {
            get
            {
                GUIStyle titleBG;
                
                Texture2D bgTexture = Resources.Load<Texture2D>("BGTitle");

                titleBG = new GUIStyle(GUI.skin.box);
                titleBG.normal.background = bgTexture;

   

                return titleBG;
            }
        }

        private static GUIStyle HeaderBG
        {
            get
            {
                GUIStyle headerBG;
                
                Texture2D bgTexture = Resources.Load<Texture2D>("BGHeader");

                headerBG = new GUIStyle(GUI.skin.box);
                headerBG.normal.background = bgTexture;
                
                return headerBG;
            }
        }

        public static GUIStyle BodyBG
        {
            get
            {
                GUIStyle bodyBG;

                Texture2D bgTexture = Resources.Load<Texture2D>(EditorGUIUtility.isProSkin ? "BGGroupPro" : "BGGroupPersonal");

                bodyBG = new GUIStyle(GUI.skin.box);
                bodyBG.normal.background = bgTexture;
    
                return bodyBG;
            }
        }


        public static void DisplayTitle(string titleText, IconTypes iconType)
        {
            EditorGUILayout.BeginHorizontal(TitleBG);

            EditorGUILayout.LabelField("", GetIcon(iconType), GUILayout.Width(32f), GUILayout.Height(32f));

            GUILayout.Space(5);

            EditorGUILayout.LabelField(titleText, TitleText);

            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();
        }

        public static void DisplayHeader(string headerText)
        {
            EditorGUILayout.BeginHorizontal(HeaderBG);

            GUILayout.Space(5);

            EditorGUILayout.LabelField(headerText, HeadingText);

            GUILayout.FlexibleSpace();

            EditorGUILayout.EndHorizontal();
        }

        //Thanks alexanderameye from Unity Forum!
        public static void DrawUILine(Color color, int thickness = 2, int padding = 10)
        {
            Rect r = EditorGUILayout.GetControlRect(GUILayout.Height(padding + thickness));
            r.height = thickness;
            r.y += padding / 2;
            r.x -= 2;
            r.width += 6;
            EditorGUI.DrawRect(r, color);
        }
    }
}