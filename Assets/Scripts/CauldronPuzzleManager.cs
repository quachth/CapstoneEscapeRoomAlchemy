using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CauldronPuzzleManager : MonoBehaviour
{
    [SerializeField] private GameObject cauldron;
    [SerializeField] private GameObject holdableItem;
    [SerializeField] private GameObject exitPortal;

    private int exitCrystals;

    void Start()
    {
        exitCrystals = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (cauldron.transform.childCount == 3 && exitCrystals == 3)
        {
            foreach (Transform child in cauldron.transform)
            {
                child.gameObject.SetActive(false);
            }
            exitPortal.gameObject.SetActive(true);
        }
    }

    void OnCollisionEnter(Collision otherIn)
    {
        otherIn.gameObject.transform.parent = cauldron.transform;
        if (otherIn.transform.CompareTag("ExitCrystal"))
        {
            exitCrystals++;
            Debug.Log(exitCrystals);
        }
    }

    void OnCollisionExit(Collision otherOut)
    {
        otherOut.gameObject.transform.parent = holdableItem.transform;
        if(otherOut.transform.CompareTag("ExitCrystal"))
        {
            exitCrystals--;
            Debug.Log(exitCrystals);
        }
    }
}
