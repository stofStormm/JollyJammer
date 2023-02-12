using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    #region pub vars
    public GameObject spawnObject;
    public float spawnRate = 1;
    public Transform[] positions;
    public Transform player;
    public float spawnSpeed=10;
    public int maxSpawns = 20;
    #endregion

    #region priv vars
    Timer spawnTimer;
    [HideInInspector]
    public int spawnCount = 0;
    #endregion

    void Start () {
        spawnTimer = gameObject.AddComponent(typeof(Timer)) as Timer;
        spawnTimer.Construct(ChooseSide, spawnRate);
        spawnTimer.loop = true;
        spawnTimer.Run();
    }
	
    void ChooseSide()
    {
        if (spawnCount < maxSpawns)
        {
            int rand = Random.Range(0, 4);

            switch (rand)
            {
                case 0: //TOP
                    Spawn(positions[3].position.x, positions[1].position.x, positions[0].position.y, 0);
                    break;

                case 1: //RIGHT
                    Spawn(positions[2].position.y, positions[0].position.y, positions[1].position.x, 1);
                    break;

                case 2: //BOTTOM
                    Spawn(positions[3].position.x, positions[1].position.x, positions[2].position.y, 0);
                    break;

                case 3: //LEFT
                    Spawn(positions[2].position.y, positions[0].position.y, positions[3].position.x, 1);
                    break;
            }
        }
    }
    void Spawn(float a1, float a2, float b, int rangeVar)
    {
        float a = Random.Range(a1, a2);
        GameObject instSpawn;
        if(rangeVar==0)
        {
            instSpawn=Instantiate(spawnObject, new Vector2(a, b), Quaternion.identity)as GameObject;
        }
        else
        {
            instSpawn = Instantiate(spawnObject, new Vector2(b, a), Quaternion.identity) as GameObject; ;
        }

        float randX= Random.Range(positions[3].position.x, positions[1].position.x);
        float randY= Random.Range(positions[2].position.y, positions[0].position.y);
        Vector2 dir = (new Vector2(randX, randY) - new Vector2(transform.position.x,transform.position.y)).normalized;
        instSpawn.GetComponent<Rigidbody2D>().velocity = dir* spawnSpeed;
        instSpawn.GetComponent<SpawnObj>().spawner = this;
        spawnCount++;
    }
}
