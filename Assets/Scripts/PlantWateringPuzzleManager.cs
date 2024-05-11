/*  Project: Capstone 3D Escape Room Challenge - Alchemy Room
    Author: Theresa Quach
    Description: PlantWaterPuzzleManager is responsible for implementing the behavior of related game objects (potions) that collide with another designated game object, both set with SerializeField variables
                in Unity's Inspector. On collision between the two matched game objects, the potion will be set inactive. The Win condition is checked by checking whether all gameobjects in the gameobject array
                Potions are inactive, and the prize will spawn.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantWateringPuzzleManager : MonoBehaviour
{
    [SerializeField] private GameObject plant;
    [SerializeField] private string potion;
    [SerializeField] private GameObject[] Potions;
    [SerializeField] private GameObject prizes3;
    [SerializeField] private GameObject aoe;

    private int completePotions;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (AllInactive())
        {
            prizes3.gameObject.SetActive(true);
            ModeSwap.gameInstance.donePuzzle3 = true;
            Destroy(aoe.gameObject, 10f);
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag(potion))
        {
            other.gameObject.SetActive(false);
        }
    }

    private bool AllInactive()
    {
        foreach (GameObject potion in Potions)
        {
            if (potion.activeSelf)
            {
                return false;
            }
        }
        return true;
    }
}
