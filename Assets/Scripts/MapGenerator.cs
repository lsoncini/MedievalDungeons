using System;
using System.Collections;

using UnityEngine;

[ExecuteInEditMode]
public class MapGenerator : MonoBehaviour
{
	// The pieces of dungeon
	public GameObject[] mapChunks;
	public int mapSize = 10;
    public Difficulty difficulty = Difficulty.Easy;
    public int seed;

    private GameObject[,] map;

    public void Generate()
	{
        UnityEngine.Random.seed = seed;

        map = new GameObject[mapSize, mapSize];

        for(int i = 0; i < mapSize; i++) {
            for(int j = 0; j < mapSize; j++) {
                GameObject newGO = Instantiate(mapChunks[getRandomChunkIndex()]) as GameObject;
                newGO.name = String.Format("dungeon-{0}-{1}", i, j);
                newGO.transform.parent = transform;
                
                newGO.transform.localPosition = new Vector3(i * 3, j * 3, 0);
                map[i, j] = newGO;
            }
        }
        
        // generate the array
        // populate the array
        // check everything
        // render everything
        print("MAP GENERATED!!");
		
	}
	
	public void ClearMap() {
        for (int i = 0; i < mapSize; i++) {
            for(int j = 0; j < mapSize; j++) {
                DestroyImmediate(map[i, j]);
            }
		}
    }
	
	private int prevIdx = 0;
	private int getRandomChunkIndex()
	{
		// checkear condiciones aca...
		prevIdx = UnityEngine.Random.Range(0, mapChunks.Length);
		
		return prevIdx;
	}
}
