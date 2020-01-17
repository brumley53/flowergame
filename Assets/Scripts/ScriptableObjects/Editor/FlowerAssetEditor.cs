using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;


[CustomEditor(typeof(FlowerAsset))]
public class FlowerAssetEditor : Editor
{
    SerializedProperty flowerName;
    private ReorderableList list;
    SerializedProperty teaLeavesSprite;
    SerializedProperty timeToGrow;
    SerializedProperty growthCurve;

    private void OnEnable()
    {
        flowerName = serializedObject.FindProperty("flowerName");
        list = CreateList(serializedObject, serializedObject.FindProperty("flowerStageSpriteSheetArray"));
        teaLeavesSprite = serializedObject.FindProperty("teaLeavesSprite");
        timeToGrow = serializedObject.FindProperty("timeToGrow");
        growthCurve = serializedObject.FindProperty("growthCurve");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        EditorGUILayout.PropertyField(flowerName);
        list.DoLayoutList();
        EditorGUILayout.PropertyField(teaLeavesSprite);
        EditorGUILayout.PropertyField(timeToGrow);
        EditorGUILayout.PropertyField(growthCurve);
        serializedObject.ApplyModifiedProperties();
    }

    ReorderableList CreateList(SerializedObject obj, SerializedProperty prop)
    {
        ReorderableList list = new ReorderableList(obj, prop, true, true, true, true);

        list.drawHeaderCallback = rect => {
            EditorGUI.LabelField(rect, "Flower Stage Spritesheets");
        };

        List<float> heights = new List<float>(prop.arraySize);

        list.drawElementCallback = (rect, index, active, focused) => 
        {
            SerializedProperty element = list.serializedProperty.GetArrayElementAtIndex(index);
            //Sprite s = (element.objectReferenceValue as Sprite);

            bool foldout = active;

            float height = EditorGUIUtility.singleLineHeight + EditorGUI.GetPropertyHeight(element);

            try
            {
                heights[index] = height;
            }
            catch (ArgumentOutOfRangeException e)
            {
                Debug.LogWarning(e.Message);
            }
            finally
            {
                float[] floats = heights.ToArray();
                Array.Resize(ref floats, prop.arraySize);
                heights = floats.ToList();
            }

            //float margin = height / 10;
            //rect.y += margin;
            //rect.height = (height / 5) * 4;
            //rect.width = rect.width / 2 - margin / 2;
            EditorGUI.PropertyField(new Rect(rect.x, rect.y, rect.width, rect.height), list.serializedProperty.GetArrayElementAtIndex(index), GUIContent.none, true);
            GUI.Label(new Rect(rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight), "Flower Stage " + index);

            //SerializedProperty framesProperty = element.FindPropertyRelative("frames");
            //for (int i = 0; i < framesProperty.arraySize; i++)
            //{
            //    //Sprite s = framesProperty.GetArrayElementAtIndex(i).objectReferenceValue as Sprite;
            //    EditorGUI.PropertyField(new Rect(rect.x + i * margin, rect.y, rect.width, rect.height), framesProperty.GetArrayElementAtIndex(i), GUIContent.none);
            //}

            //rect.x += rect.width + margin;
            //EditorGUI.PropertyField(rect, element, GUIContent.none);
        };

        list.elementHeightCallback = (index) => {
            Repaint();
            float height = 0;

            try
            {
                height = heights[index];
            }
            catch (ArgumentOutOfRangeException e)
            {
                Debug.LogWarning(e.Message);
            }
            finally
            {
                float[] floats = heights.ToArray();
                Array.Resize(ref floats, prop.arraySize);
                heights = floats.ToList();
            }

            return height;
        };

        list.drawElementBackgroundCallback = (rect, index, active, focused) => {
            rect.height = heights[index];
            Texture2D tex = new Texture2D(1, 1);
            tex.SetPixel(0, 0, new Color(0.33f, 0.66f, 1f, 0.66f));
            tex.Apply();
            if (active)
                GUI.DrawTexture(rect, tex as Texture);
        };

        list.onAddDropdownCallback = (rect, li) => {
            var menu = new GenericMenu();
            menu.AddItem(new GUIContent("Add Element"), false, () => {
                serializedObject.Update();
                li.serializedProperty.arraySize++;
                serializedObject.ApplyModifiedProperties();
            });

            menu.ShowAsContext();

            float[] floats = heights.ToArray();
            Array.Resize(ref floats, prop.arraySize);
            heights = floats.ToList();
        };

        return list;
    }
}
