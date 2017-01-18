using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class ShootIcon : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{

    
    private GameState State;
    private PlayerState playerState;
    
    private Image img;
    // 주인공 오브젝트
    public Player m_Player;

    // 상황에 따른 총기 아이콘의 변화를 위함
    public Sprite[] ShootIcons;

	// Use this for initialization
	void Start () {

        img = GetComponent<Image>();

        if (m_Player == null)
        {
            m_Player = GameObject.FindWithTag("Player").GetComponent<Player>();
            playerState = m_Player.playerState;
        }
        else
        {
            playerState = m_Player.playerState;
        }

        State = GameManager.NowGameState;
        
	}
	
	// Update is called once per frame
	void Update ()
    {
        State = GameManager.NowGameState;
        playerState = m_Player.playerState;

	}

        // 터치가 드래그(Drag) 했을때 호출 되는 함수
    public virtual void OnDrag(PointerEventData ped)
    {

    }


    // 터치를 하고 있을 대 발생하는 함수
    public virtual void OnPointerDown(PointerEventData ped)
    {
        

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
                                Debug.Log("Shoot Button");

                                img.sprite = ShootIcons[0];

                                m_Player.Shoot();
                            }
                            break;

                        case PlayerState.DEADEYE:
                            {
                                img.sprite = ShootIcons[1];

                                m_Player.Shoot();
                            }
                            break;

                        case PlayerState.REALBATTLE:
                            {
                                Debug.Log("Shoot Button");

                                img.sprite = ShootIcons[0];

                                m_Player.Shoot();
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

    // 터치에서 손을 땠을때 발생하는 함수
    public virtual void OnPointerUp(PointerEventData ped)
    {

    }
}
