using UnityEngine;
using System.Collections;

public class PlayerAni : MonoBehaviour {

    private GameState State;
    private GameControlState ControlState;

    [HideInInspector]
    public PlayerState playerState;

    private Player m_Player;
    private GameObject MainCamera;

	// Use this for initialization
	void Start () {
	
        if(m_Player == null)
        {
            m_Player = GameObject.FindWithTag("Player").GetComponent<Player>();

            State = GameManager.NowGameState;
            ControlState = GameManager.NowGameControlState;

            playerState = m_Player.GetPlayerState();
        }
        else
        {
            State = GameManager.NowGameState;
            ControlState = GameManager.NowGameControlState;

            playerState = m_Player.GetPlayerState();
        }

        if (MainCamera == null)
        {
            MainCamera = GameObject.FindWithTag("MainCamera");
        }

        this.transform.position = m_Player.gameObject.transform.position;
        this.transform.Rotate(new Vector3(0.0f, -180.0f, 0.0f));

        this.transform.LookAt(MainCamera.transform.position);
	}
	
	// Update is called once per frame
	void Update () {
        State = GameManager.NowGameState;
        ControlState = GameManager.NowGameControlState;

        playerState = m_Player.GetPlayerState();

        this.transform.position = m_Player.gameObject.transform.position;

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

                        case PlayerState.REALBATTLE:
                            {

                            }
                            break;

                        case PlayerState.DEADEYE:
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
