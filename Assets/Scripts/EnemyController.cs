using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    Rigidbody2D rb;
    public float speed = 1.0f;
    public float reverseDirectionTime = 5.0f;
    float timeMovingInThisDirection = 0.0f;
    int direction = 1;
    public bool moveHorizontal = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        timeMovingInThisDirection += Time.fixedDeltaTime;

        if (timeMovingInThisDirection > reverseDirectionTime)
        {
            direction = -direction;
            timeMovingInThisDirection = 0.0f;
        }

        Vector2 position = rb.position;
        if (moveHorizontal)
            position.x += direction * speed * Time.fixedDeltaTime;
        else
            position.y += direction * speed * Time.fixedDeltaTime;
        rb.position = position;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        RubyController ruby = other.gameObject.GetComponent<RubyController>();

        if (!ruby)
            return;

        ruby.ChangeHealth(-1);
    }
}
