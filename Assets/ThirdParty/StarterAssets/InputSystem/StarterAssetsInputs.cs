using UnityEngine;
#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
using UnityEngine.InputSystem;
#endif

namespace StarterAssets
{
    public class StarterAssetsInputs : MonoBehaviour
    {
        [Header("Character Input Values")]
        public Vector2 move;
        public Vector2 look;
        public bool jump;
        public bool sprint;
        public bool grab;
        public bool menu;
        public bool interact;

        [Header("Movement Settings")]
        public bool analogMovement;

        [Header("Mouse Cursor Settings")]
        public bool cursorLocked = true;
        public bool cursorInputForLook = true;
        private void Start()
        {
            ShowCursor(false);
            PlayerUI.Instance.isMenuOpen += ShowCursor;
        }

#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
        public void OnMove(InputValue value)
        {
            MoveInput(value.Get<Vector2>());
        }

        public void OnLook(InputValue value)
        {
            if (cursorInputForLook)
            {
                if (menu) return;
                LookInput(value.Get<Vector2>());
            }
        }

        public void OnJump(InputValue value)
        {
            JumpInput(value.isPressed);
        }

        public void OnSprint(InputValue value)
        {
            SprintInput(value.isPressed);
        }

        public void OnGrab(InputValue value)
        {
            GrabInput(value.isPressed);
        }
        public void OnMenu()
        {
            // Debug.Log("Tab presed");
            if (PlayerUI.Instance.IsStationActive == true)
                PlayerUI.Instance._actualView.SwitchView();
            // SwitchMenu();
        }
        public void OnInteract(InputValue value)
        {
            // Debug.Log("Tab presed");
            Interact(value.isPressed);
        }
#endif


        public void MoveInput(Vector2 newMoveDirection)
        {
            move = newMoveDirection;
        }

        public void LookInput(Vector2 newLookDirection)
        {
            look = newLookDirection;
        }

        public void JumpInput(bool newJumpState)
        {
            jump = newJumpState;
        }

        public void SprintInput(bool newSprintState)
        {
            sprint = newSprintState;
        }

        public void GrabInput(bool newGrabState)
        {
            grab = newGrabState;
        }
        public void SwitchMenu()
        {
            menu = !menu;
            ShowCursor(menu);
            // Debug.Log(menu);
        }
        public void Interact(bool newInteractState)
        {
            interact = newInteractState;
        }

        // private void OnApplicationFocus(bool hasFocus)
        // {
        //     SetCursorState(false);
        // }

        public void ShowCursor(bool newState)
        {
            menu = newState;
            Cursor.lockState = newState ? CursorLockMode.None : CursorLockMode.Locked;
        }
    }

}