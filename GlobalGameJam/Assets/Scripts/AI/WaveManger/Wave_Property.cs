using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

#if UNITY_EDITOR
[CustomPropertyDrawer(typeof(Wave))]
public class Wave_Property : PropertyDrawer
{
    private float height = 20;

    private float labelHeight = 16;
    private float labelWeight = 120;
    private float propertyHeight = 16;
    private float propertyWeight = 30;

    public Rect Spawn_Label;
    public Rect Spawn;
    public Rect Rate_Label;
    public Rect Rate;

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return height * 2;
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

        int indent = EditorGUI.indentLevel;
        EditorGUI.indentLevel = 0;


        Spawn_Label = new Rect(position.x, position.y, labelWeight, labelHeight);
        Spawn = new Rect(Spawn_Label.position.x + labelWeight, position.y, propertyWeight, propertyHeight);

        Rate_Label = new Rect(position.x, position.y + labelHeight, labelWeight, labelHeight);
        Rate = new Rect(Rate_Label.position.x + labelWeight, position.y + propertyHeight, propertyWeight, propertyHeight);

        // Draw fields - passs GUIContent.none to each so they are drawn without labels
        EditorGUI.LabelField(Spawn_Label, "NumberToSpawn:");
        EditorGUI.PropertyField(Spawn, property.FindPropertyRelative("m_numberToSpawn"), GUIContent.none);
        EditorGUI.LabelField(Rate_Label, "SpawnRate:");
        EditorGUI.PropertyField(Rate, property.FindPropertyRelative("m_spawnRate"), GUIContent.none);
                 
        
        // Set indent back to what it was
        EditorGUI.indentLevel = indent;

        EditorGUI.EndProperty();
    }
}
#endif