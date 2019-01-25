using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int startHealth = 100;

    private Health _health;

    // Start is called before the first frame update
    private void Start()
    {
        _health = gameObject.AddComponent<Health>();
        _health.KillCallback = Die;
    }

    private void Die()
    {
        Destroy(gameObject);
    }

}