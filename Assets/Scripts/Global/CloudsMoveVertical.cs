using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudsMoveVertical : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(Fly());
    }

    IEnumerator Fly()
    {
        while (true)
        {
            Vector3 pos = transform.position + Vector3.up * Random.Range(0.05f, 0.2f);
            Vector3 temp = transform.position;
            while (transform.position.y < pos.y)
            {
                transform.position = Vector3.MoveTowards(transform.position, pos, 0.1f * Time.deltaTime);
                yield return null;
            }
            transform.position = pos;

            pos = temp;
            while (transform.position.y > pos.y)
            {
                transform.position = Vector3.MoveTowards(transform.position, pos, 0.1f * Time.deltaTime);
                yield return null;
            }
        }
    }
}
