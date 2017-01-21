using UnityEngine;
using System.Collections;

public class PlayerAni : MonoBehaviour {

    private GameState State;
    private GameControlState ControlState;

    public AnimateTiledTexture _animatedTileTexture;

    [HideInInspector]
    public PlayerState playerState;
    private AnimationState PlayerAniState;

    private Player m_Player;
    private GameObject MainCamera;

	// Use this for initialization
	void Start () {
	
        if(m_Player == null)
        {
            m_Player = GameObject.FindWithTag("Player").GetComponent<Player>();

            State = GameManager.NowGameState;
            ControlState = GameManager.NowGameControlState;

            PlayerAniState = m_Player.GetPlayerAniState();
        }
        else
        {
            State = GameManager.NowGameState;
            ControlState = GameManager.NowGameControlState;

            PlayerAniState = m_Player.GetPlayerAniState();
        }

        if (MainCamera == null)
        {
            MainCamera = GameObject.FindWithTag("MainCamera");
        }

        if (_animatedTileTexture == null)
        {
            Debug.LogWarning("No animated tile texture script assigned!");
        }
        else
        {
            _animatedTileTexture.RegisterCallback(AnimationFinished);
        }

        this.transform.position = m_Player.gameObject.transform.position;
        //this.transform.Rotate(new Vector3(0.0f, -180.0f, 0.0f));

        this.transform.LookAt(MainCamera.transform.position);
	}

    // This function will get called by the AnimatedTiledTexture script when the animation is completed if the EnableEvents option is set to true
    void AnimationFinished()
    {
        // The animation is finished
    }
	
	// Update is called once per frame
	void Update () 
    {
        State = GameManager.NowGameState;
        ControlState = GameManager.NowGameControlState;

        playerState = m_Player.GetPlayerState();
        PlayerAniState = m_Player.GetPlayerAniState();

        this.transform.position = m_Player.gameObject.transform.position;

        switch (State)
        {
            case GameState.START:
                {

                }
                break;

            case GameState.PLAY:
                {
                    switch(PlayerAniState)
                    {
                        case AnimationState.DOWNSTAND:
                            {

                            }
                            break;

                        case AnimationState.UPSTAND:
                            {

                            }
                            break;

                        case AnimationState.LEFTSTAND:
                            {

                            }
                            break;

                        case AnimationState.RIGHTSTAND:
                            {

                            }
                            break;

                        case AnimationState.LEFTDOWNSTAND:
                            {

                            }
                            break;

                        case AnimationState.LEFTUPSTAND:
                            {

                            }
                            break;

                        case AnimationState.RIGHTUPSTAND:
                            {

                            }
                            break;

                        case AnimationState.RIGHTDOWNSTAND:
                            {

                            }
                            break;

                        case AnimationState.LEFTWALK:
                            {

                            }
                            break;

                        case AnimationState.RIGHTWALK:
                            {

                            }
                            break;

                        case AnimationState.UPWALK:
                            {

                            }
                            break;

                        case AnimationState.DOWNWALK:
                            {

                            }
                            break;

                        case AnimationState.LEFTUPWALK:
                            {

                            }
                            break;

                        case AnimationState.LEFTDOWNWALK:
                            {

                            }
                            break;

                        case AnimationState.RIGHTUPWALK:
                            {

                            }
                            break;

                        case AnimationState.RIGHTDOWNWALK:
                            {

                            }
                            break;

                        case AnimationState.SHOOTING:
                            {

                            }
                            break;

                        case AnimationState.DEADEYING:
                            {

                            }
                            break;

                        case AnimationState.DEAD:
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
