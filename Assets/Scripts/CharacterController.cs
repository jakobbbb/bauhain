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


    // [SerializeField]
    // protected Tilemap m_Tilemap;

    protected float m_MoveSpeed = 6.0f;
    protected float m_MoveSpeedSprintModifier = 2.0f;

    protected Vector3 m_PositionInternal;

    void Start() {
        m_PositionInternal = transform.position;
    }

    protected void Move(Vector3 delta, Scaling scaling_mode) {
        if (scaling_mode == Scaling.WITH_SPEED_AND_TIME) {
            delta *= Time.deltaTime * m_MoveSpeed;
        }

        m_PositionInternal += new Vector3(delta.x, delta.y, 0);

        // TODO Round to Tilemap grid size
        transform.position = new Vector3(
                Mathf.Round(m_PositionInternal.x),
                Mathf.Round(m_PositionInternal.y),
                0);
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

}
