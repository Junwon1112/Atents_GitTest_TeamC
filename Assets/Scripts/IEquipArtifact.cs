using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEquipArtifact
{
    void EquipItem(IEquipTarget target);
    void UnEquipItem(IEquipTarget target);
    void ToggleEquipItem(IEquipTarget target);
}
