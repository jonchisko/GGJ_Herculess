using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InventoryUi : MonoBehaviour
{
    
    public Color selectedAbilityColor;
    public GameObject[] abilitiesUi;
    private Dictionary<string, Spell> translationAbility;

    private void Awake()
    {
        translationAbility = new Dictionary<string, Spell>{
            {"Lightning", Spell.Lightning},
            {"MakeWay", Spell.MakeWay },
            {"Skeleton", Spell.Skeleton},
            {"Wine", Spell.Wine} };
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        InventoryManager.OnSpellInventoryUpdate += OnAbilityUsedUpdateUi;
        FireAbility.OnSelectedAbility += OnSelectedAbility;
    }

    private void OnDisable()
    {
        InventoryManager.OnSpellInventoryUpdate += OnAbilityUsedUpdateUi;
        FireAbility.OnSelectedAbility -= OnSelectedAbility;
    }

    private void OnSelectedAbility(FireAbility instance)
    {
        // colour the selected ability
        var selectedSpell = instance.GetCurrentlySelectedSpell();
        foreach(var element in abilitiesUi)
        {
            Image comp = element.GetComponentInChildren<Image>();
            if (element.name.Equals(selectedSpell.ToString())) 
            {
                comp.color = selectedAbilityColor;
            } else
            {
                comp.color = Color.white;
            }
        }
    }

    private void OnAbilityUsedUpdateUi(InventoryManager instance)
    {
        // update ability counter
        foreach (var element in abilitiesUi)
        {
            int value = instance.GetAbilityInventoryDict(translationAbility[element.name]);
            element.GetComponentInChildren<TextMeshProUGUI>().text = value.ToString();
        }
    }

}
