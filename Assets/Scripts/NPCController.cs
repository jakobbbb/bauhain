using UnityEngine;

public class NPCController : CharacterController {

    private Vector3 m_MoveTarget;

    private PlayerController m_Player;

    void Start() {
        m_MoveSpeed *= 0.75f;
        m_Player = GameObject.FindFirstObjectByType<PlayerController>();
    }

    void Update() {
        FollowTarget();
    }

    void FollowTarget() {
        m_MoveTarget = m_Player.transform.position;

        var direction = m_MoveTarget - m_PositionInternal;

        // Keep some distance
        if (direction.magnitude < 1.5f) {
            return;
        }

        direction.Normalize();
        Move(direction, Scaling.WITH_SPEED_AND_TIME);
    }
}
