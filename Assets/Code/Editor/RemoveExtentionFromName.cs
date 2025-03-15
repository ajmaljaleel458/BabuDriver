using UnityEngine;
using UnityEditor;
using System.IO;
public class RemoveExtentionFromName : EditorWindow
{
    private string _fbxPath;

    [MenuItem("Tools/FBX Renamer")]
    public static void ShowWindow()
    {
        GetWindow<RemoveExtentionFromName>();
    }

    void OnGUI()
    {
        GUILayout.Label("FBX Path:", EditorStyles.boldLabel);
        _fbxPath = EditorGUILayout.TextField(_fbxPath);

        if (GUILayout.Button("Rename Objects"))
        {
            ExtractMeshes(_fbxPath);
        }
    }

    private void RenameFBXObjects(string fbxPath)
    {
        if (string.IsNullOrEmpty(fbxPath))
        {
            Debug.LogWarning("Please enter the path to the FBX file.");
            return;
        }

        // 1. Locate the FBX file
        string[] guids = AssetDatabase.FindAssets(fbxPath);

        if (guids.Length == 0)
        {
            Debug.LogWarning($"FBX file not found at path: {fbxPath}");
            return;
        }

        string guid = guids[0];
        string assetPath = AssetDatabase.GUIDToAssetPath(guid);

        // 2. Load the FBX data
        ModelImporter importer = (ModelImporter)AssetImporter.GetAtPath(assetPath);

        if (importer == null)
        {
            Debug.LogWarning($"Could not load ModelImporter for FBX: {assetPath}");
            return;
        }

        // 3. Iterate through the objects and rename
        // (This part would require iterating through the ModelImporter's data)
        // and using the ModelImporter to rename the objects.
        // This is a placeholder for the actual renaming logic.

        // 4. Apply changes
        AssetDatabase.Refresh();
        Debug.Log($"Renamed FBX objects in: {assetPath}");
    }

    private void ExtractMeshes(string fbxPath)
    {
        if (string.IsNullOrEmpty(fbxPath) || !AssetDatabase.IsValidFolder(Path.GetDirectoryName(fbxPath)))
        {
            Debug.LogError("Invalid FBX Path");
            return;
        }

        Object[] assets = AssetDatabase.LoadAllAssetsAtPath(fbxPath);

        foreach (Object asset in assets)
        {
            if (asset != null)
            {
                string originalName = asset.name;
                Debug.Log($"Original Name: {originalName}");

                if (originalName.EndsWith(".tga", System.StringComparison.OrdinalIgnoreCase))
                {
                    string newName = originalName.Substring(0, originalName.Length - 4);
                    asset.name = newName;
                    Debug.Log($"Renamed to: {newName}");
                }
            }
        }

        // Save the asset changes
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }
}