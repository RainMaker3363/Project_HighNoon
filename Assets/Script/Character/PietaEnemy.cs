using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PietaEnemy : MonoBehaviour {

    public GameObject MainCamera;
    public GameObject CharacterAniObject;

    private GameState State;
    private EnemyState enemyState;
    private EnemyAIState enemyAiState;

    [HideInInspector]
    public PlayerState playerState;

    private Player m_Player;

    // 길찾기 타일
    private float walkSpeed;
    private TileMap tileMap;
    private List<PathTile> path = new List<PathTile>();

    // 적이 쏘는 총알
    public GameObject[] EnemyBullets;
    private int EnemyBulletIndex;
    private bool EnemyShootOn;
    private float EnemyShootCoolTime;

    // 적이 순찰을 할때의 위치및 순서를 제어한다.
    public GameObject[] PatrolPath;
    private bool PathEnable;
    private bool PathChangeOn;
    private bool PathControlChange;

    // 적이 플레이어와 시선이 마주쳤는지 여부
    RaycastHit hit;
    private int layerMask;
    private Vector3[] SightVectorInterpol;

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

        // 타일맵 길 찾기 관련 로직 초기화
        tileMap = FindObjectOfType(typeof(TileMap)) as TileMap;

        walkSpeed = 2.5f;
        PathEnable = true;
        PathChangeOn = true;
        PathControlChange = true;

        // 적의 시야 초기화
        SightVectorInterpol = new Vector3[2];

        SightVectorInterpol[0] = new Vector3(0.10f, 0.0f, 0.0f);
        SightVectorInterpol[1] = new Vector3(0.16f, 0.0f, 0.0f);
        layerMask = 1 << LayerMask.NameToLayer("Player");

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

        //Debug.Log("Path Count : " + path.Count);
        State = GameManager.NowGameState;
        enemyState = EnemyState.NORMAL;
        enemyAiState = EnemyAIState.PATROL;
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
                                //print((this.transform.position - m_Player.gameObject.transform.position).magnitude);
                                
                                switch(enemyState)
                                {
                                    case EnemyState.NORMAL:
                                        {
                                            switch (enemyAiState)
                                            {
                                                case EnemyAIState.PATROL:
                                                    {
                                                        //if (PathEnable == true)
                                                        //{
                                                        //    if (PathControlChange == true)
                                                        //    {
                                                        //        PathControlChange = false;

                                                        //        if (PathChangeOn == true)
                                                        //        {
                                                        //            if (tileMap.FindPath(PatrolPath[0].transform.position, PatrolPath[1].transform.position, path))
                                                        //            {
                                                        //                StopCoroutine(WalkPath());
                                                        //                StartCoroutine(WalkPath());
                                                        //            }

                                                        //        }
                                                        //        else
                                                        //        {
                                                        //            if (tileMap.FindPath(PatrolPath[1].transform.position, PatrolPath[0].transform.position, path))
                                                        //            {
                                                        //                StopCoroutine(WalkPath());
                                                        //                StartCoroutine(WalkPath());
                                                        //            }
                                                        //        }
                                                        //    }
                                                           

                                                        //}

                                                        
                                                        // 만약 시야안에 들어오는 조건이 됬을시에 적의 상태를 바꿈
                                                        Debug.DrawRay(this.transform.position, this.transform.forward * 10.0f, Color.green);
                                                        Debug.DrawRay(this.transform.position, ((this.transform.forward + SightVectorInterpol[0]).normalized * 10.0f), Color.green);
                                                        Debug.DrawRay(this.transform.position, ((this.transform.forward + SightVectorInterpol[1]).normalized * 10.0f), Color.green);
                                                        Debug.DrawRay(this.transform.position, ((this.transform.forward - SightVectorInterpol[0]).normalized * 10.0f), Color.green);
                                                        Debug.DrawRay(this.transform.position, ((this.transform.forward - SightVectorInterpol[1]).normalized * 10.0f), Color.green);

                                                        if (Physics.Raycast(this.transform.position, (this.transform.forward * 10.0f).normalized, out hit, 10.0f, layerMask))
                                                        {
                                                            if(hit.collider.gameObject.tag.Equals("Player"))
                                                            {
                                                                //enemyAiState = EnemyAIState.CHASE;
                                                                //enemyState = EnemyState.REALBATTLE;
                                                                //m_Player.SetPlayerState(2);

                                                                Debug.Log("PlayerHit!");
                                                            }
                                                        }
                                                        else if (Physics.Raycast(this.transform.position, ((this.transform.forward + SightVectorInterpol[0])).normalized, out hit, 10.0f, layerMask))
                                                        {
                                                            if (hit.collider.gameObject.tag.Equals("Player"))
                                                            {
                                                                //enemyAiState = EnemyAIState.CHASE;
                                                                //enemyState = EnemyState.REALBATTLE;
                                                                //m_Player.SetPlayerState(2);

                                                                Debug.Log("PlayerHit!");
                                                            }
                                                        }
                                                        else if (Physics.Raycast(this.transform.position, ((this.transform.forward + SightVectorInterpol[1])).normalized, out hit, 10.0f, layerMask))
                                                        {
                                                            if (hit.collider.gameObject.tag.Equals("Player"))
                                                            {
                                                                //enemyAiState = EnemyAIState.CHASE;
                                                                //enemyState = EnemyState.REALBATTLE;
                                                                //m_Player.SetPlayerState(2);

                                                                Debug.Log("PlayerHit!");
                                                            }
                                                        }
                                                        else if (Physics.Raycast(this.transform.position, ((this.transform.forward - SightVectorInterpol[0])).normalized, out hit, 10.0f, layerMask))
                                                        {
                                                            if (hit.collider.gameObject.tag.Equals("Player"))
                                                            {
                                                                //enemyAiState = EnemyAIState.CHASE;
                                                                //enemyState = EnemyState.REALBATTLE;
                                                                //m_Player.SetPlayerState(2);

                                                                Debug.Log("PlayerHit!");
                                                            }
                                                        }
                                                        else if (Physics.Raycast(this.transform.position, ((this.transform.forward - SightVectorInterpol[1])).normalized, out hit, 10.0f, layerMask))
                                                        {
                                                            if (hit.collider.gameObject.tag.Equals("Player"))
                                                            {
                                                                //enemyAiState = EnemyAIState.CHASE;
                                                                //enemyState = EnemyState.REALBATTLE;
                                                                //m_Player.SetPlayerState(2);

                                                                Debug.Log("PlayerHit!");
                                                            }
                                                        }

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
                                                        //awdasdasfwfaddsadwd

                                                        //if (PathEnable == true)
                                                        //{
                                                        //    if (PathControlChange == true)
                                                        //    {
                                                        //        PathControlChange = false;

                                                        //        if (PathChangeOn == true)
                                                        //        {
                                                        //            if (tileMap.FindPath(PatrolPath[0].transform.position, PatrolPath[1].transform.position, path))
                                                        //            {
                                                        //                StopCoroutine(WalkPath());
                                                        //                StartCoroutine(WalkPath());
                                                        //            }

                                                        //        }
                                                        //        else
                                                        //        {
                                                        //            if (tileMap.FindPath(PatrolPath[1].transform.position, PatrolPath[0].transform.position, path))
                                                        //            {
                                                        //                StopCoroutine(WalkPath());
                                                        //                StartCoroutine(WalkPath());
                                                        //            }
                                                        //        }
                                                        //    }


                                                        //}
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

    IEnumerator WalkPathChangeProtocol()
    {

        PathChangeOn = false;

        yield return new WaitForSeconds(2.0f);
        
        PathControlChange = true;
        
    }

    IEnumerator WalkPath()
    {
        var index = 0;

        while(index < path.Count)
        {
            yield return StartCoroutine(WalkTo(path[index].transform.position));
            index++;
        }
    }

    IEnumerator WalkTo(Vector3 position)
    {
        while(Vector3.Distance(transform.position, position) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, position, walkSpeed * Time.deltaTime);
            //transform.Translate(Vector3.MoveTowards(transform.position, position, walkSpeed * Time.deltaTime));
            yield return 0;
        }

        transform.position = position;
        PathChangeOn ^= true;
        //PathChangeEnable ^= true;
    }
}
