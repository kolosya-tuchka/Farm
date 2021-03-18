using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TimeManager
{
    public static int GetUnixTime()
    {
        return (int)(System.DateTime.Now - new System.DateTime(1970, 1, 1, 0, 0, 0, 0)).TotalSeconds;
    }
}
