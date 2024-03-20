using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projective : MonoBehaviour
{
    public int damage = 10;
    public Vector2 fore;
    public GameObject fxPrefabs;
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision != null&&collision.CompareTag("Enemy"))
        {
            BehemothAI behemothAI = collision.GetComponent<BehemothAI>();
            if(behemothAI != null )
            {
                Instantiate(fxPrefabs, transform.position, Quaternion.identity);
                AudioManager.Instance.PlaySoundEffectMusic(AudioManager.Instance.explusion);
                behemothAI.TakeDamage(damage,fore,gameObject);
                Destroy(gameObject);
            }
        }
    }
}
