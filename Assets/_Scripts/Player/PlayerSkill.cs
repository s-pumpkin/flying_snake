using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class PlayerSkill : MonoBehaviourPunCallbacks
{
    private SnakeHead SnakeHead;

    public KeyCode 衝刺keybutton;
    public bool 衝刺 = true;
    [Tooltip("乘以多少倍速度")]
    public float 衝刺Value = 1.2f;
    [HideInInspector]
    public float 更動衝刺Value = 1;
    public float 衝刺持續時間 = 5f;

    public float 衝刺CD = 60f;
    private float 計算衝刺CD;

    public GameObject 衝刺effect;
    public Image 衝刺Image;
    //--------------------------------------------------------
    [Space(10)]
    public KeyCode 無敵keybutton;
    public bool 無敵 = true;
    [HideInInspector]
    public bool is無敵 = false;
    public float 無敵持續時間 = 5f;

    public float 無敵CD = 60f;
    private float 計算無敵CD;

    public GameObject 無敵effect;
    public Image 無敵Image;
    //----------------------------------------------------------
    [Space(10)]
    public KeyCode 煙霧keybutton;
    public bool 煙霧 = true;

    public float 煙霧CD = 60f;
    private float 計算煙霧CD;

    public GameObject 煙霧effect;
    public float 煙霧特效持續時間 = 10f;
    public Image 煙霧Image;
    // Start is called before the first frame update
    void Start()
    {
        SnakeHead = gameObject.GetComponent<SnakeHead>();
    }

    // Update is called once per frame
    void Update()
    {
        countCDtime();
        if (Input.GetKeyDown(衝刺keybutton) && 衝刺)
        {
            衝刺skill();
            return;
        }

        if (Input.GetKeyDown(無敵keybutton) && 無敵)
        {
            無敵skill();
            return;
        }

        if (Input.GetKeyDown(煙霧keybutton) && 煙霧)
        {
            煙霧skill();
            return;
        }
    }
    //--------------------------------------------------------------
    #region 衝刺
    public void 衝刺skill()
    {
        衝刺 = false;
        計算衝刺CD = 衝刺CD;

        更動衝刺Value = 衝刺Value;
        if (衝刺effect != null) 衝刺effect.SetActive(true);
        Invoke("恢復更動衝刺Value", 衝刺持續時間);
    }

    public void 恢復更動衝刺Value()
    {
        更動衝刺Value = 1;
        if (衝刺effect != null) 衝刺effect.SetActive(false);
    }
    #endregion
    //--------------------------------------------------------------
    #region 無敵
    public void 無敵skill()
    {
        無敵 = false;
        計算無敵CD = 無敵CD;

        is無敵 = true;
        if (無敵effect != null) 無敵effect.SetActive(true);
        Invoke("恢復is無敵", 無敵持續時間);
    }
    public void 恢復is無敵()
    {
        is無敵 = false;
        if (無敵effect != null) 無敵effect.SetActive(false);
    }
    #endregion
    //--------------------------------------------------------------
    #region 煙霧
    public void 煙霧skill()
    {
        煙霧 = false;
        計算煙霧CD = 煙霧CD;

        GameObject obj = PhotonNetwork.Instantiate(煙霧effect.name, transform.position, Quaternion.identity);
        Debug.Log(transform.name + "技能施放煙霧" + obj.name);
        Destroy(obj, 煙霧特效持續時間);
    }
    #endregion
    //-----------------------------------------------------------------
    public void countCDtime()
    {
        計算衝刺CD -= Time.deltaTime;
        計算衝刺CD = Mathf.Clamp(計算衝刺CD, 0, 衝刺CD);
        衝刺Image.fillAmount = 計算衝刺CD / 衝刺CD;

        計算無敵CD -= Time.deltaTime;
        計算無敵CD = Mathf.Clamp(計算無敵CD, 0, 無敵CD);
        無敵Image.fillAmount = 計算無敵CD / 無敵CD;

        計算煙霧CD -= Time.deltaTime;
        計算煙霧CD = Mathf.Clamp(計算煙霧CD, 0, 煙霧CD);
        煙霧Image.fillAmount = 計算煙霧CD / 煙霧CD;

        if (計算衝刺CD <= 0 && !衝刺) 衝刺 = true;
        if (計算無敵CD <= 0 && !無敵) 無敵 = true;
        if (計算煙霧CD <= 0 && !煙霧) 煙霧 = true;
    }

}
