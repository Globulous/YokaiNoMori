using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewData", menuName = "ScriptableObjects/MyDataPion")]
public class PionSO : ScriptableObject
{
    public string pionName; // 1 Le koropokkuru , 2 Le kitsune , 3 Le tanuki , 4 Le D , 5 kodama samura

    public int depalcementType;

    public MovementType movementType;

}