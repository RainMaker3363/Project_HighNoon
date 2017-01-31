using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AnimateTiledTexture : MonoBehaviour {

    public int _columns = 1;                                // 텍스쳐의 열 개수
    public int _rows = 1;                                   // 텍스쳐의 행 개수
    public int checkAgainst = 0;                            // 텍스쳐 애니메이션을 어느 정도까지 재생할지의 여부 (열)
    public int checkColumns = 0;                            // 혹시나 애니메이션의 프레임이 다를 경우 제한시킬 열의 수
    public float checkRows = 0.0f;                               // 텍스쳐 애니메이션을 어느 정도까지 재생할지의 여부 (행)
    public int PlayOnceIndex = 0;
    public Vector2 _scale = new Vector2(1.0f, 1.0f);        // 텍스쳐의 크기, 0은 안되며 -값이라면 뒤집는다.
    public Vector2 _offset = Vector2.zero;                  // 텍스쳐의 오프셋을 정해준다.
    public Vector2 _buffer = Vector2.zero;                  // You can use this to buffer frames to hide unwanted grid lines or artifacts
    public float _framesPerSecond = 10.0f;                  // 텍스쳐 애니메이션 재생 프레임 속도
    public bool _playOnce = false;                          // 텍스쳐 애니메이션 반복 여부
    public bool _disableUponCompletion = false;             // 텍스쳐 애니메이션이 끝날경우 랜더링을 계속 할 건지의 여부
    public bool _enableEvents = false;                      // 텍스쳐 애니메이션이 끝날경우 이벤트 발생 여부
    public bool _playOnEnable = true;                       // 오브젝트 활성화 시 계속 재생 시킬 여부
    public bool _newMaterialInstance = false;               // true일 경우 새로운 Material을 생성해준다.

    private int _index = 0;                                 // Keeps track of the current frame 
    public int ChangeRow = 0;
    private Vector2 _textureSize = Vector2.zero;            // Keeps track of the texture scale 
    private Material _materialInstance = null;              // Material instance of the material we create (if needed)
    private bool _hasMaterialInstance = false;              // A flag so we know if we have a material instance we need to clean up (better than a null check i think)
    private bool _isPlaying = false;                        // A flag to determine if the animation is currently playing


    public delegate void VoidEvent();                       // Event Delegate
    private List<VoidEvent> _voidEventCallbackList;         // A list of functions we need to call if events are enabled


    public void Play()
    {
        // 애니메이션이 시작되고 있다면 멈춘다.
        if (_isPlaying)
        {
            StopCoroutine("updateTiling");
            _isPlaying = false;
        }

        // Make sure the gameObject.GetComponent<MeshRenderer>() is enabled
        gameObject.GetComponent<MeshRenderer>().enabled = true;

        //Because of the way textures calculate the y value, we need to start at the max y value
        _index = _columns;

        checkAgainst = _columns;
        checkColumns = _columns;
        checkRows = 1 - (1f / _rows);
        ChangeRow = 0;
        PlayOnceIndex = 0;

        // Start the update tiling coroutine
        StartCoroutine(updateTiling());
    }

    // Use this function to register your callback function with this script
    public void RegisterCallback(VoidEvent cbFunction)
    {
        // If events are enabled, add the callback function to the event list
        if (_enableEvents)
        {
            _voidEventCallbackList.Add(cbFunction);
        }
        else
        {
            Debug.LogWarning("AnimateTiledTexture: You are attempting to register a callback but the events of this object are not enabled!");
        }
         
    }

    // Use this function to unregister a callback function with this script
    public void UnRegisterCallback(VoidEvent cbFunction)
    {
        // If events are enabled, unregister the callback function from the event list
        if (_enableEvents)
        {
            _voidEventCallbackList.Remove(cbFunction);
        }
        else
        {
            Debug.LogWarning("AnimateTiledTexture: You are attempting to un-register a callback but the events of this object are not enabled!");
        }
            
    }

    public void ChangeMaterial(Material newMaterial, bool newInstance = false)
    {
        if (newInstance)
        {
            
            // First check our material instance, if we already have a material instance
            // and we want to create a new one, we need to clean up the old one
            if (_hasMaterialInstance)
                Object.Destroy(gameObject.GetComponent<MeshRenderer>().sharedMaterial);
            
            // create the new material
            _materialInstance = new Material(newMaterial);
           
            
            // Assign it to the gameObject.GetComponent<MeshRenderer>()
            gameObject.GetComponent<MeshRenderer>().sharedMaterial = _materialInstance;

            // Set the flag
            _hasMaterialInstance = true;
        }
        else // if we dont have create a new instance, just assign the texture
            gameObject.GetComponent<MeshRenderer>().sharedMaterial = newMaterial;

        // We need to recalc the texture size (since different material = possible different texture)
        CalcTextureSize();

        
        // Assign the new texture size
        this.
        gameObject.GetComponent<MeshRenderer>().sharedMaterial.SetTextureScale("_MainTex", _textureSize);
    }

    // 몇번째 프레임부터 재생시킬지의 여부
    public void ChangeCheckColumn(int ChangeCol)
    {
        checkColumns = ChangeCol;
    }

    public void ChangeCheckRow(int _ChRow)
    {
        if(_ChRow >= _rows)
        {
            ChangeRow = _rows;
        }
        else if(_ChRow <= 0)
        {
            ChangeRow = 0;
        }
        else
        {
            ChangeRow = _ChRow;
        }
        
        //checkRows = ((1 - ((1f / _columns) * (ChangeRow - 1))));
    }

    // 맨 처음 초기화
    private void Awake()
    {
        // 이벤트가 있다면 할당 시켜준다.
        if (_enableEvents)
            _voidEventCallbackList = new List<VoidEvent>();

        //Create the material instance, if needed. else, just use this function to recalc the texture size
        ChangeMaterial(gameObject.GetComponent<MeshRenderer>().sharedMaterial, _newMaterialInstance);
    }

    // 객체가 소멸될때 호출됨
    private void OnDestroy()
    {
        // If we wanted new material instances, we need to destroy the material
        if (_hasMaterialInstance)
        {
            Object.Destroy(gameObject.GetComponent<MeshRenderer>().sharedMaterial);
            _hasMaterialInstance = false;
        }
    }

    // Handles all event triggers to callback functions
    private void HandleCallbacks(List<VoidEvent> cbList)
    {
        // For now simply loop through them all and call yet.
        for (int i = 0; i < cbList.Count; ++i)
            cbList[i]();
    }

    private void OnEnable()
    {

        CalcTextureSize();

        if (_playOnEnable)
        {
            Play();
        }
            
    }

    private void CalcTextureSize()
    {
        //set the tile size of the texture (in UV units), based on the rows and columns
        _textureSize = new Vector2(1f / _columns, 1f / _rows);

        // Add in the scale
        _textureSize.x = _textureSize.x / _scale.x;
        _textureSize.y = _textureSize.y / _scale.y;

        // Buffer some of the image out (removes gridlines and stufF)
        _textureSize -= _buffer;
    }

    // The main update function of this script
    private IEnumerator updateTiling()
    {
        _isPlaying = true;

        // This is the max number of frames
        //int checkAgainst = (_rows * _columns);

        while (true)
        {


            if (_playOnce == false)
            {
                // If we are at the last frame, we need to either loop or break out of the loop
                if (_index >= checkAgainst)
                {
                    _index = 0;  // Reset the index


                }

                if (ChangeRow <= 0)
                {
                    checkRows = 0.0f;

                }
                else if (ChangeRow >= _rows)
                {
                    checkRows = 1.0f;//(1 - ((1f / _columns)));

                }
                else
                {
                    checkRows = (1 - (((1f / _rows) * (ChangeRow))));
                }


                // Apply the offset in order to move to the next frame
                ApplyOffset();

                //Increment the index
                _index++;
            }
            else
            {
                if (PlayOnceIndex >= checkColumns)
                {
                    // We are done with the coroutine. Fire the event, if needed
                    if (_enableEvents)
                    {
                        HandleCallbacks(_voidEventCallbackList);
                    }


                    if (_disableUponCompletion)
                    {
                        gameObject.gameObject.GetComponent<MeshRenderer>().enabled = false;
                    }


                    // turn off the isplaying flag
                    _isPlaying = false;

                    // Break out of the loop, we are finished
                    yield break;
                }
                else
                {
                    //checkAgainst = _columns;    // We need to loop through one more row
                    //checkAgainst = checkColumns;
                    //PlayOnceIndex++;
                }

                if (ChangeRow <= 0)
                {
                    checkRows = 0.0f;

                }
                else if (ChangeRow >= _rows)
                {
                    checkRows = 1.0f;//(1 - ((1f / _columns)));

                }
                else
                {
                    checkRows = (1 - (((1f / _rows) * (ChangeRow))));
                }


                // Apply the offset in order to move to the next frame
                ApplyOffset();

                //Increment the index
                PlayOnceIndex++;
            }


            // Wait a time before we move to the next frame. Note, this gives unexpected results on mobile devices
            yield return new WaitForSeconds(1f / _framesPerSecond);
        }
    }

    private void ApplyOffset()
    {
        if(_playOnce == false)
        {
            //split into x and y indexes. calculate the new offsets
            Vector2 offset = new Vector2((float)_index / _columns - (_index / _columns), //x index
                                          checkRows);//1 - ((_index / _columns) / (float)_rows));    //y index

            // Reset the y offset, if needed
            if (offset.y == 1)
                offset.y = 0.0f;

            // If we have scaled the texture, we need to reposition the texture to the center of the object
            offset.x += ((1f / _columns) - _textureSize.x) / 2.0f;
            offset.y += ((1f / _rows) - _textureSize.y) / 2.0f;

            // Add an additional offset if the user does not want the texture centered
            offset.x += _offset.x;
            offset.y += _offset.y;

            // Update the material
            gameObject.GetComponent<MeshRenderer>().sharedMaterial.SetTextureOffset("_MainTex", offset);
        }
        else
        {
            //split into x and y indexes. calculate the new offsets
            Vector2 offset = new Vector2((float)PlayOnceIndex / _columns - (PlayOnceIndex / _columns), //x index
                                          checkRows);//1 - ((_index / _columns) / (float)_rows));    //y index

            // Reset the y offset, if needed
            if (offset.y == 1)
                offset.y = 0.0f;

            // If we have scaled the texture, we need to reposition the texture to the center of the object
            offset.x += ((1f / _columns) - _textureSize.x) / 2.0f;
            offset.y += ((1f / _rows) - _textureSize.y) / 2.0f;

            // Add an additional offset if the user does not want the texture centered
            offset.x += _offset.x;
            offset.y += _offset.y;

            // Update the material
            gameObject.GetComponent<MeshRenderer>().sharedMaterial.SetTextureOffset("_MainTex", offset);
        }



    }

}
