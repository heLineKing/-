using System.Collections;
using System.Collections.Generic;
using UnityEngine;

struct MyStructTest1
{
    public int WDF;
}

public class ZLMsTryTest : MonoBehaviour
{
    SoundServer soundServer;
    public bool Active = true;
    // Start is called before the first frame update
    void Start()
    {
        GameObject soundServerGo = GameObject.Find("SoundServer");
        if (soundServerGo != null)
        {
            soundServer = soundServerGo.GetComponent<SoundServer>();
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Active)
        {
            if (soundServer != null)
            {
                //if (soundServer.GetIdleSoundCallCount() == 15)
                //{
                //    soundServer.ApplySoundCallOneShot(Vector3.up, "Sound/Title Screen");
                //}
                //else
                //{
                    if (soundServer.GetIdleSoundCallCount() > 15)
                    {
                        soundServer.ApplySoundCallOneShot(transform.position, "Sound/Warp Jingle");
                    }
                //}
            }
        }
    }

    public void Psss()
    {

    }
}
