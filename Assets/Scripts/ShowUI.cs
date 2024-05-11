/*  Project: Capstone 3D Escape Room Challenge - Alchemy Room
    Author: Theresa Quach
    Description: ShowUI is responsible for controlling the text tips that appear throughout the room as the player steps on/through certain triggers, and is meant to help guide the player
                    through interacting with all of the puzzles. This script is placed on each trigger GameObject (mesh not rendered in game).
    Citations:  This script was adapted by fellow capstone project group member Zijing Liu's UI script (https://github.com/hendera2/CapstoneEscapeRoom/blob/CaribbeanPirateZijing/Assets/Scripts/TorchPickUp.cs). 
                Changes include rewriting the script to work for TMP_Text instead of TextMeshProUGUI datatype,
                    which also affected the methods used to changed the text mesh's text as well as enable and disable their appearance in game.
*/

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShowUI : MonoBehaviour
{
    [SerializeField] private GameObject uiTextAll;
    [SerializeField] private Camera puzzleCamera;
    [SerializeField] private string interactTextString;
    [SerializeField] private TMP_Text uiText;
    [SerializeField] private GameObject trigger;
    [SerializeField] private TMP_Text exitUIText;
    [SerializeField] private GameObject exitPortal;

    // Start is called before the first frame update
    void Start()
    {
        uiText.SetText("");
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
            else if (ModeSwap.gameInstance.donePuzzle2 == false)
            {
                uiText.SetText(interactTextString);
            }
            else if (ModeSwap.gameInstance.donePuzzle3 == false)
            {
                uiText.SetText(interactTextString);
            }
        }
        if (exitPortal.gameObject.activeSelf)
        {
            uiTextAll.SetActive(false);

        }
    }

    void OnTriggerExit(Collider other)
    {
        uiText.SetText("");
    }
}
