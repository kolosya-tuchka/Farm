using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmPrefabs : MonoBehaviour
{
    public List<Chuong> chuongs;
    public List<Soil> soils;
    public List<AnimalControls> animals;
    public List<Plant> plants;

    private void Start()
    {
        chuongs = new List<Chuong>();
        soils = new List<Soil>();

        var ch = FindObjectsOfType<Chuong>();
        foreach (var c in ch) chuongs.Add(c);

        var so = FindObjectsOfType<Soil>();
        foreach (var s in so) soils.Add(s);
    }
}
