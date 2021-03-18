using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject toSpawn;
    public int cost = 100;
    public Sprite disable, enable;
    Player player;
    public Type type;

    public enum Type
    {
        plant, animal
    }

    void Start()
    {
        player = FindObjectOfType<Player>();
    }

    void Update()
    {
        GetComponent<SpriteRenderer>().sprite = player.CanBuyWithCoins(cost) ? enable : disable;
    }

    private void OnMouseDown()
    {
        switch (type)
        {
            case Type.plant: player.GetComponent<FarmInteractions>().Plant(this); break;
            case Type.animal: player.GetComponent<FarmInteractions>().SpawnAnimal(this); break;
        }

    }
}
