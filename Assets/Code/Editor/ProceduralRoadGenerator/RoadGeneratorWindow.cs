using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class RoadGeneratorWindow : EditorWindow
{
    private List<Vector3> roadPoints = new List<Vector3>();

    private bool isDrawing = false;
    private GameObject roadContainer;
    private Material roadMaterial;

    [MenuItem("Tools/Procedural Road Generator")]
    public static void ShowWindow()
    {
        GetWindow<RoadGeneratorWindow>("Road Generator");
    }

    private void OnGUI()
    {
        GUILayout.Label("Road Generator", EditorStyles.boldLabel);

        roadMaterial = (Material)EditorGUILayout.ObjectField("Road Material", roadMaterial, typeof(Material), false);

        if (GUILayout.Button("Start Drawing"))
        {
            isDrawing = true;
            roadPoints.Clear();
        }

        if (GUILayout.Button("Generate Road"))
        {
            GenerateRoadMesh();
        }
    }

    private void OnSceneGUI(SceneView sceneView)
    {
        if (isDrawing)
        {
            HandleInput();
            DrawRoadPreview();
        }
    }

    private void HandleInput()
    {
        Event e = Event.current;
        if (e.type == EventType.MouseDown && e.button == 0)
        {
            Ray ray = HandleUtility.GUIPointToWorldRay(e.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                roadPoints.Add(hit.point);
                e.Use();
            }
        }
    }

    private void DrawRoadPreview()
    {
        for (int i = 0; i < roadPoints.Count - 1; i++)
        {
            Handles.DrawLine(roadPoints[i], roadPoints[i + 1]);
        }
        SceneView.RepaintAll();
    }

    private void GenerateRoadMesh()
    {
        if (roadPoints.Count < 2)
        {
            Debug.LogWarning("Need at least 2 points to generate a road.");
            return;
        }

        if (roadContainer == null)
        {
            roadContainer = new GameObject("Generated Road");
        }

        Mesh roadMesh = new Mesh();
        Vector3[] vertices = new Vector3[roadPoints.Count * 2];
        int[] triangles = new int[(roadPoints.Count - 1) * 6];

        for (int i = 0; i < roadPoints.Count; i++)
        {
            Vector3 dir = (i < roadPoints.Count - 1) ? (roadPoints[i + 1] - roadPoints[i]).normalized : (roadPoints[i] - roadPoints[i - 1]).normalized;
            Vector3 perpendicular = Vector3.Cross(dir, Vector3.up).normalized * 3f;

            vertices[i * 2] = roadPoints[i] - perpendicular;
            vertices[i * 2 + 1] = roadPoints[i] + perpendicular;
        }

        int index = 0;
        for (int i = 0; i < roadPoints.Count - 1; i++)
        {
            triangles[index++] = i * 2;
            triangles[index++] = i * 2 + 1;
            triangles[index++] = (i + 1) * 2;

            triangles[index++] = (i + 1) * 2;
            triangles[index++] = i * 2 + 1;
            triangles[index++] = (i + 1) * 2 + 1;
        }

        roadMesh.vertices = vertices;
        roadMesh.triangles = triangles;
        roadMesh.RecalculateNormals();

        GameObject road = new GameObject("RoadSegment");
        road.transform.SetParent(roadContainer.transform);
        road.AddComponent<MeshFilter>().mesh = roadMesh;
        road.AddComponent<MeshRenderer>().material = roadMaterial;
    }

    private void OnEnable()
    {
        SceneView.duringSceneGui += OnSceneGUI;
    }

    private void OnDisable()
    {
        SceneView.duringSceneGui -= OnSceneGUI;
    }
}

// This gives you a basic tool where you can click to place points, preview the road, and generate a simple road mesh. Next steps would be handling intersections and refining mesh generation! 🚀 Let me know if you want to take this further or add any features! 🔥
