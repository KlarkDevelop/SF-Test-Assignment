using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int health;
    public float attackDistance;
    public float attackDelay;
    private float lastAttackTime;

    public static event Action<int> onHealthValueChange;
    public static event Action onDeath;
    private SpriteRenderer _spriteRenderer;
    private Animator _animator;
    private AudioSource _audioSource;

    [SerializeField] private AudioClip soundAttack;
    [SerializeField] private AudioClip soundDeath;
    private bool isDead = false;
    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();

        Enemie.onAttack += onEnemyAttack;
    }

    private void Start()
    {
        onHealthValueChange?.Invoke(health);
    }

    private void Update()
    {
        if (!isDead)
        {
            if (Time.time - lastAttackTime > attackDelay)
            {
                Attack();
            }
        }
    }


    private void Attack()
    {
        float diraction = Input.GetAxisRaw("Horizontal");

        if (diraction == 0)
        {
            return;
        }
        if (diraction > 0)
        {
            _spriteRenderer.flipX = false;
        }
        else
        {
            _spriteRenderer.flipX = true;
        }
        lastAttackTime = Time.time;
        _animator.SetTrigger("Attack1");
    }

    private void OnAttackAnimEnd()
    {
        RaycastHit2D hit;
        if (!_spriteRenderer.flipX)
        {
            hit = Physics2D.Raycast(Vector2.zero, Vector2.right * attackDistance, attackDistance);
        }
        else
        {
            hit = Physics2D.Raycast(Vector2.zero, Vector2.left * attackDistance, attackDistance);
        }

        if (hit.collider != null && hit.collider.TryGetComponent<Enemie>(out Enemie _enemie)) _enemie.TakeDamage();
        _audioSource.PlayOneShot(soundAttack);
    }

    private void onEnemyAttack()
    {
        health--;
        onHealthValueChange?.Invoke(health);
        _animator.SetTrigger("Hurt");
        if (health <= 0)
        {
            onDeath?.Invoke();
            Enemie.onAttack -= onEnemyAttack;
            Die();
        }
    }

    private void Die()
    {
        _audioSource.PlayOneShot(soundDeath);
        _animator.SetTrigger("Death");
    }
}
