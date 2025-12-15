namespace ProjectSeraphBackend.Domain
{
    /// <summary>
    /// This class represents a user in the system with a username and password.
    /// It serves as a base class for different types of users, such as citizens and nurses.
    /// </summary>
    public class User
    {
        public string userName { get; set; } = string.Empty;
        public string password { get; set; } = string.Empty;

        //Parameterless constructor for MongoDB
        public User()
        {
        }

        //Constructor with parameters for creating a User object
        public User(string userName, string password)
        {
            this.userName = userName;
            this.password = password;
        }

    }
}
