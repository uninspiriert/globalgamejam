using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lava : MonoBehaviour
{
    public GameObject[] players;

    private void OnCollisionEnter2D(Collision2D other)
    {
        foreach (var player in players)
        {
            if (other.transform != player.transform) continue;
            var health = player.GetComponent<Health>();
            health.Kill();
        }
    }
}