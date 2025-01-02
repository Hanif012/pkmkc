/*
Yarn Spinner is licensed to you under the terms found in the file LICENSE.md.
*/

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

namespace Yarn.Unity.Example 
{
    /// <summary>
    /// clones dialogue bubbles for the ChatDialogue example
    /// </summary>
    public class PhoneChatDialogueHelper : DialogueViewBase
    {
        DialogueRunner runner;
        public TMPro.TextMeshProUGUI text;
        public GameObject optionsContainer;
        public OptionView optionPrefab;

        [Tooltip("This is the chat message bubble UI object (what we are cloning for each message!)... NOT the container group for all chat bubbles")]
        public GameObject dialogueBubblePrefab;
        public float lettersPerSecond = 10f;
        bool isFirstMessage = true;
        bool isRightAlignment = true;
        Color currentBGColor = Color.black, currentTextColor = Color.white;
        int PaddingLeft = 0;
        int PaddingRight = 0;

        void Awake()
        {
            var padding = dialogueBubblePrefab.GetComponent<HorizontalLayoutGroup>();
            PaddingLeft = padding.padding.left;
            PaddingRight = padding.padding.right;
            runner = GetComponent<DialogueRunner>();
            runner.AddCommandHandler("Me", SetSenderMe); 
            runner.AddCommandHandler("Them", SetSenderThem); 
            runner.AddCommandHandler<int>("Deposit", DepositMoney); 
            runner.AddCommandHandler<int>("Withdraw", WithdrawMoney);
            runner.AddCommandHandler<string>("AddOrder", AddOrder);
            runner.AddCommandHandler<string>("AddFood", AddFood);
            optionsContainer.SetActive(false);
        }

        void Start () 
        {
            dialogueBubblePrefab.SetActive(false);
            UpdateMessageBoxSettings();
        }

        // YarnCommand <<Me>>, but does not use YarnCommand C# attribute, registers in Awake() instead
        public void SetSenderMe() 
        {
            isRightAlignment = true;
            currentBGColor = Color.blue;
            currentTextColor = Color.white;
        }

        // YarnCommand <<Them>> does not use YarnCommand C# attribute, registers in Awake() instead
        public void SetSenderThem() 
        {
            isRightAlignment = false;
            currentBGColor = Color.white;
            currentTextColor = Color.black;
        }

        public void DepositMoney(int amount)
        {
            FindObjectOfType<Bank>().Deposit(amount);
        }

        public void WithdrawMoney(int amount)
        {
            FindObjectOfType<Bank>().Withdraw(amount);
        }

        public void AddOrder(string foodName)
        {
            var orderClass = FindObjectOfType<OrderClass>();
            var food = orderClass.availableFoods.foodList.Find(f => f.foodName == foodName);
            if (food != null)
            {
                orderClass.AddOrder(food, 1);
                Debug.Log(food.foodName + " added to order.");
                return;
            }
            else
            {
                Debug.Log(foodName + " not found in available foods.");
                return;
            }
        }

        public void AddFood(string foodName)
        {
            var inventory = FindObjectOfType<Inventory>();
            var food = inventory.FoodsInventory.Find(f => f.foodName == foodName);
            if (food != null)
            {
                inventory.AddFood(food);
                return;
            }
            else
            {
                Debug.Log(foodName + " not found in food list.");
                return;
            }
        }



        // when we clone a new message box, re-style the message box based on whether SetSenderMe or SetSenderThem was most recently called
        void UpdateMessageBoxSettings() 
        {
            var bg = dialogueBubblePrefab.GetComponentInChildren<Image>();
            bg.color = currentBGColor;
            var message = dialogueBubblePrefab.GetComponentInChildren<TMPro.TextMeshProUGUI>();
            message.text = "";
            message.color = currentTextColor;

            var layoutGroup = dialogueBubblePrefab.GetComponent<HorizontalLayoutGroup>();
            
            if ( isRightAlignment ) 
            {
                layoutGroup.padding.left = PaddingLeft;
                layoutGroup.padding.right = PaddingRight;
                bg.transform.SetAsLastSibling();
            }
            else
            {
                layoutGroup.padding.left = PaddingRight;
                layoutGroup.padding.right = PaddingLeft;
                bg.transform.SetAsFirstSibling();
            }
        }

        public void CloneMessageBoxToHistory()
        {
            // if this isn't the very first message, then clone current message box and move it up
            if ( isFirstMessage == false )
            {
                var oldClone = Instantiate( 
                    dialogueBubblePrefab, 
                    dialogueBubblePrefab.transform.position, 
                    dialogueBubblePrefab.transform.rotation, 
                    dialogueBubblePrefab.transform.parent
                );
                dialogueBubblePrefab.transform.SetAsLastSibling();
            }
            isFirstMessage = false;

            // reset message box and configure based on current settings
            dialogueBubblePrefab.SetActive(true);
            UpdateMessageBoxSettings();
        }

        Coroutine currentTypewriterEffect;

        public override void RunLine(LocalizedLine dialogueLine, Action onDialogueLineFinished)
        {
            if (currentTypewriterEffect != null)
            {
                StopCoroutine(currentTypewriterEffect);
            }

            CloneMessageBoxToHistory();

            text.text = dialogueLine.TextWithoutCharacterName.Text;

            currentTypewriterEffect = StartCoroutine(ShowTextAndNotify());

            IEnumerator ShowTextAndNotify() {
                yield return StartCoroutine(Effects.Typewriter(text, lettersPerSecond, null));
                currentTypewriterEffect = null;
                onDialogueLineFinished();
            }
        }

        public override void RunOptions(DialogueOption[] dialogueOptions, Action<int> onOptionSelected)
        {
            foreach(Transform child in optionsContainer.transform)
            {
                Destroy(child.gameObject);
            }

            optionsContainer.SetActive(true);

            for (int i = 0; i < dialogueOptions.Length; i++)
            {
                DialogueOption option = dialogueOptions[i];
                var optionView = Instantiate(optionPrefab);
                
                optionView.transform.SetParent(optionsContainer.transform, false);

                optionView.Option = option;

                optionView.OnOptionSelected = (selectedOption) =>
                {
                    optionsContainer.SetActive(false);
                    onOptionSelected(selectedOption.DialogueOptionID);
                };
            }
        }
    }

}
