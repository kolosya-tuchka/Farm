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

    public GameObject errorLog;

    void Start()
    {
        url = "http://kolosya.beget.tech/BD/";
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

        if (www.error == "Cannot resolve destination host")
        {
            regLogs.text = "No internet connection!";
            yield break;
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

        if (www.error == "Cannot resolve destination host")
        {
            loginLogs.text = "No internet connection!";
            yield break;
        }
    }

    public IEnumerator ExitWithError(string text)
    {
        SceneManager.LoadScene(0);
        yield return new WaitUntil(()=>SceneManager.GetActiveScene().buildIndex == 0);
        NewErrorWindow(text);
        Destroy(gameObject);
    }

    void NewErrorWindow(string text)
    {
        GameObject error = Instantiate(errorLog);
        errorLog.GetComponent<ErrorLog>().log.text = text;
    }
}
