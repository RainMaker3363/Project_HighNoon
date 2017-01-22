using UnityEngine;
using System.Collections;

public class PietaEnemyAni : MonoBehaviour {

    private GameObject MainCamera;
    public PietaEnemy m_PietaEnemy;

    public AnimateTiledTexture _animatedTileTexture;

    private GameState State;
    private AnimationState EnemyAniState;

	// Use this for initialization
	void Start () {

        if (MainCamera == null)
        {
            MainCamera = GameObject.FindWithTag("MainCamera");
        }

        if (m_PietaEnemy == null)
        {
            Debug.Log("Enemy Null Object countion!");
        }

        if (_animatedTileTexture == null)
        {
            Debug.LogWarning("No animated tile texture script assigned!");
        }
        else
        {
            _animatedTileTexture.RegisterCallback(AnimationFinished);
        }

        

        // Quad 텍스쳐를 처리해주는 부분
        //this.transform.LookAt(MainCamera.transform.position);
        //this.transform.Rotate(new Vector3(0.0f, -180.0f, 0.0f));

        State = GameManager.NowGameState;
        EnemyAniState = m_PietaEnemy.GetenemyAniState();
        this.transform.position = new Vector3(m_PietaEnemy.gameObject.transform.position.x, m_PietaEnemy.gameObject.transform.position.y + 0.5f, m_PietaEnemy.gameObject.transform.position.z);

        // 애니메이션 기본 값
        _animatedTileTexture.ChangeCheckRow(16);
	}

    // This function will get called by the AnimatedTiledTexture script when the animation is completed if the EnableEvents option is set to true
    void AnimationFinished()
    {
        // The animation is finished
    }
	
	// Update is called once per frame
	void Update () {

        EnemyAniState = m_PietaEnemy.GetenemyAniState();

        State = GameManager.NowGameState;

        switch (State)
        {
            case GameState.START:
                {

                }
                break;

            case GameState.PLAY:
                {
                    this.gameObject.SetActive(m_PietaEnemy.gameObject.activeSelf);
                    //this.transform.LookAt(MainCamera.transform.position);
                    //this.transform.Rotate(new Vector3(0.0f, -180.0f, 0.0f));
                    this.transform.position = new Vector3(m_PietaEnemy.gameObject.transform.position.x, m_PietaEnemy.gameObject.transform.position.y + 0.5f, m_PietaEnemy.gameObject.transform.position.z);

                    switch (EnemyAniState)
                    {
                        case AnimationState.DOWNSTAND:
                            {
                                _animatedTileTexture.ChangeCheckRow(16);
                                //_animatedTileTexture.ChangeCheckRow(1);
                            }
                            break;

                        case AnimationState.UPSTAND:
                            {
                                _animatedTileTexture.ChangeCheckRow(2);
                            }
                            break;

                        case AnimationState.LEFTSTAND:
                            {
                                _animatedTileTexture.ChangeCheckRow(4);
                            }
                            break;

                        case AnimationState.RIGHTSTAND:
                            {
                                _animatedTileTexture.ChangeCheckRow(3);
                            }
                            break;

                        case AnimationState.LEFTDOWNSTAND:
                            {
                                _animatedTileTexture.ChangeCheckRow(6);
                            }
                            break;

                        case AnimationState.LEFTUPSTAND:
                            {
                                _animatedTileTexture.ChangeCheckRow(8);
                            }
                            break;

                        case AnimationState.RIGHTUPSTAND:
                            {
                                _animatedTileTexture.ChangeCheckRow(7);
                            }
                            break;

                        case AnimationState.RIGHTDOWNSTAND:
                            {
                                _animatedTileTexture.ChangeCheckRow(5);
                            }
                            break;

                        case AnimationState.LEFTWALK:
                            {
                                _animatedTileTexture.ChangeCheckRow(12);
                            }
                            break;

                        case AnimationState.RIGHTWALK:
                            {
                                _animatedTileTexture.ChangeCheckRow(11);
                            }
                            break;

                        case AnimationState.UPWALK:
                            {
                                _animatedTileTexture.ChangeCheckRow(10);
                            }
                            break;

                        case AnimationState.DOWNWALK:
                            {
                                _animatedTileTexture.ChangeCheckRow(9);
                            }
                            break;

                        case AnimationState.LEFTUPWALK:
                            {
                                _animatedTileTexture.ChangeCheckRow(16);
                            }
                            break;

                        case AnimationState.LEFTDOWNWALK:
                            {
                                _animatedTileTexture.ChangeCheckRow(14);
                            }
                            break;

                        case AnimationState.RIGHTUPWALK:
                            {
                                _animatedTileTexture.ChangeCheckRow(15);
                            }
                            break;

                        case AnimationState.RIGHTDOWNWALK:
                            {
                                _animatedTileTexture.ChangeCheckRow(13);
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
