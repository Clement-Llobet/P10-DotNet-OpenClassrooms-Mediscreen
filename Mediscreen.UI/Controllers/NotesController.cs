using Mediscreen.Infrastructure.SqlServerDatabase.Entities;
using Mediscreen.UI.Controllers.Services.NotesService;
using Mediscreen.UI.Controllers.Services.TriggersService;
using Mediscreen.UI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Mediscreen.UI.Controllers;

public class NotesController : Controller
{
    private readonly INotesService _noteService;
    private readonly ITriggersService _triggersService;
    private readonly UserManager<User> _userManager;

    public NotesController(INotesService noteService, ITriggersService triggersService, UserManager<User> userManager)
    {
        _noteService = noteService;
        _triggersService = triggersService;
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
    public async Task<ActionResult> NoteDetailsCreate(int id)
    {
        var triggersList = await _triggersService.GetAllTriggers();

        GetNotesViewModel noteViewModel = new()
        {
            PatientId = id,
            Triggers = triggersList.ToList()
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

            notesViewModel.Practitioner = practitioner.Id;
            notesViewModel.CreatedDate = DateTime.Now;

            var noteCreated = await _noteService.CreateNote(notesViewModel);

            return RedirectToAction(nameof(ValidationCreation));
        }
        catch
        {
            return View();
        }
    }

    // GET: NotesController/Edit/5
    public async Task<ActionResult> NoteDetailsEdit(int id)
    {
        var triggersList = await _triggersService.GetAllTriggers();
        var note = await _noteService.GetPatientNoteById(id);

        if (note == null)
            return NotFound();

        foreach (var trigger in triggersList)
        {
            trigger.IsSelected = note.Triggers.Any(x => x.TriggerId == trigger.TriggerId);
        }

        note.Triggers = triggersList.ToList();

        return View(note);
    }

    // POST: NotesController/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> NoteDetailsEditPut(NotesViewModel notesViewModel)
    {
        try
        {
            var noteUpdated = await _noteService.UpdateNote(notesViewModel);
            return RedirectToAction(nameof(ValidationUpdate));
        }
        catch
        {
            return View();
        }
    }

    public ActionResult ValidationCreation()
    {
        return View();
    }

    public ActionResult ValidationUpdate()
    {
        return View();
    }
}
