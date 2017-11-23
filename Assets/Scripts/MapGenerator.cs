using System;
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
    public GameObject lightPrefab;

    private int keyCount = 0;
    private int mapCount = 0;
    private int keyAmount;
    private int mapAmount;
    private int itemsAvailable;

    private MapChunk[,] map;

    public void Start() {
        Generate();
    }

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
        putLightsToMap();
        // check everything
        print("MAP GENERATED!!");
		
	}
	
	public void ClearMap() {

        MapChunk[] mc = gameObject.GetComponentsInChildren<MapChunk>();
        for (int i = 0; i < mc.Length; i++) {
            DestroyImmediate(mc[i].gameObject);
        }
        Light[] lights = gameObject.GetComponentsInChildren<Light>();
        for (int i = 0; i < lights.Length; i++) {
            DestroyImmediate(lights[i].gameObject);
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

    private void putLightsToMap() {
        int counter = 0;
        for (int i = 0; i <= mapSize; i++) {
            for (int j = 0; j <= mapSize; j++) {
                GameObject newGO = Instantiate(lightPrefab);
                newGO.name = String.Format("light-{0}", counter++);
                newGO.transform.parent = transform;

                newGO.transform.localPosition = new Vector3(i * 3 - 1.5f, j * 3 - 1.5f, 0.2f);
            }
        }
    }

    public MapChunk GetChunkAt(Vector3 position) {
        if(map == null) {
            print("map is null");
            return null;
        }
        int x, y;
        if(!getIndexesForPosition(position,out x,out y)) {
            return null;
        }
        return map[x, y];
    }
    
    private bool getIndexesForPosition(Vector3 position, out int x, out int y) {
        if(position.x < -1.5f || position.y < -1.5f || position.y > 3*mapSize-1.5f || position.x > 3 * mapSize - 1.5f) {
            x = y = -1;
            return false;
        }
        x = (int) Math.Floor((position.x + 1.5f)/3);
        y = (int) Math.Floor((position.y + 1.5f) / 3);

        return true;
    }
}
