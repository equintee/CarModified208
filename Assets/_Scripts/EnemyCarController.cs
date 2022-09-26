using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCarController : MonoBehaviour
{
    private float movementSpeedZ;

    private void Awake()
    {
        movementSpeedZ = FindObjectOfType<PlayerController>().movementSpeedZ;
    }
    public void StartMovement()
    {
        Transform raceFinishLine = FindObjectOfType<RaceFinishLineTrigger>().transform;
        LevelController levelController = FindObjectOfType<LevelController>();
        transform.DOMoveZ(raceFinishLine.position.z, movementSpeedZ).SetSpeedBased().SetEase(Ease.Linear).OnComplete( () => levelController.endGame(false));
    }

    public void StopMovement()
    {
        DOTween.Kill(transform);
    }

}
