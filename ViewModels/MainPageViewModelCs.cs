using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Counter.Models;
using Counter.Services;

namespace Counter.ViewModels
{
    public class MainPageViewModel : BindableObject
    {
        public ObservableCollection<CounterData> Counters { get; set; }

        public ICommand AddCounterCommand { get; }
        public ICommand IncreaseCommand { get; }
        public ICommand DecreaseCommand { get; }

        public MainPageViewModel()
        {

            var loaded = CounterStorage.Load();
            Counters = new ObservableCollection<CounterData>(loaded);

            
            AddCounterCommand = new Command(AddCounter);
            IncreaseCommand = new Command<CounterData>(Increase);
            DecreaseCommand = new Command<CounterData>(Decrease);
        }

        private void AddCounter()
        {
            int nextNumber = Counters.Count + 1;
            Counters.Add(new CounterData { Name = $"Counter {nextNumber}", Value = 0 });

            
            CounterStorage.Save(Counters.ToList());
        }

        private void Increase(CounterData counter)
        {
            counter.Value++;
            OnPropertyChanged(nameof(Counters)); 
            CounterStorage.Save(Counters.ToList());
        }

        private void Decrease(CounterData counter)
        {
            counter.Value--;
            OnPropertyChanged(nameof(Counters)); 
            CounterStorage.Save(Counters.ToList());
        }
    }
}
