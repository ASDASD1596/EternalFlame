using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEnding : MonoBehaviour
{
    public CameraShake cameraShake;
    public LightControll lightControll;
   
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            lightControll.Gameclear();
            StartCoroutine(cameraShake.Shake(20f, 0.2f));
            StartCoroutine(wait());


            SoundManager.instance.Mute(SoundManager.SoundName.BGM1);
            SoundManager.instance.Play(SoundManager.SoundName.Cavecollapse);
        }
    }

    IEnumerator wait()
    {
        yield return new WaitForSeconds(3f);
        SoundManager.instance.Mute(SoundManager.SoundName.Cavecollapse);
        SceneManager.LoadScene("EndScene");

    }
}
