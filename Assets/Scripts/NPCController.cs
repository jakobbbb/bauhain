using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NPCController : CharacterController {

    private Vector3 m_MoveTarget;

    private PlayerController m_Player;

    [SerializeField]
    private Animator m_StateMachine;

    [System.Serializable]
    public class RoomPreferences {
        [Range(0.0f, 1.0f)]
        public float Entrance = 1.0f;
        [Range(0.0f, 1.0f)]
        public float Dancefloor = 1.0f;
        [Range(0.0f, 1.0f)]
        public float DJStage = 1.0f;
        [Range(0.0f, 1.0f)]
        public float CokeRoom = 1.0f;
        [Range(0.0f, 1.0f)]
        public float Backstage = 1.0f;
        [Range(0.0f, 1.0f)]
        public float Toilets = 1.0f;
        [Range(0.0f, 1.0f)]
        public float Stairs = 1.0f;
        [Range(0.0f, 1.0f)]
        public float Upstairs = 1.0f;
        [Range(0.0f, 1.0f)]
        public float Bar = 1.0f;

        public List<float> AsList() {
            return new List<float>{
                    Entrance,
                    Dancefloor,
                    DJStage,
                    CokeRoom,
                    Backstage,
                    Toilets,
                    Stairs,
                    Upstairs,
                    Bar,
            };
        }

        public List<float> Normalized() {
            var l = AsList();
            float sum = 0.0f;
            foreach (var el in l) {
                sum += el;
            }
            for (int i = 0; i < l.Count; ++i) {
                l[i] /= sum;
            }
            return l;
        }
    }

    [SerializeField]
    private RoomPreferences m_RoomPreferences;

    public int ChooseRandomRoom() {
        // TODO Weigh in if others are in the room already
        var psum = m_RoomPreferences.Normalized();
        for (int i = 1; i < psum.Count; ++i) {
            psum[i] += psum[i - 1];
        }

        float roll = Random.Range(0.0f, 1.0f);

        for (int i = 0; i < psum.Count; ++i) {
            if (roll < psum[i]) {
                return i;
            }
        }
        Debug.LogWarning("Did not find a room???");
        return 0;
    }

    public void Start() {
        m_MoveSpeed *= 0.75f;
        m_Player = GameObject.FindFirstObjectByType<PlayerController>();

        StartCoroutine(RandomRoomCoroutine());
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
        UpdateAnimator(Vector2.zero);
    }

    void MoveToTarget() {
        // m_MoveTarget = m_Player.transform.position;

        //var direction = m_MoveTarget - m_PositionInternal.position;
        var direction = m_MoveTarget - transform.position;

        m_StateMachine.SetBool("TargetReached", direction.magnitude < 1.5f);;

        direction.Normalize();
        Move(direction, Scaling.WITH_SPEED_AND_TIME);

        UpdateAnimator(direction);
    }

    private IEnumerator RandomRoomCoroutine() {
        while (true) {
            // TODO enable once rooms are set up
            yield return new WaitForSeconds(1.0f);
            var room_idx = ChooseRandomRoom();
            var room = GameManager.Instance.Rooms[room_idx];
            m_MoveTarget = room.RandomPositionWithinRoom();
            m_StateMachine.SetTrigger("MoveToTarget");
            Debug.Log(name + " moving to " + room.name);
            yield return new WaitForSeconds(14.0f);
        }
    }
}
