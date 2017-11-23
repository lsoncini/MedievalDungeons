using System;
using System.Collections;

using UnityEngine;

[ExecuteInEditMode]
public class MapGenerator : MonoBehaviour {
    // The pieces of dungeon
    public MapChunk[] mapChunks;
    public int mapSize = 10;
    public Difficulty difficulty = Difficulty.Easy;
    public int seed;

    public GameObject keyPrefab;
    public GameObject mapPrefab;
    private int keyCount = 0;
    private int mapCount = 0;

    private int keyAmount;
    private int mapAmount;
    private int itemsAvailable;

    private MapChunk[,] map;

    public void Generate() {
        UnityEngine.Random.InitState(seed);

        map = new MapChunk[mapSize, mapSize];
        keyAmount = (mapSize * mapSize) / 5;
        mapAmount = (mapSize * mapSize) / 10;
        itemsAvailable = 0;
        for (int i = 0; i < mapSize; i++) {
            for (int j = 0; j < mapSize; j++) {
                MapChunk newGO = Instantiate(mapChunks[getRandomChunkIndex()]);
                newGO.name = String.Format("dungeon-{0}-{1}", i, j);
                newGO.transform.parent = transform;

                newGO.transform.localPosition = new Vector3(i * 3, j * 3, 0);
                map[i, j] = newGO;
                itemsAvailable += newGO.itemsAvailable();
            }
        }

        populateKeys();
        populateMaps();

        // populate the array
        // check everything
        // render everything
        print("MAP GENERATED!!");
		
	}
	
	public void ClearMap() {

        MapChunk[] mc = gameObject.GetComponentsInChildren<MapChunk>();

        for (int i = 0; i < mc.Length; i++) {
            DestroyImmediate(mc[i].gameObject);
        }
        map = null;
        keyCount = 0;
        mapCount = 0;

    }
	
	private int prevIdx = 0;
	private int getRandomChunkIndex()
	{
		// checkear condiciones aca...
		prevIdx = UnityEngine.Random.Range(0, mapChunks.Length);
		
		return prevIdx;
	}

    private void populateKeys() {
        while(itemsAvailable > 0 && keyAmount - keyCount > 0) {
            int randX = UnityEngine.Random.Range(0, mapSize);
            int randY = UnityEngine.Random.Range(0, mapSize);
            if (map[randX, randY].AddItemAtPoint(keyPrefab)) {
                itemsAvailable--;
                keyCount++;
            }
        }
    }
    private void populateMaps() {
        while (itemsAvailable > 0 && mapAmount - mapCount > 0) {
            int randX = UnityEngine.Random.Range(0, mapSize);
            int randY = UnityEngine.Random.Range(0, mapSize);
            if (map[randX, randY].AddItemAtPoint(mapPrefab)) {
                itemsAvailable--;
                mapCount++;
            }
        }
    }
}
