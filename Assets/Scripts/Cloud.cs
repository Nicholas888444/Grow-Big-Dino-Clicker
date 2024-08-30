using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{
    private float Speed;
    private Vector3 dir = new Vector3(-1, 0, 0);

    public GameObject[] clouds;

    public float x, yMin, yMax, z;

    void Awake()
    {
        Speed = Random.Range(1.0f, 1.5f); //
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += (Speed * Time.deltaTime) * dir;

        if(transform.localPosition.x <= -x)
        {
            GameObject newCloud = Instantiate(clouds[Random.Range(0, 2)], new Vector3(x, Random.Range(yMin, yMax), z), Quaternion.identity, transform.parent);
            Destroy(gameObject);
        }
    }
}
