using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class MultiShootIcon : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    //private GameState State;
    //private MultiPlayerState playerState;

    private Image img;
    // 주인공 오브젝트
    public MulPlayer m_Player;
    private bool DeadEyeReady;
    private bool DeadEyeActive;

    // 상황에 따른 총기 아이콘의 변화를 위함
    public Sprite[] ShootIcons;

    // Use this for initialization
    void Start()
    {

        if (img == null)
        {
            img = GameObject.Find("ShootIcon").GetComponent<Image>();
            img.sprite = ShootIcons[0];
        }
        else
        {
            img.sprite = ShootIcons[0];
        }

        if (m_Player == null)
        {
            m_Player = GameObject.FindWithTag("Player").GetComponent<MulPlayer>();
            //playerState = m_Player.playerState;
            DeadEyeReady = m_Player.GetDeadEyeReady();
        }
        else
        {
            //playerState = m_Player.playerState;
            DeadEyeReady = m_Player.GetDeadEyeReady();
        }



        //State = MultiGameManager.NowGameState;

    }

    // Update is called once per frame
    void Update()
    {
        //State = MultiGameManager.NowGameState;
        //playerState = m_Player.playerState;

        DeadEyeReady = m_Player.GetDeadEyeReady();
        DeadEyeActive = m_Player.GetDeadEyeActive();

        if (DeadEyeReady == true)
        {
            img.sprite = ShootIcons[1];


        }
        else
        {

            img.sprite = ShootIcons[0];
        }
    }

    // 터치가 드래그(Drag) 했을때 호출 되는 함수
    public virtual void OnDrag(PointerEventData ped)
    {

    }


    // 터치를 하고 있을 대 발생하는 함수
    public virtual void OnPointerDown(PointerEventData ped)
    {
        if (DeadEyeReady)
        {
            Debug.Log("DeadEyeShoot Button");



            m_Player.SetPlayerState(1);
            m_Player.Shoot();

        }
        else
        {
            Debug.Log("Shoot Button");


            m_Player.Shoot();

        }

        if (DeadEyeActive == true)
        {
            m_Player.SetDeadEyeShootOn(true);

            m_Player.DeadEyeCancel();
        }

        //switch (State)
        //{
        //    case GameState.START:
        //        {

        //        }
        //        break;

        //    case GameState.PLAY:
        //        {
        //            switch (playerState)
        //            {
        //                case PlayerState.NORMAL:
        //                    {
        //                        Debug.Log("Shoot Button");

        //                        img.sprite = ShootIcons[0];

        //                        m_Player.Shoot();
        //                    }
        //                    break;

        //                case PlayerState.DEADEYE:
        //                    {
        //                        img.sprite = ShootIcons[1];

        //                        m_Player.Shoot();
        //                    }
        //                    break;

        //                case PlayerState.REALBATTLE:
        //                    {
        //                        Debug.Log("Shoot Button");

        //                        img.sprite = ShootIcons[0];

        //                        m_Player.Shoot();
        //                    }
        //                    break;

        //                case PlayerState.DEAD:
        //                    {

        //                    }
        //                    break;
        //            }

        //        }
        //        break;

        //    case GameState.PAUSE:
        //        {

        //        }
        //        break;

        //    case GameState.EVENT:
        //        {

        //        }
        //        break;

        //    case GameState.GAMEOVER:
        //        {

        //        }
        //        break;

        //    case GameState.VICTORY:
        //        {

        //        }
        //        break;
        //}
    }

    // 터치에서 손을 땠을때 발생하는 함수
    public virtual void OnPointerUp(PointerEventData ped)
    {

    }
}
