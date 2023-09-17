using UnityEngine;

public class Enemy : MonoBehaviour
{
    public EnemyStates state;
    public SpriteRenderer parachuteSpriteRenderer;
    public float parachuteOpenDuration = 1.0f;
    public float speed = 2f;
    public bool canAttack = false;
    private Rigidbody2D rb;

    private bool isTouchingGround = false;
    private bool isDying = false;
    private bool touchingAnotherEnemy = false;
    private Enemy enemyInTouch = null;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        SetState(EnemyStates.Parachuting);
    }

    private void OnDisable()
    {
        if (transform.position.x < 0)
        {
            EnemyController.instance.enemyOnLeft.Remove(this);
        }
        else
        {
            EnemyController.instance.enemyOnRight.Remove(this);
        }
    }

    public void SetState(EnemyStates newState)
    {
        if (state == newState)
            return;
        ExitState();
        state = newState;
        EnterState();
        Debug.Log($"Enemy state: {state}");
    }

    public void Shot()
    {
        if(state == EnemyStates.Parachuting)
        {
            SetState(EnemyStates.Falling);
        }
        else
        {
            SetState(EnemyStates.Dying);
        }

    }

    private void EnterState()
    {
        switch (state)
        {
            case EnemyStates.Parachuting:
                ParachutingEnter();
                break;
            case EnemyStates.Falling:
                FallingEnter();
                break;
            case EnemyStates.Idle:
                IdleEnter();
                break;
            case EnemyStates.Attacking:
                AttackingEnter();
                break;
            case EnemyStates.TouchingBase:
                TouchingBaseEnter();
                break;
            case EnemyStates.Dying:
                DyingEnter();
                break;
        }
    }

    private void ExitState()
    {
        switch (state)
        {
            case EnemyStates.Parachuting:
                ParachutingExit();
                break;
            case EnemyStates.Falling:
                FallingExit();
                break;
            case EnemyStates.Idle:
                IdleExit();
                break;
            case EnemyStates.Attacking:
                AttackingExit();
                break;
            case EnemyStates.TouchingBase:
                TouchingBaseExit();
                break;
            case EnemyStates.Dying:
                DyingExit();
                break;
        }
    }

    private void ParachutingEnter()
    {
        parachuteSpriteRenderer.enabled = true;
        rb.gravityScale = 0.1f;
    }

    private void ParachutingExit()
    {
        parachuteSpriteRenderer.enabled = false;
        rb.gravityScale = 1f;
    }

    private void FallingEnter()
    {
        parachuteSpriteRenderer.enabled = false;
    }

    private void FallingExit()
    {
        if (isTouchingGround || touchingAnotherEnemy)
        {
            if (!isDying)
            {
                SetState(EnemyStates.Dying);
            }
        }
    }

    private void IdleEnter()
    {
        if (transform.position.x < 0)
        {
            EnemyController.instance.AddEnemyOnLeft(this);
        }
        else
        {
            EnemyController.instance.AddEnemyOnRight(this);
        }
    }

    private void IdleExit()
    {
    }

    private void AttackingEnter()
    {
        canAttack = true;
    }

    private void AttackingExit()
    {
    }

    private void TouchingBaseEnter()
    {
        canAttack = false;
    }

    private void TouchingBaseExit()
    {
    }

    private void DyingEnter()
    {
        isDying = true;
        GameManager.instance.UpdateScore(5);
        gameObject.SetActive(false);
    }

    private void DyingExit()
    {
        isDying=false;
    }

    private void Update()
    {
        if (canAttack)
        {
            //start moving towards 0 in x axis
            if (transform.position.x > 0)
            {
                transform.Translate(Vector3.left * speed * Time.deltaTime);
            }
            else
            {
                transform.Translate(Vector3.right * speed * Time.deltaTime);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.CompareTag("Player"))
        {
            SetState(EnemyStates.TouchingBase);
        }
        else if (collision.gameObject.CompareTag("Enemy"))
        {
            touchingAnotherEnemy = true;
            if (state == EnemyStates.Falling)
            {
                SetState(EnemyStates.Dying);
                collision.gameObject.GetComponent<Enemy>().SetState(EnemyStates.Dying);
                return;
            }
            else if(state == EnemyStates.Attacking)
            {
                if(collision.gameObject.GetComponent<Enemy>().state == EnemyStates.TouchingBase)
                {
                    SetState(EnemyStates.TouchingBase);
                    return;
                }
            }
        }
        else if (collision.gameObject.CompareTag("Ground"))
        {
            if (state == EnemyStates.Falling)
            {
                SetState(EnemyStates.Dying);
                return;
            }
            isTouchingGround = true;
            SetState(EnemyStates.Idle);
        }

    }
}
