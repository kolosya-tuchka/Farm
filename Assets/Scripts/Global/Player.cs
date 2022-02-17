using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int coins = 3000, gems = 100;
    public List<Item> items = new List<Item>();
    public UIInventory inventoryUI;

    public bool CanBuyWithCoins(int coins)
    {
        return this.coins - coins >= 0;
    }

    public bool CanBuyWithGems(int gems)
    {
        return this.gems - gems >= 0;
    }

    public void AddItem(Item item)
    {
        for (int i = 0; i < items.Count; ++i)
        {
            if (item.name == items[i].name)
            {
                items[i].count += item.count;
                return;
            }
        }
        NewItem(item);
    }

    public void NewItem(Item item)
    {
        items.Add(item);
    }

}

[System.Serializable]
public class Item
{
    public string name;
    public int count;
    public Sprite icon;

    public Item(string name, Sprite icon, int count = 1)
    {
        this.name = name;
        this.count = count;
        this.icon = icon;
    }

    public Item(Item item)
    {
        this.count = item.count;
        this.icon = item.icon;
        this.name = item.name;
    }

    public Item(TradeItem trade)
    {
        this.count = trade.count;
        this.icon = trade.icon;
        this.name = trade.name;
    }

    public Item(CraftItem craft)
    {
        this.count = craft.count;
        this.name = craft.name;
    }
}

[System.Serializable]
public class TradeItem : Item
{
    public int cost;

    public TradeItem(Item item, int cost) : base(item)
    {
        this.cost = cost;
        this.count = item.count;
        this.icon = item.icon;
        this.name = item.name;
    }

    public TradeItem(TradeItem trade) : base(trade)
    {
        this.cost = trade.cost;
        this.count = trade.count;
        this.icon = trade.icon;
        this.name = trade.name;
    }
}

[System.Serializable]
public class CraftItem
{
    public string name;
    public int count = 1;
    public int makeTime;
    public List<Item> material;
}
