using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTarget : MonoBehaviour
{
    public delegate void OnMovementFinished();

    public OnMovementFinished MovementFinished;

    public AnimationCurve movementCurve;

    public void FollowPlayer(GameObject player)
    {
        StartCoroutine(ValidateCurve(2f, player));
    }

    private IEnumerator ValidateCurve(float duration, GameObject player)
    {
        float initialTime = Time.time;
        float endTime = initialTime + duration;
        float t;
        Vector3 currentPosition = transform.position;

        while (Time.time < endTime)
        {
            t = Mathf.InverseLerp(initialTime, endTime, Time.time);
            Vector3 newPosition = player.transform.position;
            transform.position = Vector3.Lerp(currentPosition, newPosition, movementCurve.Evaluate(t));
            yield return null;
        }

        MovementFinished();
    }
}