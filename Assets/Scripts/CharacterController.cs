using UnityEngine;
// using UnityEngine.Tilemaps;

public abstract class CharacterController : MonoBehaviour {

    [SerializeField]
    protected Animator m_Animator;

    [SerializeField]
    protected Rigidbody2D m_RigidBody;

    public enum Scaling {
        NONE,
        WITH_SPEED_AND_TIME,
    }

    private enum Direction {
        UP = 0,
        DOWN = 1,
        LEFT = 2,
        RIGHT = 3,
    }

    protected Vector2 m_Target;

    // [SerializeField]
    // protected Tilemap m_Tilemap;

    protected float m_MoveSpeed = 6.0f;
    protected float m_MoveSpeedSprintModifier = 2.0f;

    // [SerializeField]
    // protected Transform m_PositionInternal;
    // [SerializeField]
    // protected Rigidbody2D m_PositionInternalRB;

    public void Awake() {
        m_Target = transform.position;
        // m_PositionInternal.transform.position = transform.position;
        // m_PositionInternal.transform.SetParent(null);
    }

    protected void Move(Vector3 delta, Scaling scaling_mode) {
        if (scaling_mode == Scaling.WITH_SPEED_AND_TIME) {
            delta *= Time.deltaTime * m_MoveSpeed;
        }

        m_RigidBody.MovePosition(m_RigidBody.transform.position + delta);
        /*
        m_PositionInternalRB.MovePosition(m_PositionInternal.position + new Vector3(delta.x, delta.y, 0));

        var target = new Vector3(
                Mathf.Round(m_PositionInternal.position.x),
                Mathf.Round(m_PositionInternal.position.y),
                0);
        var delta_target = target - transform.position;
        if (delta_target.magnitude > 1.0f) {
            delta_target.Normalize();
        }

        var move = delta_target * 15.0f * Time.deltaTime;
        if (m_RigidBody) {
            m_RigidBody.MovePosition(transform.position + move);
        } else {
            //transform.position += move;
        }

        m_Target = target;
        */
    }

    protected void UpdateAnimator(Vector2 delta) {
        bool stopped = delta.magnitude < 0.05f;

        if (!stopped) {
            Direction dir = Direction.DOWN;
            if (delta.x > 0.9f) {
                dir = Direction.RIGHT;
            } else if (delta.x < -0.9f) {
                dir = Direction.LEFT;
            } else if (delta.y > 0.9f) {
                dir = Direction.UP;
            }

            m_Animator.SetInteger("DirUDLR", (int)dir);
            m_Animator.SetFloat("Speed", 0.5f);
        } else {
            m_Animator.SetFloat("Speed", 0.0f);
        }
    }

    void OnDrawGizmos() {
        // Gizmos.color = Color.yellow;
        // Gizmos.DrawSphere(m_PositionInternal.position, 0.15f);
        // Gizmos.color = Color.blue;
        // Gizmos.DrawSphere(m_Target, 0.15f);
    }
}
