using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dino_Canvas : MonoBehaviour
{
    private Dinosaur dino;
    public Menu_Navigation mn;
    private TextMeshProUGUI nameText, dietText, plantText, meatText, heightText, widthText, levelText;

    private Camera cam;

    private bool toggle;

    void Awake() {
        cam = Camera.main;
    }


    // Start is called before the first frame update
    void Start()
    {
        ToggleCanvas(false);
        nameText = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        levelText = transform.GetChild(2).GetComponent<TextMeshProUGUI>();
        heightText = transform.GetChild(3).GetComponent<TextMeshProUGUI>();
        widthText = transform.GetChild(4).GetComponent<TextMeshProUGUI>();

        dietText = transform.GetChild(6).GetComponent<TextMeshProUGUI>();
        plantText = transform.GetChild(7).GetComponent<TextMeshProUGUI>();
        meatText = transform.GetChild(8).GetComponent<TextMeshProUGUI>();
    }

    public void SetDino(Dinosaur d) {
        mn.TurnCanvasOff();
        ToggleCanvas(true);
        dino = d;
        dino.flashing = true;
        InitializeDinoCanvas();
    }

    public void DinoOff() {
        ToggleCanvas(false);
        dino.flashing = false;
        dino = null;
    }

    private void InitializeDinoCanvas() {
        nameText.text = dino.GetName();
        dietText.text = dino.GetDiet();
        plantText.text = dino.GetPlantPerSecond() * dino.GetLevel() + " plants per second";
        meatText.text = dino.GetMeatPerSecond() * dino.GetLevel() + " meat per second";
        heightText.text = "Height: " + dino.GetHeight() + " m";
        widthText.text = "Width: " + dino.GetWidth() + " m";
        levelText.text = "Level " + dino.GetLevel();
    }

    // Update is called once per frame
    void Update()
    {
        if(!toggle) {
            return;
        }
        CheckPhysicalClicks();
        if(dino != null) {
            cam.transform.position = new Vector3(dino.transform.GetChild(0).transform.position.x, dino.transform.GetChild(0).transform.position.y, cam.transform.position.z);
            cam.orthographicSize = dino.GetWidth();
            plantText.text = dino.GetPlantPerSecond() * dino.GetLevel() + " plants per second";
            meatText.text = dino.GetMeatPerSecond() * dino.GetLevel() + " meat per second";
            heightText.text = "Height: " + dino.GetHeight() + " m";
            widthText.text = "Width: " + dino.GetWidth() + " m";
            levelText.text = "Level " + dino.GetLevel();
        }
    }

    

    private void CheckPhysicalClicks()
    {
        Vector2 touchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetMouseButtonDown(0))
        {
            Collider2D touchedCollider = Physics2D.OverlapPoint(touchPosition, 1 << 7);
            if (touchedCollider != null)
            {
                if(dino == null) {
                    SetDino(touchedCollider.GetComponentInParent<Dinosaur>());
                } else {
                    DinoOff();
                }
            }
        }
    }

    public void ToggleCanvas(bool tog) {
        GetComponent<Canvas>().enabled = tog;
    }

    public void ToggleTouch(bool tog) {
        toggle = tog;
    }


}
