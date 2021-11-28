using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ErrorLog : MonoBehaviour
{
    public Text log;
    
    public void Exit()
    {
        Destroy(gameObject);
    }

}
