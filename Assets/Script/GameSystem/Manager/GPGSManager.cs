using UnityEngine;
using System.Collections;
using System;
using GooglePlayGames;
using GooglePlayGames.BasicApi.SavedGame;
using GooglePlayGames.BasicApi;


// 하이어라키에 GPGSManager 오브젝트를 추가해줄 것
public class GPGSManager : Singleton<GPGSManager>
{

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

        PlayGamesPlatform.Activate();
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
