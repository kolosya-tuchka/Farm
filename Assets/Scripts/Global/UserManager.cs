using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UserManager : MonoBehaviour
{
    public TMP_InputField logName, logPass, regName, regPass;
    public Text loginLogs, regLogs;

    public string login, password;
    public string url;

    void Start()
    {
        url = "http://kolosya.beget.tech/BD/";
    }

    void Update()
    {

    }

    public void LogIn()
    {
        StartCoroutine(LogCor());
    }

    public void Reg()
    {
        StartCoroutine(RegCor());
    }

    IEnumerator RegCor()
    {
        login = regName.text;
        password = regPass.text;

        regName.text = null;
        regPass.text = null;
        regLogs.text = null;

        if (login.Length < 4 || password.Length < 4)
        {
            regLogs.text = "invalid name or password";
            yield break;
        }

        WWWForm form = new WWWForm();
        form.AddField("login", login);
        form.AddField("password", password);
        WWW www = new WWW(url+"register.php", form);
        yield return www;

        Debug.Log(www.error);
        Debug.Log(www.text);

        if (www.text == "1")
        {
            regLogs.text = "This profile alredy exists!";
            yield break;
        }

        if (www.text == "0")
        {
            DontDestroyOnLoad(gameObject);
            SceneManager.LoadScene(1);
        }
    }

    IEnumerator LogCor()
    {
        login = logName.text;
        password = logPass.text;

        logName.text = null;
        logPass.text = null;
        loginLogs.text = null;

        WWWForm form = new WWWForm();
        form.AddField("login", login);
        form.AddField("password", password);
        WWW www = new WWW(url + "login.php", form);
        yield return www;

        Debug.Log(www.error);
        Debug.Log(www.text);

        if (www.text == "1")
        {
            loginLogs.text = "Incorrect name!";
            yield break;
        }

        if (www.text == "2")
        {
            loginLogs.text = "Incorrect paswword!";
            yield break;
        }

        if (www.text == "0")
        {
            DontDestroyOnLoad(gameObject);
            SceneManager.LoadScene(1);
        }
    }
}
