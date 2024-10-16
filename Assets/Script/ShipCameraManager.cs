using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Cinemachine;

public class ShipCameraManager : MonoBehaviour
{
    public Transform[] targets;
    

    public int currentTargetIndex = 0;
    public int beforeTargetIndex = 0;
    
    private CinemachineVirtualCamera vcam;

    void Start()
    {
        vcam = GetComponent<CinemachineVirtualCamera>();
        foreach (var item in targets)
        {
            item.gameObject.SetActive(true);
            item.GetComponent<NaveEspacial>().movimientoHabilitado = false;
        }
        targets[currentTargetIndex].GetComponent<NaveEspacial>().movimientoHabilitado = true;
    }


    void Update()
    {
        // vcam.LookAt = targets[currentTargetIndex];
        vcam.Follow = targets[currentTargetIndex];

        targets[beforeTargetIndex].GetComponent<NaveEspacial>().movimientoHabilitado = false;
        targets[currentTargetIndex].GetComponent<NaveEspacial>().movimientoHabilitado = true;

        // transform.position = targets[currentTargetIndex].position - transform.rotation * Vector3.forward * distance;

        if (Input.GetMouseButtonDown(1))
        {
            beforeTargetIndex = currentTargetIndex;
            currentTargetIndex++;


            if (currentTargetIndex >= targets.Length)
            {
                currentTargetIndex = 0;

            }
        }
    }
}


