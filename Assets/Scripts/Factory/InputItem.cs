using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InputItem : MonoBehaviour
{
    public MachineInteractions machine;
    public Sprite disabled, enabled;
    public string craftName;

    Player player;
    private CraftItem _craft;

    void Start()
    {
        player = FindObjectOfType<Player>();
        _craft = machine.craftsDB.crafts.FirstOrDefault(craft => craft.name == craftName);
    }

    void Update()
    {
        GetComponent<SpriteRenderer>().sprite = CanPut() ? enabled : disabled;
    }

    private void OnMouseDown()
    {
        if (CanPut())
        {
            int i = 0;
            foreach (var mat in _craft.material)
            {
                i = 0;
                while (player.items[i].name != mat.name)
                {
                    ++i;
                }
                player.items[i].count -= mat.count;
            }

            machine.curCraft = craftName;

            machine.popup.SetActive(false);
            machine.output.GetComponent<SpriteRenderer>().sprite = GetComponent<SpriteRenderer>().sprite;
            machine.progress.transform.parent.gameObject.SetActive(true);
            machine.startMakingTime = TimeManager.GetUnixTime();
            machine.makeTime = _craft.makeTime;
            machine.state = MachineInteractions.State.working;
            Item outputItem = new Item(_craft);
            outputItem.icon = machine.itemPrefs.itemsDB.items.FirstOrDefault(it => it.name == outputItem.name).icon;
            machine.outputItem = outputItem;
            machine.progress.transform.parent.gameObject.SetActive(true);

        }
    }

    bool CanPut()
    {
        int i = 0;
        if (player.items.Count == 0) return false;
        foreach (var mat in _craft.material)
        {
            i = 0;
            while (player.items[i].name != mat.name)
            {
                ++i;
                if (player.items.Count == i) return false;
            }
            if (mat.count > player.items[i].count) return false;
        }
        return true;
    }
}
