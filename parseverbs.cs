using FileHelpers;
using MySql.Data.MySqlClient;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestCSharp
{
    class Program
    {

        [DelimitedRecord(",")]
        [IgnoreEmptyLines()]
        [IgnoreFirst()]
        public class Forms
        {
            public string infinitive;
            public string gerund;
            public string present_participle;
            public string past_participle;
            public string compound_verb;
            public string indicative_present_first_person_singular;
            public string indicative_present_second_person_singular;
            public string indicative_present_third_person_singular;
            public string indicative_present_first_person_plural;
            public string indicative_present_second_person_plural;
            public string indicative_present_third_person_plural;
            public string indicative_imperfect_first_person_singular;
            public string indicative_imperfect_second_person_singular;
            public string indicative_imperfect_third_person_singular;
            public string indicative_imperfect_first_person_plural;
            public string indicative_imperfect_second_person_plural;
            public string indicative_imperfect_third_person_plural;
            public string indicative_past_historic_first_person_singular;
            public string indicative_past_historic_second_person_singular;
            public string indicative_past_historic_third_person_singular;
            public string indicative_past_historic_first_person_plural;
            public string indicative_past_historic_second_person_plural;
            public string indicative_past_historic_third_person_plural;
            public string indicative_future_first_person_singular;
            public string indicative_future_second_person_singular;
            public string indicative_future_third_person_singular;
            public string indicative_future_first_person_plural;
            public string indicative_future_second_person_plural;
            public string indicative_future_third_person_plural;
            public string indicative_conditional_first_person_singular;
            public string indicative_conditional_second_person_singular;
            public string indicative_conditional_third_person_singular;
            public string indicative_conditional_first_person_plural;
            public string indicative_conditional_second_person_plural;
            public string indicative_conditional_third_person_plural;
            public string subjunctive_present_first_person_singular;
            public string subjunctive_present_second_person_singular;
            public string subjunctive_present_third_person_singular;
            public string subjunctive_present_first_person_plural;
            public string subjunctive_present_second_person_plural;
            public string subjunctive_present_third_person_plural;
            public string subjunctive_imperfect_first_person_singular;
            public string subjunctive_imperfect_second_person_singular;
            public string subjunctive_imperfect_third_person_singular;
            public string subjunctive_imperfect_first_person_plural;
            public string subjunctive_imperfect_second_person_plural;
            public string subjunctive_imperfect_third_person_plural;
            public string imperative_first_person_singular;
            public string imperative_second_person_singular;
            public string imperative_third_person_singular;
            public string imperative_first_person_plural;
            public string imperative_second_person_plural;
            public string imperative_third_person_plural;

            [FieldHidden]
            public int id { get; private set; }

            public override string ToString()
            {
                return "('" + infinitive + "','" +
                indicative_past_historic_first_person_singular + "','" +
                indicative_past_historic_second_person_singular + "','" +
                indicative_past_historic_third_person_singular + "','" +
                indicative_past_historic_first_person_plural + "','" +
                indicative_past_historic_second_person_plural + "','" +
                indicative_past_historic_third_person_plural + "')"; 

            }
        }
	
        public class Verb
        {
            [PrimaryKey, AutoIncrement, NotNull]
            public int id { get; set; }
            public string word { get; set; }
            public string language { get; set; }
        }

        static void Main(string[] args)
        {
            var filename = @"E:\Downloads\Chrome\french-verb-conjugation.csv";
            var engine = new FileHelperEngine<Forms>();
            engine.Encoding = Encoding.UTF8;
            var records = engine.ReadFile(filename);

            var conn = new SQLiteConnection(@"E:\Projects\FrenchVerbs\FrenchVerbs\FrenchVerbs\verbs.db");
            
            //conn.Execute("INSERT INTO verbs (word) values "
            //    + string.Join(",", records.Select(x => "(\"" + x.infinitive + "\")"))
            //    + ";");

            //var verb_ids = conn.Query<Verb>("select * from verbs").ToDictionary(x=> x.word, y=>y.id);

            conn.Execute("INSERT INTO past_historic_indicative (" 
                + "word," 
                + "first_singular," 
                + "second_singular," 
                + "third_singular," 
                + "first_plural," 
                + "second_plural," 
                + "third_plural" +
                ") values "
    + string.Join(",", records.Select(x => x.ToString()))
    + ";");
        }
    }
}
