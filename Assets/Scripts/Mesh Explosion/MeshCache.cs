using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class MeshCache : ScriptableObject
{
    public MeshInfo[] Pieces;
}

public class MeshInfo : ScriptableObject
{
    public Vector3[] vertices;
    public Vector3[] normals;
    public Vector2[] uvs;
}
