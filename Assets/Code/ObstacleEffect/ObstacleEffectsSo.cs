using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ObstacleEffectsSo : ScriptableObject
{
    [SerializeField] 
    private Vector3 rotateHerculess;

    [SerializeField] 
    private float rotationWeight;

    [SerializeField] 
    private ObstacleType.ObstacleTypes obstacleType;

    public ObstacleType.ObstacleTypes ObstacleType => obstacleType;

    private Quaternion rotateHerculessQuaternion;
    
    public Vector3 RotateHerculess
    {
        get => rotateHerculess;
        set => rotateHerculess = value;
    }

    public Quaternion RotateHerculessQuaternion
    {
        get
        {
            return rotateHerculessQuaternion;
        }
        set
        {
            rotateHerculessQuaternion = value;
        }
    }
    
    public float RotationWeight => rotationWeight;

    public abstract IEnumerator ExecuteObstacleEffect(HerculessMover herculess);

}

[CreateAssetMenu(menuName = "Herculess/ObstacleEffects/ChangeDirectionHerculess", fileName = "ChangeDirectionObstacleEffect")]
public class ChangeDirectionObstacleEffect : ObstacleEffectsSo
{
    public override IEnumerator ExecuteObstacleEffect(HerculessMover herculess)
    {
        Vector3 rotatedFor = Vector3.zero;
        while (Mathf.Abs(rotatedFor.y) < Mathf.Abs(RotateHerculess.y))
        {
            Vector3 rotationVector = new Vector3(0.0f, RotationWeight * Time.deltaTime, 0.0f);
            herculess.transform.Rotate(rotationVector, Space.World);
            rotatedFor += rotationVector;
            yield return null;
        }
    }
}

[CreateAssetMenu(menuName = "Herculess/ObstacleEffects/KillHerculess", fileName = "KillHerculessObstacleEffect")]
public class KillHerculessObstacleEffect : ObstacleEffectsSo
{
    public override IEnumerator ExecuteObstacleEffect(HerculessMover herculess)
    {
        // spawn animator and effects n such
        herculess.enabled = false;
        yield return new WaitForSeconds(1.0f);
        Destroy(herculess.gameObject);
    }
}

[CreateAssetMenu(menuName = "Herculess/ObstacleEffects/ChangeDirectionWineHerculess", fileName = "ChangeDirectionObstacleWineEffect")]
public class ChangeDirectionWineEffect : ObstacleEffectsSo
{
    public override IEnumerator ExecuteObstacleEffect(HerculessMover herculess)
    {
        while (!herculess.transform.rotation.Equals(RotateHerculessQuaternion))
        {
            herculess.transform.rotation = Quaternion.Slerp(herculess.transform.rotation, RotateHerculessQuaternion, Time.deltaTime * RotationWeight);
            yield return null;
        }
    }
}
