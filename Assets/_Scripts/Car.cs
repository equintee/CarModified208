using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Car", menuName = "Car")]
public class Car : ScriptableObject
{
    public float movementSpeedZ;

    public int[] rimMaterialIndex;
    public int[] exhaustMaterialIndex;
    public int[] lightsIndex;

}
