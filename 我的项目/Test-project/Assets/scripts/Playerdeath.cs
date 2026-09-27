using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playerdeath : MonoBehaviour
{ 
    [SerializeField] private LayerMask spike;
    private Rigidbody2D body;
    private GameObject SpawnPoint;
    private SoundServer SoundServe;
    private Animator animator;
    public bool IsDead { get; private set; }
    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        SpawnPoint = GameObject.Find("SpawnPoint");
        SoundServe = GameObject.Find("SoundServer").GetComponent<SoundServer>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if((spike & (1 << other.gameObject.layer)) != 0)
        {
            Die();
        }
    }
    private void Die()
    {
        SoundServe.ApplySoundCallOneShot(transform.position, "Sounds/Player Death");
        animator.SetBool("Death", true);
        body.velocity = Vector2.zero;
        animator.SetFloat("Jump", 0f);
        animator.SetFloat("speed", 0f);

        body.position = SpawnPoint.transform.position;
        animator.SetBool("Death", false);
    }
}
