using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Xml;
using UnityEditor;
using UnityEngine;


public class SVGParser : EditorWindow
{
    private string svgFilePath = "";
    private List<Vector3> roadPoints = new List<Vector3>();

    [MenuItem("Tools/SVG Road Importer")]
    public static void ShowWindow()
    {
        GetWindow<SVGParser>("SVG Road Importer");
    }

    void OnGUI()
    {
        GUILayout.Label("SVG Road Importer", EditorStyles.boldLabel);

        if (GUILayout.Button("Select SVG File"))
        {
            svgFilePath = EditorUtility.OpenFilePanel("Select SVG Road File", "", "svg");
            if (!string.IsNullOrEmpty(svgFilePath))
            {
                LoadSVGFile(svgFilePath);
            }
        }

        if (!string.IsNullOrEmpty(svgFilePath))
        {
            GUILayout.Label($"Selected File: {Path.GetFileName(svgFilePath)}");
        }

        if (GUILayout.Button("Visualize Road") && roadPoints.Count > 0)
        {
            VisualizePath();
        }
    }

    void LoadSVGFile(string filePath)
    {
        string svgContent = File.ReadAllText(filePath);
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(svgContent);

        XmlNodeList pathNodes = xmlDoc.GetElementsByTagName("path");
        roadPoints.Clear();

        foreach (XmlNode pathNode in pathNodes)
        {
            string dValue = pathNode.Attributes["d"].Value;
            ProcessPathData(dValue);
        }

        Debug.Log($"Loaded {roadPoints.Count} points from SVG.");
    }

    void ProcessPathData(string pathData)
    {
        string[] commands = pathData.Split(' ');

        foreach (var item in commands)
        {
            if (item == "l" || item == "m") continue;

            string[] cords = item.Split(',');

            roadPoints.Add(new Vector3(System.Convert.ToSingle(cords[0]), 0f, System.Convert.ToSingle(cords[1])));
        }
    }

    void VisualizePath()
    {
        GameObject roadObject = new GameObject("SVG Road Path");
        LineRenderer lineRenderer = roadObject.AddComponent<LineRenderer>();

        lineRenderer.positionCount = roadPoints.Count;
        lineRenderer.SetPositions(roadPoints.ToArray());

        lineRenderer.startWidth = 0.2f;
        lineRenderer.endWidth = 0.2f;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.startColor = Color.yellow;
        lineRenderer.endColor = Color.yellow;
    }
}