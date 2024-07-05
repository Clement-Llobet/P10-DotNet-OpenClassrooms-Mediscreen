using Mediscreen.UI.Controllers.Services.PatientServices;
using Mediscreen.UI.Models;
using Microsoft.AspNetCore.Mvc;

namespace Mediscreen.UI.Controllers;

public class PatientController : Controller
{
    private readonly IPatientService _patientService;

    public PatientController(IPatientService patientService)
    {
        _patientService = patientService;
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
        return View(patient);
    }

    // GET: PatientController/Create
    public ActionResult Create()
    {
        return View();
    }

    // POST: PatientController/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Create(IFormCollection collection)
    {
        try
        {
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

    // GET: PatientController/Delete/5
    public ActionResult Delete(int id)
    {
        return View();
    }

    // POST: PatientController/Delete/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Delete(int id, IFormCollection collection)
    {
        try
        {
            return RedirectToAction(nameof(Index));
        }
        catch
        {
            return View();
        }
    }
}
