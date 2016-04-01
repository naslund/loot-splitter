using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using LootSplitter.Annotations;
using Xamarin.Forms;

namespace LootSplitter
{
    public partial class CalculationPage : ContentPage
    {
        private CalculationPageViewModel _viewModel;

        public CalculationPage(ObservableCollection<Participant> participants)
        {
            InitializeComponent();
            _viewModel = new CalculationPageViewModel(participants);
            BindingContext = _viewModel;
        }

        private void Dismiss_OnClicked(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }

        private void Calculate_OnClicked(object sender, EventArgs e)
        {
            _viewModel.CalculateProfit();
        }
    }

    public class CalculationPageViewModel : INotifyPropertyChanged
    {
        private string _lootValue;
        private bool _equalShare;
        private ObservableCollection<Participant> _participants;
        private ObservableCollection<ParticipantOutput> _participantsOutput;

        public CalculationPageViewModel(ObservableCollection<Participant> participants)
        {
            _participants = participants;
            ParticipantsOutput = new ObservableCollection<ParticipantOutput>();
        }

        public ObservableCollection<Participant> Participants
        {
            get
            {
                return _participants;
            }
            set
            {
                _participants = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<ParticipantOutput> ParticipantsOutput
        {
            get
            {
                return _participantsOutput;
            }
            set
            {
                _participantsOutput = value;
                OnPropertyChanged();
            }
        }

        public string LootValue
        {
            get { return _lootValue; }
            set
            {
                if (value.Length < 16)
                    if (value.ToCharArray().All(char.IsDigit))
                        _lootValue = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(CalculateVisible));
            }
        }

        public bool CalculateVisible => LootValue.IsParsableToLongAndGreaterThanOrEqualToZero() && _participants.Count > 1;

        public bool EqualShare
        {
            get { return _equalShare; }
            set
            {
                _equalShare = value;
                OnPropertyChanged();
            }
        }

        public void CalculateProfit()
        {
            long lootValue = long.Parse(LootValue);
            // Todo: Find different solution? WinPhone throws exception on .Clear()
            while (ParticipantsOutput.Count > 0) 
                ParticipantsOutput.RemoveAt(0);

            if (EqualShare)
            {
                foreach (Participant participant in Participants)
                    ParticipantsOutput.Add(new ParticipantOutput { Name = participant.Name, Share = lootValue / Participants.Count });
            }
            else
            {
                long totalProfit = lootValue - _participants.Sum(x => x.Waste);
                foreach (Participant participant in Participants)
                    ParticipantsOutput.Add(new ParticipantOutput { Name = participant.Name, Share = participant.Waste + totalProfit / Participants.Count });
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
