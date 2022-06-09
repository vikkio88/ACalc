using System.Reactive;
using ReactiveUI;
using ACalc.Models;

namespace ACalc.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private double _firstValue;
        private double _secondValue;
        private Operation _operation = Operation.Add;

        public ReactiveCommand<int, Unit> AddNumberCommand { get; }
        public ReactiveCommand<Operation, Unit> ExecuteOperationCommand { get; }
        public ReactiveCommand<Unit, Unit> ResetCommand { get; }

        public double ShownValue
        {
            get => _secondValue;
            set => this.RaiseAndSetIfChanged(ref _secondValue, value);
        }

        public MainWindowViewModel()
        {
            AddNumberCommand = ReactiveCommand.Create<int>(AddNumber);
            ExecuteOperationCommand = ReactiveCommand.Create<Operation>(ExecuteOperation);
            ResetCommand = ReactiveCommand.Create(Reset);
        }

        void Reset()
        {
            ShownValue = 0;
        }

        void AddNumber(int value)
        {
            // this because will move the current value up 1 10th
            // so if you put 1, it will become 10
            ShownValue = ShownValue * 10 + value;
        }

        private void ExecuteOperation(Operation operation)
        {
            switch (_operation)
            {
                case Operation.Add:
                    {
                        _firstValue += _secondValue;
                        ShownValue = 0;
                        break;
                    }
                case Operation.Subtract:
                    {
                        _firstValue -= _secondValue;
                        ShownValue = 0;
                        break;
                    }
            }

            if (operation == Operation.Result)
            {
                ShownValue = _firstValue;
                _operation = Operation.Add;
                _firstValue = 0;
            }
            else
            {
                _operation = operation;
            }
        }
    }
}
