using UnityEditor;
using UnityEngine;

public static class SetHideFlags
{
    [MenuItem("Spinterblast/Set Hide Flags/Not Editable")]
    public static void SetNotEditableFlag()
    {
        var objects = Selection.gameObjects;
        if (objects == null) return;

        foreach (var go in objects)
        {
            go.hideFlags = HideFlags.NotEditable;
        }

        EditorApplication.RepaintHierarchyWindow();
        EditorApplication.DirtyHierarchyWindowSorting();
    }

    [MenuItem("Spinterblast/Set Hide Flags/None")]
    public static void SetNoneFlag()
    {
        var objects = Selection.gameObjects;
        if (objects == null) return;

        foreach (var go in objects)
        {
            go.hideFlags = HideFlags.None;
        }

        EditorApplication.RepaintHierarchyWindow();
        EditorApplication.DirtyHierarchyWindowSorting();
    }
}