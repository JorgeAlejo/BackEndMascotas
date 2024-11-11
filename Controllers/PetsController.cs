// TODO: Implementar con la BD
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class PetsController : ControllerBase{
    private readonly PetService _petService;
    public PetsController(PetService petService){
        _petService = petService;
    }

    [HttpGet]
    public IActionResult GetAll(){
        return Ok(_petService.GetAll());
    }

    [HttpGet("{id}")]
    public IActionResult GetById(Guid id){
        var pet = _petService.GetById(id);
        if (pet == null) return NotFound("Pet not found.");
        return Ok(pet);
    }

    [HttpPost]
    public IActionResult Create([FromBody] Pet pet){
        if (!ModelState.IsValid) return BadRequest(ModelState);
        _petService.Create(pet);
        return CreatedAtAction(nameof(GetById), new { id = pet.Id}, pet);
    }

    [HttpPut("{id}")]
    public IActionResult Update(Guid id, [FromBody] Pet pet){
        if (!ModelState.IsValid) return BadRequest(ModelState);
        if(!_petService.Update(id, pet)) return NotFound("Pet not found.");
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(Guid id){
        if(!_petService.Delete(id)) return NotFound("Pet not found.");
        return NoContent();
    }
}