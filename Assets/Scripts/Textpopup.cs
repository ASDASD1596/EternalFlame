using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Textpopup : MonoBehaviour
{
    [SerializeField] private GameObject textUi;

    IEnumerator DelayDestroy()
    {
        yield return new WaitForSeconds(5);
        Destroy(this.gameObject);
        textUi.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            textUi.SetActive(true);
            StartCoroutine("DelayDestroy");
        }
    }
}
