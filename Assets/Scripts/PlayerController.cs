using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour {

    [SerializeField]
    private Tilemap m_Tilemap;

    private InputAction m_MoveAction;
    private float m_MoveSpeed = 6.0f;
    private float m_MoveSpeedSprintModifier = 2.0f;

    private Vector3 m_PositionInternal;

    private const float REPEAT_COOLDOWN_S = 100.0f / 1000.0f;
    private float m_RepeatTimer = REPEAT_COOLDOWN_S;

    void Start() {
        m_MoveAction = InputSystem.actions.FindAction("Move");
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
        var delta = move * Time.deltaTime * m_MoveSpeed;

        if (m_MoveAction.WasPressedThisFrame()) {
            delta.Normalize();
            m_RepeatTimer = 0.0f;
        }

        m_PositionInternal += new Vector3(delta.x, delta.y, 0);

        // TODO Round to Tilemap grid size
        transform.position = new Vector3(
                Mathf.Round(m_PositionInternal.x),
                Mathf.Round(m_PositionInternal.y),
                0);
    }

    void OnDrawGizmos() {
        Gizmos.color = Color.yellow;
        if (MovementBlocked()) {
            Gizmos.color = Color.red;
        }
        Gizmos.DrawSphere(m_PositionInternal, 0.15f);
    }
}
