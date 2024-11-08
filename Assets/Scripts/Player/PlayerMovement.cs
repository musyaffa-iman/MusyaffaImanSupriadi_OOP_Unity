using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] Vector2 maxSpeed;
    [SerializeField] Vector2 timeToFullSpeed;
    [SerializeField] Vector2 timeToStop;
    [SerializeField] Vector2 stopClamp;

    Vector2 moveDirection;
    Vector2 moveVelocity;
    Vector2 moveFriction;
    Vector2 stopFriction;
    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        moveVelocity = 2 * maxSpeed / timeToFullSpeed;
        moveFriction = -2 * maxSpeed/(timeToFullSpeed * timeToFullSpeed);
        stopFriction = -2 * maxSpeed/(timeToStop * timeToStop);
    }

    public void Move()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        Vector2 currentVelocity = rb.velocity;

        moveDirection = new Vector2(moveX, moveY).normalized;
        
        if (moveDirection != Vector2.zero)
        {
            currentVelocity.x += moveDirection.x * moveVelocity.x * Time.deltaTime;
            currentVelocity.y += moveDirection.y * moveVelocity.y * Time.deltaTime;
        }
        else
        {
            if (Mathf.Abs(currentVelocity.x) < stopClamp.x)
            {
                currentVelocity.x = 0;
            }
            else
            {   
                if (currentVelocity.x > 0)
                    currentVelocity.x += GetFriction().x * Time.deltaTime;
                else if (currentVelocity.x < 0)
                    currentVelocity.x -= GetFriction().x * Time.deltaTime;
            }

            if (Mathf.Abs(currentVelocity.y) < stopClamp.y)
            {
                currentVelocity.y = 0;
            }
            else
            {
                if (currentVelocity.y > 0)
                    currentVelocity.y += GetFriction().y * Time.deltaTime;
                else if (currentVelocity.y < 0)
                    currentVelocity.y -= GetFriction().y * Time.deltaTime;
            }
        }

        currentVelocity.x = Mathf.Clamp(currentVelocity.x, -maxSpeed.x, maxSpeed.x);
        currentVelocity.y = Mathf.Clamp(currentVelocity.y, -maxSpeed.y, maxSpeed.y);

        if (currentVelocity.magnitude > maxSpeed.magnitude)
        {
            currentVelocity = currentVelocity.normalized * maxSpeed.magnitude;
        }


        Vector2 min = (Vector2)Camera.main.ViewportToWorldPoint(Vector2.zero) + new Vector2(0.225f, 0.1005f);
        Vector2 max = (Vector2)Camera.main.ViewportToWorldPoint(Vector2.one) - new Vector2(0.225f, 0.5f);

        float boundX = Mathf.Clamp(transform.position.x, min.x, max.x);
        float boundY = Mathf.Clamp(transform.position.y, min.y, max.y);
        transform.position = new Vector2(boundX, boundY);

        rb.velocity = currentVelocity;
    }

    public Vector2 GetFriction()
    {
        return rb.velocity != Vector2.zero ? moveFriction : stopFriction;
    }

    public void MoveBound()
    {
        
    }

    public bool IsMoving()
    {
        return rb.velocity.magnitude > 0;
    }
}
