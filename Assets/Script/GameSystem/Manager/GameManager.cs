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
    SHOOTING,
    DEADEYING,
    DEAD
}

public class GameManager : MonoBehaviour {

    public static GameState NowGameState;
    public static GameControlState NowGameControlState;
    public static EnemyAIState NowEnemyAiState;

    public static int NowStageEnemies;

	// Use this for initialization
	void Awake () 
    {
        NowGameState = GameState.PLAY;
        NowGameControlState = GameControlState.PC;
        NowEnemyAiState = EnemyAIState.PATROL;

        NowStageEnemies = (GameObject.FindGameObjectsWithTag("Enemy").Length);

        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("PlayerBullet"), LayerMask.NameToLayer("Player"), true);
        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("EnemyBullet"), LayerMask.NameToLayer("Enemy"), true);

        // 지정해 주면 고정비로 빌드가 되어 단말에서 지정 해상도로 출력이 된다.	
        Screen.SetResolution(1280, 720, true); // 1280 x 720 으로 조정

        //Screen.SetResolution(Screen.width, (Screen.width / 2) * 3 ); // 2:3 비율로 개발시

        //Screen.SetResolution(Screen.width, Screen.width * 16 / 9,  true); // 16:9 로 개발시
	}

    // Update is called once per frame
    void Update()
    {
        if (NowStageEnemies <= 0)
        {
            NowGameState = GameState.VICTORY;
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
