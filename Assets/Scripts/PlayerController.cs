using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : CharacterController {

    private InputAction m_MoveAction;
    private InputAction m_SprintAction;

    private const float REPEAT_COOLDOWN_S = 100.0f / 1000.0f;
    private float m_RepeatTimer = REPEAT_COOLDOWN_S;

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

        if (MovementBlocked()) {
            Debug.Log("block");
            return;
        }

        var move = m_MoveAction.ReadValue<Vector2>();
        var sprinting = m_SprintAction.IsPressed();
        var sprint_mod = sprinting ? m_MoveSpeedSprintModifier : 1.0f;
        var delta = move * Time.deltaTime * m_MoveSpeed * sprint_mod;

        if (m_MoveAction.WasPressedThisFrame()) {
            delta.Normalize();
            m_RepeatTimer = 0.0f;
        }

        Move(delta);
    }

    void OnDrawGizmos() {
        Gizmos.color = Color.yellow;
        if (MovementBlocked()) {
            Gizmos.color = Color.red;
        }
        Gizmos.DrawSphere(m_PositionInternal, 0.15f);
    }
}
