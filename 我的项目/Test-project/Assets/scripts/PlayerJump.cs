using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    [SerializeField] private float jumpForce;

    [SerializeField] private Vector2 groundCheckOffset;

    [SerializeField] private Vector2 groundCheckSize = new Vector2(1f, 0.25f);

    [SerializeField] private LayerMask groundLayer;

    private Rigidbody2D body;

    private bool jumpRequested;
    
    private Animator animator;

    private AudioSource jumpAudio;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        jumpAudio = GetComponent<AudioSource>();
    }

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.W)||Input.GetKey(KeyCode.Space))
        {
            jumpRequested = true;
        }
        else
        {
            jumpRequested = false;
        }

        bool isGrounded = CheckGrounded();

        if (jumpRequested && isGrounded)
        {
            body.velocity = new Vector2(body.velocity.x, jumpForce);

            jumpAudio.Play();
        }
        animator.SetFloat("Jump", body.velocity.y);//跳跃动画
    }

    private bool CheckGrounded()
    {
        Vector2 point = (Vector2)transform.position + groundCheckOffset;

        return Physics2D.OverlapBox(point, groundCheckSize, 0f, groundLayer) != null;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube((Vector2)transform.position + groundCheckOffset, groundCheckSize);
    }
}
