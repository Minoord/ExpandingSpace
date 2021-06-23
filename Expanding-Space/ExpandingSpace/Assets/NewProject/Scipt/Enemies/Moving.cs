using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moving : MonoBehaviour
{
    [SerializeField] private float MoneyReward; 
    [SerializeField] private WaveSpawner spawner;
    [SerializeField] private float MoveSpeed;
    [SerializeField] private ParticleSystem explosion;
    public Vector2 MinMaxMoveSpeed;
    private Rigidbody2D rb2d;
    // Start is called before the first frame update
    private void Start()
    {
        spawner = FindObjectOfType<WaveSpawner>();
        rb2d = gameObject.GetComponent<Rigidbody2D>();
        MoveSpeed = Random.Range(MinMaxMoveSpeed.x, MinMaxMoveSpeed.y);
    }

    // Update is called once per frame
    void Update()
    {
        rb2d.velocity = new Vector2(-MoveSpeed, 0f);
    }
    public void KillSelf()
    {
        spawner.EnemyKilled();
        FindObjectOfType<Wallet>().AddMoney(MoneyReward);
        FindObjectOfType<AudioManager>().Play("Coin");
        Instantiate(explosion, transform.position, Quaternion.identity);
        FindObjectOfType<AudioManager>().Play("Explosion");
        Destroy(gameObject);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Despawn"))
        {
            spawner.EnemyKilled();
            Destroy(gameObject);
        }
        if (collision.collider.CompareTag("Player"))
        {
            collision.collider.GetComponent<CollisionManager>().Health--;
            spawner.EnemyKilled();
            Destroy(gameObject);
        }
    }
}
