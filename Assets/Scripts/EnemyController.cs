using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using MoreMountains.Feedbacks;
using MoreMountains.Tools;

public class EnemyController : MonoBehaviour
{
    public BeatMatching beatMatching;
    public float moveDistance = 1.0f;
    public float moveDuration = 0.25f;
    
    public Transform attackTransform;
    public float explosionScaleValue = 5f;
    private Vector3 explosionScale;
    public float explosionDuration = 0.25f;

    private SpriteRenderer spriteRenderer;
    public Transform player;

    //Enemy Destroy
    [SerializeField] public bool isEnemyDie = false;
    [SerializeField] public Color friendlyColor = new Color(1f, 1f, 1f, 0.5f);

    [SerializeField] public MMFeedbacks enemyDieFeedbacks;
    private bool isPlayedFeedback;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        explosionScale = new Vector3(explosionScaleValue, explosionScaleValue, explosionScaleValue);

        if (beatMatching != null)
        {
            beatMatching.OnBeat += MoveOnBeat;
        }
    }

    private void Update()
    {
        if (player != null && Vector3.Distance(transform.position, player.position) > 60f) //与玩家距离 > x
        {
            Destroy(gameObject);
        }

        if (isEnemyDie)//在节奏上销毁敌人并播放特效
        {
            spriteRenderer.color = friendlyColor;
            if (beatMatching.isOntheBeat && !isPlayedFeedback)
            {
                isPlayedFeedback = true;
                enemyDieFeedbacks.PlayFeedbacks();
                Invoke("DestroyEnemy",0.2f);
            }
        }
    }

    void DestroyEnemy()
    {
        Destroy(gameObject);
    }


    void MoveOnBeat(int beatNumber)
    {
        if (beatNumber <= 3)
        {
            Vector3 targetPosition = transform.position + GetDirection() * moveDistance;
            StartCoroutine(MoveToPosition(transform.position, targetPosition, moveDuration));
        }
        else if (beatNumber == 4 && !isEnemyDie)
        {
            EnemyFourthBeat();
        }
    }
    
    Vector3 GetDirection()
    {
        if (spriteRenderer.sprite.name == "Square")
        {
            return RandomSquareDirection();
        }
        else if (spriteRenderer.sprite.name == "Triangle")
        {
            return RandomTriangleDirection();
        }
        return Vector3.zero;
    }
    
    Vector3 RandomSquareDirection()
    {
        int randomDirection = Random.Range(0, 4);
        switch (randomDirection)
        {
            case 0:
                return Vector3.up;
            case 1:
                return Vector3.down;
            case 2:
                return Vector3.left;
            case 3:
                return Vector3.right;
            default:
                return Vector3.zero;
        }
    }
    
    Vector3 RandomTriangleDirection()
    {
        int randomDirection = Random.Range(0, 3);
        switch (randomDirection)
        {
            case 0:
                return (Vector3.down + Vector3.left).normalized;
            case 1:
                return (Vector3.down + Vector3.right).normalized;
            case 2:
                return Vector3.up;
            default:
                return Vector3.zero;
        }
    }
    
    
    void EnemyFourthBeat()
    {
        if (attackTransform != null)
        {
            attackTransform.localScale = explosionScale;
            StartCoroutine(ResetAndHideAttack());
        }
    }
    
    IEnumerator ResetAndHideAttack()
    {
      
        yield return new WaitForSeconds(explosionDuration);
      
        attackTransform.localScale = Vector3.zero;
    }
    
    IEnumerator MoveToPosition(Vector3 fromPosition, Vector3 toPosition, float duration)
    {
        float elapsed = 0.0f;
        while (elapsed < duration)
        {
            transform.position = Vector3.Lerp(fromPosition, toPosition, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.position = toPosition;
    }

  
    void OnDestroy()
    {
        if (beatMatching != null)
        {
            beatMatching.OnBeat -= MoveOnBeat;
        }
    }
}
