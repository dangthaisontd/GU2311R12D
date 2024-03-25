using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterAudio : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision != null&&collision.CompareTag("Player"))
        {
            AudioManager.Instance.PlaySoundEffectMusic(AudioManager.Instance.Water);
        }
    }
}
