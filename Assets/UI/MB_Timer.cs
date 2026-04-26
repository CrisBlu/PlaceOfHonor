using System;
using System.Collections;
using UnityEngine;

public class MB_Timer : MonoBehaviour
{
    [SerializeField] int TimerSeconds;
    [SerializeField] TMPro.TextMeshProUGUI timer;
    [SerializeField] MB_GameManager manager;
    private int timerValue;




    public void StartTimer()
    {
        timerValue = TimerSeconds;
        int minutes = Mathf.FloorToInt(timerValue / 60F);
        int seconds = Mathf.FloorToInt(timerValue - minutes * 60);
        timer.text = string.Format("{0:0}:{1:00}", minutes, seconds);
        StartCoroutine(timerTick());
    }



    IEnumerator timerTick()
    {
        while (timerValue > 0)
        {
            timerValue--;
            int minutes = Mathf.FloorToInt(timerValue / 60F);
            int seconds = Mathf.FloorToInt(timerValue - minutes * 60);
            timer.text = string.Format("{0:0}:{1:00}", minutes, seconds);
            yield return new WaitForSeconds(1f);
        }

        manager.StopGame();
    }
}
