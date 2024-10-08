using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedCounterVisual : MonoBehaviour
{
    [SerializeField] ClearCounter clearCounter;
    [SerializeField] GameObject selectedCounterVisual;
    
    void Start()
    {
        Player.Instance.OnSelectedCounterChanged += Player_OnSelectedCounterChanged;
    }

    private void Player_OnSelectedCounterChanged(object sender, Player.OnSelectCounterChangedEventArgs e)
    {
        if(e.selectedCounter == clearCounter)
        {
          ShowSelectedVisuals();
        }
        else
        {
          HideSelectedVisuals();
        }
    }
    void ShowSelectedVisuals(){
        selectedCounterVisual.SetActive(true);
    }
    void HideSelectedVisuals(){
        selectedCounterVisual.SetActive(false);
    }
   
}
