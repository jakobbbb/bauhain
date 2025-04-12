using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : CharacterController {

    private InputAction m_MoveAction;
    private InputAction m_SprintAction;

    private const float REPEAT_COOLDOWN_S = 100.0f / 1000.0f;
    private float m_RepeatTimer = REPEAT_COOLDOWN_S;

    // Cooldown before going into idle anim
    private const float IDLE_COOLDOWN_S = 50.0f / 1000.0f;
    private float m_IdleTimer = IDLE_COOLDOWN_S;

    void Start() {
        m_MoveAction = InputSystem.actions.FindAction("Move");
        m_SprintAction = InputSystem.actions.FindAction("Sprint");
        m_PositionInternal = transform.position;
    }

    private bool MovementBlocked() {
        return m_RepeatTimer < REPEAT_COOLDOWN_S;
    }

    void Update() {
        m_RepeatTimer += Time.deltaTime;
        m_IdleTimer += Time.deltaTime;

        if (MovementBlocked()) {
            return;
        }

        var move = m_MoveAction.ReadValue<Vector2>();
        var sprinting = m_SprintAction.IsPressed();
        var sprint_mod = sprinting ? m_MoveSpeedSprintModifier : 1.0f;
        var delta = move * sprint_mod;

        if (m_MoveAction.WasPressedThisFrame()) {
            delta.Normalize();
            m_RepeatTimer = 0.0f;
            Move(delta, Scaling.NONE);
        } else {
            Move(delta, Scaling.WITH_SPEED_AND_TIME);
        }

        UpdateAnimator(delta);

    }

    void OnDrawGizmos() {
        Gizmos.color = Color.yellow;
        if (MovementBlocked()) {
            Gizmos.color = Color.red;
        }
        Gizmos.DrawSphere(m_PositionInternal, 0.15f);
    }
}
