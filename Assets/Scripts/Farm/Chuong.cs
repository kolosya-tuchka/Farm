using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chuong : MonoBehaviour
{
    public int animalCount, maxCount;
    public animalType animal;

    public enum animalType
    {
        chiken, pig
    }

    public bool CanSpawn()
    {
        return animalCount < maxCount;
    }

}
