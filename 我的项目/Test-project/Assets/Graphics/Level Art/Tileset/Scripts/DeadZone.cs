using UnityEngine;
public class DeadZone : MonoBehaviour
{
    [SerializeField] private Transform respawnPoint;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<PlayerMove>() == null) return;

        Rigidbody2D body = other.GetComponent<Rigidbody2D>();

        body.velocity = Vector2.zero;
        other.transform.position = respawnPoint.position;
    }
}