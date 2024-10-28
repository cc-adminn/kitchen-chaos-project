using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ProgressBarUI : MonoBehaviour
{
    [SerializeField] Image barImage;
    [SerializeField] CuttinggCounter cuttingCounter;

    private void Start()
    {
        cuttingCounter.OnProgressBarUpdate += CuttinggCounter_OnProgressBarUpdate;
        barImage.fillAmount = 0f;
        Hide();
    }

    private void CuttinggCounter_OnProgressBarUpdate(object sender, CuttinggCounter.OnProgressBarChangedEventArgs e)
    {
        barImage.fillAmount = e.progressNormalized;

        if (e.progressNormalized == 0 || e.progressNormalized == 1)
        {
            Hide();
        }
        else
        {
            Show();
        }
    }


    void Show()
    {
        gameObject.SetActive(true);
    }

    void Hide()
    {
        gameObject.SetActive(false);
    }
}
