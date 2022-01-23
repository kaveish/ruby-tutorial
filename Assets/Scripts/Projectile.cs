using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    Rigidbody2D rb;
    float launchTime;
    public float maxFlightTime = 1.0f;

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        launchTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - launchTime > maxFlightTime)
            Destroy(gameObject);
    }

    public void Launch(Vector2 direction, float force)
    {
        rb.AddForce(direction * force);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        EnemyController enemy = other.collider.GetComponent<EnemyController>();

        if (enemy)
            enemy.Fix();

        Destroy(gameObject);
    }
}
