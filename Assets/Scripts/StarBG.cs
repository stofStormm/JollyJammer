using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
[ExecuteInEditMode]
public class StarBG : MonoBehaviour {

    #region pub vars
    public Tile[] tiles;
    public int startX;
    public int startY;
    #endregion

    #region priv vars
    Tilemap tm;
    #endregion

    void Start () {
        tm = GetComponent<Tilemap>();
        Gen();
	}

    void Gen()
    {
        int sizeX = (Mathf.Abs(startX) * 2);
        int sizeY = (Mathf.Abs(startY) * 2);

        for(int i=startX;i<startX+sizeX;i++)
        {
            for (int j = startY; j < startY + sizeY; j++)
            {
                int rand = Random.Range(0, 4);
                tm.SetTile(new Vector3Int(i, j, 0), tiles[rand]);
            }
        }

    }

}
