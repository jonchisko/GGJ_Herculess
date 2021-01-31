using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleBehaviour : MonoBehaviour
{
    public ObstacleEffectsSo obstacleEffectData;
    public float reenableColliderTime = 3.0f;
    private Collider collider;
    
    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ExecuteObstacleEffect(HerculessMover herculess)
    {
        StartCoroutine(RenableColliderAfterTime());
        var wineEffect = obstacleEffectData as ChangeDirectionWineEffect;
        if (wineEffect != null)
        {
            Vector3 relativeDirection = this.transform.position - herculess.transform.position;
            relativeDirection = new Vector3(relativeDirection.x, 0.0f, relativeDirection.z);
            obstacleEffectData.RotateHerculessQuaternion = Quaternion.LookRotation(relativeDirection, Vector3.up);
            // TODO:, should be done by coroutine
            // herculess.transform.rotation = Quaternion.LookRotation(relativeDirection, Vector3.up);
        }
        StartCoroutine(obstacleEffectData.ExecuteObstacleEffect(herculess));
    }

    IEnumerator RenableColliderAfterTime()
    {
        collider.enabled = false;
        yield return new WaitForSeconds(reenableColliderTime);
        collider.enabled = true;
    }
    
}
