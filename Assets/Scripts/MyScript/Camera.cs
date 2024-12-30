using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;               // ��ң�С�򣩵�Transform
    public Vector3 cameraOffset = new Vector3(0, 5, -10); // �������ƫ����
    public float smoothSpeed = 0.125f;     // ƽ��������ٶ�
    public float focusSpeed = 1f;          // �۽����̵��ٶ�
    public Vector3 focusRotation = new Vector3(30f, 0f, 0f); // �۽����Ŀ����ת�Ƕ�

    private Vector3 initialPosition;       // ������ĳ�ʼλ��
    private Quaternion initialRotation;    // ������ĳ�ʼ��ת
    private bool isFocusing = true;        // �Ƿ����ھ۽�

    void Start()
    {
        if (player == null)
        {
            Debug.LogError("Player not assigned! Please assign the player object.");
            return;
        }

        // ��¼������ĳ�ʼλ�ú���ת
        initialPosition = transform.position;
        initialRotation = transform.rotation;
    }

    void Update()
    {
        if (player == null)
        {
            return; // ������δ���ã������в���
        }

        // Ŀ��λ�ú���ת
        Vector3 targetPosition = player.position + cameraOffset;
        Quaternion targetRotation = Quaternion.Euler(focusRotation);

        if (isFocusing)
        {
            // �ھ۽������У�ͬʱƽ������λ�ú���ת�Ƕ�
            transform.position = Vector3.Lerp(transform.position, targetPosition, focusSpeed * Time.deltaTime);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, focusSpeed * Time.deltaTime);

            // ����Ƿ�ӽ�Ŀ��״̬
            if (Vector3.Distance(transform.position, targetPosition) < 0.1f &&
                Quaternion.Angle(transform.rotation, targetRotation) < 1f)
            {
                isFocusing = false; // �۽����
            }
        }
        else
        {
            // �۽���ɺ�ƽ������С��
            transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, smoothSpeed);
        }
    }

    public void ResetFocus(Vector3 newOffset, Vector3 newRotation, float newFocusSpeed)
    {
        // �ṩ�������ڶ�̬����������ľ۽�����
        cameraOffset = newOffset;
        focusRotation = newRotation;
        focusSpeed = newFocusSpeed;

        // ����״̬
        isFocusing = true;
    }
}
