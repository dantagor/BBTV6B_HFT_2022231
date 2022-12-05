using System;
using BBTV6B_HFT_2022231.Models.Entities;
using ConsoleTools;


namespace BBTV6B_HFT_2022231.Client
{
    internal class Program
    {
        static void Main(string[] args)
        {
            RestService rest = new RestService("http://localhost:33531/", typeof(Transaction).Name);
            CrudService crud = new CrudService(rest);
            NonCrudService nonCrud = new NonCrudService(rest);

            var transSubMenu = new ConsoleMenu(args, level: 1)
                .Add("List", () => crud.List<Transaction>())
                .Add("Create", () => crud.Create<Transaction>())
                .Add("Delete", () => crud.Delete<Transaction>())
                .Add("Update", () => crud.Update<Transaction>())
                .Add("Exit", ConsoleMenu.Close);

            var stockSubMenu = new ConsoleMenu(args, level: 1)
                .Add("List", () => crud.List<Stock>())
                .Add("Create", () => crud.Create<Stock>())
                .Add("Delete", () => crud.Delete<Stock>())
                .Add("Update", () => crud.Update<Stock>())
                .Add("Exit", ConsoleMenu.Close);

            var exchangeSubMenu = new ConsoleMenu(args, level: 1)
                .Add("List", () => crud.List<Exchange>())
                .Add("Create", () => crud.Create<Exchange>())
                .Add("Delete", () => crud.Delete<Exchange>())
                .Add("Update", () => crud.Update<Exchange>())
                .Add("Exit", ConsoleMenu.Close);

            var statsSubMenu = new ConsoleMenu(args, level: 1)
                .Add("Stock with Highest Dividend from specific Region", () => nonCrud.HighestDividendStockFromRegion())
                .Add("Number of transactions in specific Exchange", () => nonCrud.TotalTransactionsByExchange())
                .Add("Most popular stock in specific Exchange", ()=> nonCrud.BestSellerStockByExchange())
                .Add("Biggest purchase by Volume", ()=> nonCrud.GetBiggestPurchase())
                .Add("Exchange Statistics", ()=>nonCrud.ReadExchangeStats())
                .Add("Transaction Statistics", ()=>nonCrud.ReadTransactionStats())
                .Add("Exit", ConsoleMenu.Close);

            var menu = new ConsoleMenu(args, level: 0)
                .Add("Transactions", () => transSubMenu.Show())
                .Add("Stocks", () => stockSubMenu.Show())
                .Add("Exchanges", () => exchangeSubMenu.Show())
                .Add("Non-CRUD", () => statsSubMenu.Show())
                .Add("Exit", ConsoleMenu.Close);

            menu.Show();
        }
    }
}
