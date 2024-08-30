using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Purchase_Tracker : MonoBehaviour
{
    private int init_plant_amount, init_meat_amount, scale_plant_amount, scale_meat_amount;
    private int plantsPerSecond, meatPerSecond;

    private string dino_name;

    private TextMeshProUGUI pText, mText;
    private TextMeshProUGUI plantPerSecondText, meatPerSecondText;
    private TextMeshProUGUI levelText;
    private TextMeshProUGUI nameText;

    private Image im;

    private Button b;

    public Dinosaur dino;
    private Click_Tracker ct;
    // Start is called before the first frame update
    void Start()
    {
        ct = GameObject.Find("GameHandler").GetComponent<Click_Tracker>();
        pText = transform.GetChild(2).GetChild(1).GetComponent<TextMeshProUGUI>();
        mText = transform.GetChild(3).GetChild(1).GetComponent<TextMeshProUGUI>();
        plantPerSecondText = transform.GetChild(4).GetChild(1).GetComponent<TextMeshProUGUI>();
        meatPerSecondText = transform.GetChild(4).GetChild(4).GetComponent<TextMeshProUGUI>();
        nameText = transform.GetChild(0).GetChild(2).GetComponent<TextMeshProUGUI>();
        levelText = transform.GetChild(0).GetChild(3).GetComponent<TextMeshProUGUI>();

        im = transform.GetChild(0).GetChild(1).GetComponent<Image>();

        b = transform.GetChild(1).GetComponent<Button>();

        init_plant_amount = dino.GetInitPlant();
        init_meat_amount = dino.GetInitMeat();
        plantsPerSecond = dino.GetPlantPerSecond();
        meatPerSecond = dino.GetMeatPerSecond();
        scale_plant_amount = dino.GetScalePlant();
        scale_meat_amount = dino.GetScaleMeat();

        InitializeUI();
    }

    // Update is called once per frame
    void Update()
    {
        if(ct.CheckPurchase((init_plant_amount + scale_plant_amount * dino.GetLevel()), (init_meat_amount + scale_meat_amount * dino.GetLevel())))
        {
            b.interactable = true;
        } else
        {
            b.interactable = false;
        }

        UpdateUI();
    }

    public void UpgradeDino()
    {
        print(init_plant_amount + scale_plant_amount * dino.GetLevel());
        if(ct.RemoveClicks((init_plant_amount + scale_plant_amount * dino.GetLevel()), (init_meat_amount + scale_meat_amount * dino.GetLevel())))
        {
            dino.IncreaseLevel();
            ct.AddAfkClicks(plantsPerSecond, meatPerSecond);
        }
    }

    private void InitializeUI()
    {
        pText.text = "" + (init_plant_amount + (scale_plant_amount * dino.GetLevel()));
        mText.text = "" + (init_meat_amount + (scale_meat_amount * dino.GetLevel()));

        plantPerSecondText.text = "+" + plantsPerSecond;
        meatPerSecondText.text = "+" + meatPerSecond;

        levelText.text = "Level: " + dino.GetLevel();

        nameText.text = "" + dino.GetName();

        im.sprite = dino.GetImage();
    }

    private void UpdateUI()
    {
        pText.text = "" + (init_plant_amount + (scale_plant_amount * dino.GetLevel()));
        mText.text = "" + (init_meat_amount + (scale_meat_amount * dino.GetLevel()));

        levelText.text = "Level: " + dino.GetLevel();
    }
}
