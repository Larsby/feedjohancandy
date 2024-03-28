using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toss : MonoBehaviour
{

    public Rigidbody2D rb;
    public AudioSource audiosource;
    bool isAutoshoot = false;
    BoxCollider2D boxCollider2;
    bool isTossed = false;

    void Start()
    {

        iTween.PunchScale(gameObject, new Vector3(0.5f, 0.5f, 0.5f), 0.40f);
        iTween.PunchRotation(gameObject, iTween.Hash("amount", new Vector3(0.0f, 40.2f, 40.0f), "delay", 2, "time", 1f));
        if (isAutoshoot == true)
        {
            BoxCollider2D boxCollider = gameObject.AddComponent<BoxCollider2D>();
            boxCollider.size = new Vector3(1, 20, 1);
        }
        else
        {

            boxCollider2 = gameObject.AddComponent<BoxCollider2D>();
            boxCollider2.size = new Vector3(15, 4);
            boxCollider2.offset = new Vector2(0, .5f);
        }
    }



    public void TossCandy()
    {
        isTossed = true;
        iTween.Stop(gameObject);
        rb.AddForce(Vector2.up * 40.0f);
        audiosource.pitch = Random.Range(0.90f, 1.10f);
        audiosource.Play();

        iTween.ScaleTo(gameObject, new Vector3(0.3f, 0.3f, 0.3f), 0.6f);

        if (transform.name.Contains("CandytosserFish"))
        {

            iTween.RotateTo(gameObject, new Vector3(0.5f, 0.5f, -90.5f), 0.31f);

        }

    }
    void OnMouseDown()
    {
        DestroyObject(boxCollider2);
        TossCandy();

    }

    void Update()
    {
        if (isTossed)
            return;
        if (Input.GetKeyDown(KeyCode.X))
        {
            DestroyObject(boxCollider2);
            TossCandy();

            print("tossing");
        }

    }

}
