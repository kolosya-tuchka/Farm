using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmManager : MonoBehaviour
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
        saveLoad.LoadFarm();

        yield return new WaitForSeconds(2);

        while (true)
        {
            string json = JsonUtility.ToJson(new PlayerData(FindObjectOfType<Player>()));
            saveLoad.Save(json, "player");

            yield return null;

            var a = FindObjectsOfType<AnimalControls>();
            var ans = new List<AnimalControls>();
            foreach (var i in a) ans.Add(i);

            var p = FindObjectsOfType<Plant>();
            var pls = new List<Plant>();
            foreach (var i in p) pls.Add(i);

            var s = FindObjectsOfType<Soil>();
            var sls = new List<Soil>();
            foreach (var i in s) sls.Add(i);

            json = JsonUtility.ToJson(new FarmData(ans, pls, sls));
            saveLoad.Save(json, "farm");

            yield return new WaitForSeconds(2);
        }
    }
}
