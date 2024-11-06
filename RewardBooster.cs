using System;
using _Development.Scripts.Booster.Enum;
using _Development.Scripts.LootLevel;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Development.Scripts.Booster
{
    public class RewardBooster : MonoBehaviour
    {
        public TypeRewardBooster TypeReward;

        [ShowIf("TypeReward", TypeRewardBooster.Effect)]
        public RPGEffect Effect;

        [ShowIf("TypeReward", TypeRewardBooster.Loot)]
        public RPGLootTable Loot;

        public AdvertisingStarter Advertising;
        
        private IRewardCalculator _rewardCalculator;

        private void Start()
        {
            Advertising.PrizeReceived += GiveOutReward;
            _rewardCalculator = new RewardCalculator();
        }

        private void OnDestroy() =>
            Advertising.PrizeReceived -= GiveOutReward;

        private void GiveOutReward(CooldownBooster BoosterTimer)
        {
            switch (TypeReward)
            {
                case TypeRewardBooster.Effect:
                    GameActionsManager.Instance.ApplyEffect(RPGCombatDATA.TARGET_TYPE.Caster,
                        GameState.playerEntity, Effect.ID);
                    break;
                case TypeRewardBooster.Loot:
                    foreach (RPGLootTable.LOOT_ITEMS lootItems in Loot.lootItems)
                        _rewardCalculator.PutInInventory(lootItems);
                    break;
                case TypeRewardBooster.Default:
                    new Exception($"{gameObject.name} There is no reward set for this object");
                    break;
            }

            BoosterTimer.SetShowTimer();
        }
    }
}