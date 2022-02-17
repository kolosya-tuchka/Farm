using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class MachineInteractions : MonoBehaviour
{
    public State state;
    public GameObject output, popup;
    public Image progress;
    public int makeTime, startMakingTime;
    public Item outputItem;
    public CraftsDB craftsDB;
    public string curCraft;
    public ItemPrefs itemPrefs;
    
    Player player;

    public enum State
    {
        hibernating, working, waiting
    }

    void Start()
    {
        player = FindObjectOfType<Player>();
        popup.SetActive(false);
        progress.transform.parent.gameObject.SetActive(false);
        output.SetActive(false);
    }

    void Update()
    {
        switch (state)
        {
            case State.hibernating:
                {
                    break;
                }
            case State.working:
                {
                    progress.fillAmount = ((float)(TimeManager.GetUnixTime() - startMakingTime) / (float)makeTime);
                    if (TimeManager.GetUnixTime() > startMakingTime + makeTime)
                    {
                        state = State.waiting;
                        output.SetActive(true);
                        progress.transform.parent.gameObject.SetActive(false);
                    }
                    break;
                }
            case State.waiting:
                {
                    break;
                }
        }
    }

    private void OnMouseDown()
    {
        switch (state)
        {
            case State.hibernating:
                {
                    popup.SetActive(!popup.activeInHierarchy);
                    break;
                }
            case State.working:
                {
                    break;
                }
            case State.waiting:
                {
                    progress.transform.parent.gameObject.SetActive(false);
                    player.AddItem(new Item(outputItem));
                    player.inventoryUI.UpdateInvUI(new Item(outputItem));
                    output.SetActive(false);
                    state = State.hibernating;
                    break;
                }
        }
    }

    public void Init(MachineData data)
    {
        this.state = (MachineInteractions.State)data.state;

        switch (this.state)
        {
            case MachineInteractions.State.hibernating:
                {
                    popup.SetActive(false);
                    output.SetActive(false);
                    progress.transform.parent.gameObject.SetActive(false);
                    break;
                }
            case MachineInteractions.State.working:
                {
                    curCraft = data.curCraft;
                    startMakingTime = data.startTime;
                    popup.SetActive(false);
                    output.GetComponent<SpriteRenderer>().sprite = itemPrefs.itemsDB.items.FirstOrDefault(it => it.name == curCraft).icon;
                    output.SetActive(false);
                    progress.transform.parent.gameObject.SetActive(true);
                    outputItem = new Item(craftsDB.crafts.FirstOrDefault(c => c.name == curCraft));
                    outputItem.icon = itemPrefs.itemsDB.items.FirstOrDefault(it => it.name == outputItem.name).icon;
                    break;
                }
            case MachineInteractions.State.waiting:
                {
                    popup.SetActive(false);
                    output.SetActive(true);
                    progress.transform.parent.gameObject.SetActive(false);
                    break;
                }
        }
    }
}
