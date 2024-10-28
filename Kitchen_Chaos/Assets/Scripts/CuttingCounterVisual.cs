using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounterVisual : MonoBehaviour
{
    static string CUT = "Cut";

    [SerializeField] CuttinggCounter cuttinggCounter;


    Animator animator;




    private void Start()
    {
        animator = GetComponent<Animator>();
        cuttinggCounter.OnCutVisuals += CuttinggCounter_OnCutVisuals;
    }

    private void CuttinggCounter_OnCutVisuals(object sender, System.EventArgs e)
    {
        animator.SetTrigger(CUT);
    }
}
