using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Inventory : MonoBehaviour
{
    public GameObject[] slots;
    //public GameObject[] bagpack;
    bool isInstantiated;

    TextMeshProUGUI amountText;

    public GameObject itemToAdd;
    public int itemAmount;

    public Dictionary<string,int> inventoryItems = new Dictionary<string, int>();

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


   

    public void CheckSlotsAvailable(GameObject itemToAdd,string itemName,int itemAmount)
    {
        isInstantiated = false;
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].transform.childCount>0) 
            {
                slots[i].GetComponent<Slots>().isUsed = true;
            }
            else if(!isInstantiated && slots[i].GetComponent<Slots>().isUsed)
            {
                if (!inventoryItems.ContainsKey(itemName))
                {
                    GameObject item = Instantiate(itemToAdd, slots[i].transform.position,Quaternion.identity);
                    item.transform.SetParent(slots[i].transform,false);
                    item.transform.localPosition = new Vector3(0,0,0);
                    item.name = item.name.Replace("(Clone) ", "");
                    isInstantiated = true;
                    inventoryItems.Add(itemName, itemAmount);
                    amountText = slots[i].GetComponentInChildren<TextMeshProUGUI>();
                    amountText.text = itemAmount.ToString();
                    break;
                }
                else
                {
                    for (int j = 0; j < slots.Length; j++)
                    {
                        if (slots[j].transform.GetChild(0).gameObject.name == itemName)
                        {
                            inventoryItems[itemName] += itemAmount;
                            amountText.text = inventoryItems[itemName].ToString();
                            break;
                        }
                    }
                    break;
                }
            }
        }
    }

}
