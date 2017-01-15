using UnityEngine;
using System.Collections;

public class PietaEnemy : MonoBehaviour {

    public GameObject MainCamera;
    public GameObject CharacterAniObject;

    private GameState State;
    private EnemyState enemyState;
    private EnemyAIState enemyAiState;

    [HideInInspector]
    public PlayerState playerState;

    private Player m_Player;

    // 적이 쏘는 총알
    public GameObject[] EnemyBullets;
    private int EnemyBulletIndex;
    private bool EnemyShootOn;
    private float EnemyShootCoolTime;

    // 적이 순찰을 할때의 위치및 순서를 제어한다.
    public GameObject[] PatrolObjects;
    public int PatrolIndex;

    // 적이 플레이어와 시선이 마주쳤는지 여부


	// Use this for initialization
	void Start () {
	
        if(MainCamera == null)
        {
            MainCamera = GameObject.FindWithTag("MainCamera");
        }

        if (m_Player == null)
        {
            m_Player = GameObject.FindWithTag("Player").GetComponent<Player>();
            playerState = m_Player.GetPlayerState();
        }
        else
        {
            playerState = m_Player.GetPlayerState();
        }

        State = GameManager.NowGameState;
        enemyState = EnemyState.NORMAL;
        enemyAiState = EnemyAIState.PATROL;

        // Quad 텍스쳐를 처리해주는 부분
        CharacterAniObject.transform.LookAt(MainCamera.transform.position);
        CharacterAniObject.transform.Rotate(new Vector3(0.0f, -180.0f, 0.0f));


        // 적의 총알 관련 로직 초기화
        for(int i =0; i<EnemyBullets.Length; i++)
        {
            EnemyBullets[i].SetActive(false);
        }

        EnemyShootOn = true;
        EnemyBulletIndex = 0;
        EnemyShootCoolTime = 0.5f;

	}
	
	// Update is called once per frame
	void Update () {

        playerState = m_Player.GetPlayerState();
        State = GameManager.NowGameState;


        switch (State)
        {
            case GameState.START:
                {

                }
                break;

            case GameState.PLAY:
                {
                    switch (playerState)
                    {
                        case PlayerState.NORMAL:
                            {
                                print((this.transform.position - m_Player.gameObject.transform.position).magnitude);

                                switch(enemyState)
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


                                                case EnemyAIState.ATTACK:
                                                    {

                                                    }
                                                    break;
                                            }
                                        }
                                        break;

                                    case EnemyState.DEAD:
                                        {

                                        }
                                        break;

                                    case EnemyState.REALBATTLE:
                                        {
                                            switch(enemyAiState)
                                            {
                                                case EnemyAIState.PATROL:
                                                    {

                                                    }
                                                    break;

                                                    // 추격시 행동들
                                                case EnemyAIState.CHASE:
                                                    {
                                                        // 플레이어의 위치로 다시 이동한다.

                                                    }
                                                    break;

                                                    
                                                case EnemyAIState.ATTACK:
                                                    {
                                                        // 범위 안에 있다면 사격
                                                        if ((this.transform.position - m_Player.gameObject.transform.position).magnitude <= 6.0f)
                                                        {
                                                            Shoot();
                                                        }
                                                        else
                                                        {
                                                            enemyAiState = EnemyAIState.CHASE;
                                                        }
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


                                                case EnemyAIState.ATTACK:
                                                    {

                                                    }
                                                    break;
                                            }
                                        }
                                        break;

                                    case EnemyState.DEAD:
                                        {

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
                                                        // 플레이어의 위치로 다시 이동한다.

                                                    }
                                                    break;


                                                case EnemyAIState.ATTACK:
                                                    {
                                                        // 범위 안에 있다면 사격
                                                        if ((this.transform.position - m_Player.gameObject.transform.position).magnitude <= 6.0f)
                                                        {
                                                            Shoot();
                                                        }
                                                        else
                                                        {
                                                            enemyAiState = EnemyAIState.CHASE;
                                                        }
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

    // 총을 발사 했을때..
    public void Shoot()
    {
        if (EnemyShootOn == true)
        {
            StopCoroutine(ShootProtocol(true));
            StartCoroutine(ShootProtocol(true));
        }


    }

    // 적의 총알 발사 행동
    IEnumerator ShootProtocol(bool On = true)
    {
        EnemyShootOn = false;

        Debug.Log("Bang!");

        // 총알 오브젝트 On
        if (EnemyBulletIndex < EnemyBullets.Length)
        {
            if (EnemyBullets[EnemyBulletIndex].gameObject.activeSelf == false)
            {
                EnemyBullets[EnemyBulletIndex].transform.position = this.transform.position;
                //EnemyBullets[NowBulletIndex].transform.LookAt(Player_Tranjectory_Object.transform.position);
                EnemyBullets[EnemyBulletIndex].transform.parent = null;

                EnemyBullets[EnemyBulletIndex].gameObject.SetActive(true);

                EnemyBulletIndex++;
            }
        }
        else
        {
            EnemyBulletIndex = 0;

            if (EnemyBullets[EnemyBulletIndex].gameObject.activeSelf == false)
            {
                EnemyBullets[EnemyBulletIndex].transform.position = this.transform.position;
                //EnemyBullets[EnemyBulletIndex].transform.LookAt(Player_Tranjectory_Object.transform.position);
                EnemyBullets[EnemyBulletIndex].transform.parent = null;

                EnemyBullets[EnemyBulletIndex].gameObject.SetActive(true);

                EnemyBulletIndex++;
            }
        }

        yield return new WaitForSeconds(EnemyShootCoolTime);

        EnemyShootOn = true;
    }

    // 적이 플레이어를 발견해서 전투 시작 애니메이션 및 준비 로직
    IEnumerator ChaseProtocol(bool On = true)
    {
        yield return new WaitForSeconds(0.5f);

        enemyState = EnemyState.REALBATTLE;
        enemyAiState = EnemyAIState.CHASE;
    }

    // 사망 로직
    IEnumerator DeadProtocol(bool On = true)
    {
        yield return new WaitForSeconds(0.5f);

        enemyState = EnemyState.DEAD;
        enemyAiState = EnemyAIState.PATROL;
    }
}
