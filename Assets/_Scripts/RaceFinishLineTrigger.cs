using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceFinishLineTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        LevelController levelController = FindObjectOfType<LevelController>();
    }
}
