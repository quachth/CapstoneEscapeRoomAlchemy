/*  Project: Capstone 3D Escape Room Challenge - Alchemy Room
    Author: Theresa Quach
    Description: TryAgainOptions is a script used to implement functionality to the buttons on the Try Again screen that appears after the
                countdown timer expires. Functions include reloading the room scene if the user chooses to replay, or to exit to the main page
                of the game.
    Citations:  Code was written following the tutorial on how to make a countdown timer in Unity by Game Dev Beginner
                on Youtube (https://www.youtube.com/watch?v=HmHPJL-OcQE&list=PLvw5MDqEo9YLISdYRQmGy9fYN6EtjTtZG&index=15).
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TryAgainOptions : MonoBehaviour
{
    // method to reload the current room for replay
    public void ReloadScene()
    {
        string currentRoomName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentRoomName);
    }

    // method to return to the main screen of the game (index 0 on build settings)
    public void ExitToMain()
    {
        SceneManager.LoadScene(0);
    }
}
