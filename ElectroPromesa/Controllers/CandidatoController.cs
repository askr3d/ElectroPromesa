using ElectroPromesa.Interfaces;
using ElectroPromesa.Models;
using ElectroPromesa.ViewModels.Candidato;
using Microsoft.AspNetCore.Mvc;

namespace ElectroPromesa.Controllers
{
    public class CandidatoController : Controller
    {
        private readonly string carpetaImagenes = "candidatos";
        private readonly ICandidatoRepository _candidatoRepository;
        private readonly IPhotoService _photoService;

        public CandidatoController(ICandidatoRepository candidatoRepository, IPhotoService photoService)
        {
            _candidatoRepository = candidatoRepository;
            _photoService = photoService;
        }
        public async Task<IActionResult> Index()
        {
            IEnumerable<Candidato> candidatos = await _candidatoRepository.GetAll();

            return View(candidatos);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCandidatoViewModel candidatoVM)
        {
            if (ModelState.IsValid)
            {
                if (candidatoVM.FotoUrl == null)
                {
                    ModelState.AddModelError("FotoUrl", "Debe subir una imagen");
                }
                else
                {
                    var result = await _photoService.AddPhotoAsync(candidatoVM.FotoUrl, carpetaImagenes);
                    var candidato = new Candidato
                    {
                        Nombre = candidatoVM.Nombre,
                        Apellidos = candidatoVM.Apellidos,
                        FotoUrl = result
                    };
                    _candidatoRepository.Add(candidato);
                    return RedirectToAction("Index");
                }
            }

            return View(candidatoVM);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var candidato = await _candidatoRepository.GetByIdAsync(id);
            EditCandidatoViewModel model = new EditCandidatoViewModel
            {
                Id = candidato.Id,
                Nombre = candidato.Nombre,
                Apellidos = candidato.Apellidos,
                FotoSrc = candidato.FotoUrl
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditCandidatoViewModel candidatoVM)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var candidatoTmp = await _candidatoRepository.GetByIdAsyncNoTracking(candidatoVM.Id);
                    candidatoVM.FotoSrc = candidatoTmp.FotoUrl;
                    if (candidatoVM.FotoUrl != null)
                    {
                        if (candidatoVM.FotoSrc != null)
                        {
                            await _photoService.DeletePhotoAsync(candidatoVM.FotoSrc);
                        }
                        candidatoVM.FotoSrc = await _photoService.AddPhotoAsync(candidatoVM.FotoUrl, carpetaImagenes);
                    }

                    Candidato candidato = new Candidato
                    {
                        Id = candidatoVM.Id,
                        Nombre = candidatoVM.Nombre,
                        Apellidos = candidatoVM.Apellidos,
                        FotoUrl = candidatoVM.FotoSrc
                    };

                    _candidatoRepository.Update(candidato);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("FotoUrl", "No se pudo subir la imagen");
                }
            }
            return View(candidatoVM);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] int id)
        {
            try
            {
                var candidato = await _candidatoRepository.GetByIdAsyncNoTracking(id);
                await _photoService.DeletePhotoAsync(candidato.FotoUrl);
                _candidatoRepository.Delete(candidato);
            }
            catch (Exception ex)
            {
                return StatusCode(505, "Error al eliminar el candidato");
            }

            return Ok();
        }
    }
}
