using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    Rigidbody2D rb;
    Animator animator;
    public float speed = 1.0f;
    public float reverseDirectionTime = 5.0f;
    float timeMovingInThisDirection = 0.0f;
    int direction = 1;
    public bool moveHorizontal = false;
    bool broken = true;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!broken)
            return;
    }

    void FixedUpdate()
    {
        if(!broken)
            return;

        timeMovingInThisDirection += Time.fixedDeltaTime;

        if (timeMovingInThisDirection > reverseDirectionTime)
        {
            direction = -direction;
            timeMovingInThisDirection = 0.0f;
        }

        Vector2 position = rb.position;
        if (moveHorizontal)
        {
            position.x += direction * speed * Time.fixedDeltaTime;
            animator.SetFloat("Move X", direction);
            animator.SetFloat("Move Y", 0f);
        }
        else
        {
            position.y += direction * speed * Time.fixedDeltaTime;
            animator.SetFloat("Move X", 0f);
            animator.SetFloat("Move Y", direction);
        }
        rb.position = position;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        RubyController ruby = other.gameObject.GetComponent<RubyController>();

        if (!ruby)
            return;

        ruby.ChangeHealth(-1);
    }

    public void Fix()
    {
        broken = false;
        rb.simulated = false;
        animator.SetTrigger("Fixed");
    }
}
