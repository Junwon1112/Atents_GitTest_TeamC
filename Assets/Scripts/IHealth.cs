using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHealth 
{
    float HP { get; set; }
    float MAXHP { get; }

    void TakeDamage(float damage);
    void TakeHeal(float heal);

    System.Action onHealthChange { get; set; }
}
