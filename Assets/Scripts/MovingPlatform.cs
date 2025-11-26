using UnityEngine;
using System.Collections.Generic;

public class MovingPlatform : MonoBehaviour
{
    public float distance = 3f;
    public float speed = 2f;
    private Vector3 startPos;
    private int direction = 1;
    private Vector3 lastPosition;
    private List<Transform> passengersOnPlatform = new List<Transform>();

    void Start()
    {
        startPos = transform.position;
        lastPosition = transform.position;
    }

    void Update()
    {
        // Calculate movement
        Vector3 movement = Vector3.right * direction * speed * Time.deltaTime;
        transform.Translate(movement);

        // Move passengers
        foreach (Transform passenger in passengersOnPlatform)
        {
            if (passenger != null)
            {
                passenger.position += movement;
            }
        }

        lastPosition = transform.position;

        // Check distance from start
        if (Mathf.Abs(transform.position.x - startPos.x) >= distance)
        {
            direction *= -1;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (!passengersOnPlatform.Contains(collision.transform))
            {
                passengersOnPlatform.Add(collision.transform);
            }
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            passengersOnPlatform.Remove(collision.transform);
        }
    }
}