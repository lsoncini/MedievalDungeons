using System;
using UnityEngine;

[ExecuteInEditMode]
public class MapGenerator : MonoBehaviour {

    // The pieces of dungeon
    public MapChunk[] mapChunks;
    private int mapSize = 5;
    public DungeonSize dungeonSize = DungeonSize.Small;
    public Difficulty difficulty = Difficulty.Easy;
    public int seed;

    public GameObject keyPrefab;
    public GameObject mapPrefab;
    public GameObject lightPrefab;
    public GameObject doorPrefab;

    private int keyCount = 0;
    private int mapCount = 0;
    public int keyAmount = -1;
    public int mapAmount = -1;
    private int itemsAvailable;

    private MapChunk[,] map;

    private DungeonSkeletonChunk[,] skeleton;

    /*public void Start() {
        ClearMap();
        Generate();
    }*/

    public void Generate() {
        UnityEngine.Random.InitState(seed);
        ClearMap();
        switch (dungeonSize) {
            case DungeonSize.Small:
                mapSize = 5;
                break;
            case DungeonSize.Normal:
                mapSize = 7;
                break;
            case DungeonSize.Big:
                mapSize = 10;
                break;
        }
        map = new MapChunk[mapSize, mapSize];
        skeleton = new DungeonSkeletonChunk[mapSize, mapSize];
        if (keyAmount == -1) {
            keyAmount = (mapSize * mapSize) / 5;
        }
        if(mapAmount == -1) {
            mapAmount = (mapSize * mapSize) / 10;
        }

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
        putDoorInRandomChunk();
        // check everything
        print("MAP GENERATED!!");
		
	}

    private void putDoorInRandomChunk() {
        MapChunk randomMapChunk;
        float zAngle = 0f;
        Vector3 localPosition = new Vector3(0, 0, 0);

        int totalBorderChunks = (mapSize == 1) ? 1 : mapSize * 4 - 4;
        int randomChunk = UnityEngine.Random.Range(0, totalBorderChunks);

        if (randomChunk < mapSize) {                                    // LEFT BORDER
            randomMapChunk = map[0, randomChunk];
            zAngle = 0;
            localPosition.x = -1.47f;
            localPosition.y = 0;
        } else if(randomChunk < mapSize * 2) {                          // RIGHT BORDER
            randomMapChunk = map[mapSize - 1, randomChunk - mapSize];
            zAngle = 180;
            localPosition.x = 1.47f;
            localPosition.y = 0;
        } else if(randomChunk < mapSize * 3 - 2) {                      // BOTTOM BORDER
            randomMapChunk = map[randomChunk - 2 * mapSize, 0];
            zAngle = 90;
            localPosition.x = 0;
            localPosition.y = -1.47f;
        } else {                                                        // TOP BORDER
            randomMapChunk = map[randomChunk - (mapSize * 3 - 2), mapSize - 1];
            zAngle = -90;
            localPosition.x = 0;
            localPosition.y = 1.47f;
        }

        GameObject newGO = Instantiate(doorPrefab);
        newGO.name = String.Format("exit");
        newGO.transform.parent = randomMapChunk.transform;
        newGO.transform.localPosition = localPosition;
        newGO.transform.Rotate(0, 0, zAngle);
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

    public Vector3 GetRandomPlayerPositionInMap() {
        int randX = UnityEngine.Random.Range(0, mapSize);
        int randY = UnityEngine.Random.Range(0, mapSize);
        return new Vector3(randX * 3,
                           randY * 3,
                           0);
    }
}
