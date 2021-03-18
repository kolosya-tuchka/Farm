using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInventory : MonoBehaviour
{
    Player player;
    public GameObject example, content;
    List<ItemUI> slots = new List<ItemUI>();

    private void Start()
    {
        player = FindObjectOfType<Player>();
    }

    public void UpdateInvUI(Item item)
    {
        for (int i = 0; i < slots.Count; ++i)
        {
            if (slots[i].item.name == item.name)
            {
                slots[i].UpdateData();
                return;
            }
        }
        NewItemUI(item);
    }

    public void NewItemUI(Item item)
    {
        var exa = Instantiate(example, content.transform);
        var ui = exa.GetComponent<ItemUI>();
        ui.item = item;
        ui.UpdateData();
        slots.Add(ui);
    }

    private void Update()
    {
        for (int i = 0; i < slots.Count; ++i)
        {
            slots[i].item = player.items[i];
            slots[i].UpdateData();
        }
    }
}
