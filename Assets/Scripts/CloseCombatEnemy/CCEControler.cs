using UnityEngine;

public class CCEControler : MonoBehaviour
{
    internal CCEModel model;
    private CCEView view;
    private PlayerView player;

    private void Awake()
    {
        model = new CCEModel();
        model.Initialise();
        view = GetComponent<CCEView>();
        view.Initialise(model);
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerView>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PlayerSword") && player.model.IsHitting)
        {
            view.TakeDamage(player.model.Damage);
        }
    }
}
