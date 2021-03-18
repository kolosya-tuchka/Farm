using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FactoryManager : MonoBehaviour
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
        saveLoad.LoadFactory();
        yield return new WaitForSeconds(2);

        while (true)
        {
            string json = JsonUtility.ToJson(new PlayerData(FindObjectOfType<Player>()));
            saveLoad.Save(json, "player");

            var machinesD = new List<MachineData>();
            var machines = GetComponent<FactoryPrefs>().machines;

            yield return null;

            foreach (var machine in machines)
            {
                var m = new MachineData((int)machine.state, machine.startMakingTime, machine.curCraft);
                machinesD.Add(m);
            }
            json = JsonUtility.ToJson(new FactoryData(machinesD));
            saveLoad.Save(json, "factory");

            yield return new WaitForSeconds(1);
        }
    }
}
