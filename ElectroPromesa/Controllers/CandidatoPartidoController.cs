using ElectroPromesa.Interfaces;
using ElectroPromesa.Models;
using ElectroPromesa.ViewModels.CandidatoPartidos;
using Microsoft.AspNetCore.Mvc;

namespace ElectroPromesa.Controllers
{
    public class CandidatoPartidoController : Controller
    {
        private readonly ICandidatoPartidoRepository _candidatoPartidoRepository;

        public CandidatoPartidoController(ICandidatoPartidoRepository candidatoPartidoRepository)
        {
            _candidatoPartidoRepository = candidatoPartidoRepository;
        }
        public async Task<IActionResult> IndexCandidato(int id)
        {
            var candidato = await _candidatoPartidoRepository.GetCandidatoByIdAsync(id);
            if (candidato == null)
            {
                return RedirectToAction("Index", "Candidato");
            }
            IndexCandidatosViewModel candidatoVM = new IndexCandidatosViewModel
            {
                Id = id,
                Nombre = candidato.Nombre,
                Apellidos = candidato.Apellidos,
                FotoUrl = candidato.FotoUrl,
                Partidos = candidato.Partidos,
            };
            return View(candidatoVM);
        }

        public async Task<IActionResult> AssignPartido(int id)
        {
            var candidato = await _candidatoPartidoRepository.GetCandidatoByIdAsync(id);
            if (candidato == null)
            {
                return RedirectToAction("Index", "Candidato");
            }

            var partidos = await _candidatoPartidoRepository.GetAllPartidos();

            AssignPartidoViewModel model = new AssignPartidoViewModel
            {
                CandidatoId = candidato.Id,
                Partidos = partidos
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AssignPartido(AssignPartidoViewModel assignVM)
        {
            if (!ModelState.IsValid)
            {
                return View(assignVM);
            }
            CandidatoPartido candidatoPartido = new CandidatoPartido
            {
                CandidatoId = assignVM.CandidatoId,
                PartidoId = assignVM.PartidoId,
                FechaInicio = DateOnly.FromDateTime(DateTime.Now)
            };
            var resultCanpar = await _candidatoPartidoRepository.ExistsCandidatoPartido(candidatoPartido);

            //Lo redirecciona en caso de querer agregar uno activo
            if(resultCanpar != null && resultCanpar.FechaInicio > resultCanpar.FechaFin)
            {
                return RedirectToAction("IndexCandidato", new { id = assignVM.CandidatoId });
            }

            //Finaliza el contrato con el partido y le da una fecha fin
            await _candidatoPartidoRepository.EndFechaFinPartido(assignVM.CandidatoId);

            //En caso de haber un partido ya existente, lo reactiva
            if (resultCanpar != null)
            {
                candidatoPartido.Id = resultCanpar.Id;
                _candidatoPartidoRepository.Update(candidatoPartido);
            }
            else //En caso de no haber pertenecido a este partido, lo agrega y lo pone como activo
            {
                _candidatoPartidoRepository.Create(candidatoPartido);
            }

            return RedirectToAction("IndexCandidato", new { id = assignVM.CandidatoId});
        }

        [HttpPost]
        public async Task<IActionResult> AssignCandidatoPartido([FromBody] int canparId)
        {
            try
            {
                var canpar = await _candidatoPartidoRepository.GetById(canparId);
                await _candidatoPartidoRepository.EndFechaFinPartido(canpar.CandidatoId);

                canpar.FechaInicio = DateOnly.FromDateTime(DateTime.Now);
                canpar.FechaFin = new DateOnly(1, 1, 1);

                _candidatoPartidoRepository.Update(canpar);

                return Ok(DateOnly.FromDateTime(DateTime.Now));
            }
            catch (Exception ex)
            {
                return StatusCode(505, ex);
            }
        }
    }
}
