using UnityEngine;

public class DungeonSkeletonChunk {

    public bool[] doors = new bool[4];
    public bool done = false;
    public int x;
    public int y;

    public DungeonSkeletonChunk(bool[] doorsNeeded, bool[] doorsNotAllowed, int x, int y) {
        this.x = x;
        this.y = y;
        if (doorsNeeded.Length == 4 || doorsNotAllowed.Length == 4) {
            for(int i = 0; i < 4; i++) {
                if(doorsNeeded[i]) {
                    doors[i] = true;
                } else if (doorsNotAllowed[i]) {
                    doors[i] = false;
                } else {
                    doors[i] = UnityEngine.Random.Range(0, 2) == 1;
                }
            }
        }
    }

    public ChunkType getSkeletonType() {
        int doorCount = 0;
        foreach (bool b in doors) {
            if (b) {
                doorCount++;
            }
        }
        if (doorCount == 4)
            return ChunkType.FourDoors;
        if (doorCount == 3)
            return ChunkType.ThreeDoors;
        if (doorCount == 1)
            return ChunkType.OneDoor;
        if (doorCount == 2 && doors[0] == doors[2])
            return ChunkType.OppositeDoors;
        return ChunkType.SideDoors;
    }
}
