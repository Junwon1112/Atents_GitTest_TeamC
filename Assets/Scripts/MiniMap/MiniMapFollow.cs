using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapFollow : MonoBehaviour
{
    //public float cameraSpeed = 5.0f;

    public GameObject player;

    private void Update()
    {
        transform.position = player.transform.position + new Vector3(0, 80, 0);
    }
}
