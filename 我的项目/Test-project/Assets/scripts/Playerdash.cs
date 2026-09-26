using UnityEngine;
public class PlayerDash : MonoBehaviour
{
    [SerializeField] private float dashSpeed;
    [SerializeField] private float dashDuration;
    [SerializeField] private float dashCooldown;
    [SerializeField] private int maxDashCount;
    private float dashTimer;
    private float cooldownTimer;
    private Rigidbody2D body;
    private SpriteRenderer spriteRenderer;
    private float defaultGravityScale;
    private int dashCount;
    private PlayerJump jump;
    public bool IsDashing => dashTimer > 0f;
    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        defaultGravityScale = body.gravityScale;
        jump = GetComponent<PlayerJump>();
    }
    private void Update()
    {
        if(jump.IsGrounded())
        {
            dashCount = 0;
        }
        // 冷却倒计时
        if (cooldownTimer > 0f)
        {
            cooldownTimer -= Time.deltaTime;
        }
        else
        {
            cooldownTimer = 0f;
        }
        if (Input.GetKeyDown(KeyCode.L) && cooldownTimer <= 0f && !IsDashing && maxDashCount>dashCount)
        {
            dashTimer = dashDuration;
            cooldownTimer = dashCooldown + dashDuration;
            dashCount++;
        }
    }

    private void FixedUpdate()
    {
        // 冲刺倒计时
        if (dashTimer > 0f)
        {
            dashTimer -= Time.fixedDeltaTime;
        }
        if (dashTimer <= 0f)
        {
            body.gravityScale = defaultGravityScale;
        }
        if (IsDashing)
        {
            body.gravityScale = 0f;
            body.velocity = new Vector2(spriteRenderer.flipX ? -dashSpeed : dashSpeed, 0f);
        }
    }
}