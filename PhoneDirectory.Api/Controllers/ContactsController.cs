using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PhoneDirectory.Api.Models;
using PhoneDirectory.Domain.Entities;
using PhoneDirectory.Domain.Interfaces;
using PhoneDirectory.Infrastructure.FileHandlers;

namespace PhoneDirectory.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
// [Authorize]
public class ContactsController : ControllerBase
{
    private readonly IContactService _contactService;
    private readonly ExcelFileHandler _excelHandler;
    private readonly TextFileHandler _textHandler;

    public ContactsController(IContactService contactService)
    {
        _contactService = contactService;
        _excelHandler = new ExcelFileHandler();
        _textHandler = new TextFileHandler();
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create([FromBody] ContactDto contactDto)
    {
        var contact = new Contact
        {
            Name = contactDto.Name,
            Email = contactDto.Email,
            PhoneNumber = contactDto.PhoneNumber,
            Balance = contactDto.Balance,
            Address = contactDto.Address,
            Group = contactDto.Group,
            Status = contactDto.Status
        };
        var result = await _contactService.CreateAsync(contact);
        return Ok(new { Message = "Contact created", Contact = result });
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var contact = await _contactService.GetByIdAsync(id);
        if (contact == null) return NotFound();
        return Ok(contact);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var contacts = await _contactService.GetAllAsync();
        return Ok(contacts);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Update(int id, [FromBody] ContactDto contactDto)
    {
        var contact = await _contactService.GetByIdAsync(id);
        if (contact == null) return NotFound();
        contact.Name = contactDto.Name;
        contact.Email = contactDto.Email;
        contact.PhoneNumber = contactDto.PhoneNumber;
        contact.Balance = contactDto.Balance;
        contact.Address = contactDto.Address;
        contact.Group = contactDto.Group;
        contact.Status = contactDto.Status;
        await _contactService.UpdateAsync(contact);
        return Ok(new { Message = "Contact updated", Contact = contact });
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int id)
    {
        await _contactService.DeleteAsync(id);
        return Ok(new { Message = "Contact deleted" });
    }

    [HttpPut("{id}/disable")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Disable(int id)
    {
        await _contactService.DisableAsync(id);
        return Ok(new { Message = "Contact disabled" });
    }

    [HttpPost("bulk-upload")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> BulkUpload(IFormFile file)
    {
        if (file == null || file.Length == 0) return BadRequest("No file uploaded");
        List<Contact> contacts;
        using (var stream = file.OpenReadStream())
        {
            if (file.FileName.EndsWith(".xlsx"))
                contacts = _excelHandler.ParseExcel(stream);
            else if (file.FileName.EndsWith(".txt"))
                contacts = _textHandler.ParseText(stream);
            else
                return BadRequest("Unsupported file format");
        }
        await _contactService.BulkUploadAsync(contacts);

        return Ok(new { Message = "Bulk upload started" });
    }

    [HttpPost("bulk-delete")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> BulkDelete([FromBody] List<int> ids)
    {
        await _contactService.BulkDeleteAsync(ids);
        return Ok(new { Message = "Bulk delete started" });
    }

    [HttpPost("bulk-disable")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> BulkDisable([FromBody] List<int> ids)
    {
        await _contactService.BulkDisableAsync(ids);
        return Ok(new { Message = "Bulk disable started" });
    }
}
