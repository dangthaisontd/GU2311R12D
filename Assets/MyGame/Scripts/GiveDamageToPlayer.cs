using UnityEngine;
using System.Collections;

public class GiveDamageToPlayer : MonoBehaviour {
	[Header("Option")]
	[Tooltip("destroy this object when hit player?")]
	public bool isDestroyWhenHitPlayer = false;
	public GameObject DestroyFx;

	[Header("Make Damage")]
	public int DamageToPlayer=100;
	[Tooltip("delay a moment before give next damage to Player")]
	public float rateDamage = 0.2f;
	public Vector2 pushPlayer = new Vector2 (0, 10);
	float nextDamage;

	[Tooltip("Give damage to this object when Player jump on his head")]
	public bool canBeKillOnHead = false;
	public int damageOnHead=10;

	void OnTriggerStay2D(Collider2D other){
		var Player = other.GetComponent<PlayerController> ();
		if (Player == null)
			return;
		if (Time.time < nextDamage + rateDamage)
			return;

		nextDamage = Time.time;

		if (canBeKillOnHead && Player.transform.position.y > transform.position.y) {

			var canTakeDamage = (ICanTakeDamage) GetComponent (typeof(ICanTakeDamage));
			if (canTakeDamage != null)
				canTakeDamage.TakeDamage (damageOnHead, Vector2.zero, gameObject);
			
			return;
		}
		if (DamageToPlayer == 0)
			return;

		Player.TakeDamage (DamageToPlayer, Vector2.zero, gameObject);
		if (isDestroyWhenHitPlayer) {
			if (DestroyFx != null)
				Instantiate (DestroyFx, transform.position, Quaternion.identity);
			Destroy (gameObject);
		}
	}
}
