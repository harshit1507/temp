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
    [SerializeField] GameObject laserPrefab;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float projectileFiringPeriod = 0.1f;
    [SerializeField] float moveSpeed = 10f; //KeyboardInput

    float xMin, xMax, yMin, yMax;   //KeyboardInput

    // Start is called before the first frame update
    void Start()
    {
        SetUpMoveBoundaries();  //KeyboardInput
        Rb = GetComponent<Rigidbody2D>();
        StartCoroutine(FireContinously());
    }   

    // Update is called once per frame
    void Update()
    {
	    Move();
    }

    void Move()
    {
        if(Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);
            Vector2 touchPos = Camera.main.ScreenToWorldPoint(touch.position);
            switch(touch.phase)
            {
                case TouchPhase.Moved:
                    Rb.MovePosition(new Vector2(touchPos.x, touchPos.y));                    
                    break;
            }           
        }

        #region keyboardInput
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;

        var newXPos = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax);
        var newYPos = Mathf.Clamp(transform.position.y + deltaY, yMin, yMax);

        transform.position = new Vector2(newXPos, newYPos);
        #endregion
    }
    #region keyboardInputMethod
    private void SetUpMoveBoundaries()
    {
        // Initialize Boundaries for the player
        // ViewPortToWorldPoint() => Convert the position of something as it relates to the camera view into the world's space view.

        Camera gameCamera = Camera.main;
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + padding;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - padding;

        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + padding;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - padding;
    }
    #endregion
    public IEnumerator FireContinously()
    {
	    while(true)
	    {
	        GameObject laser = Instantiate(laserPrefab, transform.position, Quaternion.identity) as GameObject;
	        laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, projectileSpeed);
	        yield return new WaitForSeconds(projectileFiringPeriod);
	    }
    }        
}
