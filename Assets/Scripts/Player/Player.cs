using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player PlayerInstance { get; private set; }
    public PlayerMovement playerMovement;
    public Animator animator;
    private WeaponPickup currentWeaponPickup;

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
        playerMovement.MoveBound();
        animator.SetBool("IsMoving", playerMovement.IsMoving());
    }

    public void SwitchWeapon(Weapon newWeapon, WeaponPickup newWeaponPickup)
    {
        if (currentWeaponPickup != null)
        {
            currentWeaponPickup.PickupHandler(true);
        }
        currentWeaponPickup = newWeaponPickup;
    }
}
