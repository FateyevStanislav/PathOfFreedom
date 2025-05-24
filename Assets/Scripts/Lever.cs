using UnityEngine;

public class LeverSystem : MonoBehaviour
{
    private GameObject thornObject;
    private GameObject door;
    private string enemyTag = "CCE";
    private bool isEnemyDefeated;
    private bool canActivate = false;

    private void Start()
    {
        GetComponent<BoxCollider2D>().isTrigger = true;
        GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.5f);
        thornObject = GameObject.Find("Thorn");
        door = GameObject.Find("Door");
    }

    private void Update()
    {
        if (!isEnemyDefeated)
        {
            CheckEnemyStatus();
        }
        if (canActivate && Input.GetKeyDown(KeyCode.E))
        {
            ActivateLever();
        }
    }

    private void CheckEnemyStatus()
    {
        GameObject enemy = GameObject.FindWithTag(enemyTag);
        if (enemy == null)
        {
            isEnemyDefeated = true;
            Destroy(thornObject);
        }
    }

    private void ActivateLever()
    {
        transform.rotation = Quaternion.Inverse(Quaternion.identity);
        canActivate = false;
        Destroy(door);
        Destroy(gameObject.GetComponent<BoxCollider2D>());
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && isEnemyDefeated)
        {
            canActivate = true;
            GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            canActivate = false;
            GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.5f);
        }
    }
}