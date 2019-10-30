using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using static FrenchVerbs.StartPage;

namespace FrenchVerbs
{

    public class VerbForms
    {
        [PrimaryKey]
        public string Word { get; set; }
        public string First_Singular { get; set; }
        public string Second_Singular { get; set; }
        public string Third_Singular { get; set; }
        public string First_Plural { get; set; }
        public string Second_Plural { get; set; }
        public string Third_Plural { get; set; }
    }

    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        private List<VerbForms> verbs;
        private int currWordIndx = 0;
        private SQLiteAsyncConnection dbConn;
        private Dictionary<Tense, string> tenseEnumToTable = new Dictionary<Tense, string>
            {
                {Tense.PresentIndicative, "present_indicative"},
                {Tense.FutureIndicative, "future_indicative"},
                {Tense.ImperfectIndicative, "imperfect_indicative"},
                {Tense.PastHistoricIndicative, "past_historic_indicative"}
            };
        private VerbForms currWord;
        public VerbForms CurrentWord { get
            {
                return currWord;
            } 
            set
            {
                currWord = value;

                infinitive.Text = currWord.Word;
                first_sng.TextColor = Color.Gray;
                first_sng.Text = "";
                second_sng.TextColor = Color.Gray;
                second_sng.Text = "";
                third_sng.TextColor = Color.Gray;
                third_sng.Text = "";
                first_plr.TextColor = Color.Gray;
                first_plr.Text = "";
                second_plr.TextColor = Color.Gray;
                second_plr.Text = "";
                third_plr.TextColor = Color.Gray;
                third_plr.Text = "";
            }
        }
        
        public MainPage(ISet<int> verbGroups, Tense tense)
        {
            InitializeComponent();

            dbConn = new SQLiteAsyncConnection(App.VerbsDBPath);
            
            next_btn.Clicked += (a, b) => CurrentWord = verbs[++currWordIndx];
            chk_btn.Clicked += (a, b) => CheckCorrectness();
            answr_btn.Clicked += (a, b) => ShowAnswer();
            var query = $"select * from {tenseEnumToTable[tense]}, verbs " +
                $"where verbs.word={tenseEnumToTable[tense]}.word " +
                $"and verbs.'group' in ({string.Join(",", verbGroups.Select(x => x + ""))}) " +
                $"and first_singular not like '' " +
                $"and second_singular not like '' " +
                $"and third_singular not like '' " +
                $"and first_plural not like '' " +
                $"and second_plural not like '' " +
                $"and third_plural not like '' " +
                $"order by random()";

            dbConn.QueryAsync<VerbForms>(query).ContinueWith((queryTsk) =>
            {
                verbs = queryTsk.Result;
                Device.BeginInvokeOnMainThread(() =>
                {
                    next_btn.IsEnabled = true;
                    answr_btn.IsEnabled = true;
                    chk_btn.IsEnabled = true;
                    CurrentWord = verbs[currWordIndx];
                });
            });
        }

        private void ShowAnswer()
        {
            first_sng.Text = CurrentWord.First_Singular;
            second_sng.Text = CurrentWord.Second_Singular;
            third_sng.Text = CurrentWord.Third_Singular;
            first_plr.Text = CurrentWord.First_Plural;
            second_plr.Text = CurrentWord.Second_Plural;
            third_plr.Text = CurrentWord.Third_Plural;
            CheckCorrectness();
        }

        private void CheckCorrectness()
        {
            first_sng.TextColor = first_sng.Text == currWord.First_Singular ? Color.Green : Color.Red;
            second_sng.TextColor = second_sng.Text == currWord.Second_Singular ? Color.Green : Color.Red;
            third_sng.TextColor = third_sng.Text == currWord.Third_Singular ? Color.Green : Color.Red;
            first_plr.TextColor = first_plr.Text == currWord.First_Plural ? Color.Green : Color.Red;
            second_plr.TextColor = second_plr.Text == currWord.Second_Plural ? Color.Green : Color.Red;
            third_plr.TextColor = third_plr.Text == currWord.Third_Plural ? Color.Green : Color.Red;
        }
    }
}
