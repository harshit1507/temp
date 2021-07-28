using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    private Touch touch;
    Rigidbody2D Rb;
    
    [SerializeField] float speed = 0.01f;
    [SerializeField] float padding = 1f;

    float xMin, xMax, yMin, yMax;

    // Start is called before the first frame update
    void Start()
    {
        Rb = GetComponent<Rigidbody2D>();
        SetBoundaries();
    }

    private void SetBoundaries()
    {
        Camera gameCamera = Camera.main;
        xMin = gameCamera.ViewportToScreenPoint(new Vector3(0, 0, 0)).x + padding;
        xMax = gameCamera.ViewportToScreenPoint(new Vector3(1, 0, 0)).x - padding;
        yMin = gameCamera.ViewportToScreenPoint(new Vector3(0, 0, 0)).y + padding;
        yMax = gameCamera.ViewportToScreenPoint(new Vector3(0, 1, 0)).y - padding;

    }

    // Update is called once per frame
    void Update()
    {
        /*
        if(Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);
            if(touch.phase == TouchPhase.Moved)
            {
               var Xpos = Mathf.Clamp(transform.position.x + touch.deltaPosition.x * speed, -xMin, xMax);
               var Ypos = Mathf.Clamp(transform.position.y + touch.deltaPosition.y * speed,  -yMin, yMax);

               transform.position = new Vector2(Xpos, Ypos);
            }
        }*/
        if(Input.touchCount > 0)
        {

            touch = Input.GetTouch(0);
            Vector2 touchPos = Camera.main.ScreenToWorldPoint(touch.position);
            if(touch.phase == TouchPhase.Moved)
            {
                /*if(touchPos.x < 0 && touchPos.y < 0)
                {
                    touchPos.x += 1f;
                    touchPos.y += 1f;


                }

                else if(touchPos.x > 0 && touchPos.y < 0)
                {
                    touchPos.x -= 1f;
                    touchPos.y += 1f;
                }

                else if(touchPos.x > 0 && touchPos.y > 0)
                {
                    touchPos.x -= 1f;
                    touchPos.y -= 1f;

                }

                else if(touchPos.x < 0 && touchPos.y > 0)
                {
                    touchPos.x += 1f;
                    touchPos.y -= 1f;
                }*/
                Rb.MovePosition(new Vector2(touchPos.x, touchPos.y));

                Debug.Log(touchPos.x + ", " + touchPos.y);
            }

           
        }
    }


    
}
