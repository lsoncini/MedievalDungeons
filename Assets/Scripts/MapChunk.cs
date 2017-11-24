using UnityEngine;

public class MapChunk : MonoBehaviour
{
	public Vector2[] itemPositions;
    private int itemCount = 0;
    public bool topDoor;
    public bool bottomDoor;
    public bool leftDoor;
    public bool rightDoor;

    public bool AddItemAtPoint(GameObject item) {
        if(itemCount >= itemPositions.Length) {
            return false;
        }
        GameObject go = Instantiate(item) as GameObject;
        go.name = string.Format("collectible-{0}", itemCount+1);
        go.transform.parent = transform;
        Vector2 nextPoint = itemPositions[itemCount];
        go.transform.localPosition = new Vector3(nextPoint.x, nextPoint.y, 0);
        itemCount++;
        return true;
    }

    public int itemsAvailable() {
        return itemPositions.Length;
    }

    public bool hasRightDoor() {
        return hasDoor(0);
    }
    public bool hasTopDoor() {
        return hasDoor(1);
    }
    public bool hasLeftDoor() {
        return hasDoor(2);
    }
    public bool hasBottomDoor() {
        return hasDoor(3);
    }

    private bool hasDoor(int doorNum) {  //RIGHT = 0, TOP = 1, LEFT = 2, BOTTOM = 3
        bool[] doors = { rightDoor, topDoor, leftDoor, bottomDoor };

        doorNum += getRotation();
        doorNum = doorNum % 4;
        return doors[doorNum];
    }

    private int getRotation() {
        float zRotation = this.gameObject.transform.eulerAngles.z;
        if (zRotation < 0) {
            zRotation *= -1;
        }
        return ((int)zRotation)% 90;
    }
}
