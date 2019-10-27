using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FrenchVerbs
{
    public partial class App : Application
    {

        public static string VerbsDBPath { get; private set; }
        public App()
        {
            InitializeComponent();

            var embeddedResourceDb = Assembly.GetExecutingAssembly().GetManifestResourceNames().First(s => s.Contains("verbs.db"));
            var embeddedResourceDbStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(embeddedResourceDb);


            VerbsDBPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "verbs.db");
            // Check if your DB has already been extracted.
            if (!File.Exists(VerbsDBPath) | true)
            {
                using (BinaryReader br = new BinaryReader(embeddedResourceDbStream))
                {
                    using (BinaryWriter bw = new BinaryWriter(new FileStream(VerbsDBPath, FileMode.Create)))
                    {
                        byte[] buffer = new byte[2048];
                        int len = 0;
                        while ((len = br.Read(buffer, 0, buffer.Length)) > 0)
                        {
                            bw.Write(buffer, 0, len);
                        }
                    }
                }
            }


            MainPage = new NavigationPage(new StartPage());
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
