using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collectionsstars : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.CompareTag("Player"))
        {
            PlayerDash dash = other.GetComponent<PlayerDash>();
            dash.RefreshDash();
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
