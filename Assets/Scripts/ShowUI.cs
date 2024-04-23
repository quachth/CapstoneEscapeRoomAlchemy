using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShowUI : MonoBehaviour
{
    [SerializeField] private Camera puzzleCamera;
    [SerializeField] private string interactTextString;
    [SerializeField] private TMP_Text uiText;
    [SerializeField] private GameObject trigger;
    [SerializeField] private TMP_Text exitUIText;


    // Start is called before the first frame update
    void Start()
    {
        uiText.SetText("");
        exitUIText.SetText("[Press E to exit puzzle.]");
    }

    void Update()
    {
        if (puzzleCamera.enabled)
        {
            uiText.enabled = false;
            exitUIText.enabled = true;
        } else
        {
            uiText.enabled = true;
            exitUIText.enabled = false;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && ModeSwap.gameInstance.playerCamera.enabled == true)
        {
            if (ModeSwap.gameInstance.donePuzzle1 == false)
            {
                uiText.SetText(interactTextString);
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        uiText.SetText("");
    }
}
