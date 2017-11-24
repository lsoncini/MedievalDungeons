
public class DungeonSkeletonChunk {

    public bool[] doors = new bool[4];

    public DungeonSkeletonChunk(bool[] doorsNeeded, bool[] doorsNotAllowed) {
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
}
