using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraManager : MonoBehaviour
{
    public Transform[] targets;
    public int currentTargetIndex = 0;
    public int beforeTargetIndex = 0;



    void Start()
    {
        foreach (var item in targets)
        {
            item.gameObject.SetActive(false);
        }
    }


    void Update()
    {
        targets[beforeTargetIndex].gameObject.SetActive(false);
        targets[currentTargetIndex].gameObject.SetActive(true);

        if (Input.GetKeyUp(KeyCode.Tab))
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
