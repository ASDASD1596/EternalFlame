using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Starting : MonoBehaviour
{
    void Update()
    {
        StartCoroutine(wait());
        if (Input.GetKey(KeyCode.Mouse0))
        {
            SceneManager.LoadScene("Map");
        }
    }

    IEnumerator wait()
    {

        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("Map");
        Debug.Log("Load");

    }
}
