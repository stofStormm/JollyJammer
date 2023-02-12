using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObj : MonoBehaviour {

    #region pub vars
    public float maxRotSpeed = 1;
    public float minRotSpeed = .5f;
    public float maxScale = 1.5f;
    public float minScale = 0.5f;
    public bool scales = true;
    #endregion

    #region priv vars
    float scale = 0;
    float rotSpeed = 0;
    [HideInInspector]
    public Spawner spawner;
    #endregion

    void Start () {
        if (scales)
        {
            scale = Random.Range(minScale, maxScale);
            transform.localScale = new Vector2(scale, scale);
        }
        rotSpeed= Random.Range(minRotSpeed, maxRotSpeed);
    }
	

	void FixedUpdate () {
        transform.Rotate(new Vector3(0, 0, rotSpeed * Time.deltaTime));
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        print("col");
        if(other.tag=="KillZone")
        {
            spawner.spawnCount--;
            Destroy(gameObject);
        }
    }
}
