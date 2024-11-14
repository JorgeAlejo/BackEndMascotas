using Microsoft.EntityFrameworkCore;

public class AuthService{
    private readonly VetDbContext _context;

    public AuthService(VetDbContext context){
        _context = context;
    }

    //Get All users
    public async Task<List<User>> GetAllUsers(){
        return await _context.Users.ToListAsync();
    }

    public async Task<bool> Register(User user){
        if(await _context.Users.AnyAsync(u => u.Email == user.Email)) return false;
        //password Hashing
        user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);

        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<User?> Login(string email, string password){
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        if (user == null) return null;

        //Verify password
        if (BCrypt.Net.BCrypt.Verify(password, user.Password)) return user;
        return null;
    }
}