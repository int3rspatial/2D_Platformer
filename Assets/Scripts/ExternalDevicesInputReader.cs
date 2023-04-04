using Assets.Scripts.Player;
using UnityEngine;
using UnityEngine.EventSystems;

public class ExternalDevicesInputReader : IEntityInputSource
{
    public float HorizontalDirection => Input.GetAxisRaw("Horizontal"); //-1 0 1
    public bool Jump { get; private set; }
    public bool Attack { get; private set; }

    public void OnUpdate()
    {
        if (Input.GetButtonDown("Jump"))
        {
            Jump = true;
        }
        
        if (IsPointerOverUI() && Input.GetButtonDown("Fire1"))
        {
            Attack = true;
        }
    }

    private bool IsPointerOverUI()
    {
        return EventSystem.current.currentSelectedGameObject.name == "AttackButton" && EventSystem.current.IsPointerOverGameObject();
    }

    public void ResetOneTimeAction()
    {
        Jump = false;
        Attack = false;
    }
}