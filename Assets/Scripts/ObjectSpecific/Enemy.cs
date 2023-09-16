using UnityEngine;

public class Enemy : MonoBehaviour
{
    public EnemyStates state;
    public float speed = 0.5f;
    public bool canAttack = false;
    public bool onRight = false;
    private Rigidbody2D rb;
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        SetState(EnemyStates.parachute);
    }
    private void OnDisable()
    {
        if (onRight)
        {
            EnemyController.instance.enemyOnRight.Remove(this);
        }
        else
        {
            EnemyController.instance.enemyOnLeft.Remove(this);
        }
        onRight = false;
    }

    public void SetState(EnemyStates states)
    {
        state = states;

        switch (state)
        {
            case EnemyStates.parachute:
                Parachute();
                break;
            case EnemyStates.falling:
                Fall();
                break;
            case EnemyStates.idle:
                Idle();
                break;
            case EnemyStates.attack:
                Attack();
                break;
            case EnemyStates.touchBase:
                TouchBase();
                break;
            case EnemyStates.died:
                Die();
                break;
            default:
                throw new System.ArgumentOutOfRangeException(nameof(states), states, null);
        }

    }

    public void Die()
    {
        if(state == EnemyStates.parachute)
        {
            SetState(EnemyStates.falling);
        }
        else
        {
            GameManager.instance.score += 5;
            gameObject.SetActive(false);
        }
    }

    public void Attack()
    {
        canAttack = true;
    }

    public void TouchBase()
    {
        canAttack = false;
    }

    public void Fall()
    {
        rb.mass = 1;
        rb.gravityScale = 1f;
    }

    public void Idle()
    {
        rb.gravityScale = 1f;
    }

    public void Parachute()
    {
        rb.gravityScale = 0.1f;
    }

    private void Update()
    {
        if (GameManager.instance.state == GameState.InGame)
        {
            if (canAttack)
            {
                if (transform.position.x < 0)
                {
                    transform.Translate(speed * Time.deltaTime * Vector3.right);
                }
                else if (transform.position.x > 0)
                {
                    transform.Translate(speed * Time.deltaTime * Vector3.left);
                }
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            if(state == EnemyStates.falling)
            {
                Die();
                return;
            }
            SetState(EnemyStates.idle);
            if(collision.gameObject.name == "Right")
            {
                onRight = true;
                EnemyController.instance.enemyOnRight.Add(this);
            }
            else
            {
                onRight = false;
                EnemyController.instance.enemyOnLeft.Add(this);
            }
        }
        if(collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player Touch");
            SetState(EnemyStates.touchBase);
        }
        if(collision.gameObject.CompareTag("Enemy"))
        {
            if(collision.gameObject.GetComponent<Enemy>().state == EnemyStates.touchBase)
            {
                SetState(EnemyStates.touchBase);
            }
            else if(collision.gameObject.GetComponent<Enemy>().state == EnemyStates.idle)
            {
                if (collision.gameObject.GetComponent<Enemy>().onRight)
                {
                    onRight = true;
                }
                else
                {
                    onRight = false;
                }
                SetState(EnemyStates.idle);
            }
        }
    }
}

public enum EnemyStates
{
    parachute,
    falling,
    idle,
    attack,
    touchBase,
    died
}
