using UnityEngine;
using System.Collections.Generic;

public class Gravitation : MonoBehaviour
{
    public static List<Gravitation> otherObj;
    private Rigidbody rb;
    const float G = 0.00667f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        if (otherObj == null)
        {
            otherObj = new List<Gravitation>();
        }
        otherObj.Add(this);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        foreach (Gravitation obj in otherObj)
        {
            if (obj != this)
            {
                AttackForce(obj);
            }
        }
    }

    void AttackForce(Gravitation other)
    {
        Rigidbody otherRb = other.rb; //ดึงมวล m
        Vector3 direction = rb.position - otherRb.position; // ทิศทางจากวัตถุมวล M ไป m

        float distance = direction.magnitude; // หาระยะห่าง r
        if (distance == 0f) return; // ป้องกันไม่ให้มีแรงดึงดูด เมื่อวัตถุทั้งสองอยู่ตำแหน่งเดียวกัน

        // F = G(m1 * m2) / r^2
        float forceMagnitude = G * (rb.mass * otherRb.mass) / Mathf.Pow(distance, 2);
        Vector3 gravitationForce = forceMagnitude * direction.normalized;
        otherRb.AddForce(gravitationForce);
    }
}
