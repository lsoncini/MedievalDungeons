using UnityEngine;

public class MapChunk : MonoBehaviour
{
	public Vector2[] itemPositions;
    private int itemCount = 0;

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
}
