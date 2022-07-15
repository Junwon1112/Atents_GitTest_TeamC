using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHealth 
{
    float HP { get; }
    float MAXHP { get; }

    void TakeDamage(float damage);

    System.Action onHealthChange { get; set; }
}
