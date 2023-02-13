using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    #region private Field
    private string gameVersion = "1";
    #endregion

    #region public Field
    public Text connectionInfoText; //네트워크 정보 표시
    public Button joinButton;       //룸 접속 버튼
    public Button confirmButton; // 최종 참가 버튼 확인
    public bool isClicked = false;
    #endregion

    #region method Field
    private void Start()
    {   // 게임 시작과 동시에 마스터 서버 접속 시도
        PhotonNetwork.GameVersion = gameVersion;
        PhotonNetwork.ConnectUsingSettings();

        confirmButton.gameObject.SetActive(false);
        joinButton.interactable = false; // 룸 접속 버튼 비활성
        connectionInfoText.text = "마스터 서버 접속중...";
    }

    public override void OnConnectedToMaster()
    {
        joinButton.interactable = true;
        connectionInfoText.text = "온라인 : 마스터서버와 연결됨";
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        joinButton.interactable = false; // 룸 접속 버튼 비활성
        connectionInfoText.text = "오프라인 : 마스터서버와 연결되지 않음\n접속 재시도...";

        PhotonNetwork.ConnectUsingSettings();   // 접속 재시도
    }

    public void Connect()
    {
        joinButton.interactable = false; // 룸 접속 버튼 비활성

        if (PhotonNetwork.IsConnected)
        {   //마스터 서버접속중이라면,
            connectionInfoText.text = "룸에 접...속";
            PhotonNetwork.JoinRandomRoom();
        }
        else
        {
            connectionInfoText.text = "오프라인 : 마스터서버와 연결되지 않음\n접속 재시도...";

            PhotonNetwork.ConnectUsingSettings();   // 접속 재시도
        }
    }

    public void ClickConfirm()
    {
        isClicked = !isClicked;
        if (isClicked)
        {
            PhotonNetwork.LoadLevel("player");
        }
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {   // (빈 방이 없어)랜덤 방참가에 실패한 경우 자동실행
        connectionInfoText.text = "빈 방이 없음 ㅜ 새로운 방 생성중";
        PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = 4 });
    }

    public override void OnJoinedRoom()
    {
        connectionInfoText.text = "방 참가 가능\n방 참가를 원한다면 아래 버튼을 눌러주세요 :)";
        joinButton.gameObject.SetActive(false);
        confirmButton.gameObject.SetActive(true);
    }
    #endregion
}
