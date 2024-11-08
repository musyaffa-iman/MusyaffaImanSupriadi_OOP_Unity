using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    [SerializeField] Weapon weaponHolder;
    Weapon weapon;

    private void Awake()
    {
        
    }

    void Start()
    {
        if (weapon != null)
        {
            TurnVisual(false);
            TurnVisual(false, weapon);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            weapon = Instantiate(weaponHolder);
            Weapon currentWeapon = other.GetComponentInChildren<Weapon>();

            if (currentWeapon != null)
            {
                TurnVisual(false, currentWeapon);
            }

            weapon.transform.SetParent(other.transform);
            weapon.transform.position = other.transform.position;
            TurnVisual(true, weapon);
            Debug.Log("Player equipped new weapon");
        }
    }
    
    void TurnVisual(bool on)
    {
        foreach (Renderer renderer in weapon.GetComponentsInChildren<Renderer>())
        {
            renderer.enabled = on;
        }
    }

    void TurnVisual(bool on, Weapon weapon)
    {
        foreach (Renderer renderer in weapon.GetComponentsInChildren<Renderer>())
        {
            renderer.enabled = on;
        }
        if (on == false)
        {
            Destroy(weapon.gameObject);
        }
    }
}
