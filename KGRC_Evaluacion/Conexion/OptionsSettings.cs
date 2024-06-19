using KGRC_Evaluacion.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace KGRC_Evaluacion.Conexion
{
    public class OptionsSettings
    {
        public string Baseurl { get; set; }
        public int TimeSpanMinutes { get; set; }
        public OptionsSettings()
        {
            Baseurl = "";
            TimeSpanMinutes = 0;
        }
        public OptionsSettings(IOptions<AppSettings> options)
        {
            if (options?.Value == null)
            {
                throw new ArgumentNullException(nameof(options), "AppSettings configuration is missing");
            }
            if (string.IsNullOrEmpty(options.Value.Baseurl))
            {
                throw new ArgumentException("Baseurl cannot be null or empty", nameof(options.Value.Baseurl));
            }
            Baseurl = options.Value.Baseurl;
            TimeSpanMinutes = options.Value.TimeSpanMinutes;
        }
       
    }
}
