using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveLoadManager : MonoBehaviour
{
    FarmPrefabs prefabs;
    UserManager user;

    void Start()
    {
        prefabs = GetComponent<FarmPrefabs>();
        user = FindObjectOfType<UserManager>();
    }

    public void Save(string json, string obj)
    {
        StartCoroutine(SaveCor(json, obj));
    }

    IEnumerator SaveCor(string json, string obj)
    {
        if (json == null) yield break;

        WWWForm form = new WWWForm();
        form.AddField("login", user.login);
        form.AddField("password", user.password);
        form.AddField("json", json);
        form.AddField("obj", obj);
        WWW www = new WWW(user.url + "save_obj.php", form);
        yield return www;

        Debug.Log(www.error);
        Debug.Log(www.text);

        if (www.error == "Cannot connect to destination host")
        {
            user.StartCoroutine(user.ExitWithError("No internet connection!"));
        }
    }

    public bool LoadPlayer()
    {
        StartCoroutine(LoadPlayerCor());
        return true;
    }

    IEnumerator LoadPlayerCor()
    {
        WWWForm form = new WWWForm();
        form.AddField("login", user.login);
        form.AddField("password", user.password);
        form.AddField("index", 1);
        WWW www = new WWW(user.url + "load_obj.php", form);
        yield return www;
        Debug.Log(www.text);

        if (www.error == "Cannot connect to destination host")
        {
            user.StartCoroutine(user.ExitWithError("No internet connection!"));
        }

        PlayerData data = JsonUtility.FromJson<PlayerData>(www.text);
        var player = FindObjectOfType<Player>();
        player.coins = data.coins;

        var prefs = GetComponent<ItemPrefs>();

        foreach (var item in data.inv)
        {
            int i = 0;
            while (item.name != prefs.items[i].name)
            {
                i++;
            }
            Item it = new Item(item.name, prefs.items[i].icon, item.count);
            player.AddItem(it);
            player.inventoryUI.UpdateInvUI(new Item(it));
        }
    }

    public bool LoadFarm()
    {
        StartCoroutine(LoadFarmCor());
        return true;
    }

    IEnumerator LoadFarmCor()
    {
        WWWForm form = new WWWForm();
        form.AddField("login", user.login);
        form.AddField("password", user.password);
        form.AddField("index", 2);
        WWW www = new WWW(user.url + "load_obj.php", form);
        yield return www;
        FarmData data = JsonUtility.FromJson<FarmData>(www.text);

        if (www.error == "Cannot connect to destination host")
        {
            user.StartCoroutine(user.ExitWithError("No internet connection!"));
        }

        foreach (var an in data.animals)
        {
            GameObject animal;
            int i = 0;
            while (an.name != prefabs.animals[i].item.name) i++;

            animal = prefabs.animals[i].gameObject;
            var ch = prefabs.chuongs[an.chuongIndex];
            var obj = Instantiate(animal, ch.transform.position, Quaternion.identity, ch.transform);

            var a = obj.GetComponent<AnimalControls>();
            a.startTime = an.startTime;
            a.home = ch;
        }

        foreach (var so in data.soils)
        {
            var soil = prefabs.soils[so.index];

            soil.type = (Soil.soilType)so.state;
            soil.UpdateSprite();
        }

        foreach (var pl in data.plants)
        {
            GameObject plant;
            int i = 0;
            while (pl.name != prefabs.plants[i].name) ++i;

            plant = prefabs.plants[i].gameObject;

            var so = new Soil();
            foreach (var s in prefabs.soils)
            {
                if (s.transform.position + new Vector3(0, 0.1f, 0) == new Vector3(pl.pos.x, pl.pos.y, pl.pos.z))
                {
                    so = s;
                }
            }

            var obj = Instantiate(plant, so.transform.position + new Vector3(0, 0.1f, 0), Quaternion.identity, so.transform);

            var p = obj.GetComponent<Plant>();
            so.plant = p;
            p.startTime = pl.startTime;
            p.home = so;
        }
    }

    public bool LoadFactory()
    {
        StartCoroutine(LoadFactoryCor());
        return true;
    }

    IEnumerator LoadFactoryCor()
    {
        WWWForm form = new WWWForm();
        form.AddField("login", user.login);
        form.AddField("password", user.password);
        form.AddField("index", 3);
        WWW www = new WWW(user.url + "load_obj.php", form);
        yield return www;

        if (www.error == "Cannot connect to destination host")
        {
            user.StartCoroutine(user.ExitWithError("No internet connection!"));
        }

        FactoryData data = JsonUtility.FromJson<FactoryData>(www.text);
        var player = FindObjectOfType<Player>();

        var machines = GetComponent<FactoryPrefs>().machines;
        for (int i = 0; i < machines.Count; ++i)
        {
            try
            {
                machines[i].Init(data.machines[i]);
            }
            catch { }
        }
    }
}
