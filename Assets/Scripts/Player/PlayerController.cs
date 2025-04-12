using UnityEngine;

public class PlayerController : MonoBehaviour
{
    internal PlayerModel model;
    private PlayerView view;

    private void Awake()
    {
        model = new PlayerModel();
        model.Initialise();
        view = GetComponent<PlayerView>();
        view.Initialise(model);
    }

    private void Update()
    {
        view.CheckGround();
        HandleMoveInput();
        HandleJumpInput();
        HandleFLip();
    }

    private void HandleMoveInput()
    {
        model.MoveInput = Input.GetAxis("Horizontal");
        view.Move(model.MoveInput);
    }

    private void HandleJumpInput()
    {
        if (model.IsGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            model.IsJumping = true;
            model.JumpTimeCounter = model.MaxJumpTime;
            view.Jump(model.JumpForce);
        }
        if (Input.GetKey(KeyCode.Space) && model.IsJumping)
        {
            if (model.JumpTimeCounter > 0)
            {
                view.Jump(model.JumpForce);
                model.UpdateJumpTimer();
            }
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            model.IsJumping = false;
        }
    }

    private void HandleFLip()
    {
        if ((model.MoveInput > 0 && !model.IsFacingRight) 
            || (model.MoveInput < 0 && model.IsFacingRight))
        {
            view.Flip();
        }
    }
}
