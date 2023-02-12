using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerEnt : Ent {

    #region pub vars
    //MOVEMENT
    [Header("MOVEMENT")]
    public float rotateSpeed; //degrees/s
    public ParticleSystem jetEmitter;
    public float squishScale = .8f;
    //SOUNDS
    [Header("Sounds")]
    AudioClip auMove;

    //JAM
    [Header("JAM")]
    public float maxJam = 100;
    public float jamDrawRate = 1;
    public Slider jamSlider;
    public float jamGain = 10;
    public Text jamCountText;

    //MISC
    [Header("MISC")]
    public GameObject deathEmit;
    public ScreenShake ss;
    
    #endregion

    #region priv vars
    Camera cam;
    bool moving = false;
    Vector2 target=new Vector2(0,0);
    Vector2 dir = new Vector2(0, 0);
    GlobalScript gs;
    float curJam;
    float angle=0;
    int jamCount = 0;
    bool startMove = false;
    #endregion

    public override void Start()
    {
        curJam = maxJam;
        jamCountText.text= "x 0";
        base.Start();
    }

    public override void Update()
    {
        if (Input.GetMouseButton(0))
        {
            GetMouseInput();
        }
        else
        {
            StopMove();
        }
        FaceFront();
        base.Update();
    }

    public override void FixedUpdate()
    {
        if (startMove)
        {
            //UseJam();
        }
        if(moving)
        {
            MoveToMouse();
            
        }
        if (!moving)
        {
            Unsquish();
        }
        base.FixedUpdate();
    }

    void MoveToMouse()
    {
        dir = (target - new Vector2(transform.position.x,transform.position.y)).normalized;

        Vector2 velDir = new Vector2(0, 0);
        velDir.x = Mathf.Cos(Mathf.Deg2Rad * transform.localRotation.eulerAngles.z);
        velDir.y = Mathf.Sin(Mathf.Deg2Rad * transform.localRotation.eulerAngles.z);
        MoveWithVel(velDir.x, velDir.y);
        if(!startMove)
        {
            jetEmitter.Play();
            au.Play();
            startMove = true;
        }
        
        Squish();
    }
    void StopMove()
    {
        moving = false;
    }
    void GetMouseInput()
    {
        target = cam.ScreenToWorldPoint(Input.mousePosition);
        moving = true;
    }
    void FaceFront()
    {
        angle = 0;
        angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (angle<0)
        {
            angle = 360 + angle;
        }

        Vector3 curAngle = transform.localRotation.eulerAngles;
        float z = curAngle.z;
        float f = -1;
        if (Mathf.Abs(angle - z) > 180)
        {
            f = 1;
        }
        else
        {
            f = -1;
        }

        if (Mathf.Abs(angle - z) > 2)
        {
            if (angle > z)
            {
                z += (rotateSpeed * Time.deltaTime) * -f;
            }
            else
            {
                z += (rotateSpeed * Time.deltaTime) * f;
            }
        }

        transform.localRotation = Quaternion.Euler(new Vector3(0,0,z));
    }
    void UseJam()
    {
        if (curJam - (jamDrawRate * Time.deltaTime) >= 0)
        {
            curJam -= jamDrawRate * Time.deltaTime;
            jamSlider.value = curJam / maxJam;
        }
        else
        {
            curJam = 0;
            jamSlider.value = 0;
            gs.GameOver();
        }
    }
    void GainJam()
    {
        if (curJam + jamGain <= 100)
        {
            curJam += jamGain;
            
        }
        else
        {
            curJam = 100;
        }
        ss.StartShake();
        jamSlider.value = curJam / maxJam;
        jamCount++;
        jamCountText.text = "x " + jamCount.ToString();
    }
    void Squish()
    {
        /*float scale = Mathf.Lerp(transform.localScale.y, squishScale,.1f);
        transform.localScale = new Vector3(2 - scale, scale, 1);*/
        anim.SetBool("Moving", true);
    }
    void Unsquish()
    {
        /*if (transform.localScale.y < .99)
        {
            float scale = Mathf.Lerp(transform.localScale.y, 1, .1f);
            transform.localScale = new Vector3(2-scale, scale, 1);
        }*/
        //anim.SetBool("Moving", false);
    }
    void Die()
    {
        sr.sprite = null;
        jetEmitter.Stop();
        Instantiate(deathEmit, transform.position, Quaternion.identity);
        Timer endTimer = gameObject.AddComponent(typeof (Timer)) as Timer;
        endTimer.Construct(GameOver, 1);
        endTimer.Run();
        gs.CheckScore(jamCount);
        
    }
    void GameOver()
    {
        gs.GameOver();
    }

    public override void Dep()
    {
        cam = Camera.main;
        base.Dep();
        gs = GameObject.Find("GlobalController").GetComponent<GlobalScript>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Jam")
        {
            GainJam();
            Destroy(other.gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Asteroid")
        {
            Die();
        }
    }


}
