using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playerdeath : MonoBehaviour
{ 
    [SerializeField] private LayerMask spike;
    private Rigidbody2D body;
    private GameObject SpawnPoint;
    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        SpawnPoint = GameObject.Find("SpawnPoint");
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
        Debug.Log("Player died");
        body.position = SpawnPoint.transform.position;
        body.velocity = Vector2.zero;
    }
}
