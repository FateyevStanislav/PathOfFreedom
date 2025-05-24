using UnityEngine;
using UnityEngine.SceneManagement;

public class Wife : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] internal Sprite Happy;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Static;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}