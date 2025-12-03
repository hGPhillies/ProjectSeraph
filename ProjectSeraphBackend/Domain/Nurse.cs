using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ProjectSeraphBackend.Domain
{
    /// <summary>
    /// Represents a nurse user within the system, inheriting from the User class.
    /// The Nurse class includes properties specific to a nurse, such as fullName and nurseID. The nurseID is used as a unique identifier in the MongoDB database.
    /// </summary>
    public class Nurse : User
    {
        public string fullName { get; set; } = string.Empty;

        // This property is the MongoDB ObjectId. MongoDB generates its value automatically
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? nurseID { get; set; }

        //Constructor without parameters for MongoDB
        public Nurse() : base(string.Empty, string.Empty)
        {
        }

        //Constructor with parameters for creating a Nurse object
        public Nurse(string fullName, string userName, string password)
            : base(userName, password)
        {
            this.fullName = fullName;
        }

        private void Location()
        {
            throw new NotImplementedException();
        }
    }
}
        