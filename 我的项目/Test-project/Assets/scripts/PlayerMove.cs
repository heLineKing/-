using System;
using UnityEngine;

enum LastOperate
{
    LEFT = -1,
    NUM = 0,
    RIGHT = 1
}

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    private Rigidbody2D body;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private PlayerDash dash;
    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        dash=GetComponent<PlayerDash>();
    }

    private void FixedUpdate()
    {
        if(dash.IsDashing)
        {
            return;
        }

        // -1 向左、0 不动、+1 向右。
        float direction = 0f;
        if (Input.GetKeyDown(KeyCode.A))
        {
            direction = -1f;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            direction = 1f;
        }

        body.velocity = new Vector2(direction * moveSpeed, body.velocity.y);
        //转向
        if (direction < 0f)
        {
            spriteRenderer.flipX = true;
        }
        else if (direction > 0f)
        {
            spriteRenderer.flipX = false;
        }

        animator.SetFloat("speed", Mathf.Abs(body.velocity.x));//是否播放行走动画
    }
}
