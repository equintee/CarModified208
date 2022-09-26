using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Car", menuName = "Car")]
public class Car : ScriptableObject
{
    public float movementSpeedZ;

    [HideInInspector] public Transform[] exhaustPositions;

    public int[] bodyWorkMaterialIndex;
    public int[] rimMaterialIndex;
    public int[] exhaustMaterialIndex;
    public int[] lightsIndex;


}
