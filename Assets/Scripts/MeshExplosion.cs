using UnityEngine;
using System.Collections;

// Splits a mesh into several triangles and creates
// an explosion effect
 public class MeshExplosion : MonoBehaviour {
	 
	 public int minForce = 1;
	 public int maxForce = 3;
	 public int radius = 5;
     // If the original GameObject should be destroyed
     public bool destroy;

     // A cached prefab with the triangles that will exploded
	[Header("Experimental")]
	public GameObject CachedExplosionMesh;

	public void CopyExplode() {
		var model = Instantiate (gameObject, transform.position, transform.rotation);
		model.GetComponent<MeshExplosion> ().Explode ();

	}

	 public void Explode() 
     {
         print("Call SplitMesh");
		 StartCoroutine(SplitMesh());
	 }

     public IEnumerator SplitMesh ()    {
 
         if(GetComponent<MeshFilter>() == null || GetComponent<SkinnedMeshRenderer>() == null) {
             yield return null;
         }
 
         if(GetComponent<Collider>()) {
             GetComponent<Collider>().enabled = false;
         }
 
         Mesh M = new Mesh();
         if(GetComponent<MeshFilter>()) {
             M = GetComponent<MeshFilter>().mesh;
         }
         else if(GetComponent<SkinnedMeshRenderer>()) {
             M = GetComponent<SkinnedMeshRenderer>().sharedMesh;
         }
 
         Material[] materials = new Material[0];
         if(GetComponent<MeshRenderer>()) {
             materials = GetComponent<MeshRenderer>().materials;
         }
         else if(GetComponent<SkinnedMeshRenderer>()) {
             materials = GetComponent<SkinnedMeshRenderer>().materials;
         }
 
         Vector3[] verts = M.vertices;
         Vector3[] normals = M.normals;
         Vector2[] uvs = M.uv;


         for (int submesh = 0; submesh < M.subMeshCount; submesh++) {
 
             int[] indices = M.GetTriangles(submesh);

             GameObject Pieces = new GameObject("Pieces");
 
             for (int i = 0; i < indices.Length; i += 3)    {
                 Vector3[] newVerts = new Vector3[3];
                 Vector3[] newNormals = new Vector3[3];
                 Vector2[] newUvs = new Vector2[3];
                 for (int n = 0; n < 3; n++)    {
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
				if (destroy == true) {
					Destroy(GO, 3.0f + Random.Range(0.0f, 0.5f));
				}
             }
         }
 
         GetComponent<Renderer>().enabled = false;
     }
 
 
 }