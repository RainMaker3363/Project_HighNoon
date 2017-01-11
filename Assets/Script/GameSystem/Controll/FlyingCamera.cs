using UnityEngine;
using System.Collections;

public class FlyingCamera : MonoBehaviour {

    private GameState State;
    private GameControlState ControlState;

    private PlayerState playerState;
    private Vector3 BackPos;

    public GameObject PlayerObject;
    private Player m_Player;

	// Use this for initialization
	void Start () 
    {

        State = GameManager.NowGameState;
        ControlState = GameManager.NowGameControlState;

        if (PlayerObject == null)
        {
            PlayerObject = GameObject.FindWithTag("Player");
            m_Player = PlayerObject.GetComponent<Player>();
        }
        else
        {
            m_Player = PlayerObject.GetComponent<Player>();
        }

        BackPos = this.transform.position;
	}
	
	// Update is called once per frame
	void LateUpdate () {

        State = GameManager.NowGameState;
        playerState = m_Player.GetPlayerState();

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
                                //this.transform.position = new Vector3(this.transform.position.x, BackPos.y, this.transform.position.z);
                                //this.transform.LookAt(PlayerObject.transform.position);
                            }
                            break;

                        case PlayerState.DEADEYE:
                            {

                            }
                            break;

                        case PlayerState.REALBATTLE:
                            {
                                this.transform.LookAt(PlayerObject.transform.position);
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
