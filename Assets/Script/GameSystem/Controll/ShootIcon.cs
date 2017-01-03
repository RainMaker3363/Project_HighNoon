using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class ShootIcon : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{

    private Image img;

    public Sprite[] ShootIcons;

	// Use this for initialization
	void Start () {

        img = GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}

        // 터치가 드래그(Drag) 했을때 호출 되는 함수
    public virtual void OnDrag(PointerEventData ped)
    {

    }


    // 터치를 하고 있을 대 발생하는 함수
    public virtual void OnPointerDown(PointerEventData ped)
    {
        Debug.Log("Shoot Button");
    }

    // 터치에서 손을 땠을때 발생하는 함수
    public virtual void OnPointerUp(PointerEventData ped)
    {

    }
}
