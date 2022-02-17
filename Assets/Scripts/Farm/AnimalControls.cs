using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalControls : MonoBehaviour
{
    Rigidbody2D rig;
    public float timeToGrow;
    public Paddock home;
    public GameObject simpleModel, growedModel;
    public State state;
    public Item item;
    public int startTime;

    public enum State
    {
        growing, growed
    }

    void Start()
    {
        rig = GetComponent<Rigidbody2D>();

        state = State.growing;
        simpleModel.SetActive(true);
        growedModel.SetActive(false);

        StartCoroutine(Move());
    }

    void Update()
    {
        var curTime = TimeManager.GetUnixTime();
        if (curTime - startTime > timeToGrow)
        {
            state = State.growed;
            simpleModel.SetActive(false);
            growedModel.SetActive(true);
            StopAllCoroutines();
            rig.velocity = Vector2.zero;
        }
    }

    IEnumerator Move()
    {
        while (true)
        {
            Vector2 velocity = new Vector2(Random.Range(-.5f, .5f), Random.Range(-.5f, .5f));

            rig.velocity = velocity;

            yield return new WaitForSeconds(1f);
        }
    }

    private void OnMouseDown()
    {
        if (state == State.growed)
        {
            FindObjectOfType<FarmInteractions>().PickAnimal(this);
        }
    }

    public static int GetChuongIndex(AnimalControls animal)
    {
        var ch = FindObjectOfType<FarmPrefabs>().chuongs;
        for (int i = 0; i < ch.Count; ++i)
        {
            if (ch[i] == animal.home) return i;
        }
        return -1;
    }
}
