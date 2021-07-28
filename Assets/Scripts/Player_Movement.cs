using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    private Touch touch;
    Rigidbody2D Rb;
    Coroutine firingCoroutine;
    
    [SerializeField] float speed = 0.01f;
    [SerializeField] float padding = 1f;
    [SerializeField] GameObject laserPrefab;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float projectileFiringPeriod = 1f;

    // Start is called before the first frame update
    void Start()
    {
        Rb = GetComponent<Rigidbody2D>();        
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
            if(touch.phase == TouchPhase.Moved)
            {
                Rb.MovePosition(new Vector2(touchPos.x, touchPos.y));
		firingCoroutine = StartCoroutine(FireContinously());
                
            }
	    else if(touch.phase == TouchPhase.Ended)
	    {
		//StopCoroutine(firingCoroutine);
	        StopAllCoroutines();
	    }

           
        }
    }
	
    IEnumerator FireContinously()
    {
	while(true)
	{
	    GameObject laser = Instantiate(laserPrefab, transform.position, Quaternion.identity) as GameObject;
	    laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, projectileSpeed);
	    yield return new WaitForSeconds(projectileFiringPeriod);
	}
    }
    


    
}
