using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;


[CustomPropertyDrawer(typeof(SpriteSheet))]
public class SpriteSheetPropertyDrawer : PropertyDrawer
{
    Dictionary<string, ReorderableList> m_PerPropertyViewData = new Dictionary<string, ReorderableList>();

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        float height = 0;
        EditorGUI.BeginProperty(position, label, property);
        {
            height += EditorGUIUtility.singleLineHeight;

            Rect rect = new Rect(position.x, position.y + height, position.width, EditorGUIUtility.singleLineHeight);

            EditorGUI.PropertyField(rect,property.FindPropertyRelative("framesPerSecond"));

            ReorderableList list;
            if (!m_PerPropertyViewData.TryGetValue(property.propertyPath, out list))
            {
                list = CreateList(property.FindPropertyRelative("frames"));
                m_PerPropertyViewData[property.propertyPath] = list;
            }

            list.DoList(new Rect(rect.x,rect.y + EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing * 2, rect.width,rect.height));
        }
        EditorGUI.EndProperty();
    }


    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        ReorderableList list;
        if (m_PerPropertyViewData.TryGetValue(property.propertyPath, out list))
        {
            return base.GetPropertyHeight(property, label) + list.GetHeight() + EditorGUIUtility.singleLineHeight;
        }
        else
        {
            return base.GetPropertyHeight(property, label);
        }
    }

    /*
    private void OnEnable()
    {
        list = CreateList(serializedObject, serializedObject.FindProperty("frames"));
        *//*list.drawElementCallback =
    (Rect rect, int index, bool isActive, bool isFocused) =>
    {
        var element = list.serializedProperty.GetArrayElementAtIndex(index);
        rect.y += 2;

        List<float> heights = new List<float>(serializedObject.FindProperty("flowerStageSpriteSheetArray").arraySize);

        float height = EditorGUIUtility.singleLineHeight * 1.25f;
        height = EditorGUIUtility.singleLineHeight * 5;
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
            Array.Resize(ref floats, serializedObject.FindProperty("flowerStageSpriteSheetArray").arraySize);
            heights = floats.ToList();
        }

        float margin = height / 10;
        rect.y += margin;
        rect.height = (height / 5) * 4;
        rect.width = rect.width / 2 - margin / 2;

        //EditorGUI.DrawPreviewTexture(rect, s.texture);

        rect.x += rect.width + margin;

        EditorGUI.PropertyField(new Rect(rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight), element, GUIContent.none, true);
        //EditorGUI.PropertyField(new Rect(rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight), element.FindPropertyRelative("framesPerSecond"), GUIContent.none);
        //EditorGUI.PropertyField(new Rect(rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight), element.FindPropertyRelative("frames"), GUIContent.none);
    };*//*
    }
    */

    ReorderableList CreateList( SerializedProperty prop)
    {
        ReorderableList list = new ReorderableList(prop.serializedObject, prop, true, true, true, true);

        list.drawHeaderCallback = rect => {
            EditorGUI.LabelField(rect, "Spritesheet");
        };

        List<float> heights = new List<float>(prop.arraySize);

        list.drawElementCallback = (rect, index, active, focused) =>
        {
            SerializedProperty element = list.serializedProperty.GetArrayElementAtIndex(index);
            //Sprite s = (element.objectReferenceValue as Sprite);

            bool foldout = active;
            float height = EditorGUIUtility.singleLineHeight * 5;

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

            float margin = height / 10;
            rect.y += margin;
            rect.height = (height / 5) * 4;
            rect.width = rect.width / 2 - margin / 2;

            EditorGUI.PropertyField(new Rect(rect.x, rect.y, rect.width, rect.height), element, GUIContent.none);

            //rect.x += rect.width + margin;
            //EditorGUI.PropertyField(rect, element, GUIContent.none);
        };

        list.elementHeightCallback = (index) => {
            //Repaint();
            EditorUtility.SetDirty(prop.serializedObject.targetObject);
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
                prop.serializedObject.Update();
                li.serializedProperty.arraySize++;
                prop.serializedObject.ApplyModifiedProperties();
            });

            menu.ShowAsContext();

            float[] floats = heights.ToArray();
            Array.Resize(ref floats, prop.arraySize);
            heights = floats.ToList();
        };

        return list;
    }
}
