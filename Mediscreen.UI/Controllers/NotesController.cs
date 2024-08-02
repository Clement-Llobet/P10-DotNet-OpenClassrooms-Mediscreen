using Mediscreen.Infrastructure.SqlServerDatabase.Entities;
using Mediscreen.UI.Controllers.Services.NotesService;
using Mediscreen.UI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Mediscreen.UI.Controllers;

public class NotesController : Controller
{
    private readonly INotesService _noteService;
    private readonly UserManager<User> _userManager;

    public NotesController(INotesService noteService, UserManager<User> userManager)
    {
        _noteService = noteService;
        _userManager = userManager;
    }

    // GET: NotesController/Details/5
    public async Task<ActionResult> NoteDetails(int id)
    {
        var note = await _noteService.GetPatientNoteById(id);

        if (note == null)
        {
            return NotFound();
        }

        return View(note);
    }

    // GET: NotesController/Create
    public ActionResult NoteDetailsCreate(int id)
    {
        NotesViewModel noteViewModel = new()
        {
            PatientId = id,
        };

        return View(noteViewModel);
    }

    // POST: NotesController/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> NoteDetailsCreate(NotesViewModel notesViewModel)
    {
        try
        {
            var practitioner = await _userManager.GetUserAsync(User);

            if (practitioner == null)
            {
                return NotFound();
            }

            var note = new NotesViewModel
            {
                PatientId = notesViewModel.PatientId,
                Note = notesViewModel.Note,
                Practitioner = practitioner.Id,
                CreatedDate = DateTime.Now,
            };

            var noteCreated = await _noteService.CreateNote(note);
            return RedirectToAction(nameof(NoteDetails), new { });
        }
        catch
        {
            return View();
        }
    }

    // GET: NotesController/Edit/5
    public async Task<ActionResult> NoteDetailsEdit(int id)
    {
        var note = await _noteService.GetPatientNoteById(id);

        if (note == null)
        {
            return NotFound();
        }

        return View(note);
    }

    // POST: NotesController/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> NoteDetailsEditPut(int id, NotesViewModel notesViewModel)
    {
        try
        {
            var noteUpdated = await _noteService.UpdateNote(id, notesViewModel);
            return RedirectToAction(nameof(NoteDetails), new { id });
        }
        catch
        {
            return View();
        }
    }
}
