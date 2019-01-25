using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Lava : MonoBehaviour
{
    public GameObject[] players;

    private IList<GameObject> _players;

    private void Start()
    {
        _players = players.ToList();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        GameObject removed = null;
        
        foreach (var player in _players)
        {
            if (other.transform != player.transform) continue;
            var health = player.GetComponent<Health>();
            _players.Remove(player);
            health.Kill();
            removed = player;
            break;
        }
        
        if (removed != null)
        {
            _players.Remove(removed);
        }
    }
}