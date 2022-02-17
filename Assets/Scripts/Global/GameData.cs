using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    
}

[System.Serializable]
public class PlayerData
{
    public int coins;
    public List<it> inv = new List<it>();

    public PlayerData(Player player)
    {
        coins = player.coins;
        foreach (var item in player.items)
        {
            it i = new it(item);
            inv.Add(i);
        }
    }

    [System.Serializable]
    public class it
    {
        public int count;
        public string name;

        public it(Item item)
        {
            this.name = item.name;
            this.count = item.count;
        }
    }
}

[System.Serializable]
public class FarmData
{
    public List<animal> animals = new List<animal>();
    public List<plant> plants = new List<plant>();
    public List<soil> soils = new List<soil>();

    public FarmData(List<AnimalControls> ans, List<Plant> pls, List<Soil> sls)
    {
        foreach (var an in ans)
        {
            animals.Add(new animal(an));
        }

        foreach (var pl in pls)
        {
            plants.Add(new plant(pl));
        }

        foreach (var so in sls)
        {
            soils.Add(new soil(so));
        }
    }

    [System.Serializable]
    public class animal
    {
        public int chuongIndex;
        public int startTime;
        public string name;

        public animal(AnimalControls an)
        {
            this.chuongIndex = AnimalControls.GetChuongIndex(an);
            this.startTime = an.startTime;
            this.name = an.item.name;
        }
    }

    [System.Serializable]
    public class plant
    {
        public int startTime;
        public string name;
        public vec3 pos;

        public plant(Plant pl)
        {
            this.startTime = pl.startTime;
            this.name = pl.name;
            this.pos = new vec3(pl.transform.position);
        }
    }

    [System.Serializable]
    public class soil
    {
        public int state;
        public int index;

        public soil(Soil so)
        {
            this.state = (int)so.type;
            this.index = Soil.GetSoilIndex(so);
        }
    }
}

[System.Serializable]
public class FactoryData
{
    public List<MachineData> machines;

    public FactoryData(List<MachineData> machines)
    {
        this.machines = machines;
    }
}

[System.Serializable]
public class MachineData
{
    public int state;
    public int startTime;
    public string curCraft;

    public MachineData(int state, int startTime, string curCraft)
    {
        this.state = state;
        this.startTime = startTime;
        this.curCraft = curCraft;
    }
}

[System.Serializable]
public class vec3
{
    public float x, y, z;

    public vec3(Vector3 vector)
    {
        this.x = vector.x;
        this.y = vector.y;
        this.z = vector.z;
    }
}
