using UnityEngine;
using UnityEngine.UI;

public class InputController: MonoBehaviour {

    public Text text;
    public Text placeholder;
    
    public int getValue() {
        int aux;
        if (text.text.Length > 0 && int.TryParse(text.text, out aux)) {
            return aux;
        } else {
            return int.Parse(placeholder.text);
        }
    }
}