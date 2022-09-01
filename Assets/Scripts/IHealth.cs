using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHealth 
{
    float HP { get; }
    float MaxHP { get; }

    void TakeHeal(float heal);

    System.Action onHealthChange { get; set; }
}
