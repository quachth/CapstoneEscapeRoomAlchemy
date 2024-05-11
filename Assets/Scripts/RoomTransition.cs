/*  Project: Capstone 3D Escape Room Challenge - Alchemy Room
    Author: Theresa Quach
    Description: RoomTransition is a script similar to ScreenChanger in that it is used to change scenes along with a fade animation. The difference between the two is that
                this script is used primarily for when the player finishes the escape room and interacts with the exit, leading to a scene change. For my room specifically,
                the player interacts with the exit using the E key targeted on a portal game object. When this occurs, the scene change is triggered. It also uses the index 
                of the next scene in the build to change to.
    Citations: This script was written following Brackeys "How to Fase Between Scenes in Unity" tutorial on Youtube (https://www.youtube.com/watch?v=Oadq-IrOazg&list=PLvw5MDqEo9YLISdYRQmGy9fYN6EtjTtZG&index=13).
*/

using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChanger : MonoBehaviour
{

    [SerializeField] private Transform playerCameraTransform;
    [SerializeField] private Animator animator;
    private int newSceneIdx;

    void Start()
    {
        newSceneIdx = SceneManager.GetActiveScene().buildIndex;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (Physics.Raycast(playerCameraTransform.position, playerCameraTransform.forward, out RaycastHit raycastHit, 2.0f))     
            {
                // if the object hit by the raycast is the exit portal, transition out of room to next scene
                if (raycastHit.transform.CompareTag("ExitPortal")) 
                {
                    newSceneIdx++;
                    FadeScreens(newSceneIdx);
                }
            }
        }
    }

    public void FadeScreens(int sceneIndex)
    {
        newSceneIdx = sceneIndex;
        animator.SetTrigger("FadeOut");
    }

    public void OnFadeComplete()
    {
        SceneManager.LoadScene(newSceneIdx);
    }
}
