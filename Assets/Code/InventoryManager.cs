using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{

    private Dictionary<Spell, int> abilityInventory;

    public delegate void SpellUpdate(InventoryManager inv);
    public static event SpellUpdate OnSpellInventoryUpdate;

    private void Awake()
    {
        abilityInventory = new Dictionary<Spell, int>();
    }

    // Start is called before the first frame update
    void Start()
    {
        SetUpSpellInventory();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public int GetAbilityInventoryDict(Spell spell)
    {
        int value;
        abilityInventory.TryGetValue(spell, out value);
        return value;
    }

    private void SetUpSpellInventory()
    {
        abilityInventory.Add(Spell.Lightning, 5);
        abilityInventory.Add(Spell.Skeleton, 5);
        abilityInventory.Add(Spell.Wine, 5);
        abilityInventory.Add(Spell.MakeWay, 5);
        OnSpellInventoryUpdate?.Invoke(this);
    }

    public bool BuySpell(Spell spellType)
    {
        if (abilityInventory[spellType] > 0)
        {
            abilityInventory[spellType] -= 1;
            OnSpellInventoryUpdate?.Invoke(this);
            return true;
        }
        return false;
    }

}
