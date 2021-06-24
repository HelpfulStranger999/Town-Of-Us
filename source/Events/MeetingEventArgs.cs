using System;

namespace TownOfUs.Events
{
    public delegate void MeetingStartEventHandler(object sender, MeetingStartEventArgs args);

    public delegate void MeetingFrameUpdateEventHandler(object sender, MeetingFrameUpdateEventArgs args);

    public delegate void MeetingEndEventHandler(object sender, MeetingEndEventArgs args);

    public delegate void VotingStartEventHandler(object sender, VotingStartEventArgs args);

    public delegate void VoteSelectEventHandler(object sender, VoteSelectEventArgs args);

    public delegate void VoteClearEventHandler(object sender, VoteClearEventArgs args);

    public delegate void VoteConfirmEventHandler(object sender, VoteConfirmEventArgs args);

    public delegate void VotingCompleteEventHandler(object sender, VotingCompleteEventArgs args);

    public class MeetingStartEventArgs : EventArgs
    {
        public MeetingHud Instance { get; }

        public MeetingStartEventArgs(MeetingHud instance)
        {
            Instance = instance;
        }
    }

    public class MeetingFrameUpdateEventArgs : EventArgs
    {
    }

    public class MeetingEndEventArgs : EventArgs
    {
    }

    public class VotingStartEventArgs : EventArgs
    {
        public MeetingHud Instance { get; }

        public VotingStartEventArgs(MeetingHud instance)
        {
            Instance = instance;
        }
    }

    public class VoteSelectEventArgs : EventArgs
    {
        public MeetingHud Instance { get; }
        public int SuspectStateId { get; }

        public VoteSelectEventArgs(MeetingHud instance, int suspectId)
        {
            Instance = instance;
            SuspectStateId = suspectId;
        }
    }

    public class VoteClearEventArgs : EventArgs
    {
    }

    public class VoteConfirmEventArgs : EventArgs
    {
    }

    public class VotingCompleteEventArgs : EventArgs
    {
    }
}