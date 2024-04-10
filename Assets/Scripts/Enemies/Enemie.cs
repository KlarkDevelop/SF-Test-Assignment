using UnityEngine;
using System;

public class Enemie : MonoBehaviour
{
    public float speed;
    public float attackDistance;
    public float attackDelay;
    private float lastAtackTime;

    public static event Action onDeath;
    public static event Action onAttack;

    private AudioSource _audioSource;
    [SerializeField] private AudioClip soundAttack;
    [SerializeField] private AudioClip soundDeath;

    private Animator _animator;
    private Vector2 destination;
    private Vector2 moveVector;
    private bool isDead = false;
    private bool isRight;
    private bool isGameOver;
    private void Awake()
    {
        Physics2D.IgnoreLayerCollision(3, 3);
        if (transform.position.x > 0)
        {
            isRight = true;
            destination = new Vector2(0 + attackDistance, transform.position.y);
            moveVector = Vector2.left;
        }
        else
        {
            isRight = false;
            destination = new Vector2(0 - attackDistance, transform.position.y);
            GetComponent<SpriteRenderer>().flipX = true;
            moveVector = Vector2.right;
        }
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();

        Player.onDeath += OnPlayerDeath;
    }

    private void OnPlayerDeath()
    {
        _animator.SetInteger("AnimState", 0);
        isGameOver = true;
    }

    private void Update()
    {
        if (!isGameOver)
        {
            if (!isDead)
            {
                if ((isRight && transform.position.x > destination.x) || (!isRight && transform.position.x < destination.x))
                    Move();
                else
                    if (Time.time - lastAtackTime > attackDelay) Atack();
            }
        }
    }

    private void Move()
    {
        transform.Translate(moveVector * speed * Time.deltaTime);
        _animator.SetInteger("AnimState", 2);
    }

    private void Atack()
    {
        _animator.SetTrigger("Attack");
        lastAtackTime = Time.time;
    }

    private void OnAtackAnimEnd()
    {
        onAttack?.Invoke();
        _audioSource.PlayOneShot(soundDeath);
    }

    public void TakeDamage()
    {
        Die();
    }

    private void Die()
    {
        _audioSource.PlayOneShot(soundDeath);
        isDead = true;
        onDeath?.Invoke();
        _animator.SetTrigger("Hurt");
    }

    private void onDieAnimationEnd()
    {
        Player.onDeath -= OnPlayerDeath;
        Destroy(this.gameObject);
    }
}
