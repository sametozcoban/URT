using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectCoint : MonoBehaviour
{
    [SerializeField] private int coin;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.AddGold(coin);
        }
    } // Kullanmadık ama coin eklemek için yöntem.
}
