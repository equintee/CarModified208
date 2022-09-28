using Cinemachine;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

[System.Serializable]
public struct UI
{
    public UIBarController repairBar;
    public GameObject gasPedal;
    public GameObject raceStartCounter;
}
public class LevelController : MonoBehaviour
{
    public float sensivity;
    public UI ui;
    public Transform playerRacingPlatformPosition;

    private PlayerController playerController;

    private void Awake()
    {
        playerController = FindObjectOfType<PlayerController>();
    }

    public async void StartRacing()
    {
        CinemachineStateDrivenCamera cinemachine = FindObjectOfType<CinemachineStateDrivenCamera>();
        Transform playerTransform = playerController.transform;

        playerController.StopWheelsRotation();
        playerController.playerEvents -= playerController.playerPlatformMovement;

        cinemachine.m_DefaultBlend.m_Time = Vector3.Distance(playerRacingPlatformPosition.position, playerTransform.position) / playerController.movementSpeedZ;
        cinemachine.m_AnimatedTarget.Play("racingCamera");
        
        await playerTransform.DOMove(playerRacingPlatformPosition.position, playerController.movementSpeedZ).SetSpeedBased().AsyncWaitForCompletion();
        await StartCountingForFinalRace(3);

        ui.gasPedal.SetActive(true);
        FindObjectOfType<EnemyCarController>().StartMovement();
    }

    private async Task StartCountingForFinalRace(int counter)
    {
        TextMeshProUGUI startText = ui.raceStartCounter.GetComponent<TextMeshProUGUI>();
        startText.text = counter--.ToString();

        await ui.raceStartCounter.transform.DOScale(new Vector3(15, 15, 15), 0.5f).SetEase(Ease.OutSine).AsyncWaitForCompletion();
        await ui.raceStartCounter.transform.DOScale(new Vector3(0, 0, 0), 0.5f).SetEase(Ease.OutSine).AsyncWaitForCompletion();

        if (counter != 0)
            await StartCountingForFinalRace(counter);
    }

    public void endGame(bool playerWin)
    {
        if (playerWin)
            //winUI;
            Debug.Log("win");
        else
            //LoseUI
            Debug.Log("lose");

        playerController.playerEvents -= playerController.playerMovementInRacingPlatform;
        FindObjectOfType<EnemyCarController>().StopMovement();



    }
}
