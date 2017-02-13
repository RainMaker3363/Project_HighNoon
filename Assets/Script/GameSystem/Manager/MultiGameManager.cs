using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GooglePlayGames.BasicApi.Multiplayer;



public class MultiGameManager : MonoBehaviour {

    public static GameState NowGameState;
    public static GameControlState NowGameControlState;
    public static EnemyAIState NowEnemyAiState;

    public GameObject MyCharacter;
    public GameObject EnemyCharacter;

    private bool _multiplayerReady;
	private string _myParticipantId;
    private Vector2 _startingPoint;
	
    // Use this for initialization
    void Awake()
    {
        NowGameState = GameState.PLAY;
        NowGameControlState = GameControlState.PC;
        NowEnemyAiState = EnemyAIState.PATROL;

        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("PlayerBullet"), LayerMask.NameToLayer("Player"), true);
        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("PlayerBullet"), LayerMask.NameToLayer("EnemyBullet"), true);
        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("PlayerBullet"), LayerMask.NameToLayer("PlayerBullet"), true);
        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("PlayerBullet"), LayerMask.NameToLayer("Item"), true);

        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("EnemyBullet"), LayerMask.NameToLayer("Enemy"), true);
        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("EnemyBullet"), LayerMask.NameToLayer("PlayerBullet"), true);
        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("EnemyBullet"), LayerMask.NameToLayer("EnemyBullet"), true);
        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("EnemyBullet"), LayerMask.NameToLayer("Item"), true);

        // 지정해 주면 고정비로 빌드가 되어 단말에서 지정 해상도로 출력이 된다.	
        Screen.SetResolution(1280, 720, true); // 1280 x 720 으로 조정
        //Screen.SetResolution(1920, 1080, true); // 1280 x 720 으로 조정

        //Screen.SetResolution(Screen.width, (Screen.width / 2) * 3 ); // 2:3 비율로 개발시

        //Screen.SetResolution(Screen.width, Screen.width * 16 / 9,  true); // 16:9 로 개발시

        GPGSManager.GetInstance.SignInAndStartMPGame();
	}

    public void UpdateReceived(string senderId, float posX, float posY, float velX, float velY, float rotZ)
    {
        //if (_multiplayerReady)
        //{
        //    OpponentCarController opponent = _opponentScripts[senderId];
        //    if (opponent != null)
        //    {
        //        opponent.SetCarInformation(posX, posY, velX, velY, rotZ);
        //    }
        //}
    }

    void SetupMultiplayerGame()
    {

        // 1
        _myParticipantId = GPGSManager.GetInstance.GetMyParticipantId();
        // 2
        //List<Participant> allPlayers = MultiplayerController.Instance.GetAllPlayers();
        //_opponentScripts = new Dictionary<string, OpponentCarController>(allPlayers.Count - 1);
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
	
	// Update is called once per frame
	void Update () 
    {
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
