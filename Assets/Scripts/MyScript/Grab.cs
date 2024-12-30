using UnityEngine;

public class CubeGrab : MonoBehaviour
{
    private Rigidbody cubeRigidbody; // 立方体的刚体
    private Transform player; // 玩家（小球）的 Transform
    private bool isBeingGrabbed = false; // 立方体是否正在被抓取
    private bool isCollidingWithPlayer = false; // 立方体是否正在与小球碰撞
    private Quaternion originalRotation; // 记录立方体的原始旋转
    public float maxDistance = 3f; // 立方体相对小球的最大移动距离
    public float moveSpeed = 10f; // 立方体移动的平滑速度
    public float throwForceMultiplier = 10f; // 抛出时施加的力大小

    private Vector3 lastMousePosition; // 记录上一帧鼠标的位置
    private Vector3 mouseDelta; // 光标的移动方向和速度

    void Start()
    {
        // 获取立方体的 Rigidbody
        cubeRigidbody = GetComponent<Rigidbody>();

        // 找到玩家（小球）的 Transform
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
        }
        else
        {
            Debug.LogError("Player object not found. Make sure the player has the tag 'Player'.");
        }

        // 记录立方体的初始旋转
        originalRotation = transform.rotation;
    }

    void Update()
    {
        // 如果立方体正在被抓取
        if (isBeingGrabbed)
        {
            // 获取光标位置的射线
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Plane plane = new Plane(Vector3.up, player.position); // 定义一个水平平面，以小球位置为基准
            if (plane.Raycast(ray, out float distance))
            {
                // 获取射线与平面的交点
                Vector3 targetPosition = ray.GetPoint(distance);

                // 限制立方体移动范围（保持在小球附近）
                Vector3 direction = targetPosition - player.position; // 计算从小球到目标点的方向
                if (direction.magnitude > maxDistance)
                {
                    targetPosition = player.position + direction.normalized * maxDistance; // 限制在最大距离范围内
                }

                // 平滑移动立方体到目标位置
                transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * moveSpeed);

                // 保持立方体的旋转不变
                transform.rotation = originalRotation;

                // 记录鼠标移动
                mouseDelta = Input.mousePosition - lastMousePosition; // 计算光标的移动方向和速度
                lastMousePosition = Input.mousePosition; // 更新上一帧的鼠标位置
            }

            // 如果松开鼠标左键，释放立方体
            if (Input.GetMouseButtonUp(0))
            {
                isBeingGrabbed = false;
                cubeRigidbody.useGravity = true; // 启用重力
                cubeRigidbody.isKinematic = false; // 恢复物理模拟

                // 抛出立方体
                Vector3 throwForce = new Vector3(mouseDelta.x, 0, mouseDelta.y) * throwForceMultiplier;
                cubeRigidbody.AddForce(throwForce, ForceMode.Impulse);
            }
        }
        else
        {
            // 检测鼠标左键按下，并且小球正在与立方体碰撞
            if (isCollidingWithPlayer && Input.GetMouseButtonDown(0))
            {
                isBeingGrabbed = true;
                cubeRigidbody.useGravity = false; // 关闭重力
                cubeRigidbody.isKinematic = true; // 关闭物理模拟

                // 在抓取时记录当前的旋转状态
                originalRotation = transform.rotation;

                // 初始化鼠标记录
                lastMousePosition = Input.mousePosition;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // 检测到与玩家（小球）碰撞
        if (collision.gameObject.CompareTag("Player"))
        {
            isCollidingWithPlayer = true; // 设置碰撞状态为 true
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        // 检测到与玩家（小球）分离
        if (collision.gameObject.CompareTag("Player"))
        {
            isCollidingWithPlayer = false; // 重置碰撞状态
        }
    }
}
