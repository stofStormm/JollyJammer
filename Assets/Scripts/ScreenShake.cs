using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShake : MonoBehaviour {

    #region pub vars
    public bool rotate=true;
    public float rotSpeed = 1;
    public bool translate=false;
    public float transSpeed = 1;
    public float shakeTime = .25f;
    #endregion

    #region priv vars
    Timer shakeTimer;
    Timer swapTimer;
    bool shaking = false;
    float dir = 1;
    #endregion

    void Start () {
        shakeTimer = gameObject.AddComponent(typeof(Timer)) as Timer;
        shakeTimer.Construct(StopShake, shakeTime);
        swapTimer = gameObject.AddComponent(typeof(Timer)) as Timer;
        swapTimer.Construct(DirChange, shakeTime/8);
        //swapTimer.loop = true;
    }
	

	void Update () {
        Shake();
	}

    public void StartShake()
    {
        if (!shaking)
        {
            shaking = true;
        }
        shakeTimer.Run();
        swapTimer.Run();
    }

    void Shake()
    {
        if(shaking)
        {
            if(rotate)
            {
                RotateShake();
            }
            if(translate)
            {
                TranslateShake();
            }
        }
    }

    void RotateShake()
    {
        transform.Rotate(new Vector3(0,0,dir * rotSpeed * Time.deltaTime));
    }

    void TranslateShake()
    {
        transform.position += new Vector3(dir * transSpeed * Time.deltaTime, dir * transSpeed * Time.deltaTime, 0);
    }

    void StopShake()
    {
        shaking = false;
        swapTimer.Stop();
        swapTimer.Construct(DirChange, shakeTime / 16);
    }

    void DirChange()
    {
        dir *= -1;
        swapTimer.Construct(DirChange, shakeTime / 8);
    }
}
