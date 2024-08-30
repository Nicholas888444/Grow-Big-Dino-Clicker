using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Floating_Text : MonoBehaviour
{
    private float Timer;
    private TextMeshProUGUI text;
    public float speed, colorSpeed;
    // Start is called before the first frame update
    void Start()
    {
        Timer = 0.0f;
        text = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(0.0f, Time.deltaTime * speed, 0.0f);
        text.color = new Color(text.color.r, text.color.b, text.color.g, text.color.a - Time.deltaTime*colorSpeed);
        if ((int)Timer == 2.0f)
        {
            Destroy(gameObject);
        }
        Timer += Time.deltaTime;
    }
}
