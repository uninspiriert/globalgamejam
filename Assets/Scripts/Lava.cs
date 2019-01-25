using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lava: MonoBehaviour
{
    public GameObject player;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform != player.transform) return;
        
        var health = player.GetComponent<Health>();
        health.Kill();
    }
}