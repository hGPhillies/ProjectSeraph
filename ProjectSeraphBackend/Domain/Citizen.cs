namespace ProjectSeraphBackend.Domain
{
    public class Citizen : User
    {
        public String lastName { get; set; } = string.Empty;
        public String firstName { get; set; } = string.Empty;
        //Citizen ID could be CPR-Nummer, we are using an ID for simplicity of the project and to avoid handling sensitive information
        public String citizenID { get; set; } = string.Empty;
        public Home home { get; set; } = new Home();
        public int age { get; set; }

        
        




        public Citizen(string userName, string password) : base(userName, password)
        {
        }

        private void homeAdress()
        {

        }

        private void myMeasurements()
        {
        }

    }
}
