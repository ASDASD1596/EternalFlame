using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestFallingPlatform : MonoBehaviour
{
    [SerializeField] private float fallDelay;
    [SerializeField] private float destroyDelay;

    [SerializeField] private Rigidbody2D rb;
    private Animator anio;

    private void Start()
    {
        anio = GetComponent<Animator>();
    }

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
        anio.SetBool("isFalling", true);
        yield return new WaitForSeconds(fallDelay);
        rb.bodyType = RigidbodyType2D.Dynamic;
        Destroy(gameObject, destroyDelay);
        
    }
}