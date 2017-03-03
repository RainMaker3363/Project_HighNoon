using UnityEngine;
using System.Collections;

public class ZombieEnemyAni : MonoBehaviour {

    private GameState State;
    private GameControlState ControlState;

    public AnimateTiledTexture _animatedTileTexture;

    private AnimationState AniState;
    public ZombieEnemy m_Zombie;

    private bool AniCheckOn;

	// Use this for initialization
	void Start () {
        if (m_Zombie == null)
        {
            m_Zombie = GameObject.FindWithTag("ZombieEnemy").GetComponent<ZombieEnemy>();

            State = GameManager.NowGameState;
            ControlState = GameManager.NowGameControlState;

            AniState = m_Zombie.GetAniState();

            this.transform.position = new Vector3(m_Zombie.gameObject.transform.position.x, m_Zombie.gameObject.transform.position.y + 0.3f, m_Zombie.gameObject.transform.position.z);
        }
        else
        {
            State = GameManager.NowGameState;
            ControlState = GameManager.NowGameControlState;

            AniState = m_Zombie.GetAniState();

            this.transform.position = new Vector3(m_Zombie.gameObject.transform.position.x, m_Zombie.gameObject.transform.position.y + 0.3f, m_Zombie.gameObject.transform.position.z);
        }

        if (_animatedTileTexture == null)
        {
            Debug.LogWarning("No animated tile texture script assigned!");
        }
        else
        {
            //_animatedTileTexture.RegisterCallback(AnimationFinished);
            //_animatedTileTexture.RegisterCallback(ShootAnimationFinished);
        }
        //_animatedTileTexture.RegisterCallback(AnimationFinished);

        AniCheckOn = false;

        this.transform.Rotate(new Vector3(0.0f, -180.0f, 0.0f));
	}

    void OnEnable()
    {
        if (m_Zombie == null)
        {
            m_Zombie = GameObject.FindWithTag("ZombieEnemy").GetComponent<ZombieEnemy>();

            State = GameManager.NowGameState;
            ControlState = GameManager.NowGameControlState;

            AniState = m_Zombie.GetAniState();

            this.transform.position = new Vector3(m_Zombie.gameObject.transform.position.x, m_Zombie.gameObject.transform.position.y + 0.3f, m_Zombie.gameObject.transform.position.z);
        }
        else
        {
            State = GameManager.NowGameState;
            ControlState = GameManager.NowGameControlState;

            AniState = m_Zombie.GetAniState();

            this.transform.position = new Vector3(m_Zombie.gameObject.transform.position.x, m_Zombie.gameObject.transform.position.y + 0.3f, m_Zombie.gameObject.transform.position.z);
        }

        if (_animatedTileTexture == null)
        {
            Debug.LogWarning("No animated tile texture script assigned!");
        }
        else
        {
            //_animatedTileTexture.RegisterCallback(AnimationFinished);
            //_animatedTileTexture.RegisterCallback(ShootAnimationFinished);
        }
        //_animatedTileTexture.RegisterCallback(AnimationFinished);

        AniCheckOn = false;

        //this.transform.Rotate(new Vector3(0.0f, -180.0f, 0.0f));
    }

    //// This function will get called by the AnimatedTiledTexture script when the animation is completed if the EnableEvents option is set to true
    //void AnimationFinished()
    //{
    //    _animatedTileTexture._playOnce = false;
    //    _animatedTileTexture._enableEvents = false;
    //    //_animatedTileTexture.SetIndex(0);


    //    AniState = m_Zombie.GetAniState();

    //    AniCheckOn = false;
    //}

	// Update is called once per frame
	void Update () {

        State = GameManager.NowGameState;
        ControlState = GameManager.NowGameControlState;

        AniState = m_Zombie.GetAniState();

        switch(State)
        {
            case GameState.START:
                {
                    this.transform.position = new Vector3(m_Zombie.gameObject.transform.position.x, m_Zombie.gameObject.transform.position.y + 0.2f, m_Zombie.gameObject.transform.position.z);

                    switch (AniState)
                    {
                        case AnimationState.LEFTWALK:
                            {
                                _animatedTileTexture._playOnce = false;
                                _animatedTileTexture._enableEvents = false;
                                _animatedTileTexture.ChangeCheckRow(1);
                            }
                            break;

                        case AnimationState.RIGHTWALK:
                            {
                                _animatedTileTexture._playOnce = false;
                                _animatedTileTexture._enableEvents = false;
                                _animatedTileTexture.ChangeCheckRow(2);
                            }
                            break;

                        case AnimationState.LEFTSTAND:
                            {
                                _animatedTileTexture._playOnce = false;
                                _animatedTileTexture._enableEvents = false;
                                _animatedTileTexture.ChangeCheckRow(3);
                            }
                            break;

                        case AnimationState.RIGHTSTAND:
                            {
                                _animatedTileTexture._playOnce = false;
                                _animatedTileTexture._enableEvents = false;
                                _animatedTileTexture.ChangeCheckRow(4);
                            }
                            break;

                        case AnimationState.LEFTDEAD:
                            {
                                if (AniCheckOn == false)
                                {
                                    AniCheckOn = true;

                                    _animatedTileTexture.SetIndex(0);
                                    _animatedTileTexture._playOnce = true;
                                    _animatedTileTexture._enableEvents = false;
                                    _animatedTileTexture.ChangeCheckRow(5);
                                }

                            }
                            break;

                        case AnimationState.RIGHTDEAD:
                            {
                                if (AniCheckOn == false)
                                {
                                    AniCheckOn = true;

                                    _animatedTileTexture.SetIndex(0);
                                    _animatedTileTexture._playOnce = true;
                                    _animatedTileTexture._enableEvents = false;
                                    _animatedTileTexture.ChangeCheckRow(6);
                                }

                            }
                            break;
                    }
                }
                break;

            case GameState.PLAY:
                {
                    this.transform.position = new Vector3(m_Zombie.gameObject.transform.position.x, m_Zombie.gameObject.transform.position.y + 0.2f, m_Zombie.gameObject.transform.position.z);

                    switch(AniState)
                    {
                        case AnimationState.LEFTWALK:
                            {
                                _animatedTileTexture._playOnce = false;
                                _animatedTileTexture._enableEvents = false;
                                _animatedTileTexture.ChangeCheckRow(1);
                            }
                            break;

                        case AnimationState.RIGHTWALK:
                            {
                                _animatedTileTexture._playOnce = false;
                                _animatedTileTexture._enableEvents = false;
                                _animatedTileTexture.ChangeCheckRow(2);
                            }
                            break;

                        case AnimationState.LEFTSTAND:
                            {
                                _animatedTileTexture._playOnce = false;
                                _animatedTileTexture._enableEvents = false;
                                _animatedTileTexture.ChangeCheckRow(3);
                            }
                            break;

                        case AnimationState.RIGHTSTAND:
                            {
                                _animatedTileTexture._playOnce = false;
                                _animatedTileTexture._enableEvents = false;
                                _animatedTileTexture.ChangeCheckRow(4);
                            }
                            break;

                        case AnimationState.LEFTDEAD:
                            {
                                if (AniCheckOn == false)
                                {
                                    AniCheckOn = true;

                                    _animatedTileTexture.SetIndex(0);
                                    _animatedTileTexture._playOnce = true;
                                    _animatedTileTexture._enableEvents = false;
                                    _animatedTileTexture.ChangeCheckRow(5);
                                }

                            }
                            break;

                        case AnimationState.RIGHTDEAD:
                            {
                                if (AniCheckOn == false)
                                {
                                    AniCheckOn = true;

                                    _animatedTileTexture.SetIndex(0);
                                    _animatedTileTexture._playOnce = true;
                                    _animatedTileTexture._enableEvents = false;
                                    _animatedTileTexture.ChangeCheckRow(6);
                                }

                            }
                            break;
                    }
                }
                break;

            case GameState.PAUSE:
                {
                    this.transform.position = new Vector3(m_Zombie.gameObject.transform.position.x, m_Zombie.gameObject.transform.position.y + 0.2f, m_Zombie.gameObject.transform.position.z);

                    switch (AniState)
                    {
                        case AnimationState.LEFTWALK:
                            {
                                _animatedTileTexture._playOnce = false;
                                _animatedTileTexture._enableEvents = false;
                                _animatedTileTexture.ChangeCheckRow(1);
                            }
                            break;

                        case AnimationState.RIGHTWALK:
                            {
                                _animatedTileTexture._playOnce = false;
                                _animatedTileTexture._enableEvents = false;
                                _animatedTileTexture.ChangeCheckRow(2);
                            }
                            break;

                        case AnimationState.LEFTSTAND:
                            {
                                _animatedTileTexture._playOnce = false;
                                _animatedTileTexture._enableEvents = false;
                                _animatedTileTexture.ChangeCheckRow(3);
                            }
                            break;

                        case AnimationState.RIGHTSTAND:
                            {
                                _animatedTileTexture._playOnce = false;
                                _animatedTileTexture._enableEvents = false;
                                _animatedTileTexture.ChangeCheckRow(4);
                            }
                            break;

                        case AnimationState.LEFTDEAD:
                            {
                                if (AniCheckOn == false)
                                {
                                    AniCheckOn = true;

                                    //_animatedTileTexture.SetIndex(0);
                                    _animatedTileTexture._playOnce = true;
                                    _animatedTileTexture._enableEvents = false;
                                    _animatedTileTexture.ChangeCheckRow(5);
                                }

                            }
                            break;

                        case AnimationState.RIGHTDEAD:
                            {
                                if (AniCheckOn == false)
                                {
                                    AniCheckOn = true;

                                    //_animatedTileTexture.SetIndex(0);
                                    _animatedTileTexture._playOnce = true;
                                    _animatedTileTexture._enableEvents = false;
                                    _animatedTileTexture.ChangeCheckRow(6);
                                }

                            }
                            break;
                    }
                }
                break;

            case GameState.EVENT:
                {
                    this.transform.position = new Vector3(m_Zombie.gameObject.transform.position.x, m_Zombie.gameObject.transform.position.y + 0.2f, m_Zombie.gameObject.transform.position.z);

                    switch (AniState)
                    {
                        case AnimationState.LEFTWALK:
                            {
                                _animatedTileTexture._playOnce = false;
                                _animatedTileTexture._enableEvents = false;
                                _animatedTileTexture.ChangeCheckRow(1);
                            }
                            break;

                        case AnimationState.RIGHTWALK:
                            {
                                _animatedTileTexture._playOnce = false;
                                _animatedTileTexture._enableEvents = false;
                                _animatedTileTexture.ChangeCheckRow(2);
                            }
                            break;

                        case AnimationState.LEFTSTAND:
                            {
                                _animatedTileTexture._playOnce = false;
                                _animatedTileTexture._enableEvents = false;
                                _animatedTileTexture.ChangeCheckRow(3);
                            }
                            break;

                        case AnimationState.RIGHTSTAND:
                            {
                                _animatedTileTexture._playOnce = false;
                                _animatedTileTexture._enableEvents = false;
                                _animatedTileTexture.ChangeCheckRow(4);
                            }
                            break;

                        case AnimationState.LEFTDEAD:
                            {
                                if (AniCheckOn == false)
                                {
                                    AniCheckOn = true;

                                    _animatedTileTexture.SetIndex(0);
                                    _animatedTileTexture._playOnce = true;
                                    _animatedTileTexture._enableEvents = false;
                                    _animatedTileTexture.ChangeCheckRow(5);
                                }

                            }
                            break;

                        case AnimationState.RIGHTDEAD:
                            {
                                if (AniCheckOn == false)
                                {
                                    AniCheckOn = true;

                                    _animatedTileTexture.SetIndex(0);
                                    _animatedTileTexture._playOnce = true;
                                    _animatedTileTexture._enableEvents = false;
                                    _animatedTileTexture.ChangeCheckRow(6);
                                }

                            }
                            break;
                    }
                }
                break;

            case GameState.GAMEOVER:
                {
                    this.transform.position = new Vector3(m_Zombie.gameObject.transform.position.x, m_Zombie.gameObject.transform.position.y + 0.2f, m_Zombie.gameObject.transform.position.z);

                    switch (AniState)
                    {
                        case AnimationState.LEFTWALK:
                            {
                                _animatedTileTexture._playOnce = false;
                                _animatedTileTexture._enableEvents = false;
                                _animatedTileTexture.ChangeCheckRow(1);
                            }
                            break;

                        case AnimationState.RIGHTWALK:
                            {
                                _animatedTileTexture._playOnce = false;
                                _animatedTileTexture._enableEvents = false;
                                _animatedTileTexture.ChangeCheckRow(2);
                            }
                            break;

                        case AnimationState.LEFTSTAND:
                            {
                                _animatedTileTexture._playOnce = false;
                                _animatedTileTexture._enableEvents = false;
                                _animatedTileTexture.ChangeCheckRow(3);
                            }
                            break;

                        case AnimationState.RIGHTSTAND:
                            {
                                _animatedTileTexture._playOnce = false;
                                _animatedTileTexture._enableEvents = false;
                                _animatedTileTexture.ChangeCheckRow(4);
                            }
                            break;

                        case AnimationState.LEFTDEAD:
                            {
                                if (AniCheckOn == false)
                                {
                                    AniCheckOn = true;

                                    _animatedTileTexture.SetIndex(0);
                                    _animatedTileTexture._playOnce = true;
                                    _animatedTileTexture._enableEvents = false;
                                    _animatedTileTexture.ChangeCheckRow(5);
                                }

                            }
                            break;

                        case AnimationState.RIGHTDEAD:
                            {
                                if (AniCheckOn == false)
                                {
                                    AniCheckOn = true;

                                    _animatedTileTexture.SetIndex(0);
                                    _animatedTileTexture._playOnce = true;
                                    _animatedTileTexture._enableEvents = false;
                                    _animatedTileTexture.ChangeCheckRow(6);
                                }

                            }
                            break;
                    }
                }
                break;

            case GameState.VICTORY:
                {
                    this.transform.position = new Vector3(m_Zombie.gameObject.transform.position.x, m_Zombie.gameObject.transform.position.y + 0.2f, m_Zombie.gameObject.transform.position.z);

                    switch (AniState)
                    {
                        case AnimationState.LEFTWALK:
                            {
                                _animatedTileTexture._playOnce = false;
                                _animatedTileTexture._enableEvents = false;
                                _animatedTileTexture.ChangeCheckRow(1);
                            }
                            break;

                        case AnimationState.RIGHTWALK:
                            {
                                _animatedTileTexture._playOnce = false;
                                _animatedTileTexture._enableEvents = false;
                                _animatedTileTexture.ChangeCheckRow(2);
                            }
                            break;

                        case AnimationState.LEFTSTAND:
                            {
                                _animatedTileTexture._playOnce = false;
                                _animatedTileTexture._enableEvents = false;
                                _animatedTileTexture.ChangeCheckRow(3);
                            }
                            break;

                        case AnimationState.RIGHTSTAND:
                            {
                                _animatedTileTexture._playOnce = false;
                                _animatedTileTexture._enableEvents = false;
                                _animatedTileTexture.ChangeCheckRow(4);
                            }
                            break;

                        case AnimationState.LEFTDEAD:
                            {
                                if (AniCheckOn == false)
                                {
                                    AniCheckOn = true;

                                    _animatedTileTexture.SetIndex(0);
                                    _animatedTileTexture._playOnce = true;
                                    _animatedTileTexture._enableEvents = false;
                                    _animatedTileTexture.ChangeCheckRow(5);
                                }

                            }
                            break;

                        case AnimationState.RIGHTDEAD:
                            {
                                if (AniCheckOn == false)
                                {
                                    AniCheckOn = true;

                                    _animatedTileTexture.SetIndex(0);
                                    _animatedTileTexture._playOnce = true;
                                    _animatedTileTexture._enableEvents = false;
                                    _animatedTileTexture.ChangeCheckRow(6);
                                }

                            }
                            break;
                    }
                }
                break;
        }
           
    }
}
