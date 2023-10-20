using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorEntrance : MonoBehaviour
{
    private bool enterAllowed;


    private void OnTriggerEnter2D(Collider2D collision)
    {

        enterAllowed = true;
        GameController.instance.EnterCastle();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        enterAllowed = false;
        GameController.instance.ExitCastle();
    }

    private void Update()
    {
        if(enterAllowed && Input.GetKey(KeyCode.Return))
        {
            GameController.instance.ShowEndGame();
        }
    }
}
