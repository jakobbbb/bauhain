using UnityEngine;
using UnityEngine.Tilemaps;

public abstract class CharacterController : MonoBehaviour {

    [SerializeField]
    protected Tilemap m_Tilemap;

    protected float m_MoveSpeed = 6.0f;
    protected float m_MoveSpeedSprintModifier = 2.0f;

    protected Vector3 m_PositionInternal;

    void Start() {
        m_PositionInternal = transform.position;
    }

    protected void Move(Vector3 delta) {
        m_PositionInternal += new Vector3(delta.x, delta.y, 0);

        // TODO Round to Tilemap grid size
        transform.position = new Vector3(
                Mathf.Round(m_PositionInternal.x),
                Mathf.Round(m_PositionInternal.y),
                0);
    }
}
