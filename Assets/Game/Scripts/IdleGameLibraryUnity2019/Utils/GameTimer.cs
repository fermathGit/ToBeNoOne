using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTimer
{
    DateTime Time = DateTime.Now;
    double TotalTime = 0;
    bool IsEnd = false;
    string savekey = "";

    public GameTimer(string _savekey = "")
    {
        savekey = _savekey;
        if(savekey != "") TotalTime = double.Parse(PlayerPrefs.GetString(savekey, "0"));
    }

    public void TimeEnd()
    {
        IsEnd = true;
        TotalTime += (DateTime.Now - Time).TotalSeconds;
        SaveTime();
    }

    public void TimeStart()
    {
        IsEnd = false;
        Time = DateTime.Now;
    }

    public int GetTotalTime()
    {
        if (!IsEnd)
        {
            TotalTime += (DateTime.Now - Time).TotalSeconds;
            Time = DateTime.Now;
            SaveTime();
        }
        return (int)TotalTime;
    }

    public void ResetTime()
    {
        Time = DateTime.Now;
        TotalTime = 0;
        SaveTime();
    }

    public void SaveTime(bool IsGet = false)
    {
        if(IsGet)
        {
            if (!IsEnd)
            {
                TotalTime += (DateTime.Now - Time).TotalSeconds;
                Time = DateTime.Now;
            }
        }
        if (savekey != "")
        {
            PlayerPrefs.SetString(savekey, TotalTime.ToString());
            PlayerPrefs.Save();
        }
    }
}
