using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soil : MonoBehaviour
{
    public List<Sprite> sprites;
    public SpriteRenderer sprite;
    public soilType type;
    public Plant plant;
    
    FarmInteractions interactions;

    public enum soilType
    {
        dirt, unwatered, watered, planted
    }

    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        interactions = FindObjectOfType<FarmInteractions>();
    }

    void Update()
    {
        
    }

    public void UpdateSprite()
    {
        sprite.sprite = sprites[(int)type];
    }

    public static int GetSoilIndex(Soil so)
    {
        var soils = FindObjectOfType<FarmPrefabs>().soils;
        for (int i = 0; i < soils.Count; ++i)
        {
            if (so == soils[i]) return i;
        }
        return -1;
    }
}
