namespace ProjectSeraphBackend.Domain
{
    public class Nurse : User
    {
        private string fullName;
        private string nurseID;

        public Nurse(string fullName, string nurseID, string userName, string password)
            : base(userName, password)
        {
            this.fullName = fullName;
            this.nurseID = nurseID;
        }

        private void Location()
        {

        }
    }
}
