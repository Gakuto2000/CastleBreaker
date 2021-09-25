using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public AudioSource playerAS;
    public AudioClip OpenShoji;
    public AudioClip CloseShoji;
    public pickup target;

    // public bool IsClose;

    public void DoorSE()
    {
        if (target.IsClose)
        {
            playerAS.clip = CloseShoji;
            Debug.Log("close");
        }
        else
        {
            playerAS.clip = OpenShoji;
            Debug.Log("open");
        }
        playerAS.Play();
    }
}
