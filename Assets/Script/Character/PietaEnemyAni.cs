using UnityEngine;
using System.Collections;

public class PietaEnemyAni : MonoBehaviour {

    private GameObject MainCamera;
    public PietaEnemy m_PietaEnemy;

    private GameState State;
    private EnemyState enemyState;
    private EnemyAIState enemyAiState;

    [HideInInspector]
    public PlayerState playerState;

	// Use this for initialization
	void Start () {

        if (MainCamera == null)
        {
            MainCamera = GameObject.FindWithTag("MainCamera");
        }

        if (m_PietaEnemy == null)
        {
            Debug.Log("Enemy Null Object countion!");
        }

        // Quad 텍스쳐를 처리해주는 부분
        this.transform.LookAt(MainCamera.transform.position);
        this.transform.Rotate(new Vector3(0.0f, -180.0f, 0.0f));

        State = GameManager.NowGameState;
        enemyState = m_PietaEnemy.GetenemyState();
        enemyAiState = m_PietaEnemy.GetenemyAiState();

	}
	
	// Update is called once per frame
	void Update () {

        enemyState = m_PietaEnemy.GetenemyState();
        enemyAiState = m_PietaEnemy.GetenemyAiState();

        playerState = m_PietaEnemy.GetPlayer().GetPlayerState();

        State = GameManager.NowGameState;

        

        switch (State)
        {
            case GameState.START:
                {

                }
                break;

            case GameState.PLAY:
                {
                    this.gameObject.SetActive(m_PietaEnemy.gameObject.activeSelf);
                    this.transform.position = new Vector3(m_PietaEnemy.gameObject.transform.position.x, m_PietaEnemy.gameObject.transform.position.y + 0.4f, m_PietaEnemy.gameObject.transform.position.z);

                    switch (playerState)
                    {
                        case PlayerState.NORMAL:
                            {


                                switch (enemyState)
                                {
                                    case EnemyState.NORMAL:
                                        {
                                            switch (enemyAiState)
                                            {
                                                case EnemyAIState.PATROL:
                                                    {

                                                    }
                                                    break;

                                                case EnemyAIState.CHASE:
                                                    {

                                                    }
                                                    break;
                                            }
                                        }
                                        break;

                                    case EnemyState.DEAD:
                                        {
                                            switch (enemyAiState)
                                            {
                                                case EnemyAIState.PATROL:
                                                    {

                                                    }
                                                    break;

                                                case EnemyAIState.CHASE:
                                                    {

                                                    }
                                                    break;
                                            }
                                        }
                                        break;

                                    case EnemyState.REALBATTLE:
                                        {
                                            switch (enemyAiState)
                                            {
                                                case EnemyAIState.PATROL:
                                                    {

                                                    }
                                                    break;

                                                case EnemyAIState.CHASE:
                                                    {

                                                    }
                                                    break;
                                            }

                                        }
                                        break;
                                }
                            }
                            break;

                        case PlayerState.DEADEYE:
                            {
                                // 데드 아이 상태가 되면 모든 행동을 멈춘다.
                                switch (enemyAiState)
                                {
                                    case EnemyAIState.PATROL:
                                        {

                                        }
                                        break;

                                    case EnemyAIState.CHASE:
                                        {

                                        }
                                        break;
                                }
                            }
                            break;

                        case PlayerState.REALBATTLE:
                            {
                                switch (enemyState)
                                {
                                    case EnemyState.NORMAL:
                                        {
                                            switch (enemyAiState)
                                            {
                                                case EnemyAIState.PATROL:
                                                    {
                                                        // 만약 시야안에 들어오는 조건이 됬을시에 적의 상태를 바꿈
                                                    }
                                                    break;


                                                case EnemyAIState.CHASE:
                                                    {

                                                    }
                                                    break;
                                            }
                                        }
                                        break;

                                    case EnemyState.DEAD:
                                        {
                                            switch (enemyAiState)
                                            {
                                                case EnemyAIState.PATROL:
                                                    {

                                                    }
                                                    break;

                                                case EnemyAIState.CHASE:
                                                    {

                                                    }
                                                    break;
                                            }
                                        }
                                        break;

                                    case EnemyState.REALBATTLE:
                                        {
                                            switch (enemyAiState)
                                            {
                                                case EnemyAIState.PATROL:
                                                    {

                                                    }
                                                    break;

                                                // 추격시 행동들
                                                case EnemyAIState.CHASE:
                                                    {
                                                       

                                                    }
                                                    break;
                                            }

                                        }
                                        break;
                                }
                            }
                            break;

                        case PlayerState.DEAD:
                            {
                                switch (enemyAiState)
                                {
                                    case EnemyAIState.PATROL:
                                        {

                                        }
                                        break;

                                    case EnemyAIState.CHASE:
                                        {

                                        }
                                        break;
                                }
                            }
                            break;
                    }

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

            case GameState.GAMEOVER:
                {

                }
                break;

            case GameState.VICTORY:
                {

                }
                break;
        }
	}
}
