using UnityEngine;
public class PlayerDash : MonoBehaviour
{
    [Header("冲刺属性")]
    [SerializeField] private float dashSpeed;
    [SerializeField] private float dashDuration;
    [SerializeField] private float dashCooldown;
    [SerializeField] private int maxDashCount;
    [Header("残影特效")]
    [SerializeField] private bool enableDashGhost = true;
    [SerializeField] private float ghostInterval = 0.03f;
    [SerializeField] private float ghostLifetime = 0.25f;
    [SerializeField] private float ghostDepthOffset = 0.1f;
    [SerializeField] private Color ghostColor = new Color(0.55f, 0.85f, 1f, 0.6f);

    private float ghostTimer;
    private float dashTimer;
    private float cooldownTimer;
    private Rigidbody2D body;
    private SpriteRenderer spriteRenderer;
    private float defaultGravityScale;
    private int dashCount;
    private Vector2 direction;
    private PlayerJump jump;
    private Playerdeath death;
    public bool IsDashing => dashTimer > 0f;
    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        defaultGravityScale = body.gravityScale;
        jump = GetComponent<PlayerJump>();
        death = GetComponent<Playerdeath>();
    }
    private void Update()
    {
        if (death.IsDead)
        {
            dashTimer = 0f;
            return;
        }
        if (jump.IsGrounded())
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
        if (IsDashing) return;
        direction.x = getx() * dashSpeed;
        direction.y = gety() * dashSpeed;
        if (Input.GetKeyDown(KeyCode.L) && cooldownTimer <= 0f && !IsDashing && maxDashCount > dashCount)
        {
            dashTimer = dashDuration;
            cooldownTimer = dashCooldown + dashDuration;
            dashCount++;
            ghostTimer = 0f;
        }
    }
    private int getx()
    {
        if (Input.GetKey(KeyCode.A))
        {
            return -1;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            return 1;
        }
        else
        {
            return 0;
        }
    }
    private int gety()
    {
        if (Input.GetKey(KeyCode.W))
        {
            return 1;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            return -1;
        }
        else
        {
            return 0;
        }
    }
    private void FixedUpdate()
    {
        if (death.IsDead)
        {
            return;
        }
        // 冲刺倒计时
        if (dashTimer > 0f)
        {
            dashTimer -= Time.fixedDeltaTime;
        }
        if (dashTimer < 0f)
        {
            body.gravityScale = defaultGravityScale;
            direction.x /= 3;
            direction.y /= 3;
            body.velocity = direction;
            dashTimer = 0f;
        }
        if (IsDashing)
        {
            body.gravityScale = 0f;
            body.velocity = direction;

            if (enableDashGhost)
            {
                ghostTimer -= Time.fixedDeltaTime;
                if (ghostTimer <= 0f)
                {
                    ghostTimer = ghostInterval;
                    SpawnGhost();
                }
            }
        }
    }

    private void SpawnGhost()
    {
        if (spriteRenderer.sprite == null)
        {
            return;
        }

        GameObject ghostGo = new GameObject("DashGhost");
        ghostGo.transform.SetPositionAndRotation(new Vector3(transform.position.x, transform.position.y, transform.position.z + ghostDepthOffset), transform.rotation);
        ghostGo.transform.localScale = transform.lossyScale;

        ghostGo.AddComponent<SpriteRenderer>();
        DashGhost ghost = ghostGo.AddComponent<DashGhost>();

        ghost.Init(
            spriteRenderer.sprite,
            spriteRenderer.flipX,
            spriteRenderer.sortingLayerID,
            spriteRenderer.sortingOrder,
            ghostColor,
            ghostLifetime);
    }
}