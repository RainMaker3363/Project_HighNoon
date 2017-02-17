using UnityEngine;
using System.Collections;



public class Player : MonoBehaviour {

    private GameState State;
    private GameControlState ControlState;
    
    public JoyStickCtrl JoyStickControl;

    [HideInInspector]
    public PlayerState playerState;
    private PlayerState PrevPlayerState;
    private AnimationState PlayerAniState;
    
    // 플레이어의 상태 값
    private float HP;
    private GameObject DeadEyeBox;
    private bool DeadEyeActive;
    private bool DeadEyeReady;
    private bool DeadEyeShootOn;

    // 플레이어의 움직임
    private float MoveSpeed;
    private Vector3 MoveVector;
    private Vector3 PlayerDir;
    private Vector3 PlayerDeadEyePosBack;
    private Vector3 PlayerPosBack;
    private Vector3 CameraPosBack;

    // 현재 컨트롤러가 플레이어를 조종할 수 있는지의 여부
    private bool NowMovePlayer;

    // 플레이어가 총을 쏠 수 있을지의 여부
    private bool ShootOn;
    private bool ReloadSuccessOn;
    private float ShootCoolTime;
    private int BulletQuantity;
    private int BulletStack;
    private int NowBulletIndex;

    // 플레이어와 연관된 오브젝트들
    public GameObject[] Bullets;
    public PlayerAni m_PlayerAni;
    private bool ShootAniOn;

    // 카메라
    public GameObject CameraObject;
    private GameObject MainCamera;
    private bool CameraMoveOn;
    private RaycastHit Wallhit;
    private int WalllayerMask;

    // 플레이어의 사선
    private Vector3[] Line_Tranjectory_Transform;
    public GameObject Player_Tranjectory_Object;
    private LineRenderer Player_Tranjectory;

    // 플레이어의 타일을 계산
    private RaycastHit hit;
    private int layerMask;
    private Vector3 PlayerPos;

    // 플레이어의 코루틴 연산들
    private IEnumerator DeadEyeProtocol;

	// Use this for initialization
	void Start () {
	
        State = GameManager.NowGameState;
        ControlState = GameManager.NowGameControlState;
        PlayerAniState = AnimationState.DOWNSTAND;
        
        if(JoyStickControl == null)
        {
            JoyStickControl = GameObject.Find("Joystick_Pad").GetComponent<JoyStickCtrl>();
        }

        if(CameraObject == null)
        {
            CameraObject = GameObject.Find("Camera");
        }

        if (MainCamera == null)
        {
            MainCamera = GameObject.FindWithTag("MainCamera");
        }
        
        CameraPosBack = Vector3.zero;

        if (Player_Tranjectory == null)
        {
            Player_Tranjectory = GetComponent<LineRenderer>();
            Player_Tranjectory.enabled = false;

            Line_Tranjectory_Transform = new Vector3[2];
            Line_Tranjectory_Transform[0] = this.gameObject.transform.position - new Vector3(0.0f, 0.2f, 0.0f);
            Line_Tranjectory_Transform[1] = Player_Tranjectory_Object.transform.position;
            Player_Tranjectory.SetPositions(Line_Tranjectory_Transform);
        }
        else
        {
            Player_Tranjectory.enabled = false;

            Line_Tranjectory_Transform = new Vector3[2];
            Line_Tranjectory_Transform[0] = this.gameObject.transform.position - new Vector3(0.0f, 0.2f, 0.0f);
            Line_Tranjectory_Transform[1] = Player_Tranjectory_Object.transform.position;
            Player_Tranjectory.SetPositions(Line_Tranjectory_Transform);
        }

        if (DeadEyeBox == null)
        {
            DeadEyeBox = GameObject.FindWithTag("DeadEyeBox");
        }


        // 플레이어의 상태 값
        HP = 100.0f;
        playerState = PlayerState.NORMAL;
        //PlayerAniState = AnimationState.DOWNSTAND;

        // 방향 계산
        if (ShootOn == true)
        {
            if ((transform.rotation.eulerAngles.y > 340.0f && transform.rotation.eulerAngles.y <= 0.0f)
                || (transform.rotation.eulerAngles.y > 0.0f && transform.rotation.eulerAngles.y <= 25.0f))
            {
                if (NowMovePlayer == false)
                {
                    PlayerAniState = AnimationState.UPSTAND;
                }
                else
                {
                    PlayerAniState = AnimationState.UPWALK;
                }
            }
            else if (transform.rotation.eulerAngles.y > 25.0f && transform.rotation.eulerAngles.y <= 70.0f)
            {
                if (NowMovePlayer == false)
                {
                    PlayerAniState = AnimationState.RIGHTUPSTAND;
                }
                else
                {
                    PlayerAniState = AnimationState.RIGHTUPWALK;
                }
            }
            else if (transform.rotation.eulerAngles.y > 70.0f && transform.rotation.eulerAngles.y <= 110.0f)
            {
                if (NowMovePlayer == false)
                {
                    PlayerAniState = AnimationState.RIGHTSTAND;
                }
                else
                {
                    PlayerAniState = AnimationState.RIGHTWALK;
                }
            }
            else if (transform.rotation.eulerAngles.y > 110.0f && transform.rotation.eulerAngles.y <= 155.0f)
            {
                if (NowMovePlayer == false)
                {
                    PlayerAniState = AnimationState.RIGHTDOWNSTAND;
                }
                else
                {
                    PlayerAniState = AnimationState.RIGHTDOWNWALK;
                }
            }
            else if (transform.rotation.eulerAngles.y > 155.0f && transform.rotation.eulerAngles.y <= 200.0f)
            {
                if (NowMovePlayer == false)
                {
                    PlayerAniState = AnimationState.DOWNSTAND;
                }
                else
                {
                    PlayerAniState = AnimationState.DOWNWALK;
                }
            }
            else if (transform.rotation.eulerAngles.y > 200.0f && transform.rotation.eulerAngles.y <= 245.0f)
            {
                if (NowMovePlayer == false)
                {
                    PlayerAniState = AnimationState.LEFTDOWNSTAND;
                }
                else
                {
                    PlayerAniState = AnimationState.LEFTDOWNWALK;
                }
            }
            else if (transform.rotation.eulerAngles.y > 245.0f && transform.rotation.eulerAngles.y <= 290.0f)
            {
                if (NowMovePlayer == false)
                {
                    PlayerAniState = AnimationState.LEFTSTAND;
                }
                else
                {
                    PlayerAniState = AnimationState.LEFTWALK;
                }
            }
            else if (transform.rotation.eulerAngles.y > 290.0f && transform.rotation.eulerAngles.y <= 340.0f)
            {
                if (NowMovePlayer == false)
                {
                    PlayerAniState = AnimationState.LEFTUPSTAND;
                }
                else
                {
                    PlayerAniState = AnimationState.LEFTUPWALK;
                }
            }
        }
        else
        {
            // 사격 애니메이션 출력

            if ((transform.rotation.eulerAngles.y > 340.0f && transform.rotation.eulerAngles.y <= 0.0f)
               || (transform.rotation.eulerAngles.y > 0.0f && transform.rotation.eulerAngles.y <= 25.0f))
            {
                PlayerAniState = AnimationState.UPSHOOT;
            }
            else if (transform.rotation.eulerAngles.y > 25.0f && transform.rotation.eulerAngles.y <= 70.0f)
            {

                PlayerAniState = AnimationState.RIGHTUPSHOOT;
            }
            else if (transform.rotation.eulerAngles.y > 70.0f && transform.rotation.eulerAngles.y <= 110.0f)
            {

                PlayerAniState = AnimationState.RIGHTSHOOT;
            }
            else if (transform.rotation.eulerAngles.y > 110.0f && transform.rotation.eulerAngles.y <= 155.0f)
            {

                PlayerAniState = AnimationState.RIGHTDOWNSHOOT;
            }
            else if (transform.rotation.eulerAngles.y > 155.0f && transform.rotation.eulerAngles.y <= 200.0f)
            {

                PlayerAniState = AnimationState.DOWNSHOOT;
            }
            else if (transform.rotation.eulerAngles.y > 200.0f && transform.rotation.eulerAngles.y <= 245.0f)
            {

                PlayerAniState = AnimationState.LEFTDOWNSHOOT;
            }
            else if (transform.rotation.eulerAngles.y > 245.0f && transform.rotation.eulerAngles.y <= 290.0f)
            {


                PlayerAniState = AnimationState.LEFTSHOOT;
            }
            else if (transform.rotation.eulerAngles.y > 290.0f && transform.rotation.eulerAngles.y <= 340.0f)
            {

                PlayerAniState = AnimationState.LEFTUPSHOOT;
            }
        }

        DeadEyeActive = false;
        DeadEyeReady = false;
        DeadEyeShootOn = false;

        // 플레이어 이동 방향 초기화
        MoveVector = Vector3.zero;
        MoveSpeed = 2.0f;
        PlayerDir = Vector3.zero;
        PlayerPosBack = Vector3.zero;
        PlayerDeadEyePosBack = Vector3.zero;

        // 플레이어가 사격할 수 있는지의 여부
        ShootOn = true;
        ReloadSuccessOn = true;
        ShootCoolTime = 1.0f;
        BulletStack = 6;

        // 총알의 수
        BulletQuantity = 1000;

        NowMovePlayer = false;
        CameraMoveOn = true;

        // 총알 오브젝트의 출력 순서를 제어하는 변수
        NowBulletIndex = 0;

        // 총알 오브젝트 설정
        for(int i = 0; i<Bullets.Length; i++)
        {
            Bullets[i].gameObject.SetActive(false);
        }

        if (m_PlayerAni == null)
        {
            Debug.Log("PlayerAni Object is Null ");
        }

        ShootAniOn = false;

        // 코루틴 설정
        DeadEyeProtocol = null;

        DeadEyeProtocol = DeadEyeShootProtocol(true);

        PlayerPos = Vector3.zero;
        layerMask = (1 << LayerMask.NameToLayer("Ground"));
        WalllayerMask = ((1 << LayerMask.NameToLayer("Wall")) | (1 << LayerMask.NameToLayer("DeadEyeBox")));
	}

    // Update is called once per frame
    void Update()
    {
        State = GameManager.NowGameState;
        ShootAniOn = m_PlayerAni.GetShootAniOn();
        MoveVector = PoolInput();

        if (MoveVector == Vector3.zero)
        {
            if(ShootOn == true)
            {
                transform.rotation = Quaternion.LookRotation(PlayerDir);
                NowMovePlayer = false;
            }

        }
        else
        {
            if (ShootOn == true)
            {
                PlayerDir = MoveVector;
                transform.rotation = Quaternion.LookRotation(PlayerDir);
                NowMovePlayer = true;
            }

        }



        //print("transform.rotation.y : " + transform.rotation.eulerAngles);

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

                                Player_Tranjectory.enabled = true;

                                //if (NowMovePlayer)
                                //{
                                //    if (CameraMoveOn)
                                //    {
                                //        //this.transform.Translate((Vector3.forward * MoveSpeed) * Time.deltaTime);
                                //        this.transform.Translate((new Vector3(0.0f, 0.0f, MoveVector.magnitude) * MoveSpeed) * Time.deltaTime);
                                //        CameraObject.transform.Translate((MoveVector * MoveSpeed) * Time.deltaTime);
                                //    }
                                //}

                                if (NowMovePlayer)
                                {
                                    if ((CameraMoveOn == true) && (ShootOn == true))
                                    {
                                        //this.transform.Translate((Vector3.forward * MoveSpeed) * Time.deltaTime);
                                        this.transform.Translate((new Vector3(0.0f, 0.0f, MoveVector.magnitude) * MoveSpeed) * Time.deltaTime);
                                        CameraObject.transform.Translate((MoveVector * MoveSpeed) * Time.deltaTime);
                                    }
                                }

                                Line_Tranjectory_Transform[0] = this.gameObject.transform.position - new Vector3(0.0f, 0.2f, 0.0f);
                                Line_Tranjectory_Transform[1] = Player_Tranjectory_Object.transform.position;
                                Player_Tranjectory.SetPositions(Line_Tranjectory_Transform);

                                // 방향 계산
                                if (ShootOn == true)
                                {
                                    if ((transform.rotation.eulerAngles.y > 340.0f && transform.rotation.eulerAngles.y <= 0.0f)
                                        || (transform.rotation.eulerAngles.y > 0.0f && transform.rotation.eulerAngles.y <= 25.0f))
                                    {
                                        if (NowMovePlayer == false)
                                        {
                                            PlayerAniState = AnimationState.UPSTAND;
                                        }
                                        else
                                        {
                                            PlayerAniState = AnimationState.UPWALK;
                                        }
                                    }
                                    else if (transform.rotation.eulerAngles.y > 25.0f && transform.rotation.eulerAngles.y <= 70.0f)
                                    {
                                        if (NowMovePlayer == false)
                                        {
                                            PlayerAniState = AnimationState.RIGHTUPSTAND;
                                        }
                                        else
                                        {
                                            PlayerAniState = AnimationState.RIGHTUPWALK;
                                        }
                                    }
                                    else if (transform.rotation.eulerAngles.y > 70.0f && transform.rotation.eulerAngles.y <= 110.0f)
                                    {
                                        if (NowMovePlayer == false)
                                        {
                                            PlayerAniState = AnimationState.RIGHTSTAND;
                                        }
                                        else
                                        {
                                            PlayerAniState = AnimationState.RIGHTWALK;
                                        }
                                    }
                                    else if (transform.rotation.eulerAngles.y > 110.0f && transform.rotation.eulerAngles.y <= 155.0f)
                                    {
                                        if (NowMovePlayer == false)
                                        {
                                            PlayerAniState = AnimationState.RIGHTDOWNSTAND;
                                        }
                                        else
                                        {
                                            PlayerAniState = AnimationState.RIGHTDOWNWALK;
                                        }
                                    }
                                    else if (transform.rotation.eulerAngles.y > 155.0f && transform.rotation.eulerAngles.y <= 200.0f)
                                    {
                                        if (NowMovePlayer == false)
                                        {
                                            PlayerAniState = AnimationState.DOWNSTAND;
                                        }
                                        else
                                        {
                                            PlayerAniState = AnimationState.DOWNWALK;
                                        }
                                    }
                                    else if (transform.rotation.eulerAngles.y > 200.0f && transform.rotation.eulerAngles.y <= 245.0f)
                                    {
                                        if (NowMovePlayer == false)
                                        {
                                            PlayerAniState = AnimationState.LEFTDOWNSTAND;
                                        }
                                        else
                                        {
                                            PlayerAniState = AnimationState.LEFTDOWNWALK;
                                        }
                                    }
                                    else if (transform.rotation.eulerAngles.y > 245.0f && transform.rotation.eulerAngles.y <= 290.0f)
                                    {
                                        if (NowMovePlayer == false)
                                        {
                                            PlayerAniState = AnimationState.LEFTSTAND;
                                        }
                                        else
                                        {
                                            PlayerAniState = AnimationState.LEFTWALK;
                                        }
                                    }
                                    else if (transform.rotation.eulerAngles.y > 290.0f && transform.rotation.eulerAngles.y <= 340.0f)
                                    {
                                        if (NowMovePlayer == false)
                                        {
                                            PlayerAniState = AnimationState.LEFTUPSTAND;
                                        }
                                        else
                                        {
                                            PlayerAniState = AnimationState.LEFTUPWALK;
                                        }
                                    }
                                }
                                else
                                {
                                    // 사격 애니메이션 출력

                                    if ((transform.rotation.eulerAngles.y > 340.0f && transform.rotation.eulerAngles.y <= 0.0f)
                                       || (transform.rotation.eulerAngles.y > 0.0f && transform.rotation.eulerAngles.y <= 25.0f))
                                    {
                                        PlayerAniState = AnimationState.UPSHOOT;
                                    }
                                    else if (transform.rotation.eulerAngles.y > 25.0f && transform.rotation.eulerAngles.y <= 70.0f)
                                    {

                                        PlayerAniState = AnimationState.RIGHTUPSHOOT;
                                    }
                                    else if (transform.rotation.eulerAngles.y > 70.0f && transform.rotation.eulerAngles.y <= 110.0f)
                                    {

                                        PlayerAniState = AnimationState.RIGHTSHOOT;
                                    }
                                    else if (transform.rotation.eulerAngles.y > 110.0f && transform.rotation.eulerAngles.y <= 155.0f)
                                    {

                                        PlayerAniState = AnimationState.RIGHTDOWNSHOOT;
                                    }
                                    else if (transform.rotation.eulerAngles.y > 155.0f && transform.rotation.eulerAngles.y <= 200.0f)
                                    {

                                        PlayerAniState = AnimationState.DOWNSHOOT;
                                    }
                                    else if (transform.rotation.eulerAngles.y > 200.0f && transform.rotation.eulerAngles.y <= 245.0f)
                                    {

                                        PlayerAniState = AnimationState.LEFTDOWNSHOOT;
                                    }
                                    else if (transform.rotation.eulerAngles.y > 245.0f && transform.rotation.eulerAngles.y <= 290.0f)
                                    {


                                        PlayerAniState = AnimationState.LEFTSHOOT;
                                    }
                                    else if (transform.rotation.eulerAngles.y > 290.0f && transform.rotation.eulerAngles.y <= 340.0f)
                                    {

                                        PlayerAniState = AnimationState.LEFTUPSHOOT;
                                    }
                                }

                                Debug.DrawRay(this.transform.position, (Vector3.down) * 50.0f, Color.red);
                                Debug.DrawRay(this.transform.position, (Player_Tranjectory_Object.transform.position - this.transform.position).normalized * 0.6f, Color.yellow);

                                if (Physics.Raycast(this.transform.position, (Vector3.down), out hit, Mathf.Infinity, layerMask))
                                {
                                    //print("hit Name : " + hit.collider.name);
                                    //print("hit Point : " + hit.point);

                                    PlayerPos = hit.point;
                                    PlayerPosBack = PlayerPos;
                                }
                                else
                                {
                                    this.transform.position = new Vector3(PlayerPosBack.x, this.transform.position.y, PlayerPosBack.z);
                                }

                                if (Physics.Raycast(this.transform.position, (Player_Tranjectory_Object.transform.position - this.transform.position).normalized * 1.0f, out Wallhit, 0.3f, WalllayerMask))
                                {
                                    if (Wallhit.collider.transform.tag.Equals("Wall") == true)
                                    {
                                        CameraMoveOn = false;
                                    }

                                    if (Wallhit.collider.transform.tag.Equals("DeadEyeBox") == true)
                                    {
                                        CameraMoveOn = false;

                                        Debug.Log("DeadEye Ready");

                                        DeadEyeReady = true;
                                    }
                                    else
                                    {
                                        Debug.Log("DeadEye Not Ready");

                                        DeadEyeReady = false;
                                    }
                                }
                                else
                                {
                                    CameraMoveOn = true;

                                    DeadEyeReady = false;
                                }
                            }
                            break;

                        case PlayerState.REALBATTLE:
                            {
                                Player_Tranjectory.enabled = true;

                                if (NowMovePlayer)
                                {
                                    if (CameraMoveOn && (ShootOn == true))
                                    {
                                        //this.transform.Translate((Vector3.forward * MoveSpeed) * Time.deltaTime);
                                        this.transform.Translate((new Vector3(0.0f, 0.0f, MoveVector.magnitude) * MoveSpeed) * Time.deltaTime);
                                        CameraObject.transform.Translate((MoveVector * MoveSpeed) * Time.deltaTime);
                                    }
                                }


                                Line_Tranjectory_Transform[0] = this.gameObject.transform.position - new Vector3(0.0f, 0.2f, 0.0f);
                                Line_Tranjectory_Transform[1] = Player_Tranjectory_Object.transform.position;
                                Player_Tranjectory.SetPositions(Line_Tranjectory_Transform);

                                // 방향 계산
                                if (ShootOn == true)
                                {
                                    if ((transform.rotation.eulerAngles.y > 340.0f && transform.rotation.eulerAngles.y <= 0.0f)
                                        || (transform.rotation.eulerAngles.y > 0.0f && transform.rotation.eulerAngles.y <= 25.0f))
                                    {
                                        if (NowMovePlayer == false)
                                        {
                                            PlayerAniState = AnimationState.UPSTAND;
                                        }
                                        else
                                        {
                                            PlayerAniState = AnimationState.UPWALK;
                                        }
                                    }
                                    else if (transform.rotation.eulerAngles.y > 25.0f && transform.rotation.eulerAngles.y <= 70.0f)
                                    {
                                        if (NowMovePlayer == false)
                                        {
                                            PlayerAniState = AnimationState.RIGHTUPSTAND;
                                        }
                                        else
                                        {
                                            PlayerAniState = AnimationState.RIGHTUPWALK;
                                        }
                                    }
                                    else if (transform.rotation.eulerAngles.y > 70.0f && transform.rotation.eulerAngles.y <= 110.0f)
                                    {
                                        if (NowMovePlayer == false)
                                        {
                                            PlayerAniState = AnimationState.RIGHTSTAND;
                                        }
                                        else
                                        {
                                            PlayerAniState = AnimationState.RIGHTWALK;
                                        }
                                    }
                                    else if (transform.rotation.eulerAngles.y > 110.0f && transform.rotation.eulerAngles.y <= 155.0f)
                                    {
                                        if (NowMovePlayer == false)
                                        {
                                            PlayerAniState = AnimationState.RIGHTDOWNSTAND;
                                        }
                                        else
                                        {
                                            PlayerAniState = AnimationState.RIGHTDOWNWALK;
                                        }
                                    }
                                    else if (transform.rotation.eulerAngles.y > 155.0f && transform.rotation.eulerAngles.y <= 200.0f)
                                    {
                                        if (NowMovePlayer == false)
                                        {
                                            PlayerAniState = AnimationState.DOWNSTAND;
                                        }
                                        else
                                        {
                                            PlayerAniState = AnimationState.DOWNWALK;
                                        }
                                    }
                                    else if (transform.rotation.eulerAngles.y > 200.0f && transform.rotation.eulerAngles.y <= 245.0f)
                                    {
                                        if (NowMovePlayer == false)
                                        {
                                            PlayerAniState = AnimationState.LEFTDOWNSTAND;
                                        }
                                        else
                                        {
                                            PlayerAniState = AnimationState.LEFTDOWNWALK;
                                        }
                                    }
                                    else if (transform.rotation.eulerAngles.y > 245.0f && transform.rotation.eulerAngles.y <= 290.0f)
                                    {
                                        if (NowMovePlayer == false)
                                        {
                                            PlayerAniState = AnimationState.LEFTSTAND;
                                        }
                                        else
                                        {
                                            PlayerAniState = AnimationState.LEFTWALK;
                                        }
                                    }
                                    else if (transform.rotation.eulerAngles.y > 290.0f && transform.rotation.eulerAngles.y <= 340.0f)
                                    {
                                        if (NowMovePlayer == false)
                                        {
                                            PlayerAniState = AnimationState.LEFTUPSTAND;
                                        }
                                        else
                                        {
                                            PlayerAniState = AnimationState.LEFTUPWALK;
                                        }
                                    }
                                }
                                else
                                {
                                    // 사격 애니메이션 출력

                                    if ((transform.rotation.eulerAngles.y > 340.0f && transform.rotation.eulerAngles.y <= 0.0f)
                                       || (transform.rotation.eulerAngles.y > 0.0f && transform.rotation.eulerAngles.y <= 25.0f))
                                    {
                                        PlayerAniState = AnimationState.UPSHOOT;
                                    }
                                    else if (transform.rotation.eulerAngles.y > 25.0f && transform.rotation.eulerAngles.y <= 70.0f)
                                    {

                                        PlayerAniState = AnimationState.RIGHTUPSHOOT;
                                    }
                                    else if (transform.rotation.eulerAngles.y > 70.0f && transform.rotation.eulerAngles.y <= 110.0f)
                                    {

                                        PlayerAniState = AnimationState.RIGHTSHOOT;
                                    }
                                    else if (transform.rotation.eulerAngles.y > 110.0f && transform.rotation.eulerAngles.y <= 155.0f)
                                    {

                                        PlayerAniState = AnimationState.RIGHTDOWNSHOOT;
                                    }
                                    else if (transform.rotation.eulerAngles.y > 155.0f && transform.rotation.eulerAngles.y <= 200.0f)
                                    {

                                        PlayerAniState = AnimationState.DOWNSHOOT;
                                    }
                                    else if (transform.rotation.eulerAngles.y > 200.0f && transform.rotation.eulerAngles.y <= 245.0f)
                                    {

                                        PlayerAniState = AnimationState.LEFTDOWNSHOOT;
                                    }
                                    else if (transform.rotation.eulerAngles.y > 245.0f && transform.rotation.eulerAngles.y <= 290.0f)
                                    {


                                        PlayerAniState = AnimationState.LEFTSHOOT;
                                    }
                                    else if (transform.rotation.eulerAngles.y > 290.0f && transform.rotation.eulerAngles.y <= 340.0f)
                                    {

                                        PlayerAniState = AnimationState.LEFTUPSHOOT;
                                    }
                                }

                                Debug.DrawRay(this.transform.position, (Vector3.down) * 50.0f, Color.red);
                                Debug.DrawRay(this.transform.position, (Player_Tranjectory_Object.transform.position - this.transform.position).normalized * 0.6f, Color.yellow);

                                if (Physics.Raycast(this.transform.position, (Vector3.down), out hit, Mathf.Infinity, layerMask))
                                {
                                    //print("hit Name : " + hit.collider.name);
                                    //print("hit Point : " + hit.point);

                                    PlayerPos = hit.point;
                                    PlayerPosBack = PlayerPos;
                                }
                                else
                                {
                                    this.transform.position = new Vector3(PlayerPosBack.x, this.transform.position.y, PlayerPosBack.z);
                                }

                                if (Physics.Raycast(this.transform.position, (Player_Tranjectory_Object.transform.position - this.transform.position).normalized * 1.0f, out Wallhit, 0.3f, WalllayerMask))
                                {

                                    if (Wallhit.collider.transform.tag.Equals("Wall") == true)
                                    {

                                        CameraMoveOn = false;    
                                    }

                                    if(Wallhit.collider.transform.tag.Equals("DeadEyeBox") == true)
                                    {
                                        CameraMoveOn = false;

                                        Debug.Log("DeadEye Ready");

                                        DeadEyeReady = true;
                                    }
                                    else
                                    {
                                        Debug.Log("DeadEye Not Ready");

                                        DeadEyeReady = false;
                                    }

                                    
                                }
                                else
                                {
                                    CameraMoveOn = true;

                                    DeadEyeReady = false;
                                }
                            }
                            break;

                        case PlayerState.DEADEYE:
                            {
                                Player_Tranjectory.enabled = false;
                                PlayerAniState = AnimationState.DEADEYING;
                            }
                            break;

                        case PlayerState.DEAD:
                            {
                                //PlayerAniState = AnimationState.DEAD;


                                StopCoroutine(DeadProtocol(true));
                                StartCoroutine(DeadProtocol(true));

                            }
                            break;
                    }

                }
                break;

            case GameState.PAUSE:
                {
                    switch (playerState)
                    {
                        case PlayerState.NORMAL:
                            {

                                Player_Tranjectory.enabled = false;

                                PlayerAniState = AnimationState.DOWNSTAND;
                            }
                            break;

                        case PlayerState.REALBATTLE:
                            {
                                Player_Tranjectory.enabled = false;

                                PlayerAniState = AnimationState.DOWNSTAND;
                            }
                            break;

                        case PlayerState.DEADEYE:
                            {
                                PlayerAniState = AnimationState.DOWNSTAND;
                            }
                            break;

                        case PlayerState.DEAD:
                            {
                                if ((transform.rotation.eulerAngles.y > 315.0f && transform.rotation.eulerAngles.y <= 0.0f)
                                    || (transform.rotation.eulerAngles.y > 0.0f && transform.rotation.eulerAngles.y <= 45.0f))
                                {
                                    PlayerAniState = AnimationState.UPDEAD;
                                }
                                else if (transform.rotation.eulerAngles.y > 45.0f && transform.rotation.eulerAngles.y <= 135.0f)
                                {
                                    PlayerAniState = AnimationState.RIGHTDEAD;
                                }
                                else if (transform.rotation.eulerAngles.y > 135.0f && transform.rotation.eulerAngles.y <= 225.0f)
                                {
                                    PlayerAniState = AnimationState.DOWNDEAD;
                                }
                                else if (transform.rotation.eulerAngles.y > 225.0f && transform.rotation.eulerAngles.y <= 315.0f)
                                {
                                    PlayerAniState = AnimationState.LEFTDEAD;
                                }
                            }
                            break;
                    }
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

    //void FixedUpdate()
    //{

    //    switch (State)
    //    {
    //        case GameState.START:
    //            {

    //            }
    //            break;

    //        case GameState.PLAY:
    //            {
    //                switch(playerState)
    //                {
    //                    case PlayerState.NORMAL:
    //                        {
    //                            this.transform.Translate((MoveVector * MoveSpeed) * Time.deltaTime);
    //                        }
    //                        break;

    //                    case PlayerState.DEADEYE:
    //                        {

    //                        }
    //                        break;

    //                    case PlayerState.DEAD:
    //                        {
    //                            GameManager.NowGameState = GameState.GAMEOVER;
    //                        }
    //                        break;
    //                }
                    
    //            }
    //            break;

    //        case GameState.PAUSE:
    //            {

    //            }
    //            break;

    //        case GameState.EVENT:
    //            {

    //            }
    //            break;

    //        case GameState.GAMEOVER:
    //            {

    //            }
    //            break;

    //        case GameState.VICTORY:
    //            {

    //            }
    //            break;
    //    }
    //}

    public Vector3 PoolInput()
    {
        Vector3 Direction = Vector3.zero;

        Direction.x = JoyStickControl.GetHorizontalValue();
        Direction.z = JoyStickControl.GetVerticalValue();

        //print("Direction.x : " + Direction.x);
        //print("Direction.z : " + Direction.z);

        if(Direction.magnitude > 1)
        {
            Direction.Normalize();
        }

        return Direction;
    }

    public void Shoot()
    {
        //ShootAniOn = m_PlayerAni.GetShootAniOn();

        switch (playerState)
        {
            case PlayerState.NORMAL:
                {
                    // 총알의 개수 파악하기
                    if (BulletQuantity > 0)
                    {
                        if (BulletStack > 0)
                        {

                            // 재장전이 다 되어있다면 발사한다.
                            if (ReloadSuccessOn == true)
                            {
                                if ((ShootOn == true))
                                {
                                    StopCoroutine(ShootProtocol(true));
                                    StartCoroutine(ShootProtocol(true));
                                }
                            }
                        }
                        else
                        {
                            StopCoroutine(ReloadProtocol(true));
                            StartCoroutine(ReloadProtocol(true));
                        }
                    }
          
                }
                break;

            case PlayerState.REALBATTLE:
                {
                    // 총알의 개수 파악하기
                    if (BulletQuantity > 0)
                    {
                        if (BulletStack > 0)
                        {

                            // 재장전이 다 되어있다면 발사한다.
                            if (ReloadSuccessOn == true)
                            {
                                if ((ShootOn == true))
                                {
                                    StopCoroutine(ShootProtocol(true));
                                    StartCoroutine(ShootProtocol(true));
                                }
                            }
                        }
                        else
                        {
                            StopCoroutine(ReloadProtocol(true));
                            StartCoroutine(ReloadProtocol(true));
                        }
                    }
                    
                }
                break;

            case PlayerState.DEADEYE:
                {
                    if (DeadEyeActive == false)
                    {
                        StopCoroutine(DeadEyeProtocol);
                        
                        DeadEyeProtocol = null;
                        DeadEyeProtocol = DeadEyeShootProtocol(true);

                        StartCoroutine(DeadEyeProtocol);
                    }

                }
                break;

            case PlayerState.DEAD:
                {
                    this.gameObject.SetActive(false);
                }
                break;
        }



        
    }

    IEnumerator ShootProtocol(bool On = true)
    {
        ShootOn = false;

        //Debug.Log("Bang!");

        // 총알 오브젝트 On
        if (NowBulletIndex < Bullets.Length)
        {
            if (ShootAniOn == true)
            {

                if (Bullets[NowBulletIndex].gameObject.activeSelf == false)
                {
                    Bullets[NowBulletIndex].transform.position = this.transform.position;
                    //Bullets[NowBulletIndex].transform.LookAt(Player_Tranjectory_Object.transform.position);
                    Bullets[NowBulletIndex].transform.parent = null;

                    Bullets[NowBulletIndex].gameObject.SetActive(true);

                    NowBulletIndex++;
                }
            }

        }
        else
        {


            if (ShootAniOn == true)
            {
                NowBulletIndex = 0;

                if (Bullets[NowBulletIndex].gameObject.activeSelf == false)
                {
                    Bullets[NowBulletIndex].transform.position = this.transform.position;
                    //Bullets[NowBulletIndex].transform.LookAt(Player_Tranjectory_Object.transform.position);
                    Bullets[NowBulletIndex].transform.parent = null;

                    Bullets[NowBulletIndex].gameObject.SetActive(true);

                    NowBulletIndex++;
                }
            }

        }

        BulletStack--;
        ReloadSuccessOn = true;

        yield return new WaitForSeconds(ShootCoolTime);

        ShootOn = true;
    }

    // 재장전 하기
    IEnumerator ReloadProtocol(bool On = true)
    {
        Debug.Log("Reload!");

        ReloadSuccessOn = false;

        yield return new WaitForSeconds(0.5f);

        Debug.Log("Reload End!");

        ReloadSuccessOn = true;
        BulletStack = 6;
    }

    // 데드 아이 시전 로직
    IEnumerator DeadEyeShootProtocol(bool On = true)
    {

        // 데드 아이 시전

        print("DeadEye Start....");
        DeadEyeActive = true;

        playerState = PlayerState.DEADEYE;
        PlayerDeadEyePosBack = this.transform.position;
        CameraPosBack = MainCamera.transform.position;

        this.transform.position = new Vector3(DeadEyeBox.transform.position.x, this.transform.position.y + DeadEyeBox.transform.position.y + 0.3f, DeadEyeBox.transform.position.z);
        MainCamera.transform.Translate(new Vector3(0.0f, 0.0f, -200.0f) * Time.deltaTime);

        yield return new WaitForSeconds(4.4f);

        print("DeadEye End....");

        DeadEyeActive = false;
        DeadEyeShootOn = true;

        playerState  = PlayerState.REALBATTLE;
        this.transform.position = PlayerDeadEyePosBack;

        MainCamera.transform.position = CameraPosBack;

        DeadEyeBox.SetActive(false);
    }

    // 사망 시 애니메이션 및 순서
    IEnumerator DeadProtocol(bool On = true)
    {
        if ((transform.rotation.eulerAngles.y > 315.0f && transform.rotation.eulerAngles.y <= 0.0f)
    || (transform.rotation.eulerAngles.y > 0.0f && transform.rotation.eulerAngles.y <= 45.0f))
        {
            PlayerAniState = AnimationState.UPDEAD;
        }
        else if (transform.rotation.eulerAngles.y > 45.0f && transform.rotation.eulerAngles.y <= 135.0f)
        {
            PlayerAniState = AnimationState.RIGHTDEAD;
        }
        else if (transform.rotation.eulerAngles.y > 135.0f && transform.rotation.eulerAngles.y <= 225.0f)
        {
            PlayerAniState = AnimationState.DOWNDEAD;
        }
        else if (transform.rotation.eulerAngles.y > 225.0f && transform.rotation.eulerAngles.y <= 315.0f)
        {
            PlayerAniState = AnimationState.LEFTDEAD;
        }

        

        yield return new WaitForSeconds(1.0f);

        playerState = PlayerState.DEAD;
        GameManager.NowGameState = GameState.GAMEOVER;
    }

    // 발사 로직 쿨 타임
    //IEnumerator ShootCoolProtocol(bool On = true)
    //{
    //    Debug.Log("Shoot Cool!");

    //    yield return new WaitForSeconds(0.3f);

    //    BulletStack--;
    //    ReloadSuccessOn = true;

    //    Debug.Log("Shoot Cool End!");
    //}

    public PlayerState GetPlayerState()
    {
        return playerState;
    }

    public Vector3 GetPlayerDirection()
    {
        return (Player_Tranjectory_Object.transform.position - this.transform.position).normalized;
    }

    public Vector3 GetPlayerPosition()
    {
        if(PlayerPos == Vector3.zero)
        {
            return this.transform.position;
        }
        else
        {
            return hit.point;
        }
        
    }

    // NORMAL = 0;
    // DEADEYE = 1;
    // REALBATTLE = 2;
    // DEAD = 3;
    public void SetPlayerState(int Set)
    {
        switch(Set)
        {
            case 0:
                {
                    playerState = PlayerState.NORMAL;
                }
                break;

            case 1:
                {
                    PrevPlayerState = playerState;
                    playerState = PlayerState.DEADEYE;
                }
                break;

            case 2:
                {
                    playerState = PlayerState.REALBATTLE;
                }
                break;

            case 3:
                {
                    playerState = PlayerState.DEAD;
                }
                break;
        }
    }

    public bool GetDeadEyeReady()
    {
        return DeadEyeReady;
    }

    public GameObject GetDeadEyeObject()
    {
        return DeadEyeBox;
    }

    public GameObject GetCameraObject()
    {
        return CameraObject;
    }

    public void SetDeadEyeShootOn(bool On = true)
    {
        DeadEyeShootOn = On;
    }

    public bool GetDeadEyeShootOn()
    {
        return DeadEyeShootOn;
    }

    public AnimationState GetPlayerAniState()
    {
        return PlayerAniState;
    }

    public bool GetDeadEyeActive()
    {
        return DeadEyeActive;
    }

    public void DeadEyeCancel()
    {
        StopCoroutine(DeadEyeProtocol);

        if (DeadEyeActive == true)
        {
            print("DeadEye End....");

            DeadEyeActive = false;
            DeadEyeShootOn = true;

            playerState = PlayerState.REALBATTLE;
            this.transform.position = PlayerDeadEyePosBack;
            MainCamera.transform.position = CameraPosBack;
            //MainCamera.transform.Translate(new Vector3(0.0f, 0.0f, 200.0f) * Time.deltaTime);

            DeadEyeBox.SetActive(false);
        }

    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag.Equals("EnemyBullet") == true)
        {
            // HP를 깍는다
            if (HP <= 0)
            {
                HP = 0;

                Debug.Log("The Game End...ㅠ");

                playerState = PlayerState.DEAD;



            }
            else
            {
                HP -= 40;
            }
            
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag.Equals("EnemyBullet") == true)
        {
            // HP를 깍는다
            if(HP <= 0)
            {
                HP = 0;

                Debug.Log("The Game End...ㅠ");

                
                playerState = PlayerState.DEAD;

                StopCoroutine(DeadProtocol(true));
                StartCoroutine(DeadProtocol(true));
                
                

            }
            else
            {
                //HP -= 10;
            }
            
        }

        if (other.transform.tag.Equals("Item") == true)
        {
            Debug.Log("Bullet Get !");
            
            BulletQuantity++;

            Debug.Log("BulletQuantity : " + BulletQuantity);
        }
        //if (other.transform.tag.Equals("DeadEyeBox") == true)
        //{
        //    // 데드아이를 준비한다.

        //    print("DeadEye Ready");

        //    DeadEyeReady = true;


        //}
        //else
        //{
        //    print("DeadEye Not Ready");

        //    DeadEyeReady = false;
        //}
    }
}
