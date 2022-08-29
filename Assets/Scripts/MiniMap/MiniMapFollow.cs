using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapFollow : MonoBehaviour
{
    //미니맵이 플레이어를 쫓아다니게 해주는 스크립트

    //public float cameraSpeed = 5.0f;
    

    public GameObject player;

    /// <summary>
    /// 계속 Update해주면서 플레이어를 따라간다
    /// </summary>
    /// <param name="player">플레이어를 할당</param>
    private void Update()
    {
        transform.position = player.transform.position + new Vector3(0, 80, 0);
    }
}
