using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camerafollow : MonoBehaviour
{
    private Playerdeath death;
    private GameObject Player;
    private void Awake()
    {
        Player = GameObject.Find("Player");
        death = Player.GetComponent<Playerdeath>();
    }
    private void FixedUpdate()
    {
        if(!death.IsDead)
        {
            Vector3 a= Player.transform.position;
            a.z--;
            transform.position = a;
        }
    }
}
