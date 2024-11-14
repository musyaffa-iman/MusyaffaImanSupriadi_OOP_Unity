using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] float speed = 0.15f;
    [SerializeField] float rotateSpeed = 5.0f;
    Vector2 newPosition;

    void Start()
    {
        ChangePosition();
    }

    void Update()
    {
        transform.Rotate(Vector3.forward * rotateSpeed * Time.deltaTime);
        transform.position = Vector2.MoveTowards(transform.position, newPosition, speed * Time.deltaTime);

        // Jika portal terlalu dekat dengan newPosition, maka generate newPosition yang baru
        if (Vector2.Distance(transform.position, newPosition) < 0.5f)
        {
            ChangePosition();
        }

        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            Weapon playerWeapon = player.GetComponentInChildren<Weapon>();
            bool hasWeapon;
            if (playerWeapon != null)
                hasWeapon = true;
            else
                hasWeapon = false;

            GetComponent<SpriteRenderer>().enabled = hasWeapon;
            GetComponent<Collider2D>().enabled = hasWeapon;
        }
    }

    void ChangePosition()
    {
        // Mengatur newPosition ke suatu titik random dalam suatu batasan
        newPosition = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameManager.Instance.LevelManager.LoadScene("Main");
        }
    }
}
