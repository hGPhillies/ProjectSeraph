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
        public string? NurseID { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

        // Kan udvides med rettigheder evt. 
    }
}
