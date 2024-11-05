using UnityEngine;
using System.Collections.Generic;


public class PlateCounterVisuals : MonoBehaviour
{
    [SerializeField] PlateCounter plateCounter;
    [SerializeField] Transform plateVisuals;
    [SerializeField] Transform counterTopPoint;
    private List<GameObject> plateSpawnedList;

    private void Awake()
    {
        plateSpawnedList = new List<GameObject>();
    }
    private void Start()
    {
        plateCounter.OnPlateSpawned += PlateCounter_OnPlateSpawned;
    }

    private void PlateCounter_OnPlateSpawned(object sender, System.EventArgs e)
    {
        Transform plateVisual = Instantiate(plateVisuals, counterTopPoint);
        Debug.Log(plateVisuals.name);
        float plateOffSet = .1f;
        plateVisual.localPosition = new Vector3(0, plateOffSet * plateSpawnedList.Count);

        plateSpawnedList.Add(plateVisual.gameObject);
    }
}
