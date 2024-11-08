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
        if (PlayerInstance == null)
        {
            PlayerInstance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
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
