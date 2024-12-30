using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;               // 玩家（小球）的Transform
    public Vector3 cameraOffset = new Vector3(0, 5, -10); // 摄像机的偏移量
    public float smoothSpeed = 0.125f;     // 平滑跟随的速度
    public float focusSpeed = 1f;          // 聚焦过程的速度
    public Vector3 focusRotation = new Vector3(30f, 0f, 0f); // 聚焦后的目标旋转角度

    private Vector3 initialPosition;       // 摄像机的初始位置
    private Quaternion initialRotation;    // 摄像机的初始旋转
    private bool isFocusing = true;        // 是否正在聚焦

    void Start()
    {
        if (player == null)
        {
            Debug.LogError("Player not assigned! Please assign the player object.");
            return;
        }

        // 记录摄像机的初始位置和旋转
        initialPosition = transform.position;
        initialRotation = transform.rotation;
    }

    void Update()
    {
        if (player == null)
        {
            return; // 如果玩家未设置，不进行操作
        }

        // 目标位置和旋转
        Vector3 targetPosition = player.position + cameraOffset;
        Quaternion targetRotation = Quaternion.Euler(focusRotation);

        if (isFocusing)
        {
            // 在聚焦过程中，同时平滑调整位置和旋转角度
            transform.position = Vector3.Lerp(transform.position, targetPosition, focusSpeed * Time.deltaTime);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, focusSpeed * Time.deltaTime);

            // 检测是否接近目标状态
            if (Vector3.Distance(transform.position, targetPosition) < 0.1f &&
                Quaternion.Angle(transform.rotation, targetRotation) < 1f)
            {
                isFocusing = false; // 聚焦完成
            }
        }
        else
        {
            // 聚焦完成后，平滑跟随小球
            transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, smoothSpeed);
        }
    }

    public void ResetFocus(Vector3 newOffset, Vector3 newRotation, float newFocusSpeed)
    {
        // 提供方法用于动态调整摄像机的聚焦参数
        cameraOffset = newOffset;
        focusRotation = newRotation;
        focusSpeed = newFocusSpeed;

        // 重置状态
        isFocusing = true;
    }
}
