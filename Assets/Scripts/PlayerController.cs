using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour {

    [SerializeField]
    private Tilemap m_Tilemap;

    private InputAction m_MoveAction;
    private float m_MoveSpeed = 10.0f;

    private Vector3 m_PositionInternal;

    void Start() {
        m_MoveAction = InputSystem.actions.FindAction("Move");
        m_PositionInternal = transform.position;
    }

    void Update() {
        var move = m_MoveAction.ReadValue<Vector2>();
        var delta = move * Time.deltaTime * m_MoveSpeed;
        m_PositionInternal += new Vector3(delta.x, delta.y, 0);

        // TODO Round to Tilemap grid size
        transform.position = new Vector3(
                Mathf.Round(m_PositionInternal.x),
                Mathf.Round(m_PositionInternal.y),
                0);
    }
}
