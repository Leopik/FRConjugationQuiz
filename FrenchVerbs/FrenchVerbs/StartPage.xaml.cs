using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FrenchVerbs
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StartPage : ContentPage
    {
        public enum Tense
        {
            FutureIndicative,
            ImperfectIndicative,
            PastHistoricIndicative,
            PresentIndicative
        }

        public StartPage()
        {
            InitializeComponent();

            Dictionary<string, Tense> tenseNameToEnum = new Dictionary<string, Tense>
            {
                {"Présent indicatif", Tense.PresentIndicative},
                {"Futur indicatif", Tense.FutureIndicative},
                {"Imparfait indicatif", Tense.ImperfectIndicative},
                {"Passé simple indicatif", Tense.PastHistoricIndicative}
            };
            tense_picker.ItemsSource = tenseNameToEnum.Keys.ToList();
            tense_picker.SelectedItem = tenseNameToEnum.Keys.First();

            start_btn.Clicked += (a, b) => {
                HashSet<int> verbGroups = new HashSet<int>();
                if (first_group_chkbx.IsChecked)
                    verbGroups.Add(1);
                if (second_group_chkbx.IsChecked)
                    verbGroups.Add(2);
                if (third_group_chkbx.IsChecked)
                    verbGroups.Add(3);

                Navigation.PushAsync(new MainPage(verbGroups, tenseNameToEnum[(string) tense_picker.SelectedItem]));
            };

            first_group_chkbx.CheckedChanged += CheckBoxChanged;
            second_group_chkbx.CheckedChanged += CheckBoxChanged;
            third_group_chkbx.CheckedChanged += CheckBoxChanged;
        }

        private void CheckBoxChanged(object sender, CheckedChangedEventArgs e)
        {
            start_btn.IsEnabled = first_group_chkbx.IsChecked || second_group_chkbx.IsChecked || third_group_chkbx.IsChecked;
        }
    }
}