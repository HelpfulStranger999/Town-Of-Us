using System;
using System.Collections.Generic;
using System.Text;

namespace TownOfUs.Extensions
{
    public static class PlayerExtensions
    {
        public static void KillPlayer(this PlayerControl self, PlayerControl? target)
        {
            if (AmongUsClient.Instance.IsGameOver)
                return;

            var logger = PluginSingleton<TownOfUs>.Instance.Log;

            target = target ?? self;
            if (self.Data.Disconnected)
            {
                logger.LogWarning("A disconnected player attempted to kill another player.");
                return;
            }

            if (self.Data.IsDead)
            {
                logger.LogWarning($"{self.name} tried to kill {target.name}, but {self.name} is dead!");
                return;
            }
            else if (target.Data.IsDead)
            {
                logger.LogWarning($"{self.name} tried to kill {target.name}, but {target.name} is already dead!");
                return;
            }

            if (self.AmOwner)
            {
                if (Constants.ShouldPlaySfx()) SoundManager.Instance.PlaySound(self.KillSfx, false, 0.8f);

            }
        }
    }
}
