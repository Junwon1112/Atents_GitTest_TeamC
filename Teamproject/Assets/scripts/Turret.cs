using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public Transform target = null;
    public GameObject bullet = null;
    public float range = 5.0f;
    public float angle = 15.0f;
    public float fireInterval = 1.0f;

    
    Transform turretHead = null;
    Transform firePosition = null;
    float lookSpeed = 2.0f;
    float halfAngle = 0.0f;
    float fireCooltime = 0.0f;

    private void Awake()
    {
        turretHead = transform.Find("Head");
        firePosition = turretHead.Find("FirePosiition");
        halfAngle = angle * 0.5f;
    }

    private void Update()
    {
        Vector3 dir = target.position - transform.position;
        dir.y = 0.0f;

        fireCooltime -= Time.deltaTime;

        if (dir.sqrManitude < range * range)
        {
            turretHead.rotation = Quternion.Lerp(turretHead.rotation, Quaternion.LookRotation(dir), lookSpeed * Time.deltaTime);

            float angleBetween = Vector3.Angle(turretHead.forward, dir);
            if(angleBetween < halfAngle)
            {
                debug.Log($"Fire : {angleBetween}");
                if (fireCooltime < 0.0f;)
                {
                    Fire();
                    fireCooltime = fireInterval;
                }
            }
            
        }
    }

    void Fire()
    {
        Instantiate(bullet, firePosition.position, firePosition.rotation);
    }
}
