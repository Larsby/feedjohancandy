using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameOverChecker : MonoBehaviour {
    public goal g;
    public AudioSource audiosource;


     
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(collision.collider.gameObject);
        audiosource.Play();
        g.gameOver();
    }

    void Update () {
		
	}
}
