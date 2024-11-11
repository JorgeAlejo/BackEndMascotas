using Microsoft.EntityFrameworkCore;

public class PetService{
    private readonly VetDbContext _context;
    public PetService(VetDbContext context){
        _context = context;
    }

    //Get All active Pets
    public IEnumerable<Pet> GetAll() => _context.Pets.Where(p => p.IsActive).ToList();
    //Get a pet by ID
    public Pet? GetById(Guid id) => _context.Pets.FirstOrDefault(p => p.Id == id && p.IsActive);
    //Register a new Pet
    public void Create(Pet pet){
        _context.Pets.Add(pet);
        _context.SaveChanges();
    }
    //Update data of a pet
    public bool Update(Guid id, Pet pet){
        var existingPet = GetById(id);
        if (existingPet == null) return false;
        existingPet.Name = pet.Name;
        existingPet.Type = pet.Type;
        existingPet.Gender = pet.Gender;
        existingPet.BirthDate = pet.BirthDate;

        _context.SaveChanges();
        return true;
    }
    //Delete a pet (mark as inactive)
    public bool Delete(Guid id){
        var pet = GetById(id);
        if (pet == null) return false;
        pet.IsActive = false;
        _context.SaveChanges();
        return true; 
    }
}