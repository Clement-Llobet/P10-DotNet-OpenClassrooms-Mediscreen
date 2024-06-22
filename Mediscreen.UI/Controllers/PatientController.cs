using Mediscreen.UI.Controllers.Services.PatientServices;
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
    /// <summary>
    /// Get the patiens list
    /// </summary>
    /// <param></param>
    /// <returns>asynchronous user data</returns>
    public async Task<ActionResult> Index()
    {
        var patients = await _patientService.GetAllPatients();
        return View(patients);
    }

    // GET: PatientController/Details/5
    /// <summary>
    /// Get the patient details by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns>asynchronous user data</returns>
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
    public ActionResult Edit(int id)
    {
        return View();
    }

    // POST: PatientController/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Edit(int id, IFormCollection collection)
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
