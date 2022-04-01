using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class FoodInstantiate : MonoBehaviourPunCallbacks
{
    public GameObject _FoodPrefab;
    public float 多少秒生成一次 = 1;
    private float _Resec = 0;

    public Transform a1;
    public Transform a2;

    private float[] a1Pos = new float[3];
    private float[] a2Pos = new float[3];
    private float[] NewPos = new float[3];

    // Start is called before the first frame update
    void Start()
    {
        a1Pos[0] = a1.position.x;
        a1Pos[1] = a1.position.y;
        a1Pos[2] = a1.position.z;

        a2Pos[0] = a2.position.x;
        a2Pos[1] = a2.position.y;
        a2Pos[2] = a2.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameController.instance._GameControllerStarGame)
        {
            生成Food();
        }
    }

    public void 生成Food()
    {
        //Debug.Log(_Resec);
        if (_Resec <= 0)
        {
            _Resec = 多少秒生成一次;
            for (int i = 0; i < NewPos.Length; i++)
            {
                NewPos[i] = Random.Range(a1Pos[i], a2Pos[i]);
            }

            Vector3 Pos = new Vector3(NewPos[0], NewPos[1], NewPos[2]);

            PhotonNetwork.Instantiate(_FoodPrefab.name, Pos, Quaternion.identity);
        }

        _Resec -= Time.deltaTime;
    }
}







