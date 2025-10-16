using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]

public class Bullet2D : MonoBehaviour
{

    public int damage;

    void Start()
    {
        
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Enemy")
        {
            Destroy(gameObject);
        }

        if (coll.gameObject.tag == "Ground")
        {
            Destroy (gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Destroy(gameObject);
        }
    }
}