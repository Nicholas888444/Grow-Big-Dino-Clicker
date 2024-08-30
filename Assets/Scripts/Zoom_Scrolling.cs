using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zoom_Scrolling : MonoBehaviour
{
    private Camera camera;
    private Transform cameraTransform;
    private Rigidbody2D camRb;
    private int previous_scene;

    //Scroll controls
    private bool mouseDown;
    private Vector2 previousTouch;
    public float speed;

    private float x_Speed, y_Speed;

    //Zoom controls
    private float previousDistance;

    //Enabled
    private bool scroll_mode;
    private Menu_Navigation mn;
    public Dino_Canvas dc;

    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.main;
        mn = GetComponent<Menu_Navigation>();
        camRb = camera.GetComponent<Rigidbody2D>();
        cameraTransform = camera.transform;
        previous_scene = 0;
        cameraTransform.GetComponent<BoxCollider2D>().enabled = true;
        scroll_mode = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(!scroll_mode)
        {
            return;
        }

        /*
        if (Input.GetMouseButtonDown(0))
            Debug.Log("Pressed left-click.");

        if (Input.GetMouseButtonDown(1))
            Debug.Log("Pressed right-click.");

        if (Input.GetMouseButtonDown(2))
            Debug.Log("Pressed middle-click.");
        */
        if(Input.GetMouseButtonDown(0))
        {
            mouseDown = true;
            previousTouch = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        } else if(Input.GetMouseButtonUp(0)) {
            mouseDown = false;
            //previousTouch = null;
        }

        if(mouseDown) {
            //Scroll
            Vector2 touchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            
            if(previousTouch != null)
            {
                float xDiff = touchPosition.x - previousTouch.x;
                float yDiff = touchPosition.y - previousTouch.y;

                //cameraTransform.position = new Vector3(cameraTransform.position.x + (-1*(xDiff*speed)), cameraTransform.position.y + (-1*(yDiff*speed)), cameraTransform.position.z);
                camRb.velocity += new Vector2((-1 * (xDiff * speed)), (-1 * (yDiff * speed)));
            }

            previousTouch = touchPosition;
        }
        
        
        //Zoom

        float scrollInput = Input.mouseScrollDelta.y;
        camera.orthographicSize -= scrollInput;

        if(camera.orthographicSize < 1.0f)
        {
            camera.orthographicSize = 1.0f;
        } else if(camera.orthographicSize > 100.0f)
        {
            camera.orthographicSize = 100.0f;
        }
        
    }

    public void SetScroll()
    {
        scroll_mode = !scroll_mode;
        if(scroll_mode)
        {
            previous_scene = mn.GetScene();
            dc.ToggleTouch(true);
            mn.TurnCanvasOff();
            camRb.constraints = RigidbodyConstraints2D.FreezeRotation;
        } else
        {
            dc.ToggleTouch(false);
            dc.ToggleCanvas(false);
            mn.SwitchScene(previous_scene);
            camera.orthographicSize = 5.0f;
            cameraTransform.position = new Vector3(0.0f, 0.0f, -110.5f);
            camRb.constraints = RigidbodyConstraints2D.FreezeAll;
        }
    }


}
