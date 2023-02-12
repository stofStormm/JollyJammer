using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode]
public class CameraControl : MonoBehaviour {

    #region pub vars
    public float ppu = 16;
    public float screenHeight = 400;
    public Transform player;
    public float followSpeed = 10;
    public float xBuffer = 8;
    public float yBuffer = 6;
    public bool camBuffer=false;
    #endregion

    #region priv vars
    Camera cam;
    float dX;
    float dY;

    Vector2 playerPos;
    Vector2 playerPosOld;
    Vector2 dPlayerPos;
    #endregion

    void Start () {
        cam = Camera.main;
        playerPos = player.transform.position;
        playerPosOld = playerPos;
        SetSize();
	}
	
	void Update () {
        SetSize();
    }

    void FixedUpdate()
    {
        if (camBuffer)
        {
            FollowPlayer();
        }
        else
        {
            transform.position = new Vector3(player.position.x, player.position.y, -10);
        }
    }

    void SetSize()
    {
        cam.orthographicSize = (screenHeight / ppu) * .5f;
    }

    void FollowPlayer()
    {
        playerPosOld = playerPos;
        playerPos = player.transform.position;
        dPlayerPos = playerPos - playerPosOld;

        dX = transform.position.x - playerPos.x;
        dY = transform.position.y - playerPos.y;

        if(Mathf.Abs(dX)>xBuffer || Mathf.Abs(dY) >yBuffer)
        {
            transform.position += new Vector3(dPlayerPos.x, dPlayerPos.y, 0);
        }

    }
}
