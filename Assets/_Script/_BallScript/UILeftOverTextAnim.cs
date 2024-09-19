using DG.Tweening;
using System;
using TMPro;
using UnityEngine;


public class UILeftOverTextAnim : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI leftoverText;
    [SerializeField] CountdownTimer countdownTimer;


    //public CanvasGroup starCanvasGroup; // For fading effect (optional)

    public void LeftOverUIAnimation()
    {
        DOTween.Restart("LeftOverText");
        leftoverText.text = "+" + Mathf.Round(countdownTimer.remainingTime / 2);
    }
    
}
