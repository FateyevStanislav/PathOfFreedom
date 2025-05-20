using UnityEngine;

public class LeverSystem : MonoBehaviour
{
    public GameObject thornObject;
    public string enemyTag = "CCE";
    private bool isEnemyDefeated;
    private bool canActivate = false;

    private void Start()
    {
        GetComponent<BoxCollider2D>().isTrigger = true;
        thornObject = GameObject.Find("Thorn");
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
        }
    }

    private void ActivateLever()
    {
        transform.rotation = Quaternion.Inverse(Quaternion.identity);
        Destroy(thornObject);
        canActivate = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && isEnemyDefeated)
        {
            canActivate = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            canActivate = false;
        }
    }
}