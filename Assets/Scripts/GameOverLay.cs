using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverLay : MonoBehaviour
{

    public goal g;
    void Start()
    {

    }

    void OnMouseDown()
    {
        g.StartNewGame();
        iTween.MoveTo(gameObject, new Vector3(-11, 0, 0), 0.5f);

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            g.StartNewGame();
            iTween.MoveTo(gameObject, new Vector3(-11, 0, 0), 0.5f);

        }

    }



}
