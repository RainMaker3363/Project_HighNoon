using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using GooglePlayGames.BasicApi.Multiplayer;

public enum MultiPlayerState
{
    LIVE = 0,
    DEAD,
    DEADEYEING
}

public class MultiGameManager : MonoBehaviour, MPUpdateListener
{

    public static GameState NowGameState;
    public static GameControlState NowGameControlState;

    public Text MyInfoText;
    public Text EnemyInfoText;
    public Text NetText;

    public bool _showingGameOver;

    // 플레이어의 정보
    public GameObject MyCharacter;

    // 적의 정보
    public GameObject EnemyCharacter;
    private Dictionary<string, MulEnemy> _opponentScripts;

    private bool _multiplayerReady = false;
	private string _myParticipantId;
    private Vector2 _startingPoint;
	
    // Use this for initialization
    void Awake()
    {
        NowGameState = GameState.PLAY;
        NowGameControlState = GameControlState.PC;

        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("PlayerBullet"), LayerMask.NameToLayer("Player"), true);
        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("PlayerBullet"), LayerMask.NameToLayer("EnemyBullet"), true);
        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("PlayerBullet"), LayerMask.NameToLayer("PlayerBullet"), true);
        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("PlayerBullet"), LayerMask.NameToLayer("Item"), true);

        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("EnemyBullet"), LayerMask.NameToLayer("Enemy"), true);
        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("EnemyBullet"), LayerMask.NameToLayer("PlayerBullet"), true);
        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("EnemyBullet"), LayerMask.NameToLayer("EnemyBullet"), true);
        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("EnemyBullet"), LayerMask.NameToLayer("Item"), true);

        MyInfoText.text = "";
        EnemyInfoText.text = "";
        NetText.text = "";

        _showingGameOver = false;

        // 지정해 주면 고정비로 빌드가 되어 단말에서 지정 해상도로 출력이 된다.	
        Screen.SetResolution(1280, 720, true); // 1280 x 720 으로 조정
        //Screen.SetResolution(1920, 1080, true); // 1280 x 720 으로 조정

        //Screen.SetResolution(Screen.width, (Screen.width / 2) * 3 ); // 2:3 비율로 개발시

        //Screen.SetResolution(Screen.width, Screen.width * 16 / 9,  true); // 16:9 로 개발시


        //GPGSManager.GetInstance.InitializeGPGS();

        //GPGSManager.GetInstance.SignInAndStartMPGame();


        SetupMultiplayerGame();



	}

    public void UpdateReceived(string senderId, float posX, float posY, float velX, float velY, float rotZ)
    {
        if (_multiplayerReady)
        {
            MulEnemy opponent = _opponentScripts[senderId];

            if (opponent != null)
            {
                opponent.SetTransformInformation(posX, posY, velX, velY, rotZ);
            }


            EnemyCharacter.GetComponent<MulEnemy>().SetTransformInformation(posX, posY, velX, velY, rotZ);
        }

    }

    void SetupMultiplayerGame()
    {

        GPGSManager.GetInstance.updateListener = this;

        // 1
        _myParticipantId = GPGSManager.GetInstance.GetMyParticipantId();

        // 2
        List<Participant> allPlayers = GPGSManager.GetInstance.GetAllPlayers();
        _opponentScripts = new Dictionary<string, MulEnemy>(allPlayers.Count - 1);

        for (int i = 0; i < allPlayers.Count; i++)
        {
            string nextParticipantId = allPlayers[i].ParticipantId;
            Debug.Log("Setting up for " + nextParticipantId);


            // 나의 식별 ID일때...
            if (nextParticipantId == _myParticipantId)
            {
                // 4
                if(MyCharacter == null)
                {
                    MyCharacter = GameObject.Find("Lincoin_Body");
                }
            }
            else
            {
                if(EnemyCharacter == null)
                {
                    EnemyCharacter = GameObject.Find("EnemyLincoin_Body");

                    MulEnemy opponentScript = EnemyCharacter.GetComponent<MulEnemy>();
                    _opponentScripts[nextParticipantId] = opponentScript;
                    
                }
                else
                {
                    MulEnemy opponentScript = EnemyCharacter.GetComponent<MulEnemy>();
                    _opponentScripts[nextParticipantId] = opponentScript;
                }
                // 5
                //GameObject opponentCar = (Instantiate(opponentPrefab, carStartPoint, Quaternion.identity) as GameObject);

            }
        }

        //for (int i = 0; i < allPlayers.Count; i++)
        //{
        //    string nextParticipantId = allPlayers[i].ParticipantId;
        //    Debug.Log("Setting up car for " + nextParticipantId);
        //    // 3
        //    Vector3 carStartPoint = new Vector3(_startingPoint.x, _startingPoint.y + (i * _startingPointYOffset), 0);
        //    if (nextParticipantId == _myParticipantId)
        //    {
        //        // 4
        //        myCar.GetComponent<CarController>().SetCarChoice(i + 1, true);
        //        myCar.transform.position = carStartPoint;
        //    }
        //    else
        //    {
        //        // 5
        //        GameObject opponentCar = (Instantiate(opponentPrefab, carStartPoint, Quaternion.identity) as GameObject);
        //        OpponentCarController opponentScript = opponentCar.GetComponent<OpponentCarController>();
        //        opponentScript.SetCarNumber(i + 1);
        //        // 6
        //        _opponentScripts[nextParticipantId] = opponentScript;
        //    }
        //}
        //// 7
        //_lapsRemaining = 3;
        //_timePlayed = 0;
        //guiObject.SetLaps(_lapsRemaining);
        //guiObject.SetTime(_timePlayed);

        _multiplayerReady = true;

    }

    void ShowGameOver(bool didWin)
    {
        //gameOvertext = (didWin) ? "Woo hoo! You win!" : "Awww... better luck next time";

        NowGameState = GameState.PAUSE;

        _showingGameOver = true;

        Invoke("StartNewGame", 3.0f);
    }

    void StartNewGame()
    {
        AutoFade.LoadLevel("MultiPlayScene", 0.2f, 0.2f, Color.black);
    }


    void DoMultiplayerUpdate()
    {
        // In a multiplayer game, time counts up!
        //_timePlayed += Time.deltaTime;
        //guiObject.SetTime(_timePlayed);

        
        // We will be doing more here
        GPGSManager.GetInstance.SendMyUpdate(MyCharacter.transform.position.x,
                                                    MyCharacter.transform.position.z,
                                                  Vector2.zero,
                                                    MyCharacter.transform.rotation.eulerAngles.z);
    }
	// Update is called once per frame
	void Update () 
    {

        //NetText.text = "Net : " + GPGSManager.GetInstance.GetStateMessage().ToString();
        MyInfoText.text = "Player ID : " + GPGSManager.GetInstance.GetMyParticipantId() + "  Count : " + GPGSManager.GetInstance.GetAllPlayers().Count.ToString();
        EnemyInfoText.text = "Ohter[0] ID : " + GPGSManager.GetInstance.GetAllPlayers()[0].ParticipantId;
        NetText.text = "Other[1] ID : " + GPGSManager.GetInstance.GetAllPlayers()[1].ParticipantId;

        DoMultiplayerUpdate();

        switch(NowGameState)
        {
            case GameState.PLAY:
                {

                }
                break;

            case GameState.START:
                {

                }
                break;

            case GameState.PAUSE:
                {

                }
                break;

            case GameState.EVENT:
                {

                }
                break;

            case GameState.VICTORY:
                {

                }
                break;

            case GameState.GAMEOVER:
                {
                    ShowGameOver(false);
                }
                break;
        }

        switch (Application.platform)
        {
            case RuntimePlatform.Android:
                {
                    if (Input.GetKey(KeyCode.Escape))
                    {

                        // 할꺼 하셈

                        // Application.Quit();
                    }

                }
                break;

            default:
                {
                    if (Input.GetKey(KeyCode.Escape))
                    {

                        // 할꺼 하셈

                        // Application.Quit();

                    }
                }
                break;
        }
	}

}
