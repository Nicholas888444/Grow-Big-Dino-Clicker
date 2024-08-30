using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dinosaur : MonoBehaviour
{
    private Animator anim, flashAnim;
    private SpriteRenderer sprite, flashingSprite;

    public float minX, maxX, minY, maxY;
    private Vector2 targetPosition;
    public float speed, flashSpeed;
    private bool flashTrack;
    public bool flashing = false;

    private float waitTime, waitForTime;

    public int Level;


    public string dino_name;
    public int init_plant_price, scale_plant_price, init_meat_price, scale_meat_price;
    public int plant_per_second, meat_per_second;
    public float height, width;

    void Awake() {
        LoadDinoData();
    }

    // Start is called before the first frame update
    void Start()
    {
        waitTime = 0.0f;
        waitForTime = 0.0f;
        sprite = transform.GetChild(0).GetComponent<SpriteRenderer>();
        flashingSprite = transform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>();
        anim = transform.GetChild(0).GetComponent<Animator>();
        flashAnim = transform.GetChild(0).GetChild(0).GetComponent<Animator>();
        targetPosition = GetRandomPosition();
    }

    // Update is called once per frame
    void Update()
    {
        if(Level == 0)
        {
            sprite.enabled = false;
            flashingSprite.enabled = false;
            return;
        } else
        {
            sprite.enabled = true;
            flashingSprite.enabled = true;
        }

        if (((Vector2)transform.position) != targetPosition)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed*Time.deltaTime);
            
            if(transform.position.x < targetPosition.x)
            {
                anim.SetBool("isMoving", true);
                flashAnim.SetBool("isMoving", true);
                sprite.flipX = false;
                flashingSprite.flipX = false;
            } else if(transform.position.x > targetPosition.x)
            {
                anim.SetBool("isMoving", true); //
                sprite.flipX = true;
                flashAnim.SetBool("isMoving", true);
                flashingSprite.flipX = true;
            }
        } else
        {
            anim.SetBool("isMoving", false);
            flashAnim.SetBool("isMoving", false);
            waitForTime = Random.Range(5.0f, 10.0f);
            if(waitTime >= waitForTime)
            {
                waitTime = 0.0f;
                targetPosition = GetRandomPosition();
            }
            waitTime += Time.deltaTime;
        }

        LevelScale();
        CalculateSize();
        Flashing();
    }

    Vector2 GetRandomPosition()
    {
        float randomX = Random.Range(minX, maxX);
        float randomY = Random.Range(minY, maxY);

        return new Vector2(randomX, randomY);
    }

    void LevelScale()
    {
        transform.localScale = new Vector3(1.0f + Level*0.05f, 1.0f + Level*0.05f, 1.0f);
    }

    public void IncreaseLevel()
    {
        Level += 1;
    }

    public int GetLevel()
    {
        return Level;
    }

    public float GetHeight() {
        return height;
    }

    public float GetWidth() {
        return width;
    }

    public int GetInitPlant()
    {
        return init_plant_price;
    }

    public int GetInitMeat()
    {
        return init_meat_price;
    }

    public int GetScalePlant()
    {
        return scale_plant_price;
    }

    public int GetScaleMeat()
    {
        return scale_meat_price;
    }

    public int GetPlantPerSecond()
    {
        return plant_per_second;
    }

    public int GetMeatPerSecond()
    {
        return meat_per_second;
    }

    public string GetName()
    {
        return dino_name;
    }

    public Sprite GetImage()
    {
        return transform.GetChild(0).GetComponent<SpriteRenderer>().sprite;
    }

    void OnApplicationQuit() {
        SaveDinoData();
    }

    public void SaveDinoData()
    {
        SaveSystem.SaveDino(this);
    }

    public string GetDiet() {
        if(plant_per_second >= 1 && meat_per_second >= 1) {
            return("Omnivore");
        } else if(plant_per_second >= 1) {
            return "Herbivore";
        } else if(meat_per_second >= 1) {
            return "Carnivore";
        } else {
            return "Nothing";
        }
    }

    public void LoadDinoData() {
        Dino_Data data = SaveSystem.LoadDino(this);
        if(data != null)
        {
            Level = data.dinoLevel;
        } else
        {
            print("No data :(");
        }
    }

    public void Flashing() {
        if(flashing) {
            if(flashTrack) {
                flashingSprite.color = new Color(flashingSprite.color.r, flashingSprite.color.g, flashingSprite.color.b, 
                flashingSprite.color.a - (Time.deltaTime * flashSpeed));

                if(flashingSprite.color.a <= 0.01f) {
                    flashTrack = false;
                }
            } else {
                flashingSprite.color = new Color(flashingSprite.color.r, flashingSprite.color.g, flashingSprite.color.b, 
                flashingSprite.color.a + (Time.deltaTime * flashSpeed));

                if(flashingSprite.color.a >= 0.80f) {
                    flashTrack = true;
                }
            }
        } else {
            flashingSprite.color = new Color(flashingSprite.color.r, flashingSprite.color.g, flashingSprite.color.b, 0);
        }
    }


    private void CalculateSize() {
        height = transform.GetChild(0).GetComponent<BoxCollider2D>().size.y * transform.localScale.y;
        width = transform.GetChild(0).GetComponent<BoxCollider2D>().size.x * transform.localScale.x;
    }
}
