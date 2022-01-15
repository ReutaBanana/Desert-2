using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(InputMananger))]
public class InputManangerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        InputMananger im = target as InputMananger;

        EditorGUI.BeginChangeCheck();

        base.OnInspectorGUI();

        if(EditorGUI.EndChangeCheck())
        {
            im.RefreshTracker();
        }
    }
}
