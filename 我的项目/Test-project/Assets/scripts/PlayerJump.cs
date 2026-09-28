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
    private SoundServer soundServer;
    private PlayerDash dash;
    private Playerdeath death;
    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        jumpAudio = GetComponent<AudioSource>();
        dash=GetComponent<PlayerDash>();
        death = GetComponent<Playerdeath>();
        GameObject soundServerGo = GameObject.Find("SoundServer");
        if (soundServerGo != null)
        {
            soundServer = soundServerGo.GetComponent<SoundServer>();
        }
    }
    private void Update()
    {
        if (dash.IsDashing || death.IsDead)
        {
            jumpRequested = false;
            return;
        }
        bool onGrounded = IsGrounded();
        //Debug.Log("Jump Requested");
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space))
        {
            jumpRequested = true;
        }
        else
        {
            jumpRequested = false;
        }
        if (jumpRequested&&onGrounded)
        {
            body.velocity = new Vector2(body.velocity.x, jumpForce);
            soundServer.ApplySoundCallOneShot(transform.position, "Sounds/Player Jump");
            jumpRequested = false;
        }
        animator.SetFloat("Jump", body.velocity.y);//跳跃动画
    }
    public bool IsGrounded()
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
