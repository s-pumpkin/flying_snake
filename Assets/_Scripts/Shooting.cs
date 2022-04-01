using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Shooting : MonoBehaviour
{
    [SerializeField]
    Camera fpsCamera;

    public float fireRate = 0.1f;
    float fireTimer;

    void Update()
    {
        if (fireTimer < fireRate)
        {
            fireTimer += Time.deltaTime;
        }

        if (Input.GetButton("Fire1") && fireTimer > fireRate)
        {
            fireTimer = 0f;
            RaycastHit hit;

            Ray ray = fpsCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
            if (Physics.Raycast(ray, out hit, 100))
            {
                Debug.Log(hit.collider.gameObject.name);

                PhotonView pv = hit.collider.gameObject.GetComponent<PhotonView>();
                if (hit.collider.gameObject.CompareTag("Player") && !pv.IsMine)
                {
                    pv.RPC("TakeDamage", RpcTarget.All, 10f); //呼叫RPC的方法TakeDamage，每中一發扣10點血
                }

            }
        }
    }
}
