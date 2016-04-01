using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using LootSplitter.Annotations;
using Xamarin.Forms;

namespace LootSplitter
{
    public partial class MainPage : ContentPage
    {
        private MainPageViewModel _viewModel;

        public MainPage()
        {
            InitializeComponent();
            _viewModel = new MainPageViewModel();
            BindingContext = _viewModel;
        }

        private void AddParticipant_OnClicked(object sender, EventArgs e)
        {
            _viewModel.AddParticipant();
        }

        private void Clear_OnClicked(object sender, EventArgs e)
        {
            _viewModel.ClearParticipants();
        }

        private void Continue_OnClicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new CalculationPage(_viewModel.Participants));
        }
    }

    public class MainPageViewModel : INotifyPropertyChanged
    {
        private string _name;
        private string _waste;
        private ObservableCollection<Participant> _participants;

        public MainPageViewModel()
        {
            Participants = new ObservableCollection<Participant>();
        }

        public void AddParticipant()
        {
            Participants.Add(new Participant { Name = Name, Waste = long.Parse(Waste) });
            OnPropertyChanged(nameof(ClearVisible));
            OnPropertyChanged(nameof(ContinueVisible));
            OnPropertyChanged(nameof(ContinueText));
            OnPropertyChanged(nameof(TotalWaste));
            Name = string.Empty;
            Waste = string.Empty;
        }

        public void ClearParticipants()
        {
            // Todo: Find different solution? WinPhone throws exception on .Clear()
            while (Participants.Count > 0) 
                Participants.RemoveAt(0);
            OnPropertyChanged(nameof(ClearVisible));
            OnPropertyChanged(nameof(ContinueVisible));
            OnPropertyChanged(nameof(ContinueText));
            OnPropertyChanged(nameof(TotalWaste));
        }

        public string Name
        {
            get { return _name; }
            set
            {
                if (value.Length < 16)
                    _name = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(AddEnabled));
            }
        }

        public string Waste
        {
            get { return _waste; }
            set
            {
                if (value.Length < 16)
                    if (value.ToCharArray().All(char.IsDigit))
                        _waste = value;
                
                OnPropertyChanged();
                OnPropertyChanged(nameof(AddEnabled));
            }
        }

        public long TotalWaste => _participants.Sum(x => x.Waste);

        public bool AddEnabled => string.IsNullOrEmpty(Name) == false && Waste.IsParsableToLongAndGreaterThanOrEqualToZero();

        public bool ContinueVisible => Participants.Count > 1;

        public string ContinueText => ContinueVisible ? "Continue" : "Add at least 2 participants";

        public bool ClearVisible => Participants.Count > 0;

        public ObservableCollection<Participant> Participants
        {
            get { return _participants; }
            set
            {
                _participants = value;
                OnPropertyChanged();
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
