using UnityEngine;
using System.Collections;

public class DoorController : MonoBehaviour
{
    public float openAngle = -90f;  // Angle to open the door
    public float openSpeed = 2f;    // Speed of door opening/closing
    public float closeDelay = 3f;   // Time before door closes
    private bool isOpening = false;
    private Quaternion closedRotation;
    private Quaternion openRotation;

    void Start()
    {
        closedRotation = transform.rotation;
        openRotation = Quaternion.Euler(0, openAngle, 0) * closedRotation;
    }

    void Update()
    {
        if (isOpening)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, openRotation, Time.deltaTime * openSpeed);
        }
        else
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, closedRotation, Time.deltaTime * openSpeed);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isOpening = true;
            StopAllCoroutines();
            StartCoroutine(CloseDoorAfterDelay());
        }
    }

    IEnumerator CloseDoorAfterDelay()
    {
        yield return new WaitForSeconds(closeDelay);
        isOpening = false;
    }
}
