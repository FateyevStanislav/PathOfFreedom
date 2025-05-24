using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class LeverSystem : MonoBehaviour
{
    private GameObject thornObject;
    private GameObject door;
    private GameObject wife;
    private string enemyTag = "CCE";
    private bool isEnemyDefeated;
    private bool canActivate = false;
    private int sceneIndex;

    private void Start()
    {
        sceneIndex = SceneManager.GetActiveScene().buildIndex;
        GetComponent<BoxCollider2D>().isTrigger = true;
        GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.5f);
        door = GameObject.Find("Door");
        if (sceneIndex != 4)
        {
            thornObject = GameObject.Find("Thorn");
        }
        if (sceneIndex == 4)
        {
            wife = GameObject.Find("Wife");
        }
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
        if (sceneIndex == 4)
        {
            wife.GetComponent<SpriteRenderer>().sprite = wife.GetComponent<Wife>().Happy;
        }
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