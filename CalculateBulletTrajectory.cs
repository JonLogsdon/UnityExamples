using UnityEngine;
using System.Collections;

public class BulletTrajectory : MonoBehavior
{
    var maxPoints = 500;
    var lineMaterial : Material;
    var ballPrefab : RigidBody;
    var force = 10.0;
    private var pathLine : VectorLine;
    private var pathIndex = 0;
    private var pathPoints : Vector3[];
    
    void Start()
    {
        pathPoints = new Vector3[maxPoints];
        pathLine = new VectorLine("Path", pathPoints, lineMaterial, 12.0, LineType.Continuous);
        var ball = Instantiate(ballPrefab, Vector3.zero, Quaternion.Euler(300.0, 70.0, 310.0)) as Rigidbody;
        
        ball.useGravity = true;
        ball.AddForce(ball.transform.forward * force, ForceMode.Impulse);
        SamplePoints(ball.transform);
    }
    
    void SamplePoints(thisTransform : Transform)
    {
        var running = true;
        while(running)
        {
            pathPoints[pathIndex] = thisTransform.position;
            if (++pathIndex == maxPoints)
            {
                running = false;
            }
            yield WaitForSeconds(0.05);
            pathLine.maxDrawIndex = pathIndex - 1;
            Vector.DrawLine(pathLine);
            Vector.SetTextureScale(pathLine, 1.0);
        }
}
