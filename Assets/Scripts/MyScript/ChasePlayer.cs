using UnityEngine;

public class ChasePlayer : MonoBehaviour
{
    public float chaseSpeed = 5f; // 追击速度
    private Transform player;    // 玩家（小球）的 Transform

    void Start()
    {
        // 查找目标小球（Tag 设置为 "Player"）
        GameObject playerObject = GameObject.FindWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
        }
        else
        {
            Debug.LogError("Player object not found. Make sure the player has the tag 'Player'.");
        }
    }

    void Update()
    {
        if (player != null)
        {
            // 计算从当前物体到目标小球的方向
            Vector3 direction = (player.position - transform.position).normalized;

            // 移动当前物体朝向目标小球
            transform.position += direction * chaseSpeed * Time.deltaTime;
        }
    }
}
