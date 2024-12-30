using UnityEngine;

public class CubeGrab : MonoBehaviour
{
    private Rigidbody cubeRigidbody; // ������ĸ���
    private Transform player; // ��ң�С�򣩵� Transform
    private bool isBeingGrabbed = false; // �������Ƿ����ڱ�ץȡ
    private bool isCollidingWithPlayer = false; // �������Ƿ�������С����ײ
    private Quaternion originalRotation; // ��¼�������ԭʼ��ת
    public float maxDistance = 3f; // ���������С�������ƶ�����
    public float moveSpeed = 10f; // �������ƶ���ƽ���ٶ�
    public float throwForceMultiplier = 10f; // �׳�ʱʩ�ӵ�����С

    private Vector3 lastMousePosition; // ��¼��һ֡����λ��
    private Vector3 mouseDelta; // �����ƶ�������ٶ�

    void Start()
    {
        // ��ȡ������� Rigidbody
        cubeRigidbody = GetComponent<Rigidbody>();

        // �ҵ���ң�С�򣩵� Transform
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
        }
        else
        {
            Debug.LogError("Player object not found. Make sure the player has the tag 'Player'.");
        }

        // ��¼������ĳ�ʼ��ת
        originalRotation = transform.rotation;
    }

    void Update()
    {
        // ������������ڱ�ץȡ
        if (isBeingGrabbed)
        {
            // ��ȡ���λ�õ�����
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Plane plane = new Plane(Vector3.up, player.position); // ����һ��ˮƽƽ�棬��С��λ��Ϊ��׼
            if (plane.Raycast(ray, out float distance))
            {
                // ��ȡ������ƽ��Ľ���
                Vector3 targetPosition = ray.GetPoint(distance);

                // �����������ƶ���Χ��������С�򸽽���
                Vector3 direction = targetPosition - player.position; // �����С��Ŀ���ķ���
                if (direction.magnitude > maxDistance)
                {
                    targetPosition = player.position + direction.normalized * maxDistance; // �����������뷶Χ��
                }

                // ƽ���ƶ������嵽Ŀ��λ��
                transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * moveSpeed);

                // �������������ת����
                transform.rotation = originalRotation;

                // ��¼����ƶ�
                mouseDelta = Input.mousePosition - lastMousePosition; // ��������ƶ�������ٶ�
                lastMousePosition = Input.mousePosition; // ������һ֡�����λ��
            }

            // ����ɿ����������ͷ�������
            if (Input.GetMouseButtonUp(0))
            {
                isBeingGrabbed = false;
                cubeRigidbody.useGravity = true; // ��������
                cubeRigidbody.isKinematic = false; // �ָ�����ģ��

                // �׳�������
                Vector3 throwForce = new Vector3(mouseDelta.x, 0, mouseDelta.y) * throwForceMultiplier;
                cubeRigidbody.AddForce(throwForce, ForceMode.Impulse);
            }
        }
        else
        {
            // ������������£�����С����������������ײ
            if (isCollidingWithPlayer && Input.GetMouseButtonDown(0))
            {
                isBeingGrabbed = true;
                cubeRigidbody.useGravity = false; // �ر�����
                cubeRigidbody.isKinematic = true; // �ر�����ģ��

                // ��ץȡʱ��¼��ǰ����ת״̬
                originalRotation = transform.rotation;

                // ��ʼ������¼
                lastMousePosition = Input.mousePosition;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // ��⵽����ң�С����ײ
        if (collision.gameObject.CompareTag("Player"))
        {
            isCollidingWithPlayer = true; // ������ײ״̬Ϊ true
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        // ��⵽����ң�С�򣩷���
        if (collision.gameObject.CompareTag("Player"))
        {
            isCollidingWithPlayer = false; // ������ײ״̬
        }
    }
}
