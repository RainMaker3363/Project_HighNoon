using UnityEngine;
using System.Collections;

public class PlayerBullet : MonoBehaviour {

    private GameState State;
    private Player m_Player;

    private Vector3 MoveDir;
    private Vector3 MyPos;

    private IEnumerator DeadOrAliveCoroutine;

	// Use this for initialization
	void Start () {
        State = GameManager.NowGameState;

        MoveDir = Vector3.zero;
        MyPos = this.transform.position;

        if (m_Player == null)
        {
            m_Player = GameObject.FindWithTag("Player").GetComponent<Player>();
            MoveDir = new Vector3(m_Player.GetPlayerDirection().x, this.transform.position.y, m_Player.GetPlayerDirection().z);

        }
        else
        {
            MoveDir = new Vector3(m_Player.GetPlayerDirection().x, this.transform.position.y, m_Player.GetPlayerDirection().z);

        }

        //this.transform.Rotate(new Vector3(-90.0f, 0.0f, 0.0f));
        DeadOrAliveCoroutine = null;
        DeadOrAliveCoroutine = DeadProtocol(true);

        StopCoroutine(DeadOrAliveCoroutine);
        StartCoroutine(DeadOrAliveCoroutine);

        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("PlayerBullet"), LayerMask.NameToLayer("Player"), true);
        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("PlayerBullet"), LayerMask.NameToLayer("EnemyBullet"), true);
        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("PlayerBullet"), LayerMask.NameToLayer("PlayerBullet"), true);
        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("PlayerBullet"), LayerMask.NameToLayer("Item"), true);
        //Physics.IgnoreLayerCollision(LayerMask.NameToLayer("PlayerBullet"), LayerMask.NameToLayer("Wall"), true);
	}

    void OnEnable()
    {
        State = GameManager.NowGameState;

        MoveDir = Vector3.zero;
        MyPos = this.transform.position;

        if (m_Player == null)
        {
            m_Player = GameObject.FindWithTag("Player").GetComponent<Player>();
            MoveDir = new Vector3(m_Player.GetPlayerDirection().x, this.transform.position.y, m_Player.GetPlayerDirection().z);
        }
        else
        {
            MoveDir = new Vector3(m_Player.GetPlayerDirection().x, this.transform.position.y, m_Player.GetPlayerDirection().z);
        }

        DeadOrAliveCoroutine = null;
        DeadOrAliveCoroutine = DeadProtocol(true);

        StopCoroutine(DeadOrAliveCoroutine);
        StartCoroutine(DeadOrAliveCoroutine);

        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("PlayerBullet"), LayerMask.NameToLayer("Player"), true);
        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("PlayerBullet"), LayerMask.NameToLayer("EnemyBullet"), true);
        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("PlayerBullet"), LayerMask.NameToLayer("PlayerBullet"), true);
        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("PlayerBullet"), LayerMask.NameToLayer("Item"), true);


    }

    IEnumerator DeadProtocol(bool On = true)
    {
        yield return new WaitForSeconds(3.0f);

        this.gameObject.SetActive(false);

    }
	
	// Update is called once per frame
	void Update () {
        State = GameManager.NowGameState;

        switch (State)
        {
            case GameState.START:
                {

                }
                break;

            case GameState.PLAY:
                {
                    print("Distance : " + (this.transform.position - MyPos).magnitude);

                    if ((this.transform.position - MyPos).magnitude >= 6.0f)
                    {
                        DeadOrAliveCoroutine = null;
                        DeadOrAliveCoroutine = DeadProtocol(true);

                        StopCoroutine(DeadOrAliveCoroutine);

                        this.gameObject.SetActive(false);
                    }
                    else
                    {
                        this.transform.Translate((new Vector3(MoveDir.x, 0.0f, MoveDir.z) * 30.0f * Time.deltaTime));
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

    void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag.Equals("Enemy"))
        {
            this.gameObject.SetActive(false);
        }

        if (collision.transform.tag.Equals("Wall"))
        {
            //this.gameObject.SetActive(false);
        }
        
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag.Equals("Enemy"))
        {

            this.gameObject.SetActive(false);
        }
    }
}
