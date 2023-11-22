using Photon.Pun;
using Photon.Pun.Demo.Asteroids;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipShooting : MonoBehaviourPunCallbacks
{
    public GameObject bulletPrefab;
    public Transform[] muzzlePositions;
    public Camera playerCamera;
    public ShipRacingLineup racerProperties;
    private float fireRate;
    private float fireDelay = 0f;
    public bool isControlEnabled = true; 

    void Start()
    {
        fireRate = racerProperties.weaponFireRate;
    }

    // Update is called once per frame
    void Update()
    {
        if (isControlEnabled)
        {
            if (!photonView.IsMine)
            {
                return;
            }

            if (Input.GetMouseButtonDown(0))
            {
                if (fireDelay > fireRate)
                {
                    Debug.Log("CHECKPOINT 1");
                    photonView.RPC("ShootFromAllCannons", RpcTarget.AllBuffered); //, muzzlePositions.position, muzzlePositions.forward);
                    fireDelay = 0f;
                }
            }

            if (fireDelay < fireRate)
            {
                fireDelay += Time.deltaTime;
            }
        }
    }

    [PunRPC]
    public void ShootFromAllCannons()
    {
        foreach (Transform muzzleTransform in muzzlePositions)
        {
            Vector3 position = muzzleTransform.position;
            Vector3 direction = muzzleTransform.forward;
            Shoot(position, direction);
        }
    }

    [PunRPC]
    public void Shoot(Vector3 shootingPosition, Vector3 shootingForward)
    {
        //Ray ray = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f));

        GameObject bulletGameObject = Instantiate(bulletPrefab, shootingPosition, Quaternion.identity);
        BulletMechanics bulletMechanics = bulletGameObject.GetComponent<BulletMechanics>();
        if (bulletMechanics != null)
        {
            bulletMechanics.Initialize(shootingForward, racerProperties.weaponBulletSpeed, racerProperties.weaponDamage);
        }
        //bulletGameObject.GetComponent<BulletMechanics>().Initialize(ray.direction, racerProperties.weaponBulletSpeed, racerProperties.weaponDamage, playerWhoHit);
    }

}
