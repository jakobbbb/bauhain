using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : CharacterController {

    private InputAction m_MoveAction;
    private InputAction m_SprintAction;
    private InputAction m_InteractAction;

    private const float REPEAT_COOLDOWN_S = 100.0f / 1000.0f;
    private float m_RepeatTimer = REPEAT_COOLDOWN_S;

    // Cooldown before going into idle anim
    private const float IDLE_COOLDOWN_S = 50.0f / 1000.0f;
    private float m_IdleTimer = IDLE_COOLDOWN_S;

    private DialogueTrigger m_NearNPC;

    public void Start() {
        m_MoveAction = InputSystem.actions.FindAction("Move");
        m_SprintAction = InputSystem.actions.FindAction("Sprint");
        m_InteractAction = InputSystem.actions.FindAction("Interact");
        //m_PositionInternal = transform.position;
    }

    private bool MovementBlocked() {
        return m_RepeatTimer < REPEAT_COOLDOWN_S;
    }

    void Update() {
        m_RepeatTimer += Time.deltaTime;
        m_IdleTimer += Time.deltaTime;

        if (GameManager.Instance.DiaManager.IsDialogueRunning() || MovementBlocked()) {
            return;
        }

        var move = m_MoveAction.ReadValue<Vector2>();
        var sprinting = m_SprintAction.IsPressed();
        var sprint_mod = sprinting ? m_MoveSpeedSprintModifier : 1.0f;
        var delta = move * sprint_mod;

        /*
        if (m_MoveAction.WasPressedThisFrame()) {
            delta.Normalize();
            m_RepeatTimer = 0.0f;
            Move(delta, Scaling.NONE);
        } else {
            Move(delta, Scaling.WITH_SPEED_AND_TIME);
        }*/

        Move(delta, Scaling.WITH_SPEED_AND_TIME);

        UpdateAnimator(delta);

        var near = GetNearNPC();
        if (near) {
            if (Input.GetKeyDown(KeyCode.E)) {
                var name = near.NPCName();
                if (name != null) {
                    GameManager.Instance.DiaManager.TalkTo(near.NPCName(), near.Splash);
                    near.MarkVisited();
                }
            }
            near.EnableInteractionIcon();
        } else {
            DialogueTrigger.DisableAllInteractionIcons();
        }
    }

    private void OnTriggerStay2D(Collider2D other) {
        Room r = null;
        other.TryGetComponent<Room>(out r);
        if (r) {
            // TODO!!! fix?
            var storage = GameManager.Instance.DiaManager.Storage();
            storage.SetValue("$playerroom", r.RoomId);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        DialogueTrigger d = null;
        other.TryGetComponent<DialogueTrigger>(out d);
        if (d == null) {
            return;
        } else {
            m_NearNPC = d;
        }
    }

    private DialogueTrigger GetNearNPC() {
        var dist = (m_NearNPC.transform.position - transform.position).magnitude;
        return (dist < 1.5f) ? m_NearNPC : null;
    }
}
