using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameObject inventory;
    SaveLoadManager saveLoad;

    private void Start()
    {
        saveLoad = FindObjectOfType<SaveLoadManager>();
    }

    public void Active(GameObject obj)
    {
        obj.SetActive(!obj.activeInHierarchy);
    }

    public void AddCoins(int amount)
    {
        FindObjectOfType<Player>().coins += amount;
    }

    public void OpenMenu()
    {
        Destroy(FindObjectOfType<UserManager>().gameObject);
        SceneManager.LoadScene(0);
    }

    public void OpenFarm()
    {
        SceneManager.LoadScene(1);
    }

    public void OpenFactory()
    {
        SceneManager.LoadScene(2);
    }

    public void OpenShop()
    {
        SceneManager.LoadScene(3);
    }

    public void PlayOneShot(AudioSource clip)
    {
        clip.PlayOneShot(clip.clip);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
