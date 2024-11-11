using Microsoft.EntityFrameworkCore;

public class AuthService{
    private readonly VetDbContext _context;

    public AuthService(VetDbContext context){
        _context = context;
    }

    public async Task<bool> Register(User user){
        if(await _context.Users.AnyAsync(u => u.Email == user.Email)) return false;

        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<User?> Login(string email, string password){
        return await _context.Users
            .FirstOrDefaultAsync(u => u.Email == email && u.Password == password);
    }
}