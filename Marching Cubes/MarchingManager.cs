using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class MarchingManager : MonoBehaviour
{

    public Vector3Int numbreOfChunks;
    public float chunkSize;
    public int numbreOfCubes;
    public Material marterial;
    public float maxDepth;
    public float cubeSize;

    public float initialHeight;
    public float initialHeightValue;

    public List<MarchingChunk> marchingChunks = new List<MarchingChunk>();
    
    void Start(){
        initializeChunks();
        initializeHeight();
    }
    void initializeChunks(){
        cubeSize = chunkSize / numbreOfCubes;

        for(int x = 0; x < numbreOfChunks.x; x++){
            for(int y = 0; y < numbreOfChunks.y; y++){
                for (int z = 0; z < numbreOfChunks.z; z++){
                    GameObject chunkGo = new GameObject("Chunk " + x + " | " + y + " | " + z, typeof(MarchingChunk));
                    chunkGo.transform.parent = transform;
                    chunkGo.transform.position = new Vector3(x * chunkSize, y * chunkSize, z * chunkSize) + this.transform.position;

                    MarchingChunk chunk = chunkGo.GetComponent<MarchingChunk>();
                    chunk.initializeChunk(this);
                    marchingChunks.Add(chunk); 
                }
            }
        }
    }
    void initializeHeight(){

        for (int i = 0; i < marchingChunks.Count; i++){
            MarchingChunk chunk = marchingChunks[i];

            for (int j = 0; j < Mathf.Pow(numbreOfCubes, 3); j++){
                MarchingCube cube = chunk.marchingCubes[j];

                for (int k = 0; k < 8; k++){
                    Vector3 pos = chunk.transform.TransformPoint(cube.positions[k]);
                    if(pos.y <= initialHeight){
                        cube.values[k] = initialHeightValue;
                    }

                    else{
                        cube.values[k] = -initialHeightValue;
                    }
                }
            }
            chunk.computeCubes();
        }
    }

    public void terraform(Vector3 position, float radius, float strength)
    {
        Bounds brushBounds = new Bounds(position, Vector3.one * radius * 2);

        for (int i = 0; i < marchingChunks.Count; i++){
            MarchingChunk chunk = marchingChunks[i];

            if (chunk.bounds.Intersects(brushBounds)){
                for (int j = 0; j < Mathf.Pow(numbreOfCubes, 3); j++){

                    MarchingCube cube = chunk.marchingCubes[j];

                    for (int k = 0; k < 8; k++){
                        Vector3 pos = chunk.transform.TransformPoint(cube.positions[k]);
                        float distance = Vector3.Distance(pos, position);

                        if (distance < radius){
                            float multiplyer = (radius - distance) / radius;
                            float value = cube.values[k] + multiplyer * strength;
                            cube.values[k] = value;
                        }
                    }
                }

                chunk.computeCubes();
            }
        }
    }

    private void OnDrawGizmos(){
        drawChunksOnGizmos();
    }
    void drawChunksOnGizmos(){
        for (int x = 0; x < numbreOfChunks.x; x++){
            for (int y = 0; y < numbreOfChunks.y; y++){
                for (int z = 0; z < numbreOfChunks.z; z++){

                    Vector3 center = new Vector3(x * chunkSize + chunkSize / 2, y * chunkSize + chunkSize / 2, z * chunkSize + chunkSize / 2) + this.transform.position;
                    Vector3 size = new Vector3(chunkSize, chunkSize, chunkSize);

                    Gizmos.color = Color.green;
                    Gizmos.DrawWireCube(center, size);
                }
            }
        }
    }


}
