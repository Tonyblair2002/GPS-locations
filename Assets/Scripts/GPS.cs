﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GPS : MonoBehaviour {

    public static float longitude;
    public static float latitude;

    public static GPS instance;


    private void Awake()
    {
        if (instance == null){
            instance = this;

        }
        DontDestroyOnLoad(gameObject);
    }

    private void FixedUpdate()
    {
        if (Input.location.isEnabledByUser){
            latitude = Input.location.lastData.latitude;
            longitude = Input.location.lastData.longitude;


        }
    }

    IEnumerator Start()
    {
        // First, check if user has location service enabled
        if (!Input.location.isEnabledByUser)
            yield break;

        // Start service before querying location
        Input.location.Start(5f,10f);

        // Wait until service initializes
        int maxWait = 20;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSeconds(1);
            maxWait--;
        }

        // Service didn't initialize in 20 seconds
        if (maxWait < 1)
        {
            print("Timed out");
            yield break;
        }

        // Connection has failed
        if (Input.location.status == LocationServiceStatus.Failed)
        {
            print("Unable to determine device location");
            yield break;
        }
        else
        {
            // Access granted and location value could be retrieved
            print("Location: " + Input.location.lastData.latitude + " " + Input.location.lastData.longitude + " " + Input.location.lastData.altitude + " " + Input.location.lastData.horizontalAccuracy + " " + Input.location.lastData.timestamp);
        }

        // Stop service if there is no need to query location updates continuously
        //Input.location.Stop();
    }


}
