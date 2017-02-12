using UnityEngine;
using System.Collections;
using System;
using GooglePlayGames;
using GooglePlayGames.BasicApi.SavedGame;
using GooglePlayGames.BasicApi.Multiplayer;
using GooglePlayGames.BasicApi;


// 하이어라키에 GPGSManager 오브젝트를 추가해줄 것
public class GPGSManager : Singleton<GPGSManager>, RealTimeMultiplayerListener
{
    private uint minimumOpponents = 1;
    private uint maximumOpponents = 1;
    private uint gameVariation = 0;

	// 현재 로그인 중인지 체크
    public bool bLogin
    {
        get;
        set;
    }

    // GPGS를 초기화 합니다
    public void InitializeGPGS()
    {
        bLogin = false;

        PlayGamesPlatform.DebugLogEnabled = true;
        PlayGamesPlatform.Activate();

    }

    // P2P 방식으로 상대방을 검색하기 시작한다.
    public void StartMatchMaking()
    {
        // 최소 수용 인원
        // 최대 수용 인원
        PlayGamesPlatform.Instance.RealTime.CreateQuickGame(minimumOpponents, maximumOpponents, gameVariation, this);
    }

    // 현재 상태를 디버깅 로그로 보여주는 함수
    private void ShowMPStatus(string message)
    {
        Debug.Log(message);
    }

    // 멀티플레이 방이 얼마나 셋업이 되었는지 보여주는 리스너 함수
    public void OnRoomSetupProgress(float percent)
    {
        ShowMPStatus("We are " + percent + "% done with setup");
    }

    // 멀티플레이 방이 연결되었는지의 여부
    public void OnRoomConnected(bool success)
    {
        if (success)
        {
            ShowMPStatus("We are connected to the room! I would probably start our game now.");
        }
        else
        {
            ShowMPStatus("Uh-oh. Encountered some error connecting to the room.");
        }
    }

    // 멀티플레이어가 나갔을때 호출되는 리스너 함수
    public void OnLeftRoom()
    {
        ShowMPStatus("We have left the room. We should probably perform some clean-up tasks.");
    }

    // 해당 플레이어의 아이디가 연결을 했을 경우 호출되는 리스너 함수
    public void OnPeersConnected(string[] participantIds)
    {
        foreach (string participantID in participantIds)
        {
            ShowMPStatus("Player " + participantID + " has joined.");
        }
    }

    // 해당 플레이어의 아이디가 연결을 끊었을 경우 호출되는 리스너 함수
    public void OnPeersDisconnected(string[] participantIds)
    {
        foreach (string participantID in participantIds)
        {
            ShowMPStatus("Player " + participantID + " has left.");
        }
    }

    /// Raises the participant left event.
    /// This is called during room setup if a player declines an invitation
    /// or leaves.  The status of the participant can be inspected to determine
    /// the reason.  If all players have left, the room is closed automatically.
    public void OnParticipantLeft(Participant participant)
    {
        ShowMPStatus("All Player Out! So, The Room will be Close");

    }


    // 상대 ID로부터 메시지를 받았을때 호출되는 리스너 함수
    public void OnRealTimeMessageReceived(bool isReliable, string senderId, byte[] data)
    {
        ShowMPStatus("We have received some gameplay messages from participant ID:" + senderId);
    }

    // GPGS를 로그인 합니다.
    public void LoginGPGS()
    {
        // 로그인이 안되어 있으면..
        if(!Social.localUser.authenticated)
        {
            Social.localUser.Authenticate(LoginCallBackGPGS);
        }
    }

    // GPGS 로그인 콜백
    public void LoginCallBackGPGS(bool result)
    {
        bLogin = result;
    }

    // GPGS를 로그아웃 합니다.
    public void LogoutGPGS()
    {
        // 로그인이 되어 있으면
        if(Social.localUser.authenticated)
        {
            ((GooglePlayGames.PlayGamesPlatform)Social.Active).SignOut();
            bLogin = false;
        }
    }

    // GPGS에서 자신의 프로필 이미지를 가져옵니다.
    public Texture2D GetImageGPGS()
    {
        if(Social.localUser.authenticated)
        {
            return Social.localUser.image;
        }
        else
        {
            return null;
        }
    }

    // GPGS 에서 사용자 이름을 가져옵니다
    public string GetNameGPGS()
    {
        if (Social.localUser.authenticated)
        {
            return Social.localUser.userName;
        }
        else
        {
            return null;
        }
    }
}
