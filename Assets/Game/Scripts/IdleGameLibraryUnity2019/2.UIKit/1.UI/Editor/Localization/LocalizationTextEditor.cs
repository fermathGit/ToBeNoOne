﻿using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using System.Collections;

[CustomEditor(typeof(LocalizationText))]
public class LocalizationTextEditor : UnityEditor.UI.TextEditor
{

    public override void OnInspectorGUI()
    {
        LocalizationText component = (LocalizationText)target;
        base.OnInspectorGUI();
        component.KeyString = EditorGUILayout.TextField("Key String", component.KeyString);
        component.CustomFont = (UIFont)EditorGUILayout.ObjectField("Custom Font", component.CustomFont, typeof(UIFont), true);
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Is Open Localize",GUILayout.Width(140f));
        component.IsOpenLocalize = EditorGUILayout.Toggle(component.IsOpenLocalize);
        EditorGUILayout.EndHorizontal();
    }
}
