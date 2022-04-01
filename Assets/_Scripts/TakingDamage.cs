using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class TakingDamage : MonoBehaviourPunCallbacks
{
    public float health;
    public float maxHealth = 100f;
    [SerializeField]
    Image healthBar;

    void Start()
    {
        health = maxHealth;
        healthBar.fillAmount = health / maxHealth;
    }

    [PunRPC]
    public void TakeDamage(float damage)
    {
        health -= damage;
        healthBar.fillAmount = health / maxHealth;
        if (health <= 0f)
        {
            Die();
        }
    }

    void Die()
    {
        if (photonView.IsMine)
        {
            GameController.instance.LeaveRoom();
        }
    }



}
