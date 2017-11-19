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

    private Transform mountTransform;

	public void Generate()
	{
		mountTransform = gameObject.transform;
		mountTransform.position = new Vector3(0, 0.1f, 0);


        // generate the array
        // populate the array
        // check everything
        // render everything
        print("MAP GENERATED!!");
		
	}
	
	public void ClearMap()
	{
		MapChunk[] mc = gameObject.GetComponentsInChildren<MapChunk>();
		
		for (int i=0; i<mc.Length; i++)
		{
			UnityEngine.Object.DestroyImmediate(mc[i].gameObject);
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
