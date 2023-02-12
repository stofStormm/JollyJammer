using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ent : MonoBehaviour {

    #region pub vars
    //MOVEMENT
    [Header("MOVEMENT")]
    public float maxSpeed = 10;
    public float moveForce = 10;
    public GlobalScript gs;
    #endregion

    #region priv vars
    Rigidbody2D rb;
    [HideInInspector]
    public Animator anim;
    [HideInInspector]
    public AudioSource au;
    [HideInInspector]
    public SpriteRenderer sr;
    
    #endregion

    public virtual void Start () {
        Dep();
	}
	
	public virtual void Update () {

	}

    public virtual void FixedUpdate()
    {

    }

    public void MoveWithForce(float x, float y)
    {
        rb.AddForce(new Vector2(x, y) * moveForce);
    }

    public void MoveWithVel(float x, float y)
    {
        rb.velocity = new Vector2(x, y) * maxSpeed;
    }

    public virtual void Dep()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        au = GetComponent<AudioSource>();
        sr = GetComponent<SpriteRenderer>();
    }
}
