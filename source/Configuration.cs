namespace TownOfUs
{
    public class Configuration
    {
        // Temporary Singleton
        public static Configuration Instance { get; private set; } = new Configuration();

        public GeneralConfiguration General { get; } = new GeneralConfiguration();

        public MayorConfiguration Mayor { get; } = new MayorConfiguration();
        public LoverConfiguration Lovers { get; } = new LoverConfiguration();
        public SheriffConfiguration Sheriff { get; } = new SheriffConfiguration();
        public EngineerConfiguration Engineer { get; } = new EngineerConfiguration();
        public SwapperConfiguration Swapper { get; } = new SwapperConfiguration();
        public InvestigatorConfiguration Investigator { get; } = new InvestigatorConfiguration();
        public TimeLordConfiguration TimeLord { get; } = new TimeLordConfiguration();
        public MedicConfiguration Medic { get; } = new MedicConfiguration();
        public SeerConfiguration Seer { get; } = new SeerConfiguration();
        public SpyConfiguration Spy { get; } = new SpyConfiguration();
        public SnitchConfiguration Snitch { get; } = new SnitchConfiguration();
        public AltruistConfiguration Altruist { get; } = new AltruistConfiguration();

        public JesterConfiguration Jester { get; } = new JesterConfiguration();
        public ShifterConfiguration Shifter { get; } = new ShifterConfiguration();
        public GlitchConfiguration Glitch { get; } = new GlitchConfiguration();
        public ExecutionerConfiguration Executioner { get; } = new ExecutionerConfiguration();
        public ArsonistConfiguration Arsonist { get; } = new ArsonistConfiguration();
        public PhantomConfiguration Phantom { get; } = new PhantomConfiguration();

        public JanitorConfiguration Janitor { get; } = new JanitorConfiguration();
        public MorphlingConfiguration Morphling { get; } = new MorphlingConfiguration();
        public CamouflagerConfiguration Camouflager { get; } = new CamouflagerConfiguration();
        public MinerConfiguration Miner { get; } = new MinerConfiguration();
        public SwooperConfiguration Swooper { get; } = new SwooperConfiguration();
        public AssassinConfiguration Assassin { get; } = new AssassinConfiguration();
        public UnderdogConfiguration Underdog { get; } = new UnderdogConfiguration();
        public UndertakerConfiguration Undertaker { get; } = new UndertakerConfiguration();

        public TorchConfiguration Torch { get; } = new TorchConfiguration();
        public DiseasedConfiguration Diseased { get; } = new DiseasedConfiguration();
        public FlashConfiguration Flash { get; } = new FlashConfiguration();
        public TiebreakerConfiguration Tiebreaker { get; } = new TiebreakerConfiguration();
        public DrunkConfiguration Drunk { get; } = new DrunkConfiguration();
        public GiantConfiguration Giant { get; } = new GiantConfiguration();
        public ButtonBarryConfiguration ButtonBarry { get; } = new ButtonBarryConfiguration();

        public static Configuration GenerateConfig()
        {
            var config = new Configuration();

            config.General.DoCommsCamouflage = CustomGameOptions.ColourblindComms;
            config.General.DoCommsCamouflageMeetings = CustomGameOptions.MeetingColourblind;
            config.General.CanImpostorsSeeTeammateRoles = CustomGameOptions.ImpostorSeeRoles;
            config.General.CanDeadSeeAllRoles = CustomGameOptions.DeadSeeRoles;
            config.General.MaxImpostorRoles = CustomGameOptions.MaxImpostorRoles;
            config.General.MaxNeutralRoles = CustomGameOptions.MaxNeutralRoles;
            config.General.DoesRoleAppearUnderName = CustomGameOptions.RoleUnderName;
            config.General.VanillaProbability = CustomGameOptions.VanillaGame / 100.0f;

            config.Mayor.Probability = CustomGameOptions.MayorOn / 100.0f;
            config.Mayor.AreExtraVotesAnonymous = CustomGameOptions.MayorAnonymous;
            config.Mayor.InitialVoteBankSize = CustomGameOptions.MayorVoteBank;

            config.Lovers.Probability = CustomGameOptions.LoversOn / 100.0f;
            config.Lovers.DoBothLoversDie = CustomGameOptions.BothLoversDie;

            config.Sheriff.Probability = CustomGameOptions.SheriffOn / 100.0f;
            config.Sheriff.ShowSheriffPublicly = CustomGameOptions.ShowSheriff;
            config.Sheriff.CanMiskillCrewmate = CustomGameOptions.SheriffKillOther;
            config.Sheriff.CanKillJester = CustomGameOptions.SheriffKillsJester;
            config.Sheriff.CanKillGlitch = CustomGameOptions.SheriffKillsGlitch;
            config.Sheriff.CanKillArsonist = CustomGameOptions.SheriffKillsArsonist;
            config.Sheriff.KillCooldown = CustomGameOptions.SheriffKillCd;
            config.Sheriff.CanReportShots = CustomGameOptions.SheriffBodyReport;

            config.Engineer.Probability = CustomGameOptions.EngineerOn / 100.0f;
            config.Engineer.FixFrequency = (EngineerFixType)CustomGameOptions.EngineerFixPer;

            config.Swapper.Probability = CustomGameOptions.SwapperOn / 100.0f;

            config.Investigator.Probability = CustomGameOptions.InvestigatorOn / 100.0f;
            config.Investigator.FootprintSize = CustomGameOptions.FootprintSize;
            config.Investigator.FootprintInterval = CustomGameOptions.FootprintInterval;
            config.Investigator.FootprintDuration = CustomGameOptions.FootprintDuration;
            config.Investigator.AreFootprintsAnonymous = CustomGameOptions.AnonymousFootPrint;
            config.Investigator.AreFootprintsVisibleNearVents = CustomGameOptions.VentFootprintVisible;

            config.TimeLord.Probability = CustomGameOptions.TimeLordOn / 100.0f;
            config.TimeLord.DoesRewindRevive = CustomGameOptions.RewindRevive;
            config.TimeLord.RewindDuration = CustomGameOptions.RewindDuration;
            config.TimeLord.RewindCooldown = CustomGameOptions.RewindCooldown;
            config.TimeLord.CanUseVitals = CustomGameOptions.TimeLordVitals;

            config.Medic.Probability = CustomGameOptions.MedicOn / 100.0f;
            config.Medic.ShieldVisibility = (MedicShieldVisibility)CustomGameOptions.ShowShielded;
            config.Medic.ReportsConfig.Enabled = CustomGameOptions.ShowReports;
            config.Medic.ReportsConfig.DurationOfName = CustomGameOptions.MedicReportNameDuration;
            config.Medic.ReportsConfig.DurationOfColorType = CustomGameOptions.MedicReportColorDuration;
            config.Medic.MurderAttemptVisibility = (ShieldedMurderAttemptVisibility)CustomGameOptions.NotificationShield;
            config.Medic.DoesShieldBreakOnAttempt = CustomGameOptions.ShieldBreaks;

            config.Seer.Probability = CustomGameOptions.SeerOn / 100.0f;
            config.Seer.Cooldown = CustomGameOptions.SeerCd;
            config.Seer.SeerInfo = (SeerInfoType)CustomGameOptions.SeerInfo;
            config.Seer.RevealVisibility = (RevealStatusVisibility)CustomGameOptions.SeeReveal;
            config.Seer.DoNeutralsShowAsImpostors = CustomGameOptions.NeutralRed;

            config.Spy.Probability = CustomGameOptions.SpyOn / 100.0f;

            config.Snitch.Probability = CustomGameOptions.SnitchOn / 100.0f;
            config.Snitch.DoesKnowRole = CustomGameOptions.SnitchOnLaunch;

            config.Altruist.Probability = CustomGameOptions.AltruistOn / 100.0f;
            config.Altruist.ReviveDuration = CustomGameOptions.ReviveDuration;
            config.Altruist.DoesBodyDisappearWithReviveStart = CustomGameOptions.AltruistTargetBody;

            config.Jester.Probability = CustomGameOptions.JesterOn / 100.0f;

            config.Shifter.Probability = CustomGameOptions.ShifterOn / 100.0f;
            config.Shifter.Cooldown = CustomGameOptions.ShifterCd;
            config.Shifter.Recipient = (ShifterRecipient)CustomGameOptions.WhoShifts;

            config.Glitch.Probability = CustomGameOptions.GlitchOn / 100.0f;
            config.Glitch.MimicCooldown = CustomGameOptions.MimicCooldown;
            config.Glitch.MimicDuration = CustomGameOptions.MimicDuration;
            config.Glitch.HackCooldown = CustomGameOptions.HackCooldown;
            config.Glitch.HackDuration = CustomGameOptions.HackDuration;
            config.Glitch.HackDistance = (HackDistance)CustomGameOptions.GlitchHackDistance;
            config.Glitch.KillCooldown = CustomGameOptions.GlitchKillCooldown;
            config.Glitch.InitialKillCooldown = CustomGameOptions.InitialGlitchKillCooldown;

            config.Executioner.Probability = CustomGameOptions.ExecutionerOn / 100.0f;
            config.Executioner.RoleWhenTargetKilled = (ExecutionerFallbackRole)CustomGameOptions.OnTargetDead;

            config.Arsonist.Probability = CustomGameOptions.ArsonistOn / 100.0f;
            config.Arsonist.DouseCooldown = CustomGameOptions.DouseCd;
            config.Arsonist.CanContinueGame = CustomGameOptions.ArsonistGameEnd;

            config.Phantom.Probability = CustomGameOptions.PhantomOn / 100.0f;

            config.Janitor.Probability = CustomGameOptions.JanitorOn / 100.0f;

            config.Morphling.Probability = CustomGameOptions.MorphlingOn / 100.0f;
            config.Morphling.MorphCooldown = CustomGameOptions.MorphlingCd;
            config.Morphling.MorphDuration = CustomGameOptions.MorphlingDuration;

            config.Camouflager.Probability = CustomGameOptions.CamouflagerOn / 100.0f;
            config.Camouflager.CamouflageCooldown = CustomGameOptions.CamouflagerCd;
            config.Camouflager.CamouflageDuration = CustomGameOptions.CamouflagerDuration;

            config.Miner.Probability = CustomGameOptions.MinerOn / 100.0f;
            config.Miner.MineCooldown = CustomGameOptions.MineCd;

            config.Swooper.Probability = CustomGameOptions.SwooperOn / 100.0f;
            config.Swooper.SwoopCooldown = CustomGameOptions.SwoopCd;
            config.Swooper.SwoopDuration = CustomGameOptions.SwoopDuration;

            config.Assassin.Probability = CustomGameOptions.AssassinOn / 100.0f;
            config.Assassin.MaxKills = CustomGameOptions.AssassinKills;
            config.Assassin.CanGuessCrewmate = CustomGameOptions.AssassinCrewmateGuess;
            config.Assassin.CanGuessNeutrals = CustomGameOptions.AssassinGuessNeutrals;
            config.Assassin.CanKillMultipleTimes = CustomGameOptions.AssassinMultiKill;

            config.Undertaker.Probability = CustomGameOptions.UndertakerOn / 100.0f;
            config.Undertaker.DragCooldown = CustomGameOptions.DragCd;

            config.Underdog.Probability = CustomGameOptions.UndertakerOn / 100.0f;

            config.Torch.Probability = CustomGameOptions.TorchOn / 100.0f;
            config.Diseased.Probability = CustomGameOptions.DiseasedOn / 100.0f;
            config.Flash.Probability = CustomGameOptions.FlashOn / 100.0f;
            config.Tiebreaker.Probability = CustomGameOptions.TiebreakerOn / 100.0f;
            config.Drunk.Probability = CustomGameOptions.DrunkOn / 100.0f;
            config.Giant.Probability = CustomGameOptions.BigBoiOn / 100.0f;
            config.ButtonBarry.Probability = CustomGameOptions.ButtonBarryOn / 100.0f;

            Instance = config;
            return config;
        }
    }

    public class GeneralConfiguration
    {
        public bool DoCommsCamouflage { get; set; } = false;
        public bool DoCommsCamouflageMeetings { get; set; } = false;
        public bool CanImpostorsSeeTeammateRoles { get; set; } = false;
        public bool CanDeadSeeAllRoles { get; set; } = false;
        public int MaxImpostorRoles { get; set; } = 1;
        public int MaxNeutralRoles { get; set; } = 1;
        public bool DoesRoleAppearUnderName { get; set; } = true;
        public float VanillaProbability { get; set; } = 0.0f;
    }

    public interface IRoleConfiguration
    {
        float Probability { get; set; }
    }

    public interface IModifierConfiguration
    {
        float Probability { get; set; }
    }

    public class MayorConfiguration : IRoleConfiguration
    {
        public float Probability { get; set; } = 0.0f;
        public int InitialVoteBankSize { get; set; } = 1;
        public bool AreExtraVotesAnonymous { get; set; } = false;
    }

    public class LoverConfiguration : IRoleConfiguration
    {
        public float Probability { get; set; } = 0.0f;
        public bool DoBothLoversDie { get; set; } = true;
    }

    public class SheriffConfiguration : IRoleConfiguration
    {
        public float Probability { get; set; } = 0.0f;
        public bool ShowSheriffPublicly { get; set; } = false;

        public bool CanMiskillCrewmate { get; set; } = false;
        public bool CanKillJester { get; set; } = false;
        public bool CanKillGlitch { get; set; } = false;
        public bool CanKillArsonist { get; set; } = false;

        public float KillCooldown { get; set; } = 25.0f;
        public bool CanReportShots { get; set; } = true;
    }

    public enum EngineerFixType
    {
        PerRound = 0,
        PerGame = 1
    }

    public class EngineerConfiguration : IRoleConfiguration
    {
        public float Probability { get; set; } = 0.0f;
        public EngineerFixType FixFrequency { get; set; } = EngineerFixType.PerRound;
    }

    public class SwapperConfiguration : IRoleConfiguration
    {
        public float Probability { get; set; } = 0.0f;
    }

    public class InvestigatorConfiguration : IRoleConfiguration
    {
        public float Probability { get; set; } = 0.0f;
        public float FootprintSize { get; set; } = 4.0f;
        public float FootprintInterval { get; set; } = 1.0f;
        public float FootprintDuration { get; set; } = 10.0f;
        public bool AreFootprintsAnonymous { get; set; } = false;
        public bool AreFootprintsVisibleNearVents { get; set; } = false;
    }

    public class TimeLordConfiguration : IRoleConfiguration
    {
        public float Probability { get; set; } = 0.0f;
        public bool DoesRewindRevive { get; set; } = false;
        public float RewindDuration { get; set; } = 3.0f;
        public float RewindCooldown { get; set; } = 25.0f;
        public bool CanUseVitals { get; set; } = false;
    }

    public enum MedicShieldVisibility
    {
        Self = 0,
        Medic = 1,
        SelfWithMedic = 2,
        Everyone = 3
    }

    public enum ShieldedMurderAttemptVisibility
    {
        Medic = 0,
        ShieldedPlayer = 1,
        Everyone = 2,
        Nobody = 3
    }

    public class MedicReportConfiguration
    {
        public bool Enabled { get; set; } = true;
        public float DurationOfName { get; set; } = 0.0f;
        public float DurationOfColorType { get; set; } = 15.0f;
    }

    public class MedicConfiguration : IRoleConfiguration
    {
        public float Probability { get; set; } = 0.0f;
        public MedicShieldVisibility ShieldVisibility { get; set; } = MedicShieldVisibility.Self;
        public MedicReportConfiguration ReportsConfig { get; set; } = new MedicReportConfiguration();
        public ShieldedMurderAttemptVisibility MurderAttemptVisibility { get; set; } = ShieldedMurderAttemptVisibility.Medic;
        public bool DoesShieldBreakOnAttempt { get; set; } = false;
    }

    public enum SeerInfoType
    {
        Role = 0,
        Team = 1
    }

    public enum RevealStatusVisibility
    {
        Crewmates = 0,
        ImpostorsAndNeutrals = 1,
        Everybody = 2,
        Nobody = 3
    }

    public class SeerConfiguration : IRoleConfiguration
    {
        public float Probability { get; set; } = 0.0f;
        public float Cooldown { get; set; } = 25.0f;
        public SeerInfoType SeerInfo { get; set; } = SeerInfoType.Role;
        public RevealStatusVisibility RevealVisibility { get; set; } = RevealStatusVisibility.Crewmates;
        public bool DoNeutralsShowAsImpostors { get; set; } = false;
    }

    public class SpyConfiguration : IRoleConfiguration
    {
        public float Probability { get; set; } = 0.0f;
    }

    public class SnitchConfiguration : IRoleConfiguration
    {
        public float Probability { get; set; } = 0.0f;
        public bool DoesKnowRole { get; set; } = false;
    }

    public class AltruistConfiguration : IRoleConfiguration
    {
        public float Probability { get; set; } = 0.0f;
        public float ReviveDuration { get; set; } = 10.0f;
        public bool DoesBodyDisappearWithReviveStart { get; set; } = false;
    }

    public class JesterConfiguration : IRoleConfiguration
    {
        public float Probability { get; set; } = 0.0f;
    }

    public enum ShifterRecipient
    {
        NonImpostors = 0,
        RegularCrewmates = 1,
        Nobody = 2
    }

    public class ShifterConfiguration : IRoleConfiguration
    {
        public float Probability { get; set; } = 0.0f;
        public float Cooldown { get; set; } = 25.0f;
        public ShifterRecipient Recipient { get; set; } = ShifterRecipient.NonImpostors;
    }

    // These numbers are positions in GameOptionsData.KillDistances
    public enum HackDistance
    {
        Short = 0,
        Normal = 1,
        Long = 2
    }

    public class GlitchConfiguration : IRoleConfiguration
    {
        public float Probability { get; set; } = 0.0f;
        public float MimicCooldown { get; set; } = 30.0f;
        public float MimicDuration { get; set; } = 10.0f;
        public float HackCooldown { get; set; } = 30.0f;
        public float HackDuration { get; set; } = 10.0f;
        public HackDistance HackDistance { get; set; } = HackDistance.Short;
        public float KillCooldown { get; set; } = 30.0f;
        public float InitialKillCooldown { get; set; } = 10.0f;
    }

    public enum ExecutionerFallbackRole
    {
        Crewmate = 0,
        Jester = 1
    }

    public class ExecutionerConfiguration : IRoleConfiguration
    {
        public float Probability { get; set; } = 0.0f;
        public ExecutionerFallbackRole RoleWhenTargetKilled { get; set; } = ExecutionerFallbackRole.Crewmate;
    }

    public class ArsonistConfiguration : IRoleConfiguration
    {
        public float Probability { get; set; } = 0.0f;
        public float DouseCooldown { get; set; } = 25.0f;
        public bool CanContinueGame { get; set; } = false;
    }

    public class PhantomConfiguration : IRoleConfiguration
    {
        public float Probability { get; set; } = 0.0f;
    }

    public class JanitorConfiguration : IRoleConfiguration
    {
        public float Probability { get; set; } = 0.0f;
    }

    public class MorphlingConfiguration : IRoleConfiguration
    {
        public float Probability { get; set; } = 0.0f;
        public float MorphCooldown { get; set; } = 25.0f;
        public float MorphDuration { get; set; } = 10.0f;
    }

    public class CamouflagerConfiguration : IRoleConfiguration
    {
        public float Probability { get; set; } = 0.0f;
        public float CamouflageCooldown { get; set; } = 25.0f;
        public float CamouflageDuration { get; set; } = 10.0f;
    }

    public class MinerConfiguration : IRoleConfiguration
    {
        public float Probability { get; set; } = 0.0f;
        public float MineCooldown { get; set; } = 25.0f;
    }

    public class SwooperConfiguration : IRoleConfiguration
    {
        public float Probability { get; set; } = 0.0f;
        public float SwoopCooldown { get; set; } = 25.0f;
        public float SwoopDuration { get; set; } = 10.0f;
    }

    public class AssassinConfiguration : IRoleConfiguration
    {
        public float Probability { get; set; } = 0.0f;
        public int MaxKills { get; set; } = 1;
        public bool CanGuessCrewmate { get; set; } = false;
        public bool CanGuessNeutrals { get; set; } = false;
        public bool CanKillMultipleTimes { get; set; } = true;
    }

    public class UndertakerConfiguration : IRoleConfiguration
    {
        public float Probability { get; set; } = 0.0f;
        public float DragCooldown { get; set; } = 25.0f;
    }

    public class UnderdogConfiguration : IRoleConfiguration
    {
        public float Probability { get; set; } = 0.0f;
    }

    public class TorchConfiguration : IModifierConfiguration
    {
        public float Probability { get; set; }
    }

    public class DiseasedConfiguration : IModifierConfiguration
    {
        public float Probability { get; set; }
    }

    public class FlashConfiguration : IModifierConfiguration
    {
        public float Probability { get; set; }
    }

    public class TiebreakerConfiguration : IModifierConfiguration
    {
        public float Probability { get; set; }
    }

    public class DrunkConfiguration : IModifierConfiguration
    {
        public float Probability { get; set; }
    }

    public class GiantConfiguration : IModifierConfiguration
    {
        public float Probability { get; set; }
    }

    public class ButtonBarryConfiguration : IModifierConfiguration
    {
        public float Probability { get; set; }
    }
}