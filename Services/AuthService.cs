// TODO: Implementar un servicio de token seguro como JWT
public class AuthService{
    private readonly List<User> _users = new List<User>();

    public bool Register(User user){
        if(_users.Any(u => u.Email == user.Email)) return false;
        _users.Add(user);
        return true;
    }

    public User? Login(string email, string password){
        return _users.FirstOrDefault(u => u.Email == email && u.Password == password);
    }
}