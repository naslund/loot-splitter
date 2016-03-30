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
            if (_viewModel.Participants.Count < 1)
                return;

            Navigation.PushModalAsync(new CalculationPage(_viewModel.Participants));
        }
    }

    public class MainPageViewModel : INotifyPropertyChanged
    {
        private string _name;
        private string _waste;
        private bool _addEnabled;
        private bool _continueEnabled;
        private bool _clearVisible;
        private ObservableCollection<Participant> _participants;
        private Participant _selectedParticipant;
        private string _continueText;

        public MainPageViewModel()
        {
            Participants = new ObservableCollection<Participant>();
            ToggleContinue();
        }

        public void AddParticipant()
        {
            Participants.Add(new Participant { Name = Name, Waste = decimal.Parse(Waste) });
            ToggleClear();
            ToggleContinue();
            Name = string.Empty;
            Waste = string.Empty;
        }

        public void ClearParticipants()
        {
            Participants.Clear();
            ToggleClear();
            ToggleContinue();
        }

        public void ToggleAdd()
        {
            decimal waste;
            AddEnabled = string.IsNullOrEmpty(Name) == false && decimal.TryParse(Waste, out waste) && waste < 0 == false;
        }

        public void ToggleClear()
        {
            ClearVisible = Participants.Count > 0;
        }

        public void ToggleContinue()
        {
            ContinueEnabled = Participants.Count > 1;
            ContinueText = ContinueEnabled ? "Continue" : "Add at least 2 participants";
        }

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged();
                ToggleAdd();
            }
        }

        public string Waste
        {
            get { return _waste; }
            set
            {
                _waste = value;
                OnPropertyChanged();
                ToggleAdd();
            }
        }

        public bool AddEnabled
        {
            get { return _addEnabled; }
            set
            {
                _addEnabled = value;
                OnPropertyChanged();
            }
        }

        public bool ContinueEnabled
        {
            get { return _continueEnabled; }
            set
            {
                _continueEnabled = value;
                OnPropertyChanged();
            }
        }

        public bool ClearVisible
        {
            get { return _clearVisible; }
            set
            {
                _clearVisible = value;
                OnPropertyChanged();
            }
        }

        public string ContinueText
        {
            get { return _continueText; }
            set
            {
                _continueText = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Participant> Participants
        {
            get { return _participants; }
            set
            {
                _participants = value;
                OnPropertyChanged();
            }
        }

        public Participant SelectedParticipant
        {
            get { return _selectedParticipant; }
            set
            {
                _selectedParticipant = value;
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
