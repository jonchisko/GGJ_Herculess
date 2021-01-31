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

    public delegate void SelectedAbility(FireAbility fireManager);
    public static event SelectedAbility OnSelectedAbility;

    public GameObject skeletonAbility;
    public GameObject wineAbility;
    public InventoryManager inventory;
    private Spell[] spells = new Spell[] { Spell.Lightning, Spell.MakeWay, Spell.Skeleton, Spell.Wine };
    private const int MaxAbilities = 4;
    private bool canFire = true;

    public int CurrentlySelectedAbility { get; private set; } = 0;

    // Start is called before the first frame update
    void Start()
    {
        OnSelectedAbility?.Invoke(this);
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
            CurrentlySelectedAbility += 1;
            if (CurrentlySelectedAbility >= MaxAbilities)
            {
                CurrentlySelectedAbility = 0;
            }
            Debug.Log("Ability: " + spells[CurrentlySelectedAbility]);
            OnSelectedAbility?.Invoke(this);
        }
    }

    private void FireSpellAbility()
    {
        if (Input.GetAxis("Fire1") > 0 && canFire)
        {
            if (inventory.BuySpell(spells[CurrentlySelectedAbility]))
            {
                Debug.Log("BOUGHT " + spells[CurrentlySelectedAbility]);
                StartCoroutine(ReloadTime());
                RaycastHit hit;
                if (Physics.Raycast(camera.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity))
                {
                    Debug.Log(hit.transform.name);
                    var hitObstacle = hit.transform.GetComponent<ObstacleBehaviour>();
                    var hitObject = hitObstacle?.obstacleEffectData;
                    if (hitObject != null)
                    {
                        if (spells[CurrentlySelectedAbility] == Spell.Lightning && hitObject.ObstacleType == ObstacleType.ObstacleTypes.Forest
                            || spells[CurrentlySelectedAbility] == Spell.MakeWay && hitObject.ObstacleType == ObstacleType.ObstacleTypes.River)
                        {
                            Instantiate(hitObject.ReplaceObject, hit.transform.position, hit.transform.rotation);
                            hit.transform.gameObject.SetActive(false);
                        }

                        if (spells[CurrentlySelectedAbility] == Spell.Skeleton)
                        {
                            Instantiate(skeletonAbility, hitObstacle.GetSurfacePosition(), Quaternion.identity);
                        }

                        if (spells[CurrentlySelectedAbility] == Spell.Wine)
                        {
                            Instantiate(wineAbility, hitObstacle.GetSurfacePosition(), Quaternion.identity);
                        }
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

    public Spell GetCurrentlySelectedSpell()
    {
        return spells[CurrentlySelectedAbility];
    }

}
