using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using DG.Tweening;
using System.Threading.Tasks;

public class UIBarController : MonoBehaviour
{
    private Image bar;
    private Tweener fillSequence;
    public bool isFuelBar;
    private void Awake()
    {
        bar = GetComponent<Image>();
    }

    public async Task IncrementValue(float value, float speed)
    {
        if(isFuelBar)
            DOTween.Kill(2);
        float target = Mathf.Clamp(bar.fillAmount + value, 0, 1);
        fillSequence = DOTween.To(() => bar.fillAmount, x => bar.fillAmount = x, target, speed).SetSpeedBased();
        await fillSequence.AsyncWaitForCompletion();
    }

    public void DecrementFuelBar()
    {
        fillSequence = DOTween.To(() => bar.fillAmount, x => bar.fillAmount = x, 0.3f, 0.02f).SetSpeedBased().OnComplete(() => FindObjectOfType<LevelController>().endGame(false)).OnKill(() => Debug.Log("yarr")).SetId(2);
    }


}
