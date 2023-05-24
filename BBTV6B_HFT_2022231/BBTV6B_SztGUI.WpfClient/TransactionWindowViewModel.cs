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
    class TransactionWindowViewModel : ObservableRecipient
    {
        Random r;
        public RestCollection<Transaction> Transactions { get; set; }

        private Transaction selectedTransaction;
        private RestService service;

        public Transaction SelectedTransaction
        {
            get { return selectedTransaction; }
            set
            {
                if (value != null)
                {
                    selectedTransaction = new Transaction()
                    {
                        Id = value.Id,
                        Amount = value.Amount,
                        Date = value.Date
                    };
                    OnPropertyChanged();
                    (DeleteTransactionCommand as RelayCommand).NotifyCanExecuteChanged();
                    (CreateTransactionCommand as RelayCommand).NotifyCanExecuteChanged();
                    (UpdateTransactionCommand as RelayCommand).NotifyCanExecuteChanged();
                    //BestSellerStockByExchange = new Stock();
                }
            }
        }


        public TransactionWindowViewModel()
        {
            if (!IsInDesignMode)
            {
                Transactions = new RestCollection<Transaction>("http://localhost:33531/", "transaction", "hub");

                CreateTransactionCommand = new RelayCommand(() =>
                {
                    Transactions.Add(new Transaction()
                    {
                        Id = IdGenerator(),
                        Amount = SelectedTransaction.Amount,
                        Date = SelectedTransaction.Date
                    });
                });

                UpdateTransactionCommand = new RelayCommand(() =>
                {
                    try
                    {
                        Transactions.Update(SelectedTransaction);
                    }
                    catch (ArgumentException ex)
                    {
                        ErrorMessage = ex.Message;
                    }

                });

                DeleteTransactionCommand = new RelayCommand(() => {
                    Transactions.Delete(SelectedTransaction.Id);

                }, () =>
                {
                    return SelectedTransaction != null;
                });
                SelectedTransaction = new Transaction();
                service = new RestService("http://localhost:33531/");
            }
        }

        public ICommand CreateTransactionCommand { get; set; }
        public ICommand DeleteTransactionCommand { get; set; }
        public ICommand UpdateTransactionCommand { get; set; }

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
            } while (Transactions.Contains(new Transaction() { Id = res }));
            //Exchanges.Select(t => t.Id == res).Count() > 0
            return res;
        }

        //private Stock bestSellerStockByExchange;

        //public Stock BestSellerStockByExchange
        //{
        //    get { return bestSellerStockByExchange; }
        //    set
        //    {
        //        if (value != null)
        //        {
        //            bestSellerStockByExchange = service.GetSingle<Stock>(" ");
        //        }
        //        OnPropertyChanged();
        //        (DeleteTransactionCommand as RelayCommand).NotifyCanExecuteChanged();
        //        (CreateTransactionCommand as RelayCommand).NotifyCanExecuteChanged();
        //        (UpdateTransactionCommand as RelayCommand).NotifyCanExecuteChanged();
        //    }
        //}
    }
}
