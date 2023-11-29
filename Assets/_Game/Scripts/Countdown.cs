using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Countdown : MonoBehaviour
{
    [SerializeField] private Text timeTxt;
    private float timer = Constant.TIME_TO_PLAY;

    private void OnEnable() {
        timer = Constant.TIME_TO_PLAY;
    }

    private IEnumerator WaitForHide() {
        yield return CacheComponent.GetWFS(Constant.TIME_TO_HIDE_COUNTDOWN);
        gameObject.SetActive(false);
    }
    private void Update() {
        if (timer > 0) {
            timer -= Time.deltaTime;
        }

        int second = Mathf.CeilToInt(timer % 60);
        string text = second > 0 ? second.ToString() : "Start";
        timeTxt.text = second.ToString(text);

        if (second <= 0) {
            StartCoroutine(WaitForHide());
        }
    }
}
