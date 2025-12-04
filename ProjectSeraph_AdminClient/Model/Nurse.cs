using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSeraph_AdminClient.Model
{
    /// <summary>
    /// Nurse DTO?
    /// </summary>
    public class Nurse
    {
        public string? nurseID { get; set; }
        public string fullName { get; set; } = string.Empty;
        public string userName { get; set; } = string.Empty;
        public string password { get; set; } = string.Empty;

        // Kan udvides med rettigheder evt. 
    }
}
