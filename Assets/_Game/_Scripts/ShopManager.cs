using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace _Game._Scripts
{
    public class ShopManager : MonoBehaviour
    {
        public PlayerController player;
        public GameManager gM;
        public GameObject barrelPrefab;

        public TextMeshProUGUI messageText;
        public float shootSpeedIncrease = 0.2f;

        public int healthPrice = 2;
        public int barrelPrice = 50;
        public int healPrice = 10;

        public void IncreaseHealth(int amount)
        {
            if (gM.coins >= healthPrice * amount)
            {
                gM.coins -= healthPrice * amount;
                gM.playerHealth.maxHealth += amount;
                gM.RefreshInfo();
                StartCoroutine(ShowMessage("Health Increased"));
            }
            else
            {
                StartCoroutine(ShowMessage("Not Enough Money"));
            }
        }

        public void IncreaseBarrels(int amount)
        {
            if (gM.coins >= barrelPrice * amount)
            {
                if (player.barrelPositions.Length == 0)
                {
                    StartCoroutine(ShowMessage("Max Barrels Reached"));
                    return;
                }
                
                gM.coins -= barrelPrice * amount;
                
                var i = Random.Range(0,player.barrelPositions.Length - 1);
                var barrel = Instantiate(barrelPrefab, player.barrelPositions[i].position, Quaternion.identity, player.transform);
                List<Transform> list = new List<Transform>(player.barrelPositions);
                list.RemoveAt(i);
                player.barrelPositions = list.ToArray();
                
                List<Transform> list2 = new List<Transform>(player.shootPoint);
                list2.Add(barrel.transform);
                
                player.shootPoint = list2.ToArray();

                player.shootTime += shootSpeedIncrease;
                
                StartCoroutine(ShowMessage("Barrels Added"));
            }
            else
            {
                StartCoroutine(ShowMessage("Not Enough Money"));
            }
        }

        public void Heal()
        {
            if (gM.coins >= healPrice)
            {
            	if (gM.playerHealth.currentHealth == gM.playerHealth.maxHealth)
            	{
            		StartCoroutine(ShowMessage("Already Healed"));
            	}
            	
                gM.coins -= healPrice;
                gM.playerHealth.currentHealth = gM.playerHealth.maxHealth;
                StartCoroutine(ShowMessage("Healed"));
            }
            else
            {
                StartCoroutine(ShowMessage("Not Enough Money"));
            }
            
        }

        IEnumerator ShowMessage(string message)
        {
            messageText.text = message;

            yield return new WaitForSeconds(5f);

            messageText.text = "";
        }
    }
}