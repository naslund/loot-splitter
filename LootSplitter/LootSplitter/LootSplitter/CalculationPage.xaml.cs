using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
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
            _viewModel.CalculateProfit(decimal.Parse(LootValue.Text));
        }
    }

    public class CalculationPageViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Participant> _participants;
        private ObservableCollection<ParticipantOutput> _participantsOutput;

        public CalculationPageViewModel(ObservableCollection<Participant> participants)
        {
            _participants = participants;
            ParticipantsOutput = new ObservableCollection<ParticipantOutput>();
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

        public void CalculateProfit(decimal lootValue)
        {
            decimal totalProfit = lootValue - _participants.Sum(x => x.Waste);
            ParticipantsOutput.Clear();
            foreach (Participant participant in _participants)
            {
                ParticipantsOutput.Add(new ParticipantOutput { Name = participant.Name, Profit = participant.Waste + totalProfit / _participants.Count });
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
