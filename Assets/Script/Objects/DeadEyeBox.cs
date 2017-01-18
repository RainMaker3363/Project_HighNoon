using UnityEngine;
using System.Collections;

public class DeadEyeBox : MonoBehaviour {

    private GameState State;
    private GameControlState ControlState;

    private Player m_Player;
    private PlayerState playerState;

	// Use this for initialization
	void Start () {
        State = GameManager.NowGameState;
        ControlState = GameManager.NowGameControlState;

        if (m_Player == null)
        {
            m_Player = GameObject.FindWithTag("Player").GetComponent<Player>();
            playerState = m_Player.GetPlayerState();
        }
        else
        {
            playerState = m_Player.GetPlayerState();
        }
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
