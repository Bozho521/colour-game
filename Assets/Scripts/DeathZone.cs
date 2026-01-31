using System;
using Unity.VisualScripting;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    public GameObject player;
    private float resetTimer = 0f;
    private void OnTriggerEnter2D(Collider2D other)
    {
        ResetPlayer();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.R))
        {
            resetTimer += Time.deltaTime;
        }
        else
            resetTimer = 0f;

        if (resetTimer >= 1f)
            ResetPlayer();

        Debug.Log(resetTimer);
    }

    private void ResetPlayer()
    {
        if (player != null) 
            player.transform.position = new Vector3(1, -1, 0);
        resetTimer = 0f;
    }
}
