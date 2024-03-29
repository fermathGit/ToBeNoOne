﻿using UnityEngine;
using UnityEngine.UI;

namespace UnityEditor.UI
{
    // TODO REVIEW
    // Have material live under text
    // move stencil mask into effects *make an efects top level element like there is
    // paragraph and character

    /// <summary>
    /// Editor class used to edit UI Labels.
    /// </summary>

    [CustomEditor(typeof(TextVirtical), true)]
    [CanEditMultipleObjects]
    public class TextProEditor : GraphicEditor
    {
        SerializedProperty m_Text;
        SerializedProperty m_FontData;
        SerializedProperty IsVirtical;

        protected override void OnEnable()
        {
            base.OnEnable();
            m_Text = serializedObject.FindProperty("m_Text");
            IsVirtical = serializedObject.FindProperty("IsVirtical");
            m_FontData = serializedObject.FindProperty("m_FontData");
        }

        [MenuItem("GameObject/UI/TextVirtical")]
        static void TextPro()
        {
            GameObject parent = Selection.activeGameObject;
            RectTransform parentCanvasRenderer = (parent != null) ? parent.GetComponent<RectTransform>() : null;
            if (parentCanvasRenderer)
            {
                GameObject go = new GameObject("TextVirtical");
                go.transform.SetParent(parent.transform, false);
                go.AddComponent<RectTransform>();
                go.AddComponent<CanvasRenderer>();
                TextVirtical tp = go.AddComponent<TextVirtical>();
                tp.text = "New Text";
                Selection.activeGameObject = go;
            }
            else
            {
               
                EditorUtility.DisplayDialog("TextVirtical", "You must make the CricleImage object as a child of a Canvas.", "Ok");
            }
        }
        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.PropertyField(m_Text);
            EditorGUILayout.PropertyField(IsVirtical);
            EditorGUILayout.PropertyField(m_FontData);
            AppearanceControlsGUI();
            RaycastControlsGUI();
            serializedObject.ApplyModifiedProperties();
        }
    }
}