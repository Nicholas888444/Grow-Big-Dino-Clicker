using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dino_Data
{
    public int dinoLevel;
    
    public Dino_Data(Dinosaur d)
    {
        dinoLevel = d.GetLevel();
    }
}
