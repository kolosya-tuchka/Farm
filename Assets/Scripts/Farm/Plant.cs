using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour
{
    public float growTime;
    public State state = State.growing; 
    public Sprite planted, growed;
    public int count = 1;
    public string name;
    public Sprite icon;
    public Item item;
    public Soil home;

    public int startTime;

    void Start()
    {
        GetComponent<SpriteRenderer>().sprite = planted;
    }

    void Update()
    {
        if (TimeManager.GetUnixTime() - startTime > growTime)
        {
            state = State.growed;
            GetComponent<SpriteRenderer>().sprite = growed;
        }
    }

    public enum State
    {
        growing, growed
    }
}
