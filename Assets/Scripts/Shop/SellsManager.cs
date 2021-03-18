using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SellsManager : MonoBehaviour
{
    public Image icon;
    public Text count;

    public TradeItem toSell;
    List<TradeItem> items;
    Player player;

    bool down;
    int reward;

    void Start()
    {
        down = false;
        player = FindObjectOfType<Player>();
        items = FindObjectOfType<TradeItemPrefs>().tradeItems;
        Create();
    }

    void Create()
    {
        toSell = new TradeItem(items[Random.Range(0, items.Count)]);

        toSell.count = Random.Range(1, 4);
        icon.gameObject.SetActive(true);
        icon.sprite = toSell.icon;
        count.text = toSell.count.ToString();

        reward = (int)(toSell.cost * toSell.count * Random.Range(0.85f, 1.25f));
    }

    void Buy()
    {
        int i = 0;
        while (toSell.name != player.items[i].name) ++i;
        if (CanBuy())
        {
            player.items[i].count -= toSell.count;
            //player.inventoryUI.UpdateInvUI(new Item(player.items[i]));
            player.coins += reward;

            var src = FindObjectOfType<SaveLoadManager>().GetComponent<AudioSource>();
            src.PlayOneShot(src.clip);
            count.text = null;
            icon.gameObject.SetActive(false);

            StartCoroutine(Delay(10));
        }
    }

    bool CanBuy()
    {
        int i = 0;
        while (toSell.name != player.items[i].name) ++i;
        return player.items[i].count >= toSell.count;
    }

    IEnumerator Delay(int sec)
    {
        yield return new WaitForSeconds(sec);
        Create();
    }

    private void OnMouseDown()
    {
        down = true;
    }

    private void OnMouseUp()
    {
        if (down) Buy();
    }
}
