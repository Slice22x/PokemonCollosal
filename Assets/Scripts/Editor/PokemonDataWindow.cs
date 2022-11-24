using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using static UnityEngine.GUILayout;
using static UnityEditor.EditorGUILayout;

public class PokemonDataWindow : EditorWindow
{
    PokemonData editingPokemon;
    string pokemonName;

    [MenuItem("Window/Pokemon Data Creater")]
    public static void ShowWindow()
    {
        GetWindow<PokemonDataWindow>("Pokemon Data Creater");
    }

    private void OnGUI()
    {
        //Place pokemon sprite inside a label
        editingPokemon = (PokemonData)EditorGUILayout.ObjectField("Manager", editingPokemon, typeof(PokemonData), true);


        if (editingPokemon == null) 
        {
            pokemonName = TextField("Pokemon Name", pokemonName);

            EditorGUILayout.HelpBox("No Pokemon Editing (Set a pokemon to edit)", MessageType.Error);
            if (Button("Create"))
            {
                //Creates an empty instance of the PokemonData
                PokemonData p = CreateInstance<PokemonData>();

                //Creates the asset in the correct place and saves all assets
                AssetDatabase.CreateAsset(p, "Assets/ScriptableObject/Pokemon/" + pokemonName + ".asset");
                AssetDatabase.SaveAssets();

                PokemonDatabase.database.pokemonData.Add(p);

                editingPokemon = p;
            }
        } 

        if(editingPokemon != null)
        {
            SerializedObject so = new SerializedObject(editingPokemon);

            //Info
            SerializedProperty name = so.FindProperty("pokemonName");
            SerializedProperty desc = so.FindProperty("description");
            SerializedProperty ID = so.FindProperty("ID");

            PropertyField(name, true);
            PropertyField(desc, true);
            PropertyField(ID, true);

            GUILayout.Space(20f);

            //Base stats

            SerializedProperty type = so.FindProperty("type");
            PropertyField(type, true);

            Label("Base stats");
            SerializedProperty hp = so.FindProperty("baseHP");
            SerializedProperty atk = so.FindProperty("baseATK");
            SerializedProperty def = so.FindProperty("baseDEF");
            SerializedProperty spatk = so.FindProperty("baseSPAtk");
            SerializedProperty spdef = so.FindProperty("baseSPDef");
            SerializedProperty spd = so.FindProperty("baseSPD");

            PropertyField(hp, true);
            PropertyField(atk, true);
            PropertyField(def, true);
            PropertyField(spatk, true);
            PropertyField(spdef, true);
            PropertyField(spd, true);

            GUILayout.Space(20f);

            //Sprite
            Label("Pokemon Sprite");
            SerializedProperty s = so.FindProperty("sprite");
            EditorGUILayout.BeginHorizontal();
            PropertyField(s, true);
            Label(editingPokemon.frontSprite.texture ?? Texture2D.whiteTexture);
            EditorGUILayout.EndHorizontal();

            so.ApplyModifiedProperties();
        }

    }
}
