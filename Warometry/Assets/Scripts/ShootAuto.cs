using UnityEngine;
using System.Collections;

public class ShootAuto : MonoBehaviour
{

    public float speed = 30; // �������� ����
    public Rigidbody2D bullet; // ������ ����� ����
    public Transform gunPoint; // ����� ��������
    public float fireRate = 1; // ����������������

    public Transform zRotate; // ������ ��� �������� �� ��� Z
    public bool facingRight = true;
    private int invert;

    // ����������� ��������
    public float minAngle = -40;
    public float maxAngle = 40;

    private float curTimeout, angle;
    private Vector3 mouse;

    void Start()
    {
        if (!facingRight) invert = -1; else invert = 1;
    }

    void SetRotation()
    {
        Vector3 mousePosMain = Input.mousePosition;
        mousePosMain.z = Camera.main.transform.position.z;
        mouse = Camera.main.ScreenToWorldPoint(mousePosMain);
        Vector3 lookPos = mouse - transform.position;
        angle = Mathf.Atan2(lookPos.y, lookPos.x * invert) * Mathf.Rad2Deg;
        angle = Mathf.Clamp(angle * invert, minAngle, maxAngle);
        zRotate.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Fire();
        }
        else
        {
            curTimeout = 1000;
        }

        if (zRotate) SetRotation();

        if (mouse.x < zRotate.position.x && facingRight) Flip();
        else if (mouse.x > zRotate.position.x && !facingRight) Flip();
    }

    void Flip() // отражение по горизонтали
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;  
        invert *= -1;
        transform.localScale = theScale;
    }

        void Fire()
    {
        curTimeout += Time.deltaTime;
        if (curTimeout > fireRate)
        {
            curTimeout = 0;
            Vector3 direction = gunPoint.position - transform.position;
            Rigidbody2D clone = Instantiate(bullet, gunPoint.position, Quaternion.identity) as Rigidbody2D;
            clone.linearVelocity = transform.TransformDirection(direction.normalized * speed);
            clone.transform.right = direction.normalized;
        }
    }
}
