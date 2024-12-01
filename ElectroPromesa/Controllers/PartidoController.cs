using ElectroPromesa.Interfaces;
using ElectroPromesa.Models;
using ElectroPromesa.ViewModels.Partido;
using Microsoft.AspNetCore.Mvc;

namespace ElectroPromesa.Controllers
{
    public class PartidoController : Controller
    {
        private readonly string carpetaImagenes = "partidos";
        private readonly IPartidoRepository _partidoRepository;
        private readonly IPhotoService _photoService;

        public PartidoController(IPartidoRepository partidoRepository, IPhotoService photoService)
        {
            _partidoRepository = partidoRepository;
            _photoService = photoService;
        }
        public async Task<IActionResult> Index()
        {
            IEnumerable<Partido> partidos = await _partidoRepository.GetAll();
            return View(partidos);
        }

        public IActionResult Create()
        {
            CreatePartidoViewModel model = new CreatePartidoViewModel();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreatePartidoViewModel partidoVM)
        {
            if (ModelState.IsValid)
            {
                if (partidoVM.FotoUrl == null)
                {
                    ModelState.AddModelError("FotoUrl", "Debe subir una imagen.");
                }
                else
                {
                    var result = await _photoService.AddPhotoAsync(partidoVM.FotoUrl, carpetaImagenes);
                    var partido = new Partido
                    {
                        Nombre = partidoVM.Nombre,
                        Abreviatura = partidoVM.Abreviatura,
                        FotoUrl = result
                    };
                    _partidoRepository.Add(partido);
                    return RedirectToAction("Index");
                }
            }

            return View(partidoVM);
        }

        public async Task<IActionResult> Edit(int id) {
            var partido = await _partidoRepository.GetByIdAsync(id);
            EditPartidoViewModel model = new EditPartidoViewModel
            {
                Id = partido.Id,
                Nombre = partido.Nombre,
                Abreviatura= partido.Abreviatura,
                FotoSrc = partido.FotoUrl,
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditPartidoViewModel partidoVM)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var partidoTmp = await _partidoRepository.GetByIdAsyncNoTracking(partidoVM.Id);
                    partidoVM.FotoSrc = partidoTmp.FotoUrl;
                    if (partidoVM.FotoUrl != null)
                    {
                        if (partidoVM.FotoSrc != null)
                        {
                            await _photoService.DeletePhotoAsync(partidoVM.FotoSrc);
                        }
                        partidoVM.FotoSrc = await _photoService.AddPhotoAsync(partidoVM.FotoUrl, carpetaImagenes);
                    }

                    Partido partido = new Partido
                    {
                        Id = partidoVM.Id,
                        Nombre = partidoVM.Nombre,
                        Abreviatura = partidoVM.Abreviatura,
                        FotoUrl = partidoVM.FotoSrc
                    };

                    _partidoRepository.Update(partido);

                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("FotoUrl", "No se puedo subir la imagen");
                }
            }

            return View(partidoVM);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] int id)
        {
            try
            {
                var partido = await _partidoRepository.GetByIdAsyncNoTracking(id);
                await _photoService.DeletePhotoAsync(partido.FotoUrl);
                _partidoRepository.Delete(partido);
            }
            catch (Exception ex)
            {
                return StatusCode(505, "Error al eliminar el partido");
            }
            return Ok();
        }
    }
}
