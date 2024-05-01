﻿using Calculator.DataAccessLayer;
using Calculator.MVVM.Models;
using Microsoft.Extensions.Logging;
using MVVM;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Calculator.MVVM.ViewModels
{
    public class MainViewModel:BaseViewModel
    {
        private IRepository repository;
        public ObservableCollection<Operation> Operations { get; } = new ObservableCollection<Operation>();
        private Operation _selectedOperation;
        public Operation SelectedOperation
        {
            get => _selectedOperation;
            set => UpdateObservable(ref _selectedOperation, value, () =>
            {
                if(value != null)
                {
                    ServiceLocator.InputResultViewModel.InputText = value.OpValue;
                    ServiceLocator.InputResultViewModel.OutputText = value.OpResult;
                    IsBottomDrawerOpen = !IsBottomDrawerOpen;
                }
            });
        }
        private bool _isBottomDrawerOpen;
        public bool IsBottomDrawerOpen
        {
            get => _isBottomDrawerOpen;
            set => UpdateObservable(ref _isBottomDrawerOpen, value);
        }
        private readonly ILogger<MainViewModel> logger;
        public ICommand DeleteAllCommand { get; private set; }
        public MainViewModel(IRepository repository, ILogger<MainViewModel> _log)
        {
            logger = _log;
            logger.LogInformation("MainViewModel started......");
            this.repository = repository;
            load();
            DeleteAllCommand = new DelegateCommand(On_DeleteAll);
        }
        private async void On_DeleteAll(object parameter)
        {
            await repository.DeleteAll();
            await Load();
        }
        public async Task Load()
        {
            Operations.Clear();
            var data = await repository.GetAll();
            foreach (Operation operation in data)
            {
                Operations.Add(operation);
            }
        }
        private async void load()
        {
            await Load();
        }
    }
}
