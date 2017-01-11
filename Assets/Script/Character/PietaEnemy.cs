using UnityEngine;
using System.Collections;

public class PietaEnemy : MonoBehaviour {

    public GameObject MainCamera;
    public GameObject CharacterAniObject;

    private GameState State;

    [HideInInspector]
    public PlayerState playerState;

    private Player m_Player;

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
	}
	
	// Update is called once per frame
	void Update () {

        playerState = m_Player.GetPlayerState();
        State = GameManager.NowGameState;

        // Quad 텍스쳐를 처리해주는 부분
        CharacterAniObject.transform.LookAt(MainCamera.transform.position);
        CharacterAniObject.transform.Rotate(new Vector3(0.0f, -180.0f, 0.0f));

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

                            }
                            break;

                        case PlayerState.DEADEYE:
                            {

                            }
                            break;

                        case PlayerState.REALBATTLE:
                            {

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
}
