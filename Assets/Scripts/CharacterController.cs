using UnityEngine;
// using UnityEngine.Tilemaps;

public abstract class CharacterController : MonoBehaviour {

    [SerializeField]
    protected Animator m_Animator;

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

    protected Vector3 m_PositionInternal;

    void Start() {
        m_PositionInternal = transform.position;
        m_Target = transform.position;
    }

    protected void Move(Vector3 delta, Scaling scaling_mode) {
        if (scaling_mode == Scaling.WITH_SPEED_AND_TIME) {
            delta *= Time.deltaTime * m_MoveSpeed;
        }

        m_PositionInternal += new Vector3(delta.x, delta.y, 0);

        var target = new Vector3(
                Mathf.Round(m_PositionInternal.x + 0.5f) - 0.5f,
                Mathf.Round(m_PositionInternal.y + 0.5f) - 0.5f,
                0);
        var delta_target = target - transform.position;
        if (delta_target.magnitude > 1.0f) {
            delta_target.Normalize();
        }
        transform.position += delta_target * 15.0f * Time.deltaTime;

        m_Target = target;
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
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(m_PositionInternal, 0.15f);
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(m_Target, 0.15f);
    }
}
