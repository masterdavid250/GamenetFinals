using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class ShipHealth : MonoBehaviourPunCallbacks
{
    public float maxHealth = 100f;
    public float health;
    public Image healthBar;
    private float lastDamageTime = 0f;
    private float damageCooldown = 0.3f;


    private void Start()
    {
        health = maxHealth;
        healthBar.fillAmount = health / maxHealth;
    }

    [PunRPC]
    public void TakeDamage(float damage) 
    {
        if (Time.time > lastDamageTime + damageCooldown)
        {
            health -= damage;
            healthBar.fillAmount = health / maxHealth;
            if (health <= 0f)
            {
                //photonView.RPC("PlayerStunned", RpcTarget.AllBuffered);
                PlayerStunned();
            }
            lastDamageTime = Time.time;
        }
    }

    //[PunRPC]
    private void PlayerStunned()
    {
        this.GetComponent<ShipMovement>().isControlEnabled = false;
        this.GetComponent<ShipShooting>().isControlEnabled = false;
        Invoke(nameof(AfterStun), 3f); 
    }

    //[PunRPC]
    private void AfterStun()
    {
        this.GetComponent<ShipMovement>().isControlEnabled = true;
        this.GetComponent<ShipShooting>().isControlEnabled = true;
        health = maxHealth;
        healthBar.fillAmount = health / maxHealth;
    }
}
