using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Spell
{
    Lightning,
    MakeWay,
    Skeleton,
    Wine,
}

public class FireAbility : MonoBehaviour
{
    [SerializeField]
    Camera camera;

    public GameObject skeletonAbility;
    public GameObject wineAbility;

    private int currentlySelectedAbility = 0;
    private Spell[] spells = new Spell[] { Spell.Lightning, Spell.MakeWay, Spell.Skeleton, Spell.Wine };
    private const int MaxAbilities = 4;
    private bool canFire = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SelectAbility();
        FireSpellAbility();
    }

    private void SelectAbility()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            currentlySelectedAbility += 1;
            if (currentlySelectedAbility >= MaxAbilities)
            {
                currentlySelectedAbility = 0;
            }
            Debug.Log("Ability: " + spells[currentlySelectedAbility]);
        }
    }

    private void FireSpellAbility()
    {
        if (Input.GetAxis("Fire1") > 0 && canFire)
        {
            StartCoroutine(ReloadTime());
            RaycastHit hit;
            if (Physics.Raycast(camera.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity))
            {
                Debug.Log(hit.transform.name);
                var hitObstacle = hit.transform.GetComponent<ObstacleBehaviour>();
                var hitObject = hitObstacle?.obstacleEffectData;
                if (hitObject != null)
                {
                    if (spells[currentlySelectedAbility] == Spell.Lightning && hitObject.ObstacleType == ObstacleType.ObstacleTypes.Forest
                        || spells[currentlySelectedAbility] == Spell.MakeWay && hitObject.ObstacleType == ObstacleType.ObstacleTypes.River)
                    {
                        Instantiate(hitObject.ReplaceObject, hit.transform.position, hit.transform.rotation);
                        hit.transform.gameObject.SetActive(false);
                    }

                    if (spells[currentlySelectedAbility] == Spell.Skeleton)
                    {
                        Instantiate(skeletonAbility, hitObstacle.GetSurfacePosition(), Quaternion.identity);
                    }

                    if (spells[currentlySelectedAbility] == Spell.Wine)
                    {
                        Instantiate(wineAbility, hitObstacle.GetSurfacePosition(), Quaternion.identity);
                    }
                }

            }
        }

        IEnumerator ReloadTime()
        {
            canFire = false;
            yield return new WaitForSeconds(1.0f);
            canFire = true;
        }
    }

}
