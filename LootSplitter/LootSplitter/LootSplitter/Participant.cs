namespace LootSplitter
{
    public class Participant
    {
        public string Name { get; set; }

        public long Waste { get; set; }
    }

    public class ParticipantOutput
    {
        public string Name { get; set; }

        public long Share { get; set; }
    }
}
