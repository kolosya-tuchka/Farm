using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    SaveLoadManager saveLoad;

    void Start()
    {
        saveLoad = GetComponent<SaveLoadManager>();
        StartCoroutine(SaveLoop());
    }

    IEnumerator SaveLoop()
    {
        yield return null;
        saveLoad.LoadPlayer();
        yield return new WaitForSeconds(2);

        while (true)
        {
            yield return null;
            string json = JsonUtility.ToJson(new PlayerData(FindObjectOfType<Player>()));
            saveLoad.Save(json, "player");

            yield return new WaitForSeconds(1);
        }
    }
}
