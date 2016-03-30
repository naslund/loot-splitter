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
            _viewModel.Participants.Clear();
        }

        private void Calculate_OnClicked(object sender, EventArgs e)
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
        private ObservableCollection<Participant> participants;
        private Participant selectedParticipant;

        public MainPageViewModel()
        {
            Participants = new ObservableCollection<Participant>();
        }

        public void AddParticipant()
        {
            Participants.Add(new Participant { Name = Name, Waste = decimal.Parse(Waste) });
            Name = string.Empty;
            Waste = string.Empty;
        }

        public void ToggleAdd()
        {
            decimal waste;
            AddEnabled = string.IsNullOrEmpty(Name) == false && decimal.TryParse(Waste, out waste) && waste < 0 == false;
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

        public ObservableCollection<Participant> Participants
        {
            get { return participants; }
            set
            {
                participants = value;
                OnPropertyChanged();
            }
        }

        public Participant SelectedParticipant
        {
            get { return selectedParticipant; }
            set
            {
                selectedParticipant = value;
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
