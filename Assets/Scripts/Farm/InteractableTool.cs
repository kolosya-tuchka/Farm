using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableTool : MonoBehaviour
{
    public InteractType type;
    FarmInteractions soil;

    public enum InteractType
    {
        shovel, water
    }


    private void Start()
    {
        soil = FindObjectOfType<FarmInteractions>();
    }

    private void OnMouseDown()
    {
        switch (type)
        {
            case InteractType.shovel: soil.Shovel(); break;
            case InteractType.water: soil.Water(); break;
        }
    }
}
