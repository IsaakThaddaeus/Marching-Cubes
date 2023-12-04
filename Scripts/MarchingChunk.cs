using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshCollider))]

public class MarchingChunk : MonoBehaviour
{
    Mesh mesh;
    MeshCollider collider;

    public Bounds bounds;
    public List<MarchingCube> marchingCubes;
    public void initializeChunk(MarchingManager manager){
        mesh = GetComponent<MeshFilter>().mesh;
        collider = GetComponent<MeshCollider>();
        GetComponent<MeshRenderer>().material = manager.marterial;

        marchingCubes = new List<MarchingCube>();

        for (int x = 0; x < manager.numbreOfCubes; x++){
            for (int y = 0; y < manager.numbreOfCubes; y++){
                for (int z = 0; z < manager.numbreOfCubes; z++){
                    Vector3 position = new Vector3(x * manager.cubeSize, y * manager.cubeSize, z * manager.cubeSize);
                    marchingCubes.Add(new MarchingCube(position, manager));
                }
            }
        }

        bounds = getBounds(manager);
    }
    public void computeCubes(){
        List<Vector3> vertices = new List<Vector3>();

        foreach(MarchingCube mc in marchingCubes){
            vertices.AddRange(mc.computeCube());
        }

        mesh.Clear();
        int[] triangles = Enumerable.Range(0, vertices.Count).ToArray();
        
        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangles;
        mesh.RecalculateBounds();
        mesh.RecalculateNormals();

        if(mesh.vertices.Length > 0){
            collider.sharedMesh = mesh;
        }

    }
    public Bounds getBounds(MarchingManager manager)
    {
        Vector3 center = transform.position + Vector3.one * (manager.chunkSize / 2);
        Vector3 size = Vector3.one * manager.chunkSize;
        return new Bounds(center, size);
    }
}
