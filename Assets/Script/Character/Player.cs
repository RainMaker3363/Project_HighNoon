using UnityEngine;
using System.Collections;

public enum PlayerState
{
    NORMAL = 0,
    DEADEYE,
    DEAD
}

public class Player : MonoBehaviour {

    private GameState State;
    private GameControlState ControlState;
    
    public JoyStickCtrl JoyStickControl;


    public PlayerState playerState;
    
    // 플레이어의 상태 값
    private float HP;

    // 플레이어의 움직임
    private float MoveSpeed;
    private Vector3 MoveVector;

    // 플레이어가 총을 쏠 수 있을지의 여부
    private bool ShootOn;
    private bool ReloadSuccessOn;
    private float ShootCoolTime;
    private int BulletStack;

	// Use this for initialization
	void Start () {
	
        State = GameManager.NowGameState;
        ControlState = GameManager.NowGameControlState;

        if(JoyStickControl == null)
        {
            JoyStickControl = GameObject.Find("Joystick_Pad").GetComponent<JoyStickCtrl>();
        }

        // 플레이어의 상태 값
        HP = 100.0f;
        playerState = PlayerState.NORMAL;

        // 플레이어 이동 방향 초기화
        MoveVector = Vector3.zero;
        MoveSpeed = 2.0f;

        // 플레이어가 사격할 수 있는지의 여부
        ShootOn = true;
        ReloadSuccessOn = false;
        ShootCoolTime = 1.0f;
        BulletStack = 6;
	}

    // Update is called once per frame
    void Update()
    {
        State = GameManager.NowGameState;

        MoveVector = PoolInput();
    }

    void FixedUpdate()
    {

        switch (State)
        {
            case GameState.START:
                {

                }
                break;

            case GameState.PLAY:
                {
                    switch(playerState)
                    {
                        case PlayerState.NORMAL:
                            {
                                this.transform.Translate((MoveVector * MoveSpeed) * Time.deltaTime);
                            }
                            break;

                        case PlayerState.DEADEYE:
                            {

                            }
                            break;

                        case PlayerState.DEAD:
                            {
                                GameManager.NowGameState = GameState.GAMEOVER;
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

    public Vector3 PoolInput()
    {
        Vector3 Direction = Vector3.zero;

        Direction.x = JoyStickControl.GetHorizontalValue();
        Direction.z = JoyStickControl.GetVerticalValue();

        if(Direction.magnitude > 1)
        {
            Direction.Normalize();
        }

        return Direction;
    }

    public void Shoot()
    {
        // 총알의 개수 파악하기
        if (BulletStack > 0)
        {
            BulletStack--;
            ReloadSuccessOn = true;
        }
        else
        {
            StopCoroutine(ReloadProtocol(true));
            StartCoroutine(ReloadProtocol(true));
        }

        // 재장전이 다 되어있다면 발사한다.
        if (ReloadSuccessOn == true)
        {
            if (ShootOn == true)
            {
                StopCoroutine(ShootProtocol(true));
                StartCoroutine(ShootProtocol(true));
            }
        }
        
    }

    IEnumerator ShootProtocol(bool On = true)
    {
        ShootOn = false;

        yield return new WaitForSeconds(ShootCoolTime);

        ShootOn = true;
    }

    // 재장전 하기
    IEnumerator ReloadProtocol(bool On = true)
    {

        ReloadSuccessOn = false;

        yield return new WaitForSeconds(1.5f);

        ReloadSuccessOn = true;
        BulletStack = 6;
    }
}
