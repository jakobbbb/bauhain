using UnityEngine;
using UnityEngine.Tilemaps;

public class Room : MonoBehaviour {
    public int RoomId = -1;

    private TilemapCollider2D m_Collider;

    public static Vector2 GetRandomWithinBounds(Bounds bounds) {
        return new Vector2(
                Random.Range(bounds.min.x, bounds.max.x),
                Random.Range(bounds.min.y, bounds.max.y)
        );
    }

    public void Start() {
        m_Collider = GetComponent<TilemapCollider2D>();
    }

    public Vector2 RandomPositionWithinRoom() {
        Vector2 p = Vector2.zero;
        bool found_point = false;
        int n = 0;
        while (!found_point) {
            p = GetRandomWithinBounds(m_Collider.composite.bounds);
            var closest = m_Collider.ClosestPoint(p);
            var dist = (closest - p).magnitude;
            found_point = dist < 0.01f || n > 100;
            ++n;
        }
        if (n > 100) {
            Debug.LogWarning("Did not find a point :c");
        }
        return p;
    }
}
