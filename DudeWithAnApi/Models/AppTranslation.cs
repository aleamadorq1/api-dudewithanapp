using System;

namespace DudeWithAnApi.Models
{
    public class AppTranslation
    {
        public AppTranslation()
        {
        }

        public int Id { get; set; }
        public string AppName { get; set; }
        public string PremiumViewTitle { get; set; }
        public string PremiumViewText1 { get; set; }
        public string PremiumViewText2 { get; set; }
        public string PremiumViewText3 { get; set; }
        public string PremiumViewButtonTextTry { get; set; }
        public string PremiumViewButtonTextPatreon { get; set; }
        public string PremiumViewButtonTextRestore { get; set; }
        public string LanguageCode { get; set; }
    }
}

