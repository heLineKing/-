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
        //Debug.Log("Player died");
        body.position = SpawnPoint.transform.position;
        body.velocity = Vector2.zero;
        animator.SetBool("Death", false);
    }
}
