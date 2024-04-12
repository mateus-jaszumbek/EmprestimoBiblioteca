using Biblioteca.Data;
using Biblioteca.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Biblioteca.Controllers
{
    public class EmprestimoController : Controller
    {
        readonly private SistemaDbContext _db;

        public EmprestimoController(SistemaDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            IEnumerable<EmprestimoModels> emprestimos = _db.Emprestimos;
            return View(emprestimos);
        }
        [HttpGet]
        public IActionResult Cadastrar()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Editar(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            EmprestimoModels emprestimos = _db.Emprestimos.FirstOrDefault(x => x.Id == id);

            if (id == null) { return NotFound(); }

            return View(emprestimos);
        }

        [HttpGet]
        public IActionResult Excluir(int? id)
        {
            if (id == null || id == 0) { return NotFound(); }

            EmprestimoModels emprestimo = _db.Emprestimos.FirstOrDefault(x => x.Id == id);

            if (emprestimo == null) { NotFound(); }

            return View(emprestimo);
        }

        [HttpPost("EditarEmprestimo/{id}")]
        public IActionResult Cadastrar(EmprestimoModels emprestimos)
        {
            if (ModelState.IsValid)
            {
                _db.Emprestimos.Add(emprestimos);
                _db.SaveChanges();

                return RedirectToAction("Index");
            }
            return View();
        }
        [HttpPost]
        public IActionResult Editar(EmprestimoModels emprestimo)
        {
            if (ModelState.IsValid)
            {
                _db.Emprestimos.Update(emprestimo);
                _db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(emprestimo);
        }

        [HttpPost]
        public IActionResult Excluir(EmprestimoModels emprestimo)
        {
            if (emprestimo == null) {  return NotFound(); }

            _db.Emprestimos.Remove(emprestimo);
            _db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
