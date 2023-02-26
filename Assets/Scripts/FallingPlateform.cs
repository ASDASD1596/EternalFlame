using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlateform : MonoBehaviour
{
    [SerializeField]private float fallDelay;
    [SerializeField]private float destroyDelay;

    [SerializeField]private Rigidbody2D rb;

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            
            StartCoroutine(Fall());
            SoundManager.instance.Play(SoundManager.SoundName.Fallingplatform);
        }
    }

    private IEnumerator Fall()
    {
        yield return new WaitForSeconds(fallDelay);
        rb.bodyType = RigidbodyType2D.Dynamic;
        Destroy(gameObject,destroyDelay);
    }
}
