using System;
using System.Reflection;

using Xamarin.Forms;

namespace zd5_Borisov
{
    public partial class MainPage : TabbedPage
    {
        public MainPage(string username)
        {
            InitializeComponent();
            DateLabel.Text = DateTime.Now.ToString("dd/MM/yyyy");
            CurrencyDatePicker.Date = DateTime.Now;

            user.Text = $"Кредитный калькулятор: {username}";

            OnCalculateClicked(null, null);
        }
        private void TermSlider_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            TermEntry.Text = ((int)e.NewValue).ToString();
            OnCalculateClicked(null, null);
        }

        private void RateSlider_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            OnCalculateClicked(null, null);
        }

        private void CurrencyDatePicker_DateSelected(object sender, DateChangedEventArgs e)
        {
            DateLabel.Text = e.NewDate.ToString("dd/MM/yyyy");
        }

        private void OnCalculateClicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(AmountEntry.Text) || AmountEntry.Text == "0") return;
            if (string.IsNullOrWhiteSpace(TermEntry.Text) || TermEntry.Text == "0") return;

            double amount, rate;
            int term;

            if (!double.TryParse(AmountEntry.Text, out amount)) return;
            if (!int.TryParse(TermEntry.Text, out term)) return;

            rate = RateSlider.Value;

            PercentStavka.Text = $"Процентная ставка: {RateSlider.Value}";

            bool isAnnuity = PaymentTypePicker.SelectedIndex == 0;

            if (isAnnuity)
            {
                CalculateAnnuity(amount, term, rate);
            }
            else
            {
                CalculateDifferentiated(amount, term, rate);
            }
        }

        private void CalculateAnnuity(double amount, int term, double rate)
        {
            double monthlyRate = rate / 100 / 12;
            double coefficient = (monthlyRate * Math.Pow(1 + monthlyRate, term)) /
                                (Math.Pow(1 + monthlyRate, term) - 1);

            double monthlyPayment = amount * coefficient;
            double totalPayment = monthlyPayment * term;
            double overpayment = totalPayment - amount;

            MonthlyPaymentLabel.Text = monthlyPayment.ToString("N2");
            TotalPaymentLabel.Text = totalPayment.ToString("N2");
            OverpaymentLabel.Text = overpayment.ToString("N2");
        }

        private void CalculateDifferentiated(double amount, int term, double rate)
        {
            double mainDebt = amount / term;
            double totalPayment = 0;

            double firstPayment = mainDebt + (amount * rate / 100 / 12);
            totalPayment = (firstPayment + mainDebt + (mainDebt * rate / 100 / 12)) / 2 * term;

            double overpayment = totalPayment - amount;

            MonthlyPaymentLabel.Text = "Не рассчитывается";
            TotalPaymentLabel.Text = totalPayment.ToString("N2");
            OverpaymentLabel.Text = overpayment.ToString("N2");
        }
    }
}