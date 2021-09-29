using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DemoDelivery
{
    public static class Explosion
    {
        public static void Explode(Vector3 position, float radius, float power, float torquePower = 0f, ForceMode2D forceMode = ForceMode2D.Impulse)
        {
            // Get all colliders in the explosion radius
            Collider2D[] objectsToMove = Physics2D.OverlapCircleAll(position, radius);

            // Get all the attached rigidbodies
            IEnumerable<Rigidbody2D> rigidBodies = objectsToMove.Where(o => o.attachedRigidbody != null).Select(o => o.attachedRigidbody);

            // Add the force to the object based on direction and distance from explosion
            foreach (var rb in rigidBodies)
            {
                float distance = Vector2.Distance(rb.transform.position, position);
                Vector2 direction = ((Vector2)(rb.transform.position - position)).normalized;
                Vector2 force = direction * power * (1f - (distance / radius));

                rb.AddForceAtPosition(force, position, forceMode);
            }

            // Add the torque based on the closest point on the collider, and the direction from the explosion to it
            foreach (var rb in rigidBodies)
            {
                Vector3 closestPoint = rb.ClosestPoint(position);
                float distance = Vector2.Distance(closestPoint, position);

                Vector2 directionToCenter = ((Vector2)(rb.transform.position - position)).normalized;
                Vector2 directionToClosestPoint = ((Vector2)(closestPoint - position)).normalized;

                // I rotate the closest point vector so the dot will give values based on parallel rather than perpendicular. This way 0 = same direction, 1 is left, and -1 is right
                float dot = Vector2.Dot(directionToCenter, Quaternion.Euler(0, 0, 90f) * directionToClosestPoint);

                //Vector2 force = direction * power * (1f - (distance / radius));
                float force = dot * (1f - (distance / radius)) * torquePower;
                rb.AddTorque(force, forceMode);
            }
        }
    }
}

