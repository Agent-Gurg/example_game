using Unity.IntegerTime;
using UnityEngine;

public class AIShooting : MonoBehaviour
{
    public Bullet2D Herobullet;
    public GameObject bullet;
    public Transform bulletPos;
    public int hp = 100;

    private float timer = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer > 2)
        {
            timer = 0;
            shoot();
        }

        if (hp <= 0)
        {
            Destroy(gameObject);
        }
    }

    void shoot()
    {
        Instantiate(bullet, bulletPos.position, Quaternion.identity);

       
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            hp = hp - Herobullet.damage;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            hp = hp - Herobullet.damage;
        }
    }
}
