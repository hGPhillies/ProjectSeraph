using System;

namespace ProjectSeraph_AdminClient.Model
{
    public class Citizen
    {
        public string citizenID { get; set; } = string.Empty;

        
        public string firstName { get; set; } = string.Empty;
        public string lastName { get; set; } = string.Empty;

        // Computed full 
        public string fullName => $"{firstName} {lastName}".Trim();
    }
}
