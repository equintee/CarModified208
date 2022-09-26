using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Car", menuName = "Car")]
public class Car : ScriptableObject
{
    public float movementSpeedZ;

    public int bodyWorkMaterialObjectIndex;
    public int bodyWorkMaterialIndex;

    public int spoilerObjectIndex;

    public int[] rimMaterialObjectsIndex;

    public int lightMaterialObjectIndex;
    public int[] lightMaterialIndex;

    public Transform[] exhaustPositions;


}
