using BBTV6B_HFT_2022231.Models.Entities;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace BBTV6B_SztGUI.WpfClient
{
    public class MainWindowViewModel : ObservableRecipient
    {
        Random r;
        public RestCollection<Exchange> Exchanges { get; set; }

        public MainWindowViewModel()
        {
            if (!IsInDesignMode)
            {
                Exchanges = new RestCollection<Exchange>("http://localhost:33531/", "exchange", "hub");
                CreateExchangeCommand = new RelayCommand(() =>
                {
                    Exchanges.Add(new Exchange()
                    {
                        Id = IdGenerator(),
                        NameShort = selectedExchange.NameShort,
                        Name = selectedExchange.Name,
                        Region = selectedExchange.Region
                    });
                });

                UpdateExchangeCommand = new RelayCommand(() =>
                {
                    try
                    {
                        Exchanges.Update(selectedExchange);
                    }
                    catch (ArgumentException ex)
                    {
                        errorMessage = ex.Message;
                    }
                });

                DeleteExchangeCommand = new RelayCommand(() => {
                    Exchanges.Delete(SelectedExchange.Id);

                }, () =>
                {
                    return SelectedExchange != null;
                });
                SelectedExchange = new Exchange();
            }
        }

        private Exchange selectedExchange;

        public Exchange SelectedExchange
        {
            get { return selectedExchange; }
            set 
            {
                if (value != null)
                {
                    selectedExchange = new Exchange()
                    {
                        Id = value.Id,
                        NameShort = value.NameShort,
                        Name = value.Name,
                        Region = value.Region
                    };
                    OnPropertyChanged();
                    (CreateExchangeCommand as RelayCommand).NotifyCanExecuteChanged();
                    (DeleteExchangeCommand as RelayCommand).NotifyCanExecuteChanged();
                    (UpdateExchangeCommand as RelayCommand).NotifyCanExecuteChanged();

                }
            }
        }

        public ICommand CreateExchangeCommand { get; set; }
        public ICommand DeleteExchangeCommand { get; set; }
        public ICommand UpdateExchangeCommand { get; set; }

        public static bool IsInDesignMode
        {
            get
            {
                var prop = DesignerProperties.IsInDesignModeProperty;
                return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
            }
        }

        private string errorMessage;

        public string ErrorMessage
        {
            get { return errorMessage; }
            set { SetProperty(ref errorMessage, value); }
        }

        public int IdGenerator()
        {
            int res = -1;
            r = new Random();
            do
            {
                res = r.Next(100)+1;
            } while (Exchanges.Contains(new Exchange() { Id = res}));
            //Exchanges.Select(t => t.Id == res).Count() > 0
            return res;
        }

    }
}
