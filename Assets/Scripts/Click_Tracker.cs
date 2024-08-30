using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Purchasing;
using System.Numerics;
using System;
using UnityEngine.UI;

public class Click_Tracker : MonoBehaviour
{
    //Colliders of the trackers
    public Collider2D plantCollider, meatCollider;

    public Transform plantCanv, meatCanv;

    private TextMeshProUGUI plantText, meatText;
    private TextMeshProUGUI plantPerSecondText, meatPerSecondText;

    //Click storage
    private BigInteger plantClicks, meatClicks;
    private BigInteger plantClicks_Prev, meatClicks_Prev;
    //Clicks per second
    private float plantPerSecond, meatPerSecond;

    //Afk clicks per second
    private BigInteger afkPlant, afkMeat;

    //Afk clicks interval,
    //Clicks per second interval
    private float timerInterval;

    //Multiplier for click/afk clicks
    private int multiplierClick;
    private int multiplierAfk;

    //Amount of clicks
    public int plantClickAmount, meatClickAmount;

    private UnityEngine.Vector3 initialScale, smallScale;

    public GameObject plusNumber;

    private BigInteger logOffPlant, logOffMeat;
    public Button afkMeatButton, afkPlantButton;

    void Awake()
    {
        InitializeMenus();
        initialScale = plantCollider.transform.localScale;
        smallScale = initialScale - new UnityEngine.Vector3(0.1f, 0.1f, 0.0f);
    }

    // Start is called before the first frame update
    void Start()
    {
        LoadPlayerData();
        InitializeVariables();
    }

    // Update is called once per frame
    void Update()
    {
        //Get all new clicks
        CheckPhysicalClicks();

        if ((int)timerInterval == 1)
        {
            CheckAfkClicks();
            CheckClicksPerSecond();
            CheckPreviousClicks();
            timerInterval = 0.0f;
        }

        ApplyText();

        timerInterval += Time.deltaTime;
    }

    private void InitializeVariables() {
        plantClickAmount = 1;
        meatClickAmount = 1;
        multiplierClick = 1;
        multiplierAfk = 1;
    }


    public BigInteger GetPlants()
    {
        return plantClicks;
    }

    public BigInteger GetMeat()
    {
        return meatClicks;
    }

    public string GetPlantsRead()
    {
        return plantClicks.ToString();
    }

    public string GetMeatRead()
    {
        return meatClicks.ToString();
    }

    public string GetAfkMeatRead() {
        return afkMeat.ToString();
    }

    public string GetAfkPlantRead() {
        return afkPlant.ToString();
    }

    public string GetLogOffPlant() {
        return logOffPlant.ToString();
    }

    public string GetLogOffMeat() {
        return logOffMeat.ToString();
    }

    public bool RemovePlants(int remove)
    {
        if (remove <= plantClicks)
        {
            plantClicks -= remove;
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool RemoveMeat(int remove)
    {
        if (remove <= meatClicks)
        {
            meatClicks -= remove;
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool RemoveClicks(int p, int m)
    {
        if (CheckPurchase(p, m))
        {
            plantClicks -= p;
            meatClicks -= m;
            return true;
        }
        else
        {
            return false;
        }
    }

    public void AddAfkClicks(string which, BigInteger x)
    {
        if(which == "Plant")
        {
            afkPlant += x;
        } else if (which == "Meat")
        {
            afkMeat += x;
        }
    }

    public void AddAfkClicks(BigInteger p, BigInteger m)
    {
        afkPlant += p;
        afkMeat += m;
    }

    private void CheckPhysicalClicks()
    {
        UnityEngine.Vector2 touchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetMouseButtonDown(0))
        {
            Collider2D touchedCollider = Physics2D.OverlapPoint(touchPosition, 1 << 8);
            if (plantCollider == touchedCollider)
            {
                plantCollider.transform.localScale = smallScale;
            } else if(meatCollider == touchedCollider)
            {
                meatCollider.transform.localScale = smallScale;
            }
        } else if(Input.GetMouseButtonUp(0))
        {
            Collider2D touchedCollider = Physics2D.OverlapPoint(touchPosition, 1 << 8);
            if (plantCollider == touchedCollider)
            {
                int addedPlants = plantClickAmount*multiplierClick;
                plantClicks += addedPlants;
                GameObject plusNum = Instantiate(plusNumber, new UnityEngine.Vector3(touchPosition.x, touchPosition.y, plantCollider.transform.position.z - 1.0f), UnityEngine.Quaternion.identity);
                plusNum.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "+" + addedPlants;
                plantCollider.transform.localScale = initialScale;
            }
            else if (meatCollider == touchedCollider)
            {
                int addedMeat = meatClickAmount*multiplierClick;
                meatClicks += addedMeat;
                GameObject plusNum = Instantiate(plusNumber, new UnityEngine.Vector3(touchPosition.x, touchPosition.y, meatCollider.transform.position.z - 1.0f), UnityEngine.Quaternion.identity);
                plusNum.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "+" + addedMeat;
                meatCollider.transform.localScale = initialScale;
            } else
            {
                plantCollider.transform.localScale = initialScale;
                meatCollider.transform.localScale = initialScale;
            }
        }
    }

    private void CheckAfkClicks()
    {
        plantClicks += multiplierAfk*afkPlant;
        meatClicks += multiplierAfk*afkMeat;
    }

    private void CheckPreviousClicks()
    {
        plantClicks_Prev = plantClicks;
        meatClicks_Prev = meatClicks;
    }



    private void CheckClicksPerSecond()
    {
        plantPerSecond = ((int)(plantClicks - plantClicks_Prev)) / timerInterval;
        meatPerSecond = ((int)(meatClicks - meatClicks_Prev)) / timerInterval;
    }

    private void ApplyText()
    {
        plantText.text = "" + plantClicks;
        meatText.text = "" + meatClicks;

        plantPerSecondText.text = plantPerSecond.ToString("0.00") + " Plants Per Second";
        meatPerSecondText.text = meatPerSecond.ToString("0.00") + " Meat Per Second";
    }

    void InitializeClicks()
    {
        plantClicks = 0;
        meatClicks = 0;
    }

    void InitializeMenus()
    {
        plantText = plantCanv.GetChild(2).GetComponent<TextMeshProUGUI>();
        meatText = meatCanv.GetChild(2).GetComponent<TextMeshProUGUI>();

        plantPerSecondText = plantCanv.GetChild(3).GetComponent<TextMeshProUGUI>();
        meatPerSecondText = meatCanv.GetChild(3).GetComponent<TextMeshProUGUI>();
    }

    public bool CheckPurchase(int p, int m)
    {
        if ((plantClicks >= p) && (meatClicks >= m))
        {
            return true;
        } else
        {
            return false;
        }
    }

    public void SetPlantClickAmount(int x)
    {
        plantClickAmount = x;
    }
    public void SetMeatClickAmount(int x)
    {
        meatClickAmount = x;
    }

    public int GetPlantClickAmount()
    {
        return plantClickAmount;
    }
    public int GetMeatClickAmount()
    {
        return meatClickAmount;
    }

    public int GetAfkMultiplier() {
        return multiplierAfk;
    }

    public int GetClickMultiplier() {
        return multiplierClick;
    }

    public void LoadPlayerData()
    {
        print(Application.persistentDataPath);
        Player_Data data = SaveSystem.LoadPlayer();
        if(data != null)
        {
            plantClicks = BigInteger.Parse(data.plantClicks);
            meatClicks = BigInteger.Parse(data.meatClicks);

            plantClickAmount = data.plantClickAmount;
            meatClickAmount = data.meatClickAmount;

            afkMeat = BigInteger.Parse(data.afkMeat);
            afkPlant = BigInteger.Parse(data.afkPlant);

            multiplierAfk = data.multiplierAfk;
            multiplierClick = data.multiplierClick;

            logOffPlant = BigInteger.Parse(data.previousLogOffPlant);
            logOffMeat = BigInteger.Parse(data.previousLogOffMeat);

            CalculateLogOffAfk(data.logOffDate);
        } else
        {
            print("No data :(");
        }
    }

    public void SavePlayerData()
    {
        SaveSystem.SavePlayer(this);
    }

    void OnApplicationQuit()
    {
        SavePlayerData();
    }

    public void AddClicks(BigInteger plants, BigInteger meat) {
        plantClicks += plants;
        meatClicks += meat;
    }

    public void BuyPlants(UnityEngine.Purchasing.Product product)
    {
        plantClicks += (int) product.definition.payout.quantity;
        print("You purchased some plants");
    }

    public void CalculateLogOffAfk(string logOffDate) {
        DateTime dateValue = DateTime.Parse(logOffDate);
        DateTime logInDate = DateTime.Now;
        TimeSpan timeDiff = logInDate.Subtract(dateValue);

        int secondsOff = (int)timeDiff.TotalSeconds;
        print("Seconds off: " + secondsOff);

        BigInteger plantsEarned = secondsOff * afkPlant * multiplierAfk;
        BigInteger meatEarned = secondsOff * afkMeat * multiplierAfk;
        print("Plants: " + plantsEarned);
        print("Meat: " + meatEarned);

        logOffPlant += plantsEarned;
        logOffMeat += meatEarned;
    }

    public void CollectAfkPlants() {
        GameObject plusNum = Instantiate(plusNumber, new UnityEngine.Vector3(afkPlantButton.transform.position.x, afkPlantButton.transform.position.y, afkPlantButton.transform.position.z - 1.0f), UnityEngine.Quaternion.identity);
        plusNum.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "+" + logOffPlant;
        afkPlantButton.interactable = false;

        plantClicks += logOffPlant;
        logOffPlant = 0;
    }

    public void CollectAfkMeat() {
        GameObject plusNum = Instantiate(plusNumber, new UnityEngine.Vector3(afkMeatButton.transform.position.x, afkMeatButton.transform.position.y, afkMeatButton.transform.position.z - 1.0f), UnityEngine.Quaternion.identity);
        plusNum.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "+" + logOffMeat;
        afkMeatButton.interactable = false;

        meatClicks += logOffMeat;
        logOffMeat = 0;
    }
}
