using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 5f;
    public Vector2 direction;
    public float lifeTime = 5f;
    public bool shot;


    private void OnEnable()
    {
        Invoke(nameof(Disable), lifeTime);
    }

    public void Shoot(Vector2 dir)
    {
        direction = dir;
        shot = true;
    }

    private void Update()
    {
        if (shot)
        {
            transform.Translate(direction * speed * Time.deltaTime);
        }
        
    }

    void Disable()
    {
        shot = false;
        gameObject.SetActive(false);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            GameManager.instance.UpdateScore(5);
            collision.gameObject.GetComponent<Enemy>().Die();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Helicopter"))
        {
            GameManager.instance.UpdateScore(10);
            collision.gameObject.SetActive(false);
        }
    }
}
