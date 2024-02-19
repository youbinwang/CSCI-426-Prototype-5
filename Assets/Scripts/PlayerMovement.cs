using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float movementSpeed = 10f;
    private Rigidbody2D rb;
    private Vector2 movementInput;
    public ParticleSystem enemyHit;
    public AudioClip[] hitSounds;
    private AudioSource audioSource;

    public ParticleSystem playerDied;
    public GameObject gameEndUI;
    public GameManager gameManager;
    
    private bool isFlickering = false;
    public SpriteRenderer circleSprite; 
    

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        movementInput.x = Input.GetAxisRaw("Horizontal");
        movementInput.y = Input.GetAxisRaw("Vertical");
    }


    private void FixedUpdate()
    {
        if (movementInput.magnitude > 0)
        {
            rb.AddForce(movementInput.normalized * movementSpeed);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            PlayerDied();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("EnemyHit"))
        {
            PlayerHit(collision.gameObject);
        }
    }

    public void PlayerHit(GameObject hitObject)
    {
        enemyHit.Play();
        GameManager.score++;
        
        if (!isFlickering)
        {
            StartCoroutine(FlickerAndPause());
        }

        hitObject.GetComponent<EnemyController>().isEnemyDie = true;
        
        //if (hitSounds.Length > 0)
        //{
        //    AudioClip clip = hitSounds[UnityEngine.Random.Range(0, hitSounds.Length)];
        //    audioSource.PlayOneShot(clip);
        //}
        

        //Destroy(hitObject);//我尝试将敌人的销毁也同步到节奏上
    }


    IEnumerator FlickerAndPause()
    {
        isFlickering = true;
        float flickerDuration = 0.2f;

        for (int i = 0; i < 3; i++)
        {
            circleSprite.color = Color.white;
            yield return new WaitForSecondsRealtime(flickerDuration);
            
            circleSprite.color = new Color(1f, 0.8862f, 0.3686275f);
            yield return new WaitForSecondsRealtime(flickerDuration);
        }
        isFlickering = false;
    }
    


    public void PlayerDied()
    {
        Instantiate(playerDied, transform.position, Quaternion.identity).Play();
        gameManager.PlayerDiedSound();
        gameEndUI.SetActive(true);
        Debug.Log("Player Died!");
        Destroy(gameObject);
    }
}
