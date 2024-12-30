using UnityEngine;

public class ChasePlayer : MonoBehaviour
{
    public float chaseSpeed = 5f; // ׷���ٶ�
    private Transform player;    // ��ң�С�򣩵� Transform

    void Start()
    {
        // ����Ŀ��С��Tag ����Ϊ "Player"��
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
            // ����ӵ�ǰ���嵽Ŀ��С��ķ���
            Vector3 direction = (player.position - transform.position).normalized;

            // �ƶ���ǰ���峯��Ŀ��С��
            transform.position += direction * chaseSpeed * Time.deltaTime;
        }
    }
}
