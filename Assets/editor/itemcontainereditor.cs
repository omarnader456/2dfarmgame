using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
[CustomEditor(typeof(itemcontainer))]
public class itemcontainereditor : Editor
{
    public override void OnInspectorGUI()
    {
        itemcontainer container = target as itemcontainer;
        if (GUILayout.Button(" clear container"))
        {
            for (int i = 0; i < container.slots.Count; i++)
            {
                container.slots[i].clear();
            }
        }
        DrawDefaultInspector();
    }
}
