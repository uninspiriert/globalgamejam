using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Lava : MonoBehaviour
{
    public GameObject[] players;
    
    
    public GameObject ingameMenu;

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

        if (SceneManager.GetActiveScene().buildIndex != 3)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else
        {
            ingameMenu.SetActive(true);
        }
    }
}