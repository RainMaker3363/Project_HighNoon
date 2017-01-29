using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PietaEnemy : MonoBehaviour {

    public GameObject MainCamera;
    public GameObject Enemy_Tranjectory_Object;
    public GameObject Enemy_DeadEyeMark_Object;

    private GameState State;
    private EnemyState enemyState;
    private EnemyAIState enemyAiState;
    private AnimationState enemyAniState;

    [HideInInspector]
    public PlayerState playerState;

    private Player m_Player;
    private bool ImDead;

    // 적의 상태
    public int HP;

    // 길찾기 타일
    private float walkSpeed;
    private TileMap tileMap;
    private List<PathTile> path = new List<PathTile>();

    // 적이 쏘는 총알
    public GameObject[] EnemyBullets;
    private int EnemyBulletIndex;
    private bool EnemyShootOn;
    private float EnemyShootCoolTime;

    private IEnumerator WalkPathcoroutine;
    private IEnumerator WalkTocoroutine;
    private IEnumerator PathFindUpdateRoutine;
    

    // 적이 순찰을 할때의 위치및 순서를 제어한다.
    public GameObject[] PatrolPath;
    private bool PathEnable;
    // 순찰 경로를 바꿀때 사용
    private bool PathChangeOn;
    // 길 찾기를 끝냈는지의 여부
    private bool PathControlChange;
    // 길 찾기를 끝냈는지의 여부
    private bool PathFindEnd;

    private float AITimer;
    private Vector3 Target;

    // 적이 플레이어와 시선이 마주쳤는지 여부
    RaycastHit hit;
    private int layerMask;
    private int ChaseLayerMark;
    private int DeadEyeLayerMask;
    public GameObject[] SightPos;
    

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

        if (Enemy_Tranjectory_Object == null)
        {
            Debug.Log("Enemy_Tranjectory_Object Is Null !");
        }

        if (Enemy_DeadEyeMark_Object == null)
        {
            Debug.Log("Enemy_DeadEyeMark_Object is Null");
        }
        
        Enemy_DeadEyeMark_Object.SetActive(false);

        // 타일맵 길 찾기 관련 로직 초기화
        tileMap = FindObjectOfType(typeof(TileMap)) as TileMap;

        walkSpeed = 3.0f;
        PathEnable = true;
        PathChangeOn = true;
        PathControlChange = true;
        PathFindEnd = true;

        // 적의 상태 초기화
        HP = 100;
        ImDead = false;

        // 코루틴 연산 부분 초기화
        WalkPathcoroutine = null;
        WalkTocoroutine = null;
        PathFindUpdateRoutine = null;

        WalkPathcoroutine = WalkPath();
        WalkTocoroutine = WalkTo(Vector3.zero);
        PathFindUpdateRoutine = PathFindUpdateProtocol(true);

        Target = this.transform.position;
        AITimer = 0.0f;

        // 적의 시야 초기화

        layerMask = (1 << LayerMask.NameToLayer("Player") | (1 << LayerMask.NameToLayer("Wall") | (1 << LayerMask.NameToLayer("DeadEyeBox"))));
        ChaseLayerMark = 1 << LayerMask.NameToLayer("Player");
        DeadEyeLayerMask = (1 << LayerMask.NameToLayer("DeadEyeBox") | (1 << LayerMask.NameToLayer("DeadEyeWall")));
        //layerMask = (1 << 8);
        //layerMask = (-1) - ((1 << LayerMask.NameToLayer("Player")));




        // 적의 총알 관련 로직 초기화
        for(int i =0; i<EnemyBullets.Length; i++)
        {
            EnemyBullets[i].SetActive(false);
        }

        EnemyShootOn = true;
        EnemyBulletIndex = 0;
        EnemyShootCoolTime = 1.5f;

        //Debug.Log("Path Count : " + path.Count);
        State = GameManager.NowGameState;
        enemyState = EnemyState.NORMAL;
        enemyAiState = EnemyAIState.PATROL;
        enemyAniState = AnimationState.DOWNSTAND;


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

                                if (m_Player.GetDeadEyeShootOn() == true && ImDead == true)
                                {
                                    // 일단 죽는 모션이 없으므로...
                                    enemyAniState = AnimationState.DEADEYING;

                                    StopCoroutine(DeadEyeDeadProtocol(true));
                                    StartCoroutine(DeadEyeDeadProtocol(true));
                                }
                                else
                                {
                                    switch (enemyState)
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
                                                            //Debug.DrawRay(this.transform.position, ((this.transform.forward).normalized) * 10.0f, Color.green);
                                                            Debug.DrawRay(this.transform.position, (SightPos[0].transform.position - this.transform.position).normalized * 3.5f, Color.green);
                                                            Debug.DrawRay(this.transform.position, (SightPos[1].transform.position - this.transform.position).normalized * 3.5f, Color.green);
                                                            Debug.DrawRay(this.transform.position, (SightPos[2].transform.position - this.transform.position).normalized * 3.5f, Color.green);
                                                            Debug.DrawRay(this.transform.position, (SightPos[3].transform.position - this.transform.position).normalized * 3.5f, Color.green);
                                                            Debug.DrawRay(this.transform.position, (SightPos[4].transform.position - this.transform.position).normalized * 3.5f, Color.green);

                                                            if (Physics.Raycast(this.transform.position, ((m_Player.transform.position - this.transform.position).normalized), out hit, 3.5f, layerMask))
                                                            {
                                                                if (hit.collider.gameObject.tag.Equals("Player"))
                                                                {
                                                                    m_Player.SetPlayerState(2);
                                                                    enemyAiState = EnemyAIState.CHASE;
                                                                    enemyState = EnemyState.REALBATTLE;
                                                                }
                                                            }
                                                            else if (Physics.Raycast(this.transform.position, (SightPos[0].transform.position - this.transform.position).normalized, out hit, 3.5f, layerMask))
                                                            {
                                                                if (hit.collider.gameObject.tag.Equals("Player"))
                                                                {
                                                                    m_Player.SetPlayerState(2);
                                                                    enemyAiState = EnemyAIState.CHASE;
                                                                    enemyState = EnemyState.REALBATTLE;


                                                                    Debug.Log("PlayerHit!");
                                                                }
                                                            }
                                                            else if (Physics.Raycast(this.transform.position, (SightPos[1].transform.position - this.transform.position).normalized, out hit, 3.5f, layerMask))
                                                            {
                                                                if (hit.collider.gameObject.tag.Equals("Player"))
                                                                {
                                                                    m_Player.SetPlayerState(2);
                                                                    enemyAiState = EnemyAIState.CHASE;
                                                                    enemyState = EnemyState.REALBATTLE;


                                                                    Debug.Log("PlayerHit!");
                                                                }
                                                            }
                                                            else if (Physics.Raycast(this.transform.position, (SightPos[2].transform.position - this.transform.position).normalized, out hit, 3.5f, layerMask))
                                                            {
                                                                if (hit.collider.gameObject.tag.Equals("Player"))
                                                                {
                                                                    m_Player.SetPlayerState(2);
                                                                    enemyAiState = EnemyAIState.CHASE;
                                                                    enemyState = EnemyState.REALBATTLE;


                                                                    Debug.Log("PlayerHit!");
                                                                }
                                                            }
                                                            else if (Physics.Raycast(this.transform.position, (SightPos[3].transform.position - this.transform.position).normalized, out hit, 3.5f, layerMask))
                                                            {
                                                                if (hit.collider.gameObject.tag.Equals("Player"))
                                                                {
                                                                    m_Player.SetPlayerState(2);
                                                                    enemyAiState = EnemyAIState.CHASE;
                                                                    enemyState = EnemyState.REALBATTLE;


                                                                    Debug.Log("PlayerHit!");
                                                                }
                                                            }
                                                            else if (Physics.Raycast(this.transform.position, (SightPos[4].transform.position - this.transform.position).normalized, out hit, 3.5f, layerMask))
                                                            {
                                                                if (hit.collider.gameObject.tag.Equals("Player"))
                                                                {
                                                                    m_Player.SetPlayerState(2);
                                                                    enemyAiState = EnemyAIState.CHASE;
                                                                    enemyState = EnemyState.REALBATTLE;


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
                                                }

                                            }
                                            break;
                                    }
                                }
                                
                            }
                            break;

                        case PlayerState.DEADEYE:
                            {

                                // 데드 아이 상태가 되면 모든 행동을 멈춘다.
                                StopCoroutine(WalkPathcoroutine);
                                StopCoroutine(WalkTocoroutine);
                                StopCoroutine(ShootProtocol(true));

                                enemyAniState = AnimationState.DOWNSTAND;

                                Debug.DrawRay(this.transform.position, ((m_Player.GetDeadEyeObject().transform.position - this.transform.position).normalized) * 10.0f, Color.red);

                                if (Physics.Raycast(this.transform.position, ((m_Player.GetDeadEyeObject().transform.position - this.transform.position).normalized), out hit, Mathf.Infinity, DeadEyeLayerMask))
                                {

                                    if (hit.collider.gameObject.transform.tag.Equals("DeadEyeBox"))
                                    {
                                        Debug.Log("Enemy Deadeye Down! ");

                                        Enemy_DeadEyeMark_Object.transform.position = new Vector3(this.transform.position.x, Enemy_DeadEyeMark_Object.transform.position.y, this.transform.position.z);
                                        Enemy_DeadEyeMark_Object.SetActive(true);
                                        ImDead = true;

                                    }
                                }
                            }
                            break;

                        case PlayerState.REALBATTLE:
                            {
                                if (m_Player.GetDeadEyeShootOn() == true && ImDead == true)
                                {
                                    // 일단 죽는 모션이 없으므로...
                                    enemyAniState = AnimationState.DEADEYING;

                                    StopCoroutine(DeadEyeDeadProtocol(true));
                                    StartCoroutine(DeadEyeDeadProtocol(true));
                                }
                                else
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
                                                enemyAniState = AnimationState.DEADEYING;
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




                                                            Debug.DrawRay(this.transform.position, ((m_Player.transform.position - this.transform.position).normalized) * 10.0f, Color.green);

                                                            //Debug.Log("Player Tile Index : " + tileMap.GetIndex((int)m_Player.GetPlayerPosition().x, (int)m_Player.GetPlayerPosition().z));
                                                            //Debug.Log("Player Tile Position : " + tileMap.GetPoistion(tileMap.GetIndex((int)m_Player.GetPlayerPosition().x, (int)m_Player.GetPlayerPosition().z)));

                                                            //print("PathFindEnd : " + PathFindEnd);

                                                            enemyAniState = AnimationState.LEFTWALK;

                                                            if (AITimer <= 0.0f)
                                                            {
                                                                AITimer = 2.5f;

                                                                Target = tileMap.GetPoistion(tileMap.GetIndex((int)m_Player.GetPlayerPosition().x, (int)m_Player.GetPlayerPosition().z));

                                                                print("Target : " + Target);


                                                                if (tileMap.FindPath(this.transform.position, Target, path))
                                                                {

                                                                    print("Path.Count : " + path.Count);

                                                                    WalkPathcoroutine = WalkPath();
                                                                    StartCoroutine(WalkPathcoroutine);
                                                                    // StartCoroutine(WalkPathcoroutine);

                                                                }

                                                            }
                                                            else
                                                            {
                                                                AITimer -= Time.deltaTime;
                                                            }


                                                            if (Physics.Raycast(this.transform.position, ((m_Player.transform.position - this.transform.position).normalized), out hit, Mathf.Infinity, layerMask))
                                                            {


                                                                if (hit.collider.gameObject.transform.tag.Equals("Player"))
                                                                {
                                                                    print("Enemy Sight In");

                                                                    if ((m_Player.transform.position - this.transform.position).magnitude <= 4.5f)
                                                                    {


                                                                        //StopAllCoroutines();
                                                                        //StopCoroutine(WalkPath());

                                                                        StopCoroutine(WalkPathcoroutine);
                                                                        StopCoroutine(WalkTocoroutine);

                                                                        Shoot();
                                                                    }



                                                                }
                                                            }

                                                        }
                                                        break;
                                                }

                                            }
                                            break;
                                    }
                                }
                                
                            }
                            break;

                        case PlayerState.DEAD:
                            {
                                if (m_Player.GetDeadEyeShootOn() == true && ImDead == true)
                                {
                                    // 일단 죽는 모션이 없으므로...
                                    enemyAniState = AnimationState.DOWNSTAND;

                                    StopCoroutine(DeadEyeDeadProtocol(true));
                                    StartCoroutine(DeadEyeDeadProtocol(true));
                                }
                                else
                                {

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

    // 총을 발사 했을때..
    public void Shoot()
    {
        if (EnemyShootOn == true)
        {
            EnemyShootOn = false;

            StopCoroutine(ShootProtocol(true));
            StartCoroutine(ShootProtocol(true));
        }


    }

    // 적의 총알 발사 행동
    IEnumerator ShootProtocol(bool On = true)
    {
        

        Debug.Log("Bang!");

        // 총알 오브젝트 On
        if (EnemyBulletIndex < EnemyBullets.Length)
        {
            if (EnemyBullets[EnemyBulletIndex].gameObject.activeSelf == false)
            {
                EnemyBullets[EnemyBulletIndex].transform.position = new Vector3(this.transform.position.x, this.transform.position.y + 0.3f, this.transform.position.z);
                //EnemyBullets[NowBulletIndex].transform.LookAt(Player_Tranjectory_Object.transform.position);
                //EnemyBullets[EnemyBulletIndex].GetComponent<PietaEnemyBullet>().SetMoveDir(m_Player.transform.position);
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
                EnemyBullets[EnemyBulletIndex].transform.position = new Vector3(this.transform.position.x, this.transform.position.y + 0.3f, this.transform.position.z);
                //EnemyBullets[EnemyBulletIndex].transform.LookAt(Player_Tranjectory_Object.transform.position);
               // EnemyBullets[EnemyBulletIndex].GetComponent<PietaEnemyBullet>().SetMoveDir(m_Player.transform.position);
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

    // 길 찾기 갱신 로직
    IEnumerator PathFindUpdateProtocol(bool on = true)
    {
        PathFindEnd = false;

        yield return new WaitForSeconds(3.0f);

        PathFindEnd = true;
        //PathFindEnd = on;
    }

    // 사망 로직
    IEnumerator DeadProtocol(bool On = true)
    {
        

        yield return new WaitForSeconds(2.0f);

        enemyState = EnemyState.DEAD;
        enemyAiState = EnemyAIState.PATROL;

        if (this.gameObject.activeSelf == true)
        {
            if (GameManager.NowStageEnemies <= 0)
            {
                GameManager.NowStageEnemies = 0;
            }
            else
            {
                GameManager.NowStageEnemies -= 1;
            }

            Enemy_DeadEyeMark_Object.SetActive(false);
            this.gameObject.SetActive(false);
        }
        
    }

    IEnumerator DeadEyeDeadProtocol(bool On = true)
    {
        print("DeadEyeDeadProtocol Start");

        yield return new WaitForSeconds(0.8f);

        print("DeadEyeDeadProtocol End");

        enemyState = EnemyState.DEAD;
        enemyAiState = EnemyAIState.PATROL;
        
        if (this.gameObject.activeSelf == true)
        {
            if (GameManager.NowStageEnemies <= 0)
            {
                GameManager.NowStageEnemies = 0;
            }
            else
            {
                GameManager.NowStageEnemies -= 1;
            }

            Enemy_DeadEyeMark_Object.SetActive(false);
            this.gameObject.SetActive(false);
        }

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
        while (Vector3.Distance(transform.position, position) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, position, walkSpeed * Time.deltaTime);
            //transform.Translate(Vector3.MoveTowards(transform.position, position, walkSpeed * Time.deltaTime));
            this.transform.LookAt(position);
            yield return 0;
        }

        transform.position = position;

        //PathChangeOn ^= true;
        //PathControlChange = true;
        //PathFindEnd = true;
        //PathChangeEnable ^= true;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;

        for (int i = 0; i < path.Count; i++)
        {
            Gizmos.DrawSphere(path[i].transform.position, 0.05f);

            if (i > 0)
                Gizmos.DrawLine(path[i - 1].transform.position, path[i].transform.position);
        }
    }

    public Vector3 GetDirection()
    {
        if (Enemy_Tranjectory_Object != null)
        {
            return Vector3.zero;
        }
        else
        {
            return (Enemy_Tranjectory_Object.transform.position - this.transform.position).normalized;
        }
        
    }

    public Player GetPlayer()
    {
        return m_Player;
    }

    public EnemyState GetenemyState()
    {
        return enemyState;
    }

    public EnemyAIState GetenemyAiState()
    {
        return enemyAiState;
    }

    public AnimationState GetenemyAniState()
    {
        return enemyAniState;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag.Equals("PlayerBullet") == true)
        {
            // HP를 깍이게 한다.

            //this.gameObject.SetActive(false);

            if(HP <= 0)
            {
                HP = 0;

                //if(this.gameObject.activeSelf == true)
                //{
                //    if (GameManager.NowStageEnemies <= 0)
                //    {
                //        GameManager.NowStageEnemies = 0;
                //    }
                //    else
                //    {
                //        GameManager.NowStageEnemies -= 1;
                //    }

                //    this.gameObject.SetActive(false);
                //}
            }
            else
            {
                HP -= 50;
            }


        }
        
    }
}
