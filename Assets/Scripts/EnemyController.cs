using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] float moveSpeed;

    private void Update()
    {
        transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
    }
}
