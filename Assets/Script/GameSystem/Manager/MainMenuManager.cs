using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class MainMenuManager : MonoBehaviour {

    public static bool _showLobbyDialog;
    public static bool MainMenuBtnDownOn;
    public static bool MainModeBtnDownOn;
    public static bool StartMultiGameOn;
    private string _lobbyMessage;

    

    public GameObject MainMenuObject;
    public GameObject ModeSelectObject;

    public Button SingleModeButton;
    public Button MultiModeButton;

    public static GameModeState gameMode;

	// Use this for initialization
	void Start () {
        
        /* 
         * 유니티 엔진 사용 시 입력을 하지 않으면 모바일 장치의 화면이 어두워지다가 잠기게 되는데,
         * 그러면 플레이어는 잠김을 다시 풀어야 해서 불편합니다. 따라서 화면 잠금 방지 기능 추가는 필수적이고,
         * Screen.sleepTimeout를 아래처럼 설정하면 그걸 할 수 있습니다. 
         */
        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        GPGSManager.GetInstance.mainMenuManager = this;
        
        _showLobbyDialog = false;

        MainMenuObject.SetActive(true);
        ModeSelectObject.SetActive(false);

        MainMenuBtnDownOn = false;
        MainModeBtnDownOn = false;
        StartMultiGameOn = false;


        gameMode = GameModeState.NotSelect;
	}

    void Update()
    {
        switch(gameMode)
        {
            case GameModeState.Single:
                {
                    _lobbyMessage = "Starting a single-player game...";
                    _showLobbyDialog = true;

                    GameManager.NowGameModeState = GameModeState.Single;
                }
                break;

            case GameModeState.Multi:
                {
                    _lobbyMessage = "Starting a multi-player game...";
                    _showLobbyDialog = true;

                    //SingleModeButton.gameObject.SetActive(false);
                    //MultiModeButton.gameObject.SetActive(true);

                    GameManager.NowGameModeState = GameModeState.Multi;
                }
                break;

            case GameModeState.NotSelect:
                {
                    _showLobbyDialog = false;

                    SingleModeButton.gameObject.SetActive(true);
                    MultiModeButton.gameObject.SetActive(true);

                    GameManager.NowGameModeState = GameModeState.NotSelect;
                }
                break;
        }
    }

    public void SetLobbyStatusMessage(string message)
    {
        _lobbyMessage = message;
    }

    public void HideLobby()
    {
        _lobbyMessage = "";
        _showLobbyDialog = false;
    }

}
