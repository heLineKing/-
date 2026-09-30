using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collectionsdiamonds : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerDash dash = other.GetComponent<PlayerDash>();
            dash.addmaxdashcount();
            gameObject.SetActive(false);
        }

    }
}
