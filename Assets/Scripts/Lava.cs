using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Lava : MonoBehaviour
{
    public GameObject[] players;

    private static int boyWins;

    private static int girlWins;
    
    public GameObject Menu;

    public GameObject playAgain;

    public GameObject quit;

    public GameObject girlText;

    public GameObject boyText;

    private int playerNumber;

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
            if (other.transform != player.transform)
            {
                player.GetComponent<MoveScript>().punched = true;
                continue;
            }
            var health = player.GetComponent<Health>();
            health.Kill();
            removed = player;
            break;
        }

        if (removed != null)
        {
            playerNumber = removed.GetComponent<MoveScript>().playerNumber;
            _players.Remove(removed);
        }

        StartCoroutine(EndOfLevel());
    }

    private IEnumerator EndOfLevel()
    {
        if (boyWins < 2 || girlWins < 2)
        {   
            Menu.SetActive(true);
            
            if (playerNumber == 1)
            {
                girlText.SetActive(true);
                girlWins++;
            }
            else
            {
                boyText.SetActive(true);
                boyWins++;
                Debug.Log(boyWins);
            }
            
            yield return new WaitForSeconds(1.5f);
            
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else
        {
            Menu.SetActive(true);
            
            if (boyWins > 1)
            {
                girlText.SetActive(true);
            }
            else {
                boyText.SetActive(true);
            }
            
            playAgain.SetActive(true);
            quit.SetActive(true);
            
            boyWins = 0;
            girlWins = 0;
        }
    }
}