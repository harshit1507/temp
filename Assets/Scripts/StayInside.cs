using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StayInside : MonoBehaviour
{
        Vector2 screenBounds;
    // Start is called before the first frame update
    void Start()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        Debug.Log(screenBounds.x + ", " + screenBounds.y);
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position = new Vector2(Mathf.Clamp(transform.position.x, -4.5f, 4.5f), Mathf.Clamp(transform.position.y, -9f, 9f));
        transform.position = new Vector2(Mathf.Clamp(transform.position.x, -(screenBounds.x - 1), (screenBounds.x - 1)), Mathf.Clamp(transform.position.y, -(screenBounds.y - 1), (screenBounds.y - 1)));        
    }
}
