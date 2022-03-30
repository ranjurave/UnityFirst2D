using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class HUDScript : MonoBehaviour {
    [SerializeField] TextMeshProUGUI CoinText;
    [SerializeField] PlayerScript player;

    void Update() {
        // Getting the variable from player and applying it to the HUD text
        CoinText.text = player.CoinsCollected.ToString();
    }
}
