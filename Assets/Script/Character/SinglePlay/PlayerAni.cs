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

    private bool ShootAniOn;
    private bool AniCheckOn;

	// Use this for initialization
	void Start () {
	
        if(m_Player == null)
        {
            m_Player = GameObject.FindWithTag("Player").GetComponent<Player>();

            State = GameManager.NowGameState;
            ControlState = GameManager.NowGameControlState;

            PlayerAniState = m_Player.GetPlayerAniState();

            this.transform.position = new Vector3(m_Player.gameObject.transform.position.x, m_Player.gameObject.transform.position.y + 0.2f, m_Player.gameObject.transform.position.z);
        }
        else
        {
            State = GameManager.NowGameState;
            ControlState = GameManager.NowGameControlState;

            PlayerAniState = m_Player.GetPlayerAniState();

            this.transform.position = new Vector3(m_Player.gameObject.transform.position.x, m_Player.gameObject.transform.position.y + 0.2f, m_Player.gameObject.transform.position.z);
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
            //_animatedTileTexture.RegisterCallback(ShootAnimationFinished);
        }


        ShootAniOn = false;
        AniCheckOn = false;

        this.transform.LookAt(MainCamera.transform.position);
        this.transform.Rotate(new Vector3(0.0f, -180.0f, 0.0f));
	}

    // This function will get called by the AnimatedTiledTexture script when the animation is completed if the EnableEvents option is set to true
    void AnimationFinished()
    {
        playerState = m_Player.GetPlayerState();

        // The animation is finished
        if(playerState == PlayerState.DEAD)
        {
            //_animatedTileTexture._framesPerSecond = 12;
        }

        ShootAniOn = false;
        AniCheckOn = false;
        //_animatedTileTexture.SetIndex(0);
        _animatedTileTexture._playOnce = false;
        _animatedTileTexture._enableEvents = true;
        //_animatedTileTexture.SetIndex(0);
        

        PlayerAniState = m_Player.GetPlayerAniState();
    }
	
	// Update is called once per frame
	void Update () 
    {
        State = GameManager.NowGameState;
        ControlState = GameManager.NowGameControlState;

        playerState = m_Player.GetPlayerState();
        PlayerAniState = m_Player.GetPlayerAniState();

        if (_animatedTileTexture.GetIndex() >= 5)
        {
            ShootAniOn = true;
        }
        else
        {
            ShootAniOn = false;
        }

        print("Index : " + _animatedTileTexture.GetIndex());
        //print("PlayerAniState : " + PlayerAniState);
        //print("_animatedTileTexture.GetIndex() : " + _animatedTileTexture.GetIndex());

        switch (State)
        {
            case GameState.START:
                {
                    this.transform.position = new Vector3(m_Player.gameObject.transform.position.x, m_Player.gameObject.transform.position.y, m_Player.gameObject.transform.position.z);
                }
                break;

            case GameState.PLAY:
                {
                    this.transform.position = new Vector3(m_Player.gameObject.transform.position.x, m_Player.gameObject.transform.position.y, m_Player.gameObject.transform.position.z);

                    switch(PlayerAniState)
                    {
                        case AnimationState.DOWNSTAND:
                            {
                                _animatedTileTexture._enableEvents = false;
                                _animatedTileTexture.ChangeCheckRow(1);
                                //_animatedTileTexture.ChangeCheckRow(1);
                            }
                            break;

                        case AnimationState.UPSTAND:
                            {
                                _animatedTileTexture._enableEvents = false;
                                _animatedTileTexture.ChangeCheckRow(2);
                            }
                            break;

                        case AnimationState.LEFTSTAND:
                            {
                                _animatedTileTexture._enableEvents = false;
                                _animatedTileTexture.ChangeCheckRow(4);
                            }
                            break;

                        case AnimationState.RIGHTSTAND:
                            {
                                _animatedTileTexture._enableEvents = false;
                                _animatedTileTexture.ChangeCheckRow(3);
                            }
                            break;

                        case AnimationState.LEFTDOWNSTAND:
                            {
                                _animatedTileTexture._enableEvents = false;
                                _animatedTileTexture.ChangeCheckRow(6);
                            }
                            break;

                        case AnimationState.LEFTUPSTAND:
                            {
                                _animatedTileTexture._enableEvents = false;
                                _animatedTileTexture.ChangeCheckRow(8);
                            }
                            break;

                        case AnimationState.RIGHTUPSTAND:
                            {
                                _animatedTileTexture._enableEvents = false;
                                _animatedTileTexture.ChangeCheckRow(7);
                            }
                            break;

                        case AnimationState.RIGHTDOWNSTAND:
                            {
                                _animatedTileTexture._enableEvents = false;
                                _animatedTileTexture.ChangeCheckRow(5);
                            }
                            break;

                        case AnimationState.LEFTWALK:
                            {
                                _animatedTileTexture._enableEvents = false;
                                _animatedTileTexture.ChangeCheckRow(12);
                            }
                            break;

                        case AnimationState.RIGHTWALK:
                            {
                                _animatedTileTexture._enableEvents = false;
                                _animatedTileTexture.ChangeCheckRow(11);
                            }
                            break;

                        case AnimationState.UPWALK:
                            {
                                _animatedTileTexture._enableEvents = false;
                                _animatedTileTexture.ChangeCheckRow(10);
                            }
                            break;

                        case AnimationState.DOWNWALK:
                            {
                                _animatedTileTexture._enableEvents = false;
                                _animatedTileTexture.ChangeCheckRow(9);
                            }
                            break;

                        case AnimationState.LEFTUPWALK:
                            {
                                _animatedTileTexture._enableEvents = false;
                                _animatedTileTexture.ChangeCheckRow(16);
                            }
                            break;

                        case AnimationState.LEFTDOWNWALK:
                            {
                                _animatedTileTexture._enableEvents = false;
                                _animatedTileTexture.ChangeCheckRow(14);
                            }
                            break;

                        case AnimationState.RIGHTUPWALK:
                            {
                                _animatedTileTexture._enableEvents = false;
                                _animatedTileTexture.ChangeCheckRow(15);
                            }
                            break;

                        case AnimationState.RIGHTDOWNWALK:
                            {
                                _animatedTileTexture._enableEvents = false;
                                _animatedTileTexture.ChangeCheckRow(13);
                            }
                            break;

                        case AnimationState.LEFTSHOOT:
                            {
                                if (AniCheckOn == false)
                                {
                                    AniCheckOn = true;
                                    _animatedTileTexture.SetIndex(0);
                                    
                                    

                                    _animatedTileTexture._enableEvents = true;
                                }

                                _animatedTileTexture.ChangeCheckRow(20);

                            }
                            break;

                        case AnimationState.RIGHTSHOOT:
                            {
                                if (AniCheckOn == false)
                                {
                                    AniCheckOn = true;
                                    _animatedTileTexture.SetIndex(0);
                                    
                                    

                                    _animatedTileTexture._enableEvents = true;
                                }

                                _animatedTileTexture.ChangeCheckRow(19);
                            }
                            break;

                        case AnimationState.DOWNSHOOT:
                            {
                                if (AniCheckOn == false)
                                {
                                    AniCheckOn = true;
                                    _animatedTileTexture.SetIndex(0);
                                    
                                    

                                    _animatedTileTexture._enableEvents = true;
                                }
                                _animatedTileTexture.ChangeCheckRow(17);

                            }
                            break;

                        case AnimationState.UPSHOOT:
                            {
                                if (AniCheckOn == false)
                                {
                                    AniCheckOn = true;
                                    _animatedTileTexture.SetIndex(0);
                                    
                                    

                                    _animatedTileTexture._enableEvents = true;
                                }

                                _animatedTileTexture.ChangeCheckRow(18);
                            }
                            break;

                        case AnimationState.LEFTUPSHOOT:
                            {
                                if (AniCheckOn == false)
                                {
                                    AniCheckOn = true;
                                    //_animatedTileTexture._playOnce = true;
                                    _animatedTileTexture.SetIndex(0);
                                    
                                    

                                    _animatedTileTexture._enableEvents = true;
                                }

                                _animatedTileTexture.ChangeCheckRow(24);
                            }
                            break;

                        case AnimationState.LEFTDOWNSHOOT:
                            {
                                if (AniCheckOn == false)
                                {
                                    AniCheckOn = true;
                                    //_animatedTileTexture._playOnce = true;
                                    _animatedTileTexture.SetIndex(0);
                                    
                                    

                                    _animatedTileTexture._enableEvents = true;
                                }

                                _animatedTileTexture.ChangeCheckRow(22);

                            }
                            break;

                        case AnimationState.RIGHTUPSHOOT:
                            {
                                if (AniCheckOn == false)
                                {
                                    AniCheckOn = true;
                                    //_animatedTileTexture._playOnce = true;
                                    _animatedTileTexture.SetIndex(0);
                                    
                                    

                                    _animatedTileTexture._enableEvents = true;
                                }
                                _animatedTileTexture.ChangeCheckRow(23);
                            }
                            break;

                        case AnimationState.RIGHTDOWNSHOOT:
                            {
                                if (AniCheckOn == false)
                                {
                                    AniCheckOn = true;
                                   // _animatedTileTexture._playOnce = true;
                                    _animatedTileTexture.SetIndex(0);
                                    
                                    

                                    _animatedTileTexture._enableEvents = true;
                                }

                                _animatedTileTexture.ChangeCheckRow(21);
                            }
                            break;

                        case AnimationState.SHOOTING:
                            {

                            }
                            break;

                        case AnimationState.DEADEYING:
                            {
                                _animatedTileTexture.ChangeCheckRow(1);
                            }
                            break;

                        case AnimationState.LEFTDEAD:
                            {
                                _animatedTileTexture._playOnce = true;
                                _animatedTileTexture._enableEvents = false;
                                _animatedTileTexture.ChangeCheckColumn(12);
                                _animatedTileTexture.ChangeCheckRow(28);
                                
                                
                            }
                            break;

                        case AnimationState.UPDEAD:
                            {
                                _animatedTileTexture._playOnce = true;
                                _animatedTileTexture._enableEvents = false;
                                _animatedTileTexture.ChangeCheckColumn(12);
                                _animatedTileTexture.ChangeCheckRow(26);
                                //_animatedTileTexture.ChangeCheckColumn(12);

                            }
                            break;

                        case AnimationState.DOWNDEAD:
                            {
                                _animatedTileTexture._playOnce = true;
                                _animatedTileTexture._enableEvents = false;
                                _animatedTileTexture.ChangeCheckColumn(12);
                                _animatedTileTexture.ChangeCheckRow(25);
                                //_animatedTileTexture.ChangeCheckColumn(12);

                            }
                            break;

                        case AnimationState.RIGHTDEAD:
                            {
                                _animatedTileTexture._playOnce = true;
                                _animatedTileTexture._enableEvents = false;
                                _animatedTileTexture.ChangeCheckColumn(12);
                                //_animatedTileTexture.ChangeCheckRow(27);
                                
                            }
                            break;
                    }
                }
                break;

            case GameState.PAUSE:
                {
                    this.transform.position = new Vector3(m_Player.gameObject.transform.position.x, m_Player.gameObject.transform.position.y + 0.2f, m_Player.gameObject.transform.position.z);
                }
                break;

            case GameState.EVENT:
                {
                    this.transform.position = new Vector3(m_Player.gameObject.transform.position.x, m_Player.gameObject.transform.position.y + 0.2f, m_Player.gameObject.transform.position.z);
                }
                break;

            case GameState.GAMEOVER:
                {
                    this.transform.position = new Vector3(m_Player.gameObject.transform.position.x, m_Player.gameObject.transform.position.y + 0.2f, m_Player.gameObject.transform.position.z);
                }
                break;

            case GameState.VICTORY:
                {
                    this.transform.position = new Vector3(m_Player.gameObject.transform.position.x, m_Player.gameObject.transform.position.y + 0.2f, m_Player.gameObject.transform.position.z);
                }
                break;
        }
	}

    public bool GetShootAniOn()
    {
        return ShootAniOn;
    }
}
