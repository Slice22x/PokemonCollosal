using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

//[CustomEditor(typeof(PokemonBase), true)]
public class PokemonBaseEditor : Editor
{
    /*

    // The various categories the editor will display the variables in 
    public enum DisplayCategory
    {
        Stats, IV, EV
    }

    // The enum field that will determine what variables to display in the Inspector
    public DisplayCategory categoryToDisplay;

    // The function that makes the custom editor work
    public override void OnInspectorGUI()
    {
        // Display the enum popup in the inspector
        EditorGUILayout.PropertyField(serializedObject.FindProperty("data"));
        categoryToDisplay = (DisplayCategory)EditorGUILayout.EnumPopup("Display", categoryToDisplay);

        // Create a space to separate this enum popup from other variables 
        EditorGUILayout.Space();

        // Switch statement to handle what happens for each category
        switch (categoryToDisplay)
        {
            case DisplayCategory.Stats:
                DisplayStatInfo();
                break;

            case DisplayCategory.IV:
                DisplayIVInfo();
                break;

            case DisplayCategory.EV:
                DisplayEVInfo();
                break;

        }
        serializedObject.ApplyModifiedProperties();
    }

    // When the categoryToDisplay enum is at "Basic"
    void DisplayStatInfo()
    {
        EditorGUILayout.PropertyField(serializedObject.FindProperty("HP"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("attack"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("defence"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("spAtk"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("spDef"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("speed"));
    }

    // When the categoryToDisplay enum is at "Combat"
    void DisplayIVInfo()
    {
        EditorGUILayout.PropertyField(serializedObject.FindProperty("attack"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("attackRange"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("attackSpeed"));
    }

    // When the categoryToDisplay enum is at "Magic"
    void DisplayEVInfo()
    {
        EditorGUILayout.PropertyField(serializedObject.FindProperty("magicResistance"));

        // Store the hasMagic bool as a serializedProperty so we can access it
        SerializedProperty hasMagicProperty = serializedObject.FindProperty("hasMagic");

        // Draw a property for the hasMagic bool
        EditorGUILayout.PropertyField(hasMagicProperty);

        // Check if hasMagic is true
        if (hasMagicProperty.boolValue)
        {
            EditorGUILayout.PropertyField(serializedObject.FindProperty("mana"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("magicType"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("magicDamage"));
        }
    }
    */
}
