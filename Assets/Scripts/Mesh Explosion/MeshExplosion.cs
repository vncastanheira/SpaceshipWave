using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// Splits a mesh into several triangles and creates
// an explosion effect
public class MeshExplosion : MonoBehaviour
{
    public int minForce = 1;
    public int maxForce = 3;
    public int radius = 5;
    public string Tag;

    static Dictionary<string, GameObject> _partices;
    public static Dictionary<string, GameObject> Particles
    {
        get
        {
            if (_partices == null)
            {
                _partices = new Dictionary<string, GameObject>();
            }
            return _partices;
        }
        set
        {
            _partices = value;
        }
    }

    MeshFilter meshFilter;
    MeshRenderer meshRenderer;

    void Start()
    {
        meshFilter = GetComponent<MeshFilter>();
        meshRenderer = GetComponent<MeshRenderer>();
    }

    public void Explode()
    {
        if (string.IsNullOrEmpty(Tag))
        {
            Destroy(gameObject);
            Debug.Log("No Tag for Mesh Explosion on object" + gameObject.name);
        }
        else
        {
            if (!Particles.ContainsKey(Tag))
            {
                StartCoroutine(RuntimeSplitMesh());
            }
        }
    }

    public IEnumerator RuntimeSplitMesh()
    {
        Mesh M = meshFilter.mesh;
        Material[] materials = meshRenderer.materials;

        Vector3[] verts = M.vertices;
        Vector3[] normals = M.normals;
        Vector2[] uvs = M.uv;

        GameObject Pieces = new GameObject("Pieces");
        //Pieces.hideFlags = HideFlags.HideInHierarchy;

        for (int submesh = 0; submesh < M.subMeshCount; submesh++)
        {
            int[] indices = M.GetTriangles(submesh);

            for (int i = 0; i < indices.Length; i += 3)
            {
                Vector3[] newVerts = new Vector3[3];
                Vector3[] newNormals = new Vector3[3];
                Vector2[] newUvs = new Vector2[3];
                for (int n = 0; n < 3; n++)
                {
                    int index = indices[i + n];
                    newVerts[n] = verts[index];
                    newUvs[n] = uvs[index];
                    newNormals[n] = normals[index];
                }

                Mesh mesh = new Mesh();
                mesh.vertices = newVerts;
                mesh.normals = newNormals;
                mesh.uv = newUvs;

                mesh.triangles = new int[] { 0, 1, 2, 2, 1, 0 };

                GameObject GO = new GameObject("Triangle " + (i / 3), typeof(BoxCollider));
                GO.layer = LayerMask.NameToLayer("Particle");
                GO.transform.position = transform.position;
                GO.transform.rotation = transform.rotation;
                GO.transform.localScale = transform.localScale;
                GO.transform.SetParent(Pieces.transform);
                GO.AddComponent<MeshRenderer>().material = materials[submesh];
                GO.AddComponent<MeshFilter>().mesh = mesh;
                GO.AddComponent<BoxCollider>();
                var body = GO.AddComponent<Rigidbody>();
                Vector3 explosionPos = new Vector3(transform.position.x + Random.Range(-0.5f, 0.5f), transform.position.y + Random.Range(0f, 0.5f), transform.position.z + Random.Range(-0.5f, 0.5f));
                body.AddExplosionForce(Random.Range(minForce, maxForce), explosionPos, radius);
                //GO.hideFlags = HideFlags.HideInHierarchy;
                yield return null;
            }
        }
        Particles.Add(Tag, Pieces);
        var instance = Instantiate(Particles[Tag], transform.position, transform.rotation);
        instance.hideFlags = HideFlags.None;
        Destroy(instance, 3.0f + Random.Range(0.0f, 0.5f));
        Destroy(gameObject);

        GetComponent<Renderer>().enabled = false;
    }

    public static MeshCache LoadMeshPieces(Mesh M)
    {
        //Material[] materials = new Material[0];

        Vector3[] verts = M.vertices;
        Vector3[] normals = M.normals;
        Vector2[] uvs = M.uv;

        var cachedPieces = new List<GameObject>();

        for (int submesh = 0; submesh < M.subMeshCount; submesh++)
        {

            int[] indices = M.GetTriangles(submesh);
            for (int i = 0; i < indices.Length; i += 3)
            {
                Vector3[] newVerts = new Vector3[3];
                Vector3[] newNormals = new Vector3[3];
                Vector2[] newUvs = new Vector2[3];
                for (int n = 0; n < 3; n++)
                {
                    int index = indices[i + n];
                    newVerts[n] = verts[index];
                    newUvs[n] = uvs[index];
                    newNormals[n] = normals[index];
                }

                Mesh mesh = new Mesh();
                mesh.vertices = newVerts;
                mesh.normals = newNormals;
                mesh.uv = newUvs;

                mesh.triangles = new int[] { 0, 1, 2, 2, 1, 0 };

                GameObject GO = new GameObject("Triangle " + (i / 3));
                GO.layer = LayerMask.NameToLayer("Particle");
                //GO.transform.position = currentSelection.transform.position;
                //GO.transform.rotation = currentSelection.transform.rotation;
                //GO.transform.localScale = currentSelection.transform.localScale;

                //GO.AddComponent<MeshRenderer>().material = materials[submesh];
                GO.AddComponent<MeshFilter>().mesh = mesh;
                GO.AddComponent<BoxCollider>();
                cachedPieces.Add(GO);
                //var body = GO.AddComponent<Rigidbody>();
                //Vector3 explosionPos = new Vector3(transform.position.x + Random.Range(-0.5f, 0.5f), transform.position.y + Random.Range(0f, 0.5f), transform.position.z + Random.Range(-0.5f, 0.5f));
                //body.AddExplosionForce(Random.Range(minForce, maxForce), explosionPos, radius);
            }
        }
        MeshCache cache = ScriptableObject.CreateInstance<MeshCache>();
        //cache.Pieces = cachedPieces.ToArray();
        return cache;
    }
}