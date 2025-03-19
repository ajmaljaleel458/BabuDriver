using UnityEngine;
using UnityEditor;
using System.Linq;

public class AssignTransformsEditor : EditorWindow
{
    [MenuItem("Tools/Assign Transforms to Script")]
    public static void ShowWindow()
    {
        GetWindow<AssignTransformsEditor>("Assign Transforms");
    }

    private void OnGUI()
    {
        if (GUILayout.Button("Assign Transforms"))
        {
            AssignTransforms();
        }
    }

    private void AssignTransforms()
    {
        var targetComponent = Selection.gameObjects[0].GetComponent<WayPoint>();

        var fieldInfo = targetComponent.GetType().GetField("nextWayPoints");

#if UNITY_EDITOR
        Transform[] selectedTransforms = Selection.gameObjects.Skip(1).Select(go => go.transform).ToArray();
#endif

        fieldInfo.SetValue(targetComponent, selectedTransforms);

        EditorUtility.SetDirty(Selection.gameObjects[0]);

        Selection.gameObjects[0].transform.localScale = new Vector3(1f, 1f, 1f);
    }
}
