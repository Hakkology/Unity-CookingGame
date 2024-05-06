using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class SeparateMeshes : EditorWindow
{
    [MenuItem("Tools/Separate Meshes")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(SeparateMeshes));
    }

    void OnGUI()
    {
        if (GUILayout.Button("Separate Selected Meshes"))
        {
            Separate();
        }
    }

    private static void Separate()
    {
        GameObject selectedObject = Selection.activeGameObject;
        if (selectedObject == null)
        {
            Debug.LogError("No object selected.");
            return;
        }

        MeshFilter[] meshFilters = selectedObject.GetComponentsInChildren<MeshFilter>();
        foreach (MeshFilter meshFilter in meshFilters)
        {
            string assetPath = AssetDatabase.GetAssetPath(meshFilter.sharedMesh);
            if (!string.IsNullOrEmpty(assetPath))
            {
                CreateSeparateGameObject(meshFilter);
            }
        }
    }

    private static void CreateSeparateGameObject(MeshFilter meshFilter)
    {
        GameObject newObject = new GameObject(meshFilter.sharedMesh.name);
        newObject.transform.position = meshFilter.transform.position;
        newObject.transform.rotation = meshFilter.transform.rotation;
        newObject.transform.localScale = meshFilter.transform.localScale;
        newObject.AddComponent<MeshFilter>().sharedMesh = meshFilter.sharedMesh;
        newObject.AddComponent<MeshRenderer>().sharedMaterials = meshFilter.GetComponent<MeshRenderer>().sharedMaterials;

        Debug.Log("Created new object for mesh: " + meshFilter.sharedMesh.name);
    }
}