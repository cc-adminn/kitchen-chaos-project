using UnityEngine;
using System;

public class PlateCounter : BaseCounter
{
    
    public event EventHandler OnPlateSpawned;

    float plateSpawnTime;
    float plateSpawnTimeMax = 4f;
    int plateCount;
    int plateCountMax = 4;

    private void Update()
    {
        plateSpawnTime += Time.deltaTime;
        if (plateSpawnTime > plateSpawnTimeMax)
        {
            plateSpawnTime = 0;
            if (plateCount < plateCountMax)
            {
                plateCount++;
                OnPlateSpawned?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
