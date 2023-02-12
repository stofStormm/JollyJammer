using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour {

    #region pub vars
    public float rotSpeed=1;
    #endregion

    #region priv vars

    #endregion

	void FixedUpdate () {
        transform.Rotate(new Vector3(0, 0, rotSpeed * Time.deltaTime));
    }
}
