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

    public GameObject ReplaceObject;

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

    public virtual IEnumerator ExecuteObstacleEffect(GameObject parent, HerculessMover herculess) {
        yield return null;
    }

}

[CreateAssetMenu(menuName = "Herculess/ObstacleEffects/ChangeDirectionHerculess", fileName = "ChangeDirectionObstacleEffect")]
class ChangeDirectionObstacleEffect : ObstacleEffectsSo
{
    public override IEnumerator ExecuteObstacleEffect(GameObject parent, HerculessMover herculess)
    {
        Vector3 rotatedFor = Vector3.zero;
        while (Mathf.Abs(rotatedFor.y) < Mathf.Abs(RotateHerculess.y))
        {
            Vector3 rotationVector = new Vector3(0.0f, RotationWeight * Time.deltaTime, 0.0f);
            herculess.transform.Rotate(rotationVector, Space.World);
            rotatedFor += rotationVector;
            yield return null;
        }
        if (ObstacleType == global::ObstacleType.ObstacleTypes.Skeleton) Destroy(parent);
    }
}

[CreateAssetMenu(menuName = "Herculess/ObstacleEffects/KillHerculess", fileName = "KillHerculessObstacleEffect")]
class KillHerculessObstacleEffect : ObstacleEffectsSo
{
    public override IEnumerator ExecuteObstacleEffect(GameObject parent, HerculessMover herculess)
    {
        // spawn animator and effects n such
        herculess.enabled = false;
        yield return new WaitForSeconds(0.2f);
        Destroy(herculess.gameObject);
    }
}

[CreateAssetMenu(menuName = "Herculess/ObstacleEffects/ChangeDirectionWineHerculess", fileName = "ChangeDirectionObstacleWineEffect")]
class ChangeDirectionWineEffect : ObstacleEffectsSo
{
    public override IEnumerator ExecuteObstacleEffect(GameObject parent, HerculessMover herculess)
    {
        while (!herculess.transform.rotation.Equals(RotateHerculessQuaternion))
        {
            herculess.transform.rotation = Quaternion.Slerp(herculess.transform.rotation, RotateHerculessQuaternion, Time.deltaTime * RotationWeight);
            yield return null;
        }
        Destroy(parent);
    }
}

[CreateAssetMenu(menuName = "Herculess/ObstacleEffects/DontChangeDirection", fileName = "DontChangeDirection")]
class DontChangeDirection : ObstacleEffectsSo
{
    public override IEnumerator ExecuteObstacleEffect(GameObject parent, HerculessMover herculess)
    {
        yield return null;
    }
}

[CreateAssetMenu(menuName = "Herculess/ObstacleEffects/WinGame", fileName = "WinGameObstacle")]
class WinGame : ObstacleEffectsSo
{
    public override IEnumerator ExecuteObstacleEffect(GameObject parent, HerculessMover herculess)
    {
        herculess.MovementDirection = Vector3.zero;
        herculess.animController.SetBool("IsRunning", false);
        herculess.GetComponent<BoxCollider>().enabled = false;
        yield return null;
    }
}