﻿using Server.Models.Utilities;

namespace Server.Models.Spells.Models
{
    public class LowHealing : Spell
    {
        public LowHealing()
        {
            SpellType = SpellType.LowHealing;
            Name = "Подорожник";
            CoolDown = 45;
            Description = "Не много лечит раны.";
            ImagePath = "low_healing.png";
        }

        public override void Use(Entity user, params Entity[] target)
        {
            if (user != null)
            {
                var s = user.Stats;
                var heal = s.Strength + (s.Intelligence * 1.5);

                var resultHeal = user.TakeHealing((int)Math.Round(heal));

                string log = $"{user.Name} подлечился на {resultHeal.Count}. [{resultHeal.CurrentHP}/{resultHeal.MaxHP}]";
                SendBattleLog(log, user, target.First());
            }
        }
    }
}
