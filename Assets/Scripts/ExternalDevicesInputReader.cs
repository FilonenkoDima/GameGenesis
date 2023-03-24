using Player;
using UnityEngine;
using UnityEngine.EventSystems;

public class ExternalDevicesInputReader : IEntityInputSource
{
    public float HorizontalDirection => Input.GetAxisRaw("Horizontal");
    public float VerticalDirection => Input.GetAxisRaw("Vertical");
    public bool Jump { get; private set; }
    public bool Attack { get; private set; }
    
    public void OnUpdate()
    {
        if (Input.GetButtonDown("Jump"))
            Jump = true;

        if (!IsPointerOverUI() &&  Input.GetButtonDown("Fire1"))
            Attack = true;
    }

    private bool IsPointerOverUI() => EventSystem.current.IsPointerOverGameObject();

    public void ResetOneTimeAction()
    {
        Jump = false;
        Attack = false;
    }
}