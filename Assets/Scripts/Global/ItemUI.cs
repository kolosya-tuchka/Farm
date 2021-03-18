using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemUI : MonoBehaviour
{
    Player player;
    public Item item;

    public Text count;
    public Image icon;

    public void UpdateData()
    {
        count.text = item.count.ToString();
        icon.sprite = item.icon;
    }
}
