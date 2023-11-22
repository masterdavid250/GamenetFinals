using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Pun.UtilityScripts;

public class BulletMechanics : MonoBehaviourPunCallbacks
{
    private float bulletDamage;
    private string bulletOwner; 

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);

        if (other.gameObject.CompareTag("Player"))
        {
            if (other.gameObject.GetComponent<PhotonView>().IsMine)
            {
                other.gameObject.GetComponent<PhotonView>().RPC("TakeDamage", RpcTarget.AllBuffered, bulletDamage); 
            }
            else if (!other.gameObject.GetComponent<PhotonView>().IsMine)
            {
                other.gameObject.GetComponent<PhotonView>().RPC("TakeDamage", RpcTarget.AllBuffered, bulletDamage); 
            }
        }
    }

    public void Initialize(Vector3 direction, float speed, float damage) 
    {
        bulletDamage = damage; 
        transform.forward = direction; 
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.velocity = direction * speed;
    } 
}
