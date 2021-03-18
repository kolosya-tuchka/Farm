using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmInteractions : MonoBehaviour
{
    public GameObject shovelPopup, waterPopup, plantsPopup, chikenPopup, pigPopup;
    Player player;

    Vector3 mousePos;
    Soil curSoil;
    Chuong curChuong;
    bool canInteract;

    public AudioClip shovelSound, waterSound, plantSound, chickSound, pigSound;
    AudioSource source;

    void Start()
    {
        source = GetComponent<AudioSource>();
        player = GetComponent<Player>();
        DisablePopups();
    }

    void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetMouseButtonDown(0))
        {
            if (canInteract)
            {
                DisablePopups();
                RaycastHit2D hit;
                hit = Physics2D.Raycast(mousePos, Vector3.forward);
                var obj = hit.collider.gameObject.GetComponent<Soil>();

                if (obj != null)
                {
                    curSoil = obj;
                    curChuong = null;
                    if (curSoil.plant != null)
                    {
                        if (curSoil.plant.state == global::Plant.State.growed)
                        {
                            Pick();
                        }
                    }
                    else SelectField();
                }
                else
                {
                    var obj2 = hit.collider.gameObject.GetComponent<Chuong>();
                    if (obj2 != null)
                    {
                        curSoil = null;
                        curChuong = obj2;
                        SelectField();
                    }
                }
            }
            else canInteract = true;
        }

        
    }

    void SelectField()
    {
        DisablePopups();

        if (curSoil != null)
        {
            switch (curSoil.type)
            {
                case Soil.soilType.dirt: SetField(shovelPopup); return;
                case Soil.soilType.unwatered: SetField(waterPopup); return;
                case Soil.soilType.watered: SetField(plantsPopup); return;
            }
        }
        else if (curChuong != null)
        {
            switch (curChuong.animal)
            {
                case Chuong.animalType.chiken: SetField(chikenPopup); return;
                case Chuong.animalType.pig: SetField(pigPopup); return;
            }
        }
    }

    void SetField(GameObject popup)
    {
        popup.SetActive(true);
        popup.transform.position = new Vector3(mousePos.x - 0.5f, mousePos.y + 0.5f, 0);
        canInteract = false;
    }

    void DisablePopups()
    {
        shovelPopup.SetActive(false);
        waterPopup.SetActive(false);
        plantsPopup.SetActive(false);
        chikenPopup.SetActive(false);
        pigPopup.SetActive(false);
    }

    public void Water()
    {
        source.PlayOneShot(waterSound);
        curSoil.type = Soil.soilType.watered;
        ChangeSoil();
    }

    public void Shovel()
    {
        source.PlayOneShot(shovelSound);
        curSoil.type = Soil.soilType.unwatered;
        ChangeSoil();
    }

    public void Plant(SpawnManager plant)
    {
        if (player.CanBuyWithCoins(plant.cost))
        {
            source.PlayOneShot(plantSound);
            var pl = Instantiate(plant.toSpawn.gameObject, curSoil.transform.position + new Vector3(0, 0.1f, 0), Quaternion.identity, curSoil.transform);
            curSoil.plant = pl.GetComponent<Plant>();
            plant.toSpawn.GetComponent<Plant>().home = curSoil;
            pl.GetComponent<Plant>().startTime = TimeManager.GetUnixTime();
            player.coins -= plant.cost;
            curSoil.type = Soil.soilType.planted;
            ChangeSoil();
        }
    }

    public void SpawnAnimal(SpawnManager animal)
    {
        if (player.CanBuyWithCoins(animal.cost) && curChuong.CanSpawn())
        {
            var an = Instantiate(animal.toSpawn.gameObject, curChuong.transform.position, Quaternion.identity, curChuong.transform);
            var cont = an.GetComponent<AnimalControls>();
            cont.home = curChuong;
            cont.startTime = TimeManager.GetUnixTime();
            curChuong.animalCount++;
            player.coins -= animal.cost;

            switch (cont.item.name)
            {
                case "chicken": source.PlayOneShot(chickSound); break;
                case "pig": source.PlayOneShot(pigSound); break;
            }
        }
    }

    public void Pick()
    {
        source.PlayOneShot(plantSound);
        Item it = new Item(curSoil.plant.name, curSoil.plant.icon, curSoil.plant.count);
        player.AddItem(it);
        it = new Item(curSoil.plant.name, curSoil.plant.icon, curSoil.plant.count);
        player.inventoryUI.UpdateInvUI(it);
        Destroy(curSoil.plant.gameObject);
        curSoil.type = Soil.soilType.watered;
        ChangeSoil();
    }

    public void PickAnimal(AnimalControls an)
    {
        Item item = an.item;

        Item it = new Item(item);
        player.AddItem(it);
        it = new Item(item);
        player.inventoryUI.UpdateInvUI(it);
        an.home.animalCount--;

        switch (an.item.name)
        {
            case "chicken": source.PlayOneShot(chickSound); break;
            case "pig": source.PlayOneShot(pigSound); break;
        }

        Destroy(an.gameObject);
    }

    void ChangeSoil()
    {
        curSoil.UpdateSprite();
        DisablePopups();
    }
}
