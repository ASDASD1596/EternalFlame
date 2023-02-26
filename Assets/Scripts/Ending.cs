using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ending : MonoBehaviour
{
    void Update()
    {
        StartCoroutine(wait());
    }

    IEnumerator wait()
    {
        
        yield return new WaitForSeconds(40f);
        SceneManager.LoadScene("MainMenu");
        Debug.Log("Load");

    }
}
