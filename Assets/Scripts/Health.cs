using System;
using UnityEngine;

public class Health: MonoBehaviour
{
    private int _currentHealth;

    public int Value => _currentHealth;

    public Action KillCallback { private get; set; }

    public void Kill()
    {
        _currentHealth = 0;
        KillCallback();
    }
}