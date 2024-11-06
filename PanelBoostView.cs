using System;
using BLINK.RPGBuilder.Characters;
using BLINK.RPGBuilder.LogicMono;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Development.Scripts.Booster
{
    public class PanelBoostView : MonoBehaviour
    {
        public event Action ClickedBuyButton;
    
        public Button CloseButton;
        public Button CostButton;
        public TMP_Text TextCostButton;
        public Color DefaultColorInTextButton;
        public Color ColorNotEnoughFunds;
   
        private RPGCurrency _currency;
        private Coroutine _coroutine;
        private int _cost;

        private void OnEnable()
        {
            CostButton.onClick.AddListener(BuyEnter);
            CloseButton.onClick.AddListener(ClosePanel);

            if (_currency == null)
                return;

            if (_coroutine != null)
                StopCoroutine(_coroutine);

            _coroutine =
                RPGBuilderEssentials.Instance.RunnerElements.UpdateWhileObjectIsActive(gameObject,
                    CheckCurrentCurrency);
        }

        private void OnDisable()
        {
            CostButton.onClick.RemoveListener(BuyEnter);
            CloseButton.onClick.RemoveListener(ClosePanel);
        }

        public void SetContentButton(RPGCurrency Currency, int cost)
        {
            SetText(Currency, cost, DefaultColorInTextButton);

            _currency = Currency;
            _cost = cost;
        }
    
        private void BuyEnter() => 
            ClickedBuyButton?.Invoke();
    
        private void CheckCurrentCurrency()
        {
            SetText(_currency, _cost, Character.Instance.getCurrencyAmount(_currency) -
                _cost >= 0
                    ? DefaultColorInTextButton
                    : ColorNotEnoughFunds);
        }

        private void SetText(RPGCurrency Currency, int cost, Color color)
        {
            TextCostButton.color = color;
            TextCostButton.text = $"<sprite name={Currency.entryName}> {cost}";
        }

        private void ClosePanel() => 
            gameObject.SetActive(false);
    }
}
