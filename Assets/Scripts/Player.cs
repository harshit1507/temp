using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    private Touch touch;
    Rigidbody2D Rb;

    [Header("Player")]
    [SerializeField] int health = 200;
    [SerializeField] AudioClip deathSFX;
    [SerializeField] [Range(0, 1)] float deathSFXVolume = 0.75f;
    [SerializeField] AudioClip shootSound;
    [SerializeField] [Range(0, 1)] float shootSoundVolume = 0.25f;
    //[SerializeField] float speed = 0.01f;
    [SerializeField] float padding = 1f;    //KeyboardInput
    [SerializeField] float moveSpeed = 10f; //KeyboardInput

    [Header("Projectile")]
    [SerializeField] GameObject laserPrefab;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float projectileFiringPeriod = 0.1f;

    //GameSession gameSession;
    float xMin, xMax, yMin, yMax;   //KeyboardInput

    private void Awake()
    {
        SetUpSingleton();
    }

    private void SetUpSingleton()
    {
        if (FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        SetUpMoveBoundaries();  //KeyboardInput
        Rb = GetComponent<Rigidbody2D>();
        //gameSession = FindObjectOfType<GameSession>();
        StartCoroutine(FireContinously());
    }   

    // Update is called once per frame
    void Update()
    {
	    Move();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
        if(!damageDealer)
        {
            return;
        }
        ProcessHit(damageDealer);
    }

    private void ProcessHit(DamageDealer damageDealer)
    {
        health -= damageDealer.GetDamage();
        damageDealer.Hit();
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        FindObjectOfType<LevelController>().LoadGameOver();
        //PlayerPrefs.SetInt("HighScore", gameSession.GetScore());
        Destroy(gameObject);
        AudioSource.PlayClipAtPoint(deathSFX, Camera.main.transform.position, deathSFXVolume);
    }

    public int GetHealth()
    {
        return health;
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
            AudioSource.PlayClipAtPoint(shootSound, Camera.main.transform.position, shootSoundVolume);
	        yield return new WaitForSeconds(projectileFiringPeriod);
	    }
    }        
}
