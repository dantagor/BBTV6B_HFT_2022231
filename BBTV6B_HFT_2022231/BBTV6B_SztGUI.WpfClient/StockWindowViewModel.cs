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
    public class StockWindowViewModel : ObservableRecipient
    {
        Random r;
        public RestCollection<Stock> Stocks { get; set; }

        private Stock selectedStock;
        private RestService service;

        public Stock SelectedStock
        {
            get { return selectedStock; }
            set
            {
                if (value != null)
                {
                    selectedStock = new Stock()
                    {
                        Id = value.Id,
                        Company = value.Company,
                        Ticker = value.Ticker,
                        Price = value.Price,
                        Dividend = value.Dividend
                    };
                    OnPropertyChanged();
                    (DeleteStockCommand as RelayCommand).NotifyCanExecuteChanged();
                    (CreateStockCommand as RelayCommand).NotifyCanExecuteChanged();
                    (UpdateStockCommand as RelayCommand).NotifyCanExecuteChanged();
                }
            }
        }


        public StockWindowViewModel()
        {
            if (!IsInDesignMode)
            {
                Stocks = new RestCollection<Stock>("http://localhost:33531/", "stock", "hub");


                CreateStockCommand = new RelayCommand(() =>
                {
                    Stocks.Add(new Stock()
                    {
                        Id = IdGenerator(),
                        Company = SelectedStock.Company,
                        Ticker = SelectedStock.Ticker,
                        Price = SelectedStock.Price,
                        Dividend = SelectedStock.Dividend
                    });
                });

                UpdateStockCommand = new RelayCommand(() =>
                {
                    try
                    {
                        Stocks.Update(SelectedStock);
                    }
                    catch (ArgumentException ex)
                    {
                        ErrorMessage = ex.Message;
                    }

                });

                DeleteStockCommand = new RelayCommand(() => {
                    Stocks.Delete(SelectedStock.Id);

                }, () =>
                {
                    return SelectedStock != null;
                });
                SelectedStock = new Stock();
                service = new RestService("http://localhost:33531/");
                HighestDividendStockFromRegion = new Stock();
            }
        }

        public ICommand CreateStockCommand { get; set; }
        public ICommand DeleteStockCommand { get; set; }
        public ICommand UpdateStockCommand { get; set; }

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
                res = r.Next(100) + 1;
            } while (Stocks.Contains(new Stock() { Id = res }));
            //Exchanges.Select(t => t.Id == res).Count() > 0
            return res;
        }

        private Stock highestDividendStockFromRegion;

        public Stock HighestDividendStockFromRegion
        {
            get { return highestDividendStockFromRegion; }
            set
            {
                if (value != null)
                {
                    highestDividendStockFromRegion = service.GetSingle<Stock>("Stat/HighestDividendStockFromRegion?region=USA");
                }
                OnPropertyChanged();
                (DeleteStockCommand as RelayCommand).NotifyCanExecuteChanged();
                (CreateStockCommand as RelayCommand).NotifyCanExecuteChanged();
                (UpdateStockCommand as RelayCommand).NotifyCanExecuteChanged();
            }
        }

        
    }
}
