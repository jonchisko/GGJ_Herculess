using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleBehaviour : MonoBehaviour
{
    public ObstacleEffectsSo obstacleEffectData;
    public float reenableColliderTime = 3.0f;
    private Collider collider_;
    private Renderer rendererComponent;

    // Start is called before the first frame update
    void Start()
    {
        collider_ = GetComponent<Collider>();
        rendererComponent = GetComponentInChildren<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Vector3 GetSurfacePosition()
    {
        Vector3 tmp = this.transform.position;
        return new Vector3(tmp.x, tmp.y + rendererComponent.bounds.size.y / 2.0f, tmp.z);
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
        StartCoroutine(obstacleEffectData.ExecuteObstacleEffect(this.gameObject, herculess));
    }

    IEnumerator RenableColliderAfterTime()
    {
        collider_.enabled = false;
        yield return new WaitForSeconds(reenableColliderTime);
        collider_.enabled = true;
    }
    
}
