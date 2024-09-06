using Mediscreen.UI.Controllers.Services.NotesService;
using Mediscreen.UI.Controllers.Services.PatientServices;
using Mediscreen.UI.Models;
using Microsoft.AspNetCore.Mvc;

namespace Mediscreen.UI.Controllers;

public class PatientController : Controller
{
    private readonly IPatientService _patientService;
    private readonly INotesService _noteService;

    public PatientController(IPatientService patientService, INotesService noteService)
    {
        _patientService = patientService;
        _noteService = noteService;
    }

    // GET: PatientController
    public async Task<ActionResult> Index()
    {
        var patients = await _patientService.GetAllPatients();

        return View(patients);
    }

    // GET: PatientController/Details/5
    public async Task<ActionResult> PatientDetails(int id)
    {
        var patient = await _patientService.GetPatientById(id);

        if (patient == null)
        {
            return NotFound();
        }

        var notes = await _noteService.GetAllPatientNotes(id);
        patient.Notes = notes
            .Where(note => note.PatientId == id)
            .OrderByDescending(note => note.CreatedDate)
            .ToList();

        return View(patient);
    }

    // GET: PatientController/Create
    public ActionResult CreatePatient()
    {
        return View();
    }

    // POST: PatientController/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> CreatePatientPost(PatientViewModel patient)
    {
        try
        {
            var userUpdated = await _patientService.CreatePatient(patient);
            return RedirectToAction(nameof(Index));
        }
        catch
        {
            return View();
        }
    }

    // GET: PatientController/Edit/5
    public async Task<ActionResult> PatientDetailsEditGet(int id)
    {
        var patient = await _patientService.GetPatientById(id);
        return View(patient);
    }

    // POST: PatientController/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> PatientDetailsEditPut(int id, PatientViewModel patient)
    {
        try
        {
            var userUpdated = await _patientService.UpdatePatient(id, patient);
            return RedirectToAction(nameof(PatientDetails), new { id });
        }
        catch
        {
            return View();
        }
    }
}
