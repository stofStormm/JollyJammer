using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scroll : MonoBehaviour {

    #region pub vars
    public float scrollFactor= 0.5f;
    public Transform player;
    #endregion

    #region priv vars
    Vector2 playerPos;
    Vector2 playerOldPos;
    Vector2 playerDelta;
    #endregion

    void Start () {
        playerPos = player.position;
        playerOldPos = playerPos;
	}
	

	void FixedUpdate () {
        Scroller();
	}

    void Scroller()
    {
        playerOldPos = playerPos;
        playerPos = player.position;
        playerDelta = playerPos - playerOldPos;
        transform.localPosition -= new Vector3(playerDelta.x * scrollFactor, playerDelta.y * scrollFactor, 0);
    }
}
