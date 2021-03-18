using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuWindows : MonoBehaviour
{
    public GameObject play, selection, login, registr;

    private void Start()
    {
        Exit();
    }

    public void OpenSelection()
    {
        play.SetActive(false);
        login.SetActive(false);
        registr.SetActive(false);
        selection.SetActive(true);
    }

    public void OpenLoginning()
    {
        play.SetActive(false);
        login.SetActive(true);
        registr.SetActive(false);
        selection.SetActive(false);
    }

    public void OpenRegistration()
    {
        play.SetActive(false);
        login.SetActive(false);
        registr.SetActive(true);
        selection.SetActive(false);
    }

    public void Exit()
    {
        play.SetActive(true);
        login.SetActive(false);
        registr.SetActive(false);
        selection.SetActive(false);
    }
}
