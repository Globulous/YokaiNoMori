using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Flags]
public enum MovementType
{
    Forward_Left = 1 << 0,
    Forward_Mid = 1 << 1,
    Forward_Right = 1 << 2,
    Mid_Left = 1 << 3,
    Mid_Right = 1 << 4,
    Back_Left = 1 << 5,
    Back_Mid = 1 << 6,
    Back_Right = 1 << 7,
}
