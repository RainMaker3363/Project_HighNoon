using UnityEngine;
using System.Collections;


public enum GameControlState
{
    Mobile = 0,
    PC
}

public enum GameState
{
    START = 0,
    PAUSE,
    GAMEOVER,
    VICTORY,
    EVENT,
    PLAY
}


public enum GameModeState
{
    Single = 0,
    Multi,
    MiniGame,
    NotSelect
}

public enum PlayerState
{
    NORMAL = 0,
    DEADEYE,
    REALBATTLE,
    DEAD
}

public enum EnemyState
{ 
    NORMAL = 0,
    REALBATTLE,
    DEAD
}

public enum EnemyAIState
{
    CHASE = 0,
    PATROL
}

public enum PlayerBehaviorState
{
    IDLE = 0,
    MOVE
}

public enum AnimationState
{
    LEFTWALK = 0,
    RIGHTWALK,
    UPWALK,
    DOWNWALK,
    LEFTUPWALK,
    RIGHTUPWALK,
    LEFTDOWNWALK,
    RIGHTDOWNWALK,
    LEFTSTAND,
    RIGHTSTAND,
    UPSTAND,
    DOWNSTAND,
    LEFTUPSTAND,
    RIGHTUPSTAND,
    LEFTDOWNSTAND,
    RIGHTDOWNSTAND,
    LEFTSHOOT,
    RIGHTSHOOT,
    UPSHOOT,
    DOWNSHOOT,
    LEFTUPSHOOT,
    RIGHTUPSHOOT,
    LEFTDOWNSHOOT,
    RIGHTDOWNSHOOT,
    SHOOTING,
    DEADEYING,
    LEFTDEAD,
    RIGHTDEAD,
    UPDEAD,
    DOWNDEAD
}

public class GameManager : MonoBehaviour {

    public static GameState NowGameState = GameState.PLAY;
    public static GameControlState NowGameControlState = GameControlState.PC;
    public static EnemyAIState NowEnemyAiState = EnemyAIState.PATROL;
    public static GameModeState NowGameModeState = GameModeState.NotSelect;

    // 현재 스테이지에 있는 적들의 숫자
    public static int NowStageEnemies;
    // 데드 아이 액션 시작의 여부
    public static bool DeadEyeActiveOn;
    // 데드 아이 대결 모션의 끝난 여부
    public static bool DeadEyeVersusAction;
    // 데드 아이 리볼버 모션의 끝난 여부
    public static bool DeadEyeRevolverAction;

    // 데드 아이 성공 여부
    public static bool DeadEyeFailOn;

    // 미니게임에 쓰일 주인공 총알 정보들...
    public static int NowPlayerBulletPlusQuantity;
    public static int NowPlayerMaxBulletQuantity;

    // 미니 게임에 쓰일 정보들...
    private float Minigame_StartTimer;
    private float Minigame_RoundTimer;
    public static int MiniGame_Round;
    public static int EnemyKillCount;

	// Use this for initialization
	void Awake () 
    {
        //NowGameState = GameState.PLAY;
        //NowGameControlState = GameControlState.PC;
        //NowEnemyAiState = EnemyAIState.PATROL;

        //NowGameModeState = GameModeState.Single;

        // 임시로 하는거니 나중에 지우세요
        NowGameModeState = GameModeState.MiniGame;


        print("NowGameModeState : " + NowGameModeState.ToString());

        NowPlayerBulletPlusQuantity = 1;
        NowPlayerMaxBulletQuantity = 6;

        switch(NowGameModeState)
        {
            case GameModeState.Single:
                {
                    DeadEyeActiveOn = false;
                    DeadEyeVersusAction = false;
                    DeadEyeRevolverAction = false;
                    DeadEyeFailOn = false;

                    NowGameState = GameState.PLAY;

                    NowStageEnemies = (GameObject.FindGameObjectsWithTag("Enemy").Length);
                }
                break;

            case GameModeState.Multi:
                {
                    DeadEyeActiveOn = false;
                    DeadEyeVersusAction = false;
                    DeadEyeRevolverAction = false;
                    DeadEyeFailOn = false;

                    NowStageEnemies = (GameObject.FindGameObjectsWithTag("Enemy").Length);
                }
                break;

            case GameModeState.MiniGame:
                {
                    DeadEyeActiveOn = false;
                    DeadEyeVersusAction = false;
                    DeadEyeRevolverAction = false;
                    DeadEyeFailOn = false;

                    MiniGame_Round = 1;
                    Minigame_StartTimer = 30.0f;
                    Minigame_RoundTimer = 10.0f;
                    EnemyKillCount = 0;

                    //NowGameState = GameState.START;

                    NowStageEnemies = (GameObject.FindGameObjectsWithTag("Enemy").Length);
                }
                break;

            case GameModeState.NotSelect:
                {
                    DeadEyeActiveOn = false;
                    DeadEyeVersusAction = false;
                    DeadEyeRevolverAction = false;
                    DeadEyeFailOn = false;

                    NowStageEnemies = (GameObject.FindGameObjectsWithTag("Enemy").Length);
                }
                break;
        }



        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("PlayerBullet"), LayerMask.NameToLayer("Player"), true);
        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("PlayerBullet"), LayerMask.NameToLayer("EnemyBullet"), true);
        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("PlayerBullet"), LayerMask.NameToLayer("PlayerBullet"), true);
        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("PlayerBullet"), LayerMask.NameToLayer("Item"), true);

        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("EnemyBullet"), LayerMask.NameToLayer("Enemy"), true);
        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("EnemyBullet"), LayerMask.NameToLayer("PlayerBullet"), true);
        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("EnemyBullet"), LayerMask.NameToLayer("EnemyBullet"), true);
        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("EnemyBullet"), LayerMask.NameToLayer("Item"), true);

        /* 
         * 유니티 엔진 사용 시 입력을 하지 않으면 모바일 장치의 화면이 어두워지다가 잠기게 되는데,
         * 그러면 플레이어는 잠김을 다시 풀어야 해서 불편합니다. 따라서 화면 잠금 방지 기능 추가는 필수적이고,
         * Screen.sleepTimeout를 아래처럼 설정하면 그걸 할 수 있습니다. 
         */
        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        // 지정해 주면 고정비로 빌드가 되어 단말에서 지정 해상도로 출력이 된다.	
        Screen.SetResolution(1280, 720, true); // 1280 x 720 으로 조정
        //Screen.SetResolution(1920, 1080, true); // 1280 x 720 으로 조정

        //Screen.SetResolution(Screen.width, (Screen.width / 2) * 3 ); // 2:3 비율로 개발시

        //Screen.SetResolution(Screen.width, Screen.width * 16 / 9,  true); // 16:9 로 개발시
	}

    // Update is called once per frame
    void Update()
    {
        //if (NowStageEnemies <= 0)
        //{
        //    NowGameState = GameState.VICTORY;
        //}
        switch (NowGameModeState)
        {
            case GameModeState.Single:
                {

                }
                break;

            case GameModeState.Multi:
                {
                    switch(NowGameState)
                    {
                        case GameState.START:
                            {

                            }
                            break;

                        case GameState.PLAY:
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

                            }
                            break;
                    }
                }
                break;

            case GameModeState.MiniGame:
                {

                }
                break;

            case GameModeState.NotSelect:
                {

                }
                break;
        }

        switch(Application.platform)
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
