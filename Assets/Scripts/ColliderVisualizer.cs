using UnityEngine;

public class ColliderVisualizer : MonoBehaviour
{
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Collider collider = GetComponent<Collider>();

        if (collider is BoxCollider boxCollider)
        {
            Gizmos.matrix = Matrix4x4.TRS(boxCollider.transform.position, boxCollider.transform.rotation, boxCollider.transform.lossyScale);
            Gizmos.DrawWireCube(boxCollider.center, boxCollider.size);
        }
        else if (collider is SphereCollider sphereCollider)
        {
            Gizmos.matrix = Matrix4x4.TRS(sphereCollider.transform.position, sphereCollider.transform.rotation, sphereCollider.transform.lossyScale);
            Gizmos.DrawWireSphere(sphereCollider.center, sphereCollider.radius);
        }
        else if (collider is CapsuleCollider capsuleCollider)
        {
            Gizmos.matrix = Matrix4x4.TRS(capsuleCollider.transform.position, capsuleCollider.transform.rotation, capsuleCollider.transform.lossyScale);
            Vector3 size = new Vector3(capsuleCollider.radius * 2, capsuleCollider.height, capsuleCollider.radius * 2);
            Gizmos.DrawWireCube(capsuleCollider.center, size);
        }
    }
}
