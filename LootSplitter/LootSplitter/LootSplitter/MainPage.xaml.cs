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
            if (string.IsNullOrEmpty(nameInput.Text))
                return;

            decimal waste;
            if (decimal.TryParse(wasteInput.Text, out waste) == false)
                return;

            if (waste <= 0)
                return;

            _viewModel.Participants.Add(new Participant { Name = nameInput.Text, Waste = decimal.Parse(wasteInput.Text) });
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
        private ObservableCollection<Participant> participants;
        private Participant selectedParticipant;

        public MainPageViewModel()
        {
            Participants = new ObservableCollection<Participant>();
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
