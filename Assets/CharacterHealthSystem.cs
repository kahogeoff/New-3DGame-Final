using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterHealthSystem : MonoBehaviour {
	public float InitialMaxHealth = 100.0f;
	public float MaxHealth = 100.0f;
	public float CurrentHealth = 100.0f;

	// Use this for initialization
	void Start () {
		MaxHealth = InitialMaxHealth;
		CurrentHealth = MaxHealth;
	}
	
	// Update is called once per frame
	void Update () {
		CurrentHealth = Mathf.Clamp (CurrentHealth, 0, MaxHealth);
	}

	void ReduceHealth(float amount){
		CurrentHealth -= amount;
	}

	void RestoreHealth(float amount){
		CurrentHealth += amount;
	}
}
