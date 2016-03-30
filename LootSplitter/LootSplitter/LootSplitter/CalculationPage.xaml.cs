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
    }

    public class CalculationPageViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Participant> _participants;

        public CalculationPageViewModel(ObservableCollection<Participant> participants)
        {
            _participants = participants;
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

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
