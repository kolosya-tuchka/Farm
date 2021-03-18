using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinsUI : MonoBehaviour
{
    Player player;
    Text text;

    void Start()
    {
        text = GetComponent<Text>();
        player = FindObjectOfType<Player>();
    }

    void Update()
    {
        text.text = player.coins.ToString();
    }
}
