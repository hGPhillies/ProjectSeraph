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
        public string lastName { get; set; } = string.Empty;
        public string firstName { get; set; } = string.Empty;

        // This property is the MongoDB ObjectId. MongoDB generates its value automatically
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)] //Changed from (BsonType.ObjectId) Because citizenID is a string
        public string citizenID { get; set; } = string.Empty;
        public Home home { get; set; } = new Home();
        public int age { get; set; }

        //Citizen permissions   
        public bool canMeasureBloodPressure { get; set; }
        public bool canMeasureBloodSugar { get; set; }

        public Citizen() : base(string.Empty, string.Empty) { }
        public Citizen(string userName, string password) : base(userName, password) { }


        private void homeAdress() { }

        private void myMeasurements() { }
    }
}
