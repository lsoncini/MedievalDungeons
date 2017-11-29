using System;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class MapGenerator : MonoBehaviour {

    // The pieces of dungeon
    public MapChunk[] mapChunks;

    public MapChunk[] fourDoorsChunks;
    public MapChunk[] threeDoorsChunks;
    public MapChunk[] oneDoorChunks;
    public MapChunk[] oppositeDoorChunks;
    public MapChunk[] sideDoorChunks;

    private int mapSize = 5;
    private float trapPercentage = 0.3f;
    public DungeonSize dungeonSize = DungeonSize.Small;
    public Difficulty difficulty = Difficulty.Easy;
    public int seed;

    public GameObject keyPrefab;
    public GameObject mapPrefab;
    public GameObject speedupPrefab;

    public GameObject lightPrefab;
    public GameObject doorPrefab;

    private int keyCount = 0;
    private int mapCount = 0;
    private int speedupCount = 0;
    public int keyAmount = -1;
    public int mapAmount = -1;
    public int speedupAmount = -1;
    private int itemsAvailable;
    private int trapCount = 0;

    private MapChunk[,] map;

    private DungeonSkeletonChunk[,] skeleton;

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
        switch (difficulty) {
            case Difficulty.Easy:
                trapPercentage = 0.3f;
                break;
            case Difficulty.Medium:
                trapPercentage = 0.5f;
                break;
            case Difficulty.Hard:
                trapPercentage = 0.7f;
                break;
        }

        if (keyAmount == -1) {
            keyAmount = (mapSize * mapSize) / 5;
        }
        if (mapAmount == -1) {
            mapAmount = (mapSize * mapSize) / 10;
        }
        if (speedupAmount == -1) {
            speedupAmount = (mapSize * mapSize) / 10;
        }

        map = new MapChunk[mapSize, mapSize];
        skeleton = new DungeonSkeletonChunk[mapSize, mapSize];

        buildDungeonSkeleton();
        renderDungeonSkeleton();
        checkTrapPercentage();

        populateKeys();
        populateMaps();
        populateSpeedups();

        putLightsToMap();

        putDoorInRandomChunk();

        print("MAP GENERATED!!");
	}

    private void checkTrapPercentage() {
        print(trapPercentage * mapSize * mapSize);
        print("INITIAL TRAP COUNT: " + trapCount);
        while (trapCount < trapPercentage * mapSize * mapSize) {
            int randX = UnityEngine.Random.Range(0, mapSize);
            int randY = UnityEngine.Random.Range(0, mapSize);
            if (!map[randX, randY].isTrap) {
                itemsAvailable -= map[randX,randY].itemsAvailable();
                DestroyImmediate(map[randX, randY].gameObject);
                renderSkeletonChunk(randX, randY, true);
            }
        }
        print("ENDING TRAP COUNT: " + trapCount);
    }

    private void buildDungeonSkeleton() {
        List<DungeonSkeletonChunk> frontier = new List<DungeonSkeletonChunk>();
        int chunkCount = 0;

        while(chunkCount < mapSize * mapSize) {
            if(chunkCount == 0) {
                skeleton[0, 0] = new DungeonSkeletonChunk(new bool[] { false, false, false, false }, new bool[] { false, false, true, true }, 0, 0);
                frontier.Add(skeleton[0, 0]);
                chunkCount++;
            } else {
                for(int i = 0; i < mapSize; i++) {
                    for(int j = 0; j < mapSize; j++) {
                        if(skeleton[i,j] == null) {
                            if(i - 1 >= 0 && skeleton[i - 1, j] != null) {
                                skeleton[i - 1, j].doors[0] = true;
                            } else if(j - 1 >= 0 && skeleton[i, j - 1] != null) {
                                skeleton[i, j - 1].doors[1] = true;
                            } else if (i + 1 < mapSize && skeleton[i + 1, j] != null) {
                                skeleton[i + 1, j].doors[2] = true;
                            } else if (j + 1 < mapSize && skeleton[i, j + 1] != null) {
                                skeleton[i, j + 1].doors[3] = true;
                            }
                            frontier.Add(createSkeletonChunk(i, j));
                            chunkCount++;
                        }
                    }
                }
            }

            while (frontier.Count > 0) {
                DungeonSkeletonChunk chunk = frontier[0];
                frontier.RemoveAt(0);
                int x, y;
                for (int i = 0; i < 4; i++) {
                    if (chunk.doors[i]) {
                        switch (i) {
                            case 0:
                                x = chunk.x + 1;
                                y = chunk.y;
                                break;
                            case 1:
                                x = chunk.x;
                                y = chunk.y + 1;
                                break;
                            case 2:
                                x = chunk.x - 1;
                                y = chunk.y;
                                break;
                            default:
                                x = chunk.x;
                                y = chunk.y - 1;
                                break;
                        }
                        if (skeleton[x, y] == null) {
                            frontier.Add(createSkeletonChunk(x, y));
                            chunkCount++;
                        }
                    }
                }
            }
        }
        
    }

    private void renderDungeonSkeleton() {
        itemsAvailable = 0;

        for (int i = 0; i < mapSize; i++) {
            for (int j = 0; j < mapSize; j++) {
                renderSkeletonChunk(i, j, false);
            }
        }
    }

    private void renderSkeletonChunk(int i, int j, bool mustHaveTrap) {
        DungeonSkeletonChunk skeletonChunk = skeleton[i, j];
        MapChunk current;
        MapChunk[] chunks;
        switch (skeletonChunk.getSkeletonType()) {
            case ChunkType.FourDoors:
                chunks = fourDoorsChunks;
                break;
            case ChunkType.ThreeDoors:
                chunks = threeDoorsChunks;
                break;
            case ChunkType.OneDoor:
                chunks = oneDoorChunks;
                break;
            case ChunkType.OppositeDoors:
                chunks = oppositeDoorChunks;
                break;
            default:
                chunks = sideDoorChunks;
                break;
        }
        if (mustHaveTrap) {
            current = getRandomChunkWithTrap(chunks);
        } else {
            current = getRandomChunk(chunks);
        }
        MapChunk newGO = Instantiate(current);
        newGO.name = String.Format("dungeon-{0}-{1}", i, j);
        newGO.transform.parent = transform;
        newGO.transform.localPosition = new Vector3(i * 3, j * 3, 0);
        newGO.RotateToMatch(skeletonChunk.doors);
        map[i, j] = newGO;
        if (newGO.isTrap) {
            trapCount++;
        }
        itemsAvailable += newGO.itemsAvailable();
    }

    private void renderDungeon() {
        itemsAvailable = 0;
        for (int i = 0; i < mapSize; i++) {
            for (int j = 0; j < mapSize; j++) {
                MapChunk newGO = Instantiate(getRandomChunk(mapChunks));
                newGO.name = String.Format("dungeon-{0}-{1}", i, j);
                newGO.transform.parent = transform;

                newGO.transform.localPosition = new Vector3(i * 3, j * 3, 0);
                map[i, j] = newGO;
                itemsAvailable += newGO.itemsAvailable();
            }
        }
    }

    private DungeonSkeletonChunk createSkeletonChunk(int x, int y) {
        bool[] doorsNeeded = { false, false, false, false };
        bool[] doorsNotAllowed = { false, false, false, false };
        DungeonSkeletonChunk neighbour;
        if(x + 1 >= mapSize) {
            doorsNotAllowed[0] = true;
        } else {
            neighbour = skeleton[x + 1, y];
            if (neighbour != null) {
                if (neighbour.doors[2]) {
                    doorsNeeded[0] = true;
                } else {
                    doorsNotAllowed[0] = true;
                }
            }
        }

        if (y + 1 >= mapSize) {
            doorsNotAllowed[1] = true;
        } else {
            neighbour = skeleton[x, y + 1];
            if (neighbour != null) {
                if (neighbour.doors[3]) {
                    doorsNeeded[1] = true;
                } else {
                    doorsNotAllowed[1] = true;
                }
            }
        }

        if (x - 1 < 0) {
            doorsNotAllowed[2] = true;
        } else {
            neighbour = skeleton[x - 1, y];
            if (neighbour != null) {
                if (neighbour.doors[0]) {
                    doorsNeeded[2] = true;
                } else {
                    doorsNotAllowed[2] = true;
                }
            }
        }

        if (y - 1 < 0) {
            doorsNotAllowed[3] = true;
        } else {
            neighbour = skeleton[x, y - 1];
            if (neighbour != null) {
                if (neighbour.doors[1]) {
                    doorsNeeded[3] = true;
                } else {
                    doorsNotAllowed[3] = true;
                }
            }
        }

        skeleton[x, y] = new DungeonSkeletonChunk(doorsNeeded, doorsNotAllowed, x, y);
        return skeleton[x, y];
    }

    private void putDoorInRandomChunk() {
        MapChunk randomMapChunk;
        float zAngle = 0f;
        Vector3 localPosition = new Vector3(0, 0, 0);

        int totalBorderChunks = (mapSize == 1) ? 1 : mapSize * 4 - 4;
        int randomChunk = UnityEngine.Random.Range(0, totalBorderChunks);
        int x, y, i;
        if (randomChunk < mapSize) {                                   // LEFT BORDER
            x = 0;
            y = randomChunk;
            i = 2;
            zAngle = 0;
            localPosition.x = -1.47f;
            localPosition.y = 0;
        } else if(randomChunk < mapSize * 2) {                          // RIGHT BORDER
            x = mapSize - 1;
            y = randomChunk - mapSize;
            i = 0;
            zAngle = 180;
            localPosition.x = 1.47f;
            localPosition.y = 0;
        } else if(randomChunk < mapSize * 3 - 2) {                      // BOTTOM BORDER
            x = randomChunk - 2 * mapSize;
            y = 0;
            i = 3;
            zAngle = 90;
            localPosition.x = 0;
            localPosition.y = -1.47f;
        } else {                                                        // TOP BORDER
            x = randomChunk - (mapSize * 3 - 2);
            y = mapSize - 1;
            i = 1;
            zAngle = -90;
            localPosition.x = 0;
            localPosition.y = 1.47f;
        }

        skeleton[x, y].doors[i] = true;
        DestroyImmediate(map[x, y].gameObject);
        renderSkeletonChunk(x, y, false);
        randomMapChunk = map[x,y];

        GameObject newGO = Instantiate(doorPrefab);
        newGO.name = String.Format("exit");
        newGO.transform.parent = randomMapChunk.transform;
        newGO.transform.position = randomMapChunk.transform.position + localPosition;
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
        trapCount = 0;
        itemsAvailable = 0;
    }
	
	private MapChunk getRandomChunk(MapChunk[] mapChunks)
	{
        int idx = UnityEngine.Random.Range(0, mapChunks.Length);
		
		return mapChunks[idx];
	}
    private MapChunk getRandomChunkWithTrap(MapChunk[] mapChunks) {
        MapChunk current = null;
        while(current == null || !current.isTrap) {
            int idx = UnityEngine.Random.Range(0, mapChunks.Length);
            current = mapChunks[idx];
        }
        
        return current;
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
    private void populateSpeedups() {
        while (itemsAvailable > 0 && speedupAmount - speedupCount > 0) {
            int randX = UnityEngine.Random.Range(0, mapSize);
            int randY = UnityEngine.Random.Range(0, mapSize);
            if (map[randX, randY].AddItemAtPoint(speedupPrefab)) {
                itemsAvailable--;
                speedupCount++;
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
        return new Vector3(randX * 3, randY * 3, 0);
    }

    public int GetKeyCount() {
        return keyCount;
    }

    public int GetMapCount() {
        return mapCount;
    }

    public int GetSpeedupCount() {
        return speedupCount;
    }
}
