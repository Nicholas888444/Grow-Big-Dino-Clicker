using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Menu_Navigation : MonoBehaviour
{
    private int sceneNum;
    public GameObject[] menus;
    public GameObject[] clickers;
    //public TextMeshProUGUI materials, mps;


    void Awake()
    {
        SwitchScene(1);
    }

    public void SwitchScene(int i)
    {
        sceneNum = i;
        foreach (GameObject g in menus)
        {
            g.SetActive(false);
        }
        menus[sceneNum].SetActive(true);


            foreach (GameObject c in clickers)
            {
                c.SetActive(false);
            }

        if (sceneNum == 1 || sceneNum == 2)
        {
            clickers[sceneNum - 1].SetActive(true);
        }
    }

    public void TurnCanvasOff()
    {
        foreach (GameObject g in menus)
        {
            g.SetActive(false);
        }

        foreach (GameObject c in clickers)
        {
            c.SetActive(false);
        }
    }

    public int GetScene()
    {
        return sceneNum;
    }
}
