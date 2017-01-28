using System.IO;
using UnityEditor;
using UnityEngine;

public class MeshEditorWindow : EditorWindow
{
    public Object mesh;

    [MenuItem("Window/Mesh Editor")]
    static void Init()
    {
        // Get existing open window or if none, make a new one:
        MeshEditorWindow window = GetWindow<MeshEditorWindow>("Mesh Editor");
        window.Show();
    }

    private void OnGUI()
    {
        GUILayout.Space(10);
        mesh = EditorGUILayout.ObjectField("Mesh", mesh, typeof(Mesh), false);

        if (GUILayout.Button("Create Cached Mesh"))
        {
            var cache = MeshExplosion.LoadMeshPieces(((Mesh)mesh));
            var path = Path.Combine("Assets/Mesh Cache", "CachedMesh.asset");
            AssetDatabase.CreateAsset(cache, path);
            AssetDatabase.SaveAssets();

            EditorUtility.FocusProjectWindow();
            Selection.activeObject = cache;
        }
    }
}
