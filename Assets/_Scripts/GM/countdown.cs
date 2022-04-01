using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;


public class countdown : MonoBehaviourPunCallbacks
{
    bool startTimer = false;
    double timerIncrementValue;
    double startTime;
    [SerializeField] double timer = 20;
    public Text text;
    ExitGames.Client.Photon.Hashtable CustomeValue;
    // Start is called before the first frame update
    void Start()
    {
        if (PhotonNetwork.LocalPlayer.IsMasterClient)
        {
            CustomeValue = new ExitGames.Client.Photon.Hashtable();
            startTime = PhotonNetwork.Time;
            startTimer = true;
            CustomeValue.Add("StartTime", startTime);
            PhotonNetwork.CurrentRoom.SetCustomProperties(CustomeValue);
        }
        else
        {
            startTime = double.Parse(PhotonNetwork.CurrentRoom.CustomProperties["StartTime"].ToString());
            startTimer = true;
        }
    }

    void Update()
    {

        if (!startTimer) return;

        timerIncrementValue = PhotonNetwork.Time - startTime;
        text.text = ((int)timerIncrementValue).ToString();
        if (timerIncrementValue >= timer)
        {
            //Timer Completed
            //Do What Ever You What to Do Here
        }
    }
}
