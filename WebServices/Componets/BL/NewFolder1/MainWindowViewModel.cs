using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace Componets.BL.NewFolder1
{
    class MainWindowViewModel : ViewModelBase
    {
        private QuoteQuickService _quoteQuickService;

        public QuoteQuickService QuoteQuickService
        {
            get { return _quoteQuickService; }

        }

        public MainWindowViewModel()
        {
            _quoteQuickService = new QuoteQuickService();
        }
    }

    class QuoteServiceBase : ObservableObject
    {
        private string _stockName;
        private decimal _stockValue;

        public string StockName
        {
            get { return _stockName; }
            set
            {
                base.Set(ref _stockName, value);
            }
        }

        public decimal StockValue
        {
            get { return _stockValue; }
            set { Set(ref _stockValue, value); }
        }
    }
    class QuoteQuickService : QuoteServiceBase
    {

        private ICommand _quickQuoteCommand;

        public QuoteQuickService()
        {
            _quickQuoteCommand = new RelayCommand(() =>
            {
                var client = new ServiceReferenceQuotes.DelayedStockQuoteSoapClient();
                StockValue = client.GetQuickQuote(StockName, "0");
            });
        }
        public ICommand QuickQuoteCommand { get { return _quickQuoteCommand; } }
    }

    class QuoteFullService : QuoteServiceBase
    {

        private readonly ICommand _quoteCommand;
        private string _companyName;
        private string _fiftyTwoRange;

        public QuoteFullService()
        {
            _quoteCommand = new RelayCommand(() =>
            {
                var client = new ServiceReferenceQuotes.DelayedStockQuoteSoapClient();
                var quoteData = client.GetQuote(StockName, "0");
                StockValue = quoteData.EarnPerShare;
                CompanyName = quoteData.CompanyName;
            });
        }
        public ICommand QuoteCommand { get { return _quoteCommand; } }

        public string CompanyName
        {
            get { return _companyName; }
            set { Set(ref _companyName, value); }
        }

        public string FiftyTwoRange
        {
            get { return _fiftyTwoRange; }
            set { Set(ref _fiftyTwoRange, value); }
        }
    }


}
