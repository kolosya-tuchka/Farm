using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputItem : MonoBehaviour
{
    public MachineInteractions machine;
    public Sprite disabled, enabled;
    public CraftItem craft;
    public Item outputItem;

    Player player;

    void Start()
    {
        player = FindObjectOfType<Player>();
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
            foreach (var mat in craft.material)
            {
                i = 0;
                while (player.items[i].name != mat.name)
                {
                    ++i;
                }
                player.items[i].count -= mat.count;
            }

            machine.curCraft = 0;
            while (machine.crafts[machine.curCraft] != this) machine.curCraft++;

            machine.popup.SetActive(false);
            machine.output.GetComponent<SpriteRenderer>().sprite = GetComponent<SpriteRenderer>().sprite;
            machine.progress.transform.parent.gameObject.SetActive(true);
            machine.startMakingTime = TimeManager.GetUnixTime();
            machine.makeTime = craft.makeTime;
            machine.state = MachineInteractions.State.working;
            machine.outputItem = outputItem;
            machine.progress.transform.parent.gameObject.SetActive(true);

        }
    }

    bool CanPut()
    {
        int i = 0;
        if (player.items.Count == 0) return false;
        foreach (var mat in craft.material)
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
