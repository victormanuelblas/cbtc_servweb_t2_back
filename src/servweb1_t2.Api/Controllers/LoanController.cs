using servweb1_t2.Application.DTOs.Loan;
using servweb1_t2.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using servweb1_t2.Domain.Exceptions;

namespace servweb1_t2.Api.Controllers;
[ApiController]
[Route("api/[controller]")]
public class LoanController : ControllerBase
{
    private readonly ILoanService _loanService; 
    public LoanController(ILoanService loanService)
    {
        _loanService = loanService;
    }
    [HttpGet]
    public async Task<IActionResult> GetAllLoans()
    {
        var loans = await _loanService.GetAllLoansAsync();
        return Ok(loans);
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetLoanById(int id)
    {
        try
        {
            var loan = await _loanService.GetLoanByIdAsync(id);
            return Ok(loan);
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpPost("{id}/return")]
    public async Task<IActionResult> ReturnLoan(int id)
    {
        try
        {
            await _loanService.ReturnLoanAsync(id);
            return Ok(new { message = "Préstamo devuelto correctamente." });
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (DomainException ex)
        {
            return BadRequest(ex.Message);
        }
    }
    [HttpPost]
    public async Task<IActionResult> CreateLoan([FromBody] CreateLoanDto createLoanDto)
    {
        var createdLoan = await _loanService.CreateLoanAsync(createLoanDto);
        return CreatedAtAction(nameof(GetLoanById), new { id = createdLoan.Id }, createdLoan);
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateLoan(int id, [FromBody] UpdateLoanDto updateLoanDto)
    {
        try
        {
            var updatedLoan = await _loanService.UpdateLoanAsync(id, updateLoanDto);
            return Ok(updatedLoan); 
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteLoan(int id)
    {
        try
        {
            await _loanService.DeleteLoanAsync(id);
            return NoContent();
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }
}