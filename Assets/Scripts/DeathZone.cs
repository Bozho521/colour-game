using System;
using Unity.VisualScripting;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    public GameObject player;
    private void OnTriggerEnter2D(Collider2D other)
    {
        player.transform.position = new Vector3(1,-1,0);
    }
}
