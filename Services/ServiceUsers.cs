using MongoDB.Driver;
using WebApplication_Services.Models;

namespace WebApplication_Services.Services
{
    public interface IServiceUsers
    {
        public IEnumerable<User> Read();
        public User GetUserById(string id);
        public User Create(User user);
        public User Update(User user);
        public User GetUserByEmail(string email);
        public void Delete(string id);
    }


    public class ServiceUsers : IServiceUsers
    {
        private readonly UserContext _db;
        public ServiceUsers(UserContext db)
        {
            _db = db;
        }

        User IServiceUsers.Create(User? user)
        {
            user.Id = Guid.NewGuid().ToString();
            _db.Users.InsertOne(user);
            return user;

        }

        void IServiceUsers.Delete(string id)
        {

            var result = _db.Users.DeleteOne(u => u.Id == id);
            if (result.DeletedCount == 0)
            {
                Console.WriteLine($"User with ID {id} not found.");
            }

        }

        IEnumerable<User> IServiceUsers.Read()
        {

            return _db.Users.Find(_ => true).ToList();


        }
        public User GetUserByEmail(string email)
        {
            return _db.Users.Find(user => user.Email == email).FirstOrDefault();

        }
        public User GetUserById(string id)
        {
            return _db.Users.Find(user => user.Id == id).FirstOrDefault();

        }

        User IServiceUsers.Update(User user)
        {
            _db.Users.ReplaceOne(u => u.Id == user.Id, user);
            return user;
        }


    }
}
