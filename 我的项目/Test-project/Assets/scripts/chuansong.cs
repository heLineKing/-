using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chuansong : MonoBehaviour
{
    public chuansong targetportal;
    public float freeze = 0.5f;
    private float ignoretime;
    private void OnTriggerEnter2D(Collider2D other)
    {if (Time.time < ignoretime) return;
        if (other.CompareTag("Player"))
        {Rigidbody2D playerrb= other.GetComponent<Rigidbody2D>();
            if (playerrb == null) return;
            Vector3 newpo= targetportal.transform.position;
            newpo.z = other.transform.position.z;
            other.transform.position = newpo;
            playerrb.velocity = Vector2.zero;
            targetportal.ignoretime = Time.time + freeze;
        }
        
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
