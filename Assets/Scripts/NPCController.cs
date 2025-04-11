using UnityEngine;

public class NPCController : CharacterController {

    public enum State {
        IDLE,
        FOLLOWTARGET,
    }

    private Vector3 m_MoveTarget;

    private PlayerController m_Player;

    [SerializeField]
    private Animator m_StateMachine;

    void Start() {
        m_MoveSpeed *= 0.75f;
        m_Player = GameObject.FindFirstObjectByType<PlayerController>();
        m_StateMachine.SetTrigger("MoveToTarget");
    }

    void Update() {
        var state = m_StateMachine.GetCurrentAnimatorStateInfo(0);
        if (state.IsName("MoveToTarget")) {
            MoveToTarget();
        } else if (state.IsName("Idle")) {
            Idle();
        }
    }

    void Idle() {

    }

    void MoveToTarget() {
        m_MoveTarget = m_Player.transform.position;

        var direction = m_MoveTarget - m_PositionInternal;

        m_StateMachine.SetBool("TargetReached", direction.magnitude < 1.5f);;

        direction.Normalize();
        Move(direction, Scaling.WITH_SPEED_AND_TIME);
    }
}
