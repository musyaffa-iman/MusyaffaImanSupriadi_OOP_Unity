using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player PlayerInstance { get; private set; }
    public PlayerMovement playerMovement;
    public Animator animator;

    private void Awake()
    {
        // Check apakah sebuah PlayerInstance sudah ada
        if (PlayerInstance == null)
        {
            PlayerInstance = this; // Set object sebagai Singleton PlayerInstance
            DontDestroyOnLoad(gameObject); // Mencegah destruction pada scene load
        }
        else
        {
            Destroy(gameObject); // Destroy PlayerInstance duplikat
        }
    }

    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        animator =  GameObject.Find("Engine/EngineEffect").GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        playerMovement.Move();
    }

    void LateUpdate()
    {
        animator.SetBool("IsMoving", playerMovement.IsMoving());
    }
}
