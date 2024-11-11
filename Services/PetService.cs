public class PetService{
    private readonly List<Pet> _pets = new List<Pet>();
    public IEnumerable<Pet> GetAll() => _pets.Where(p => p.IsActive);
    public Pet? GetById(Guid id) => _pets.FirstOrDefault(p => p.Id == id && p.IsActive);
    public void Create(Pet pet) => _pets.Add(pet);
    public bool Update(Guid id, Pet pet){
        var existingPet = GetById(id);
        if (existingPet == null) return false;
        existingPet.Name = pet.Name;
        existingPet.Type = pet.Type;
        existingPet.Gender = pet.Gender;
        existingPet.BirthDate = pet.BirthDate;
        return true;
    }
    public bool Delete(Guid id){
        var pet = GetById(id);
        if (pet == null) return false;
        pet.IsActive = false;
        return true; 
    }
}