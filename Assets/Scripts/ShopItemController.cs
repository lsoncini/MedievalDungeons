using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopItemController : MonoBehaviour {

    private Image image;
    private Text price;
    public ItemType type = ItemType.TrapImmunity;
    
    private void mati() {
        Image[] images = GetComponentsInChildren<Image>();
        foreach(Image i in images) {
            if(i.gameObject.name == "ItemImage") {
                image = i;
                break;
            }
        }
        price = GetComponentInChildren<Text>();
    }
}
