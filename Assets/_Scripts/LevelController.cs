using Cinemachine;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public struct UI
{
    public GameObject tapToStart;
    public UIBarController repairBar;
    public UIBarController fuelBar;
    public GameObject gasPedal;
    public GameObject raceStartCounter;
    public GameObject winUI;
    public GameObject loseUI;
}
public class LevelController : MonoBehaviour
{
    public float sensivity;
    public UI ui;
    public float gasPerCollectable;
    [Range(0, 1)] public float gasDecreaseSpeed;
    [Range(0, 1)] public float barIncreaseSpeed;
    public Transform playerRacingPlatformPosition;

    private PlayerController playerController;

    private void Awake()
    {
        playerController = FindObjectOfType<PlayerController>();
    }

    private void Update()
    {
        if(Input.touchCount > 0) {
            this.enabled = false;
            ui.tapToStart.SetActive(false);
            playerController.playerEvents += playerController.playerPlatformMovement;
            playerController.SpinWheels();
            ui.fuelBar.DecrementFuelBar();
        }
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

        playerController.SpinWheels();
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
            ui.winUI.SetActive(true);
        else
            ui.loseUI.SetActive(true);

        playerController.playerEvents -= playerController.playerMovementInRacingPlatform;
        playerController.playerEvents -= playerController.playerPlatformMovement;
        FindObjectOfType<EnemyCarController>().StopMovement();

    }

    public void changeScene()
    {
        int level = SceneManager.GetActiveScene().buildIndex;
        level++;
        level %= SceneManager.sceneCountInBuildSettings;
        SceneManager.LoadScene(level);
    }
}
