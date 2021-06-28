using TownOfUs.CustomOption;
using TownOfUs.Services;

namespace TownOfUs.PatchesArchive.CustomOption
{
    public enum SlotBehaviorType
    {
        Import,
        Export
    }

    public class SlotButton : CustomButtonOption
    {
        public ISaveManager SaveManager { get; }
        public SlotBehaviorType BehaviorType { get; }
        public int SlotId { get; }

        public SlotButton(ISaveManager manager, SlotBehaviorType type, int slotId) : base($"Slot {slotId}")
        {
            BehaviorType = type;
            SaveManager = manager;
            SlotId = slotId;
        }

        public override void Click()
        {
            if (!AmongUsClient.Instance.AmHost) return;
            try
            {
                // TODO Finish implementing.
                switch (BehaviorType)
                {
                    case SlotBehaviorType.Import:
                        SaveManager.Load(SlotId);
                        break;
                    case SlotBehaviorType.Export:
                        SaveManager.Save(null, SlotId);
                        break;
                }
                // Flash red
            }
            catch
            {
                // Flash green
            }
        }

        private IEnumerator FlashGreen()
        {
            Setting.Cast<ToggleOption>().TitleText.color = Color.green;
            yield return new WaitForSeconds(0.5f);
            Setting.Cast<ToggleOption>().TitleText.color = Color.white;
        }

        private IEnumerator FlashRed()
        {
            Setting.Cast<ToggleOption>().TitleText.color = Color.red;
            yield return new WaitForSeconds(0.5f);
            Setting.Cast<ToggleOption>().TitleText.color = Color.white;
        }

        private IEnumerator FlashWhite()
        {
            yield return null;
        }
    }
}