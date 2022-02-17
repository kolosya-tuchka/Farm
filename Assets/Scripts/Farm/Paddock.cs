using UnityEngine;

public class Paddock : MonoBehaviour
{
    public int animalCount, maxCount;
    public AnimalType animal;

    public enum AnimalType
    {
        chicken, pig
    }

    public bool CanSpawn()
    {
        return animalCount < maxCount;
    }

}
