using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Player_Data
{
    public string plantClicks, meatClicks;
    public int plantClickAmount, meatClickAmount;
    public int multiplierAfk, multiplierClick;
    public string afkPlant, afkMeat;
    public string previousLogOffPlant, previousLogOffMeat;
    
    public string logOffDate;
    
    public Player_Data(Click_Tracker ct)
    {
        plantClicks = ct.GetPlantsRead();
        meatClicks = ct.GetMeatRead();

        plantClickAmount = ct.GetPlantClickAmount();
        meatClickAmount = ct.GetMeatClickAmount();

        multiplierAfk = ct.GetAfkMultiplier();
        multiplierClick = ct.GetClickMultiplier();

        afkMeat = ct.GetAfkMeatRead();
        afkPlant = ct.GetAfkPlantRead();

        previousLogOffPlant = ct.GetLogOffPlant();
        previousLogOffMeat = ct.GetLogOffMeat();

        logOffDate = (DateTime.Now).ToString();
    }
}
