using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Collider : MonoBehaviour
{
    public RectTransform generalCanvas;
    private BoxCollider2D boxCollider;
    void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        boxCollider.size = new Vector3(generalCanvas.rect.width * generalCanvas.localScale.x, generalCanvas.rect.height * generalCanvas.localScale.y, 1.0f);
    }
}
