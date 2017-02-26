using UnityEngine;
using System.Collections;

public class ZombieEnemy : MonoBehaviour {

    private GameModeState ModeState;
    private GameState State;

    private AnimationState ZombieAniState;

    // 충돌체 정보
    private SphereCollider sp_Col;

    // 적의 움직임 여부
    private bool NowMoveOn;
    // 데미지 틱(Tick)
    private bool AttackChecker;
    // 데드 아이 체크
    private bool DeadEyeChecker;

    // 코루틴 정보들
    private IEnumerator DamegeCoroutine;
    private IEnumerator DeadCoroutine;
    private IEnumerator AttackCoroutine;

    public GameObject Zombie_Object;
    public GameObject Zombie_Navi;
    public GameObject Player_Ojbect;

    private Vector3 BackPos;

	// Use this for initialization
	void Start () {

        State = GameManager.NowGameState;
        ModeState = GameManager.NowGameModeState;

        if (sp_Col == null)
        {
            sp_Col = GetComponent<SphereCollider>();
            sp_Col.enabled = true;
        }
        else
        {
            sp_Col.enabled = true;
        }

        if (Player_Ojbect == null)
        {
            Player_Ojbect = GameObject.FindWithTag("Player");
        }

        if (Zombie_Navi != null)
        {
            Zombie_Navi.GetComponent<NavMeshAgent>().enabled = true;
            //Zombie_Navi.GetComponent<NavMeshAgent>().SetDestination(Player_Ojbect.transform.position);
        }

        //NaviChecker = false;

        // 애니메이션 방향
        if ((Zombie_Navi.transform.eulerAngles.y >= 0.0f) || (Zombie_Navi.transform.eulerAngles.y < 180.0f))
        {
            ZombieAniState = AnimationState.LEFTSTAND;
        }
        else if ((Zombie_Navi.transform.eulerAngles.y > 180.0f) || (Zombie_Navi.transform.eulerAngles.y <= 360.0f))
        {
            ZombieAniState = AnimationState.RIGHTSTAND;
        }

        Zombie_Object.SetActive(false);
        Zombie_Navi.SetActive(false);

        DeadCoroutine = null;
        DeadCoroutine = DeadProtocol(true);

        StopCoroutine(DeadCoroutine);

        DamegeCoroutine = null;
        DamegeCoroutine = DamegeProtocol(true);

        StopCoroutine(DamegeCoroutine);

        AttackCoroutine = null;
        AttackCoroutine = AttackProtocol(true);

        StopCoroutine(AttackCoroutine);

        DeadEyeChecker = false;
        AttackChecker = false;
        NowMoveOn = true;

        BackPos = Vector3.zero;
        BackPos = Zombie_Navi.transform.position;

        this.gameObject.SetActive(false);
	}
    
    void OnEnable()
    {
        State = GameManager.NowGameState;
        ModeState = GameManager.NowGameModeState;


        if (sp_Col == null)
        {
            sp_Col = GetComponent<SphereCollider>();
            sp_Col.enabled = true;
        }
        else
        {
            sp_Col.enabled = true;
        }

        if (Player_Ojbect == null)
        {
            Player_Ojbect = GameObject.FindWithTag("Player");
        }

        if (Zombie_Navi != null)
        {
            Zombie_Navi.GetComponent<NavMeshAgent>().enabled = true;
            //Zombie_Navi.GetComponent<NavMeshAgent>().SetDestination(Player_Ojbect.transform.position);
        }


        // 애니메이션 방향
        if ((Zombie_Navi.transform.eulerAngles.y >= 0.0f) || (Zombie_Navi.transform.eulerAngles.y < 180.0f))
        {
            ZombieAniState = AnimationState.LEFTSTAND;
        }
        else if ((Zombie_Navi.transform.eulerAngles.y > 180.0f) || (Zombie_Navi.transform.eulerAngles.y <= 360.0f))
        {
            ZombieAniState = AnimationState.RIGHTSTAND;
        }

        Zombie_Object.SetActive(true);
        Zombie_Navi.SetActive(true);

        DeadCoroutine = null;
        DeadCoroutine = DeadProtocol(true);

        StopCoroutine(DeadCoroutine);

        DamegeCoroutine = null;
        DamegeCoroutine = DamegeProtocol(true);

        StopCoroutine(DamegeCoroutine);

        AttackCoroutine = null;
        AttackCoroutine = AttackProtocol(true);

        StopCoroutine(AttackCoroutine);

        DeadEyeChecker = false;
        AttackChecker = false;
        NowMoveOn = true;


        Zombie_Navi.transform.position = BackPos;
    }

    IEnumerator DeadProtocol(bool on = true)
    {

        sp_Col.enabled = false;

        if (Zombie_Navi != null)
        {
            Zombie_Navi.GetComponent<NavMeshAgent>().enabled = false;
            //Zombie_Navi.GetComponent<NavMeshAgent>().SetDestination(Player_Ojbect.transform.position);
        }


        if ((Zombie_Navi.transform.eulerAngles.y >= 0.0f) || (Zombie_Navi.transform.eulerAngles.y < 180.0f))
        {
            ZombieAniState = AnimationState.LEFTDEAD;
        }
        else if ((Zombie_Navi.transform.eulerAngles.y > 180.0f) || (Zombie_Navi.transform.eulerAngles.y <= 360.0f))
        {
            ZombieAniState = AnimationState.RIGHTDEAD;
        }

        yield return new WaitForSeconds(0.5f);

        GameManager.EnemyKillCount += 1;
        GameManager.MiniGame_KillCount += 1;


        Zombie_Navi.SetActive(false);
        Zombie_Object.SetActive(false);
        this.gameObject.SetActive(false);
    }

    IEnumerator AttackProtocol(bool on = true)
    {
        AttackChecker = true;
        
        yield return new WaitForSeconds(0.5f);
        
        AttackChecker = false;
    }

    IEnumerator DamegeProtocol(bool on = true)
    {


        sp_Col.enabled = false;

        // 애니메이션 방향
        if ((Zombie_Navi.transform.eulerAngles.y >= 0.0f) || (Zombie_Navi.transform.eulerAngles.y < 180.0f))
        {
            ZombieAniState = AnimationState.LEFTDEAD;
        }
        else if ((Zombie_Navi.transform.eulerAngles.y > 180.0f) || (Zombie_Navi.transform.eulerAngles.y <= 360.0f))
        {
            ZombieAniState = AnimationState.RIGHTDEAD;
        }

        if (Zombie_Navi != null)
        {
            Zombie_Navi.GetComponent<NavMeshAgent>().enabled = false;
            //Zombie_Navi.GetComponent<NavMeshAgent>().SetDestination(Player_Ojbect.transform.position);
        }

        Zombie_Object.GetComponent<MeshRenderer>().material.color = Color.red;
        
        yield return new WaitForSeconds(0.5f);

        GameManager.EnemyKillCount += 1;
        GameManager.MiniGame_KillCount += 1;


        Zombie_Object.GetComponent<MeshRenderer>().material.color = Color.white;
        Zombie_Object.SetActive(false);
        Zombie_Navi.SetActive(false);
        this.gameObject.SetActive(false);

    }

	// Update is called once per frame
	void Update () {

        ModeState = GameManager.NowGameModeState;

        switch(ModeState)
        {
            case GameModeState.Single:
                {
                    State = GameManager.NowGameState;

                    // 위치 갱신
                    this.gameObject.transform.position = Zombie_Navi.transform.position;

                    switch (State)
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
                    State = GameManager.NowGameState;

                    // 위치 갱신
                    this.gameObject.transform.position = Zombie_Navi.transform.position;
                    //Zombie_Object.transform.position = this.gameObject.transform.position;

                    switch (State)
                    {
                        case GameState.START:
                            {
                                // 네비게이션 작동 여부
                                if (Zombie_Navi != null)
                                {
                                    Zombie_Navi.GetComponent<NavMeshAgent>().enabled = false;
                                    //Zombie_Navi.GetComponent<NavMeshAgent>().SetDestination(Player_Ojbect.transform.position);
                                }
                            }
                            break;

                        case GameState.PLAY:
                            {
                                if(GameManager.DeadEyeActiveOn == true)
                                {
                                    // 네비게이션 작동 여부
                                    if (Zombie_Navi != null)
                                    {
                                        Zombie_Navi.GetComponent<NavMeshAgent>().enabled = false;
                                        //Zombie_Navi.GetComponent<NavMeshAgent>().SetDestination(Player_Ojbect.transform.position);
                                    }

                                    // 애니메이션 방향
                                    if ((Zombie_Navi.transform.eulerAngles.y >= 0.0f) || (Zombie_Navi.transform.eulerAngles.y < 180.0f))
                                    {
                                        ZombieAniState = AnimationState.LEFTSTAND;
                                    }
                                    else if ((Zombie_Navi.transform.eulerAngles.y > 180.0f) || (Zombie_Navi.transform.eulerAngles.y <= 360.0f))
                                    {
                                        ZombieAniState = AnimationState.RIGHTSTAND;
                                    }

                                    if(GameManager.DeadEyeFailOn == false)
                                    {
                                        // 데드 아이 일시 플레이어와의 거리를 체크해 없애줄지 없애지 않을지 체크한다.
                                        if ((Player_Ojbect.transform.position - this.transform.position).magnitude < 10.0f)
                                        {

                                            NowMoveOn = false;
                                            DeadEyeChecker = true;

                                        }
                                    }


                                }
                                else
                                {
                                    if (DeadEyeChecker == true)
                                    {
                                        DeadEyeChecker = false;

                                        // 애니메이션 방향
                                        if ((Zombie_Navi.transform.eulerAngles.y >= 0.0f) || (Zombie_Navi.transform.eulerAngles.y < 180.0f))
                                        {
                                            ZombieAniState = AnimationState.LEFTSTAND;
                                        }
                                        else if ((Zombie_Navi.transform.eulerAngles.y > 180.0f) || (Zombie_Navi.transform.eulerAngles.y <= 360.0f))
                                        {
                                            ZombieAniState = AnimationState.RIGHTSTAND;
                                        }

                                        print("zombie HighNoon..............................");

                                        DeadCoroutine = null;
                                        DeadCoroutine = DeadProtocol(true);

                                        StopCoroutine(DeadCoroutine);
                                        StartCoroutine(DeadCoroutine);
                                    }
                                    else
                                    {


                                        if (NowMoveOn == true)
                                        {
                                            // 네비게이션 작동 여부
                                            if (Zombie_Navi != null)
                                            {
                                                if (this.gameObject.activeSelf == true)
                                                {
                                                    Zombie_Navi.GetComponent<NavMeshAgent>().enabled = true;
                                                    Zombie_Navi.GetComponent<NavMeshAgent>().SetDestination(Player_Ojbect.transform.position);
                                                }

                                                //Zombie_Navi.GetComponent<NavMeshAgent>().SetDestination(Player_Ojbect.transform.position);
                                            }

                                            if ((Zombie_Navi.transform.eulerAngles.y >= 0.0f) || (Zombie_Navi.transform.eulerAngles.y < 180.0f))
                                            {
                                                ZombieAniState = AnimationState.LEFTWALK;
                                            }
                                            else if ((Zombie_Navi.transform.eulerAngles.y > 180.0f) || (Zombie_Navi.transform.eulerAngles.y <= 360.0f))
                                            {
                                                ZombieAniState = AnimationState.RIGHTWALK;
                                            }
                                        }
                                        else
                                        {
                                            // 네비게이션 작동 여부
                                            if (Zombie_Navi != null)
                                            {
                                                if (this.gameObject.activeSelf == true)
                                                {
                                                    Zombie_Navi.GetComponent<NavMeshAgent>().enabled = false;
                                                    //Zombie_Navi.GetComponent<NavMeshAgent>().SetDestination(Player_Ojbect.transform.position);
                                                }

                                                //Zombie_Navi.GetComponent<NavMeshAgent>().SetDestination(Player_Ojbect.transform.position);
                                            }

                                            if ((Zombie_Navi.transform.eulerAngles.y >= 0.0f) || (Zombie_Navi.transform.eulerAngles.y < 180.0f))
                                            {
                                                ZombieAniState = AnimationState.LEFTSTAND;
                                            }
                                            else if ((Zombie_Navi.transform.eulerAngles.y > 180.0f) || (Zombie_Navi.transform.eulerAngles.y <= 360.0f))
                                            {
                                                ZombieAniState = AnimationState.RIGHTSTAND;
                                            }
                                        }
                                    }
                                 
                                }

                            }
                            break;

                        case GameState.PAUSE:
                            {
                                // 네비게이션 작동 여부
                                if (Zombie_Navi != null)
                                {
                                    Zombie_Navi.GetComponent<NavMeshAgent>().enabled = false;
                                    //Zombie_Navi.GetComponent<NavMeshAgent>().SetDestination(Player_Ojbect.transform.position);
                                }
                            }
                            break;

                        case GameState.EVENT:
                            {
                                // 네비게이션 작동 여부
                                if (Zombie_Navi != null)
                                {
                                    Zombie_Navi.GetComponent<NavMeshAgent>().enabled = false;
                                    //Zombie_Navi.GetComponent<NavMeshAgent>().SetDestination(Player_Ojbect.transform.position);
                                }
                            }
                            break;

                        case GameState.VICTORY:
                            {
                                // 네비게이션 작동 여부
                                if (Zombie_Navi != null)
                                {
                                    Zombie_Navi.GetComponent<NavMeshAgent>().enabled = false;
                                    //Zombie_Navi.GetComponent<NavMeshAgent>().SetDestination(Player_Ojbect.transform.position);
                                }
                            }
                            break;

                        case GameState.GAMEOVER:
                            {
                                // 네비게이션 작동 여부
                                if (Zombie_Navi != null)
                                {
                                    Zombie_Navi.GetComponent<NavMeshAgent>().enabled = false;
                                    //Zombie_Navi.GetComponent<NavMeshAgent>().SetDestination(Player_Ojbect.transform.position);
                                }
                            }
                            break;
                    }
                }
                break;

            case GameModeState.Multi:
                {
                    State = GameManager.NowGameState;

                    // 위치 갱신
                    this.gameObject.transform.position = Zombie_Navi.transform.position;

                    switch (State)
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

            case GameModeState.NotSelect:
                {
                    State = GameManager.NowGameState;

                    // 위치 갱신
                    this.gameObject.transform.position = Zombie_Navi.transform.position;

                    switch (State)
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
        }

	}

    public AnimationState GetAniState()
    {
        return ZombieAniState;
    }

    //void OnCollisionEnter(Collision collision)
    //{

    //}

    void OnTriggerEnter(Collider other)
    {


        if (other.transform.tag.Equals("PlayerBullet") == true)
        {
            print("Zombie Dead");

            NowMoveOn = false;

            // 사망 처리
            DamegeCoroutine = null;
            DamegeCoroutine = DamegeProtocol(true);

            StopCoroutine(DamegeCoroutine);
            StartCoroutine(DamegeCoroutine);
        }
        
        //if (other.transform.tag.Equals("Player") == true)
        //{
        //    // 데미지 처리
        //    if(AttackChecker == false)
        //    {
        //        AttackChecker = true;

        //        AttackCoroutine = null;
        //        AttackCoroutine = AttackProtocol(true);

                

        //        StopCoroutine(AttackCoroutine);
        //        StartCoroutine(AttackCoroutine);
        //    }
        //}
    }

    void OnTriggerStay(Collider other)
    {
        if (other.transform.tag.Equals("Player") == true)
        {
            // 데미지 처리
            switch(State)
            {
                case GameState.START:
                    {

                    }
                    break;

                case GameState.PLAY:
                    {
                        if (AttackChecker == false)
                        {
                            AttackChecker = true;


                            AttackCoroutine = null;
                            AttackCoroutine = AttackProtocol(true);

                            Player_Ojbect.GetComponent<Player>().DamegeToPlayer(2);

                            StopCoroutine(AttackCoroutine);
                            StartCoroutine(AttackCoroutine);


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
        
    }
}
