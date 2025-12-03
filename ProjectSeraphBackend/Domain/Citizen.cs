using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ProjectSeraphBackend.Domain
{
    /// <summary>
    /// This class represents a citizen in the system, inheriting from the User class.
    /// It includes personal details such as name, citizen ID, home address, and age.
    /// </summary>
    public class Citizen : User
    {
        public String lastName { get; set; } = string.Empty;
        public String firstName { get; set; } = string.Empty;
        //Citizen ID could be CPR-Nummer, we are using an ID for simplicity of the project
        //and to avoid handling sensitive information

        // This property is the MongoDB ObjectId. MongoDB generates its value automatically
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? citizenID { get; set; } 
        public Home home { get; set; } = new Home();
        public int age { get; set; }

        public Citizen() : base(string.Empty, string.Empty) 
        {

        }
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
