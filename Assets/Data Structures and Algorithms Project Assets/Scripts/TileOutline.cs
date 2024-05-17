using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileOutline : MonoBehaviour
{
    public static TileOutline _instance { get; private set; }

    private void Awake()
    {
        if(_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }

        _instance = this;
    }

    public void MoveOutline(Vector2 pos)
    {
        transform.position = pos;
    }
}
