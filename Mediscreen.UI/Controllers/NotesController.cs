using Mediscreen.UI.Controllers.Services.NotesService;
using Mediscreen.UI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Mediscreen.UI.Controllers;

public class NotesController : Controller
{
    private readonly INotesService _noteService;

    public NotesController(INotesService noteService)
    {
        _noteService = noteService;
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
    public ActionResult Create()
    {
        return View();
    }

    // POST: NotesController/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Create(IFormCollection collection)
    {
        try
        {
            return View();
            //return RedirectToAction(nameof(Index));
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
