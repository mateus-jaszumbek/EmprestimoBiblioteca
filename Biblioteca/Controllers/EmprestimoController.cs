using Biblioteca.Data;
using Biblioteca.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ClosedXML.Excel;

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

            if (emprestimo == null) { return NotFound(); }

            return View(emprestimo);
        }

        public IActionResult Exportar()
        {
            var dados = GetDados();
            using (XLWorkbook workbook = new XLWorkbook())
            {
                workbook.AddWorksheet(dados, "Dados Emprestimo");

                using (MemoryStream memoriaString = new MemoryStream())
                {
                    workbook.SaveAs(memoriaString);
                    return File(memoriaString.ToArray(), "apllication/vnd.openxmlformats-officedocument.spredsheetml.sheet", "Emprestimo.xls");
                }
            }
        }

        private DataTable GetDados()
        {
            DataTable dataTable = new DataTable();
            dataTable.TableName = "Dados emprestimos";
            dataTable.Columns.Add("Id", typeof(int));
            dataTable.Columns.Add("Recebedor", typeof(string));
            dataTable.Columns.Add("Fornecedor", typeof(string));
            dataTable.Columns.Add("LivroEmprestado", typeof(string));
            dataTable.Columns.Add("Data emprestimo", typeof(DateTime));

            var dados = _db.Emprestimos.ToList();

            if (dados.Count > 0)
            {
                dados.ForEach(emprestimo =>
                {
                    dataTable.Rows.Add(emprestimo.Id, emprestimo.Recebedor, emprestimo.Fornecedor, emprestimo.LivroEmprestado, emprestimo.DataUltimaAtualizacao);
                });
            }

            return dataTable;
        }

        [HttpPost]
        public IActionResult Cadastrar(EmprestimoModels emprestimos)
        {
            if (ModelState.IsValid)
            {
                _db.Emprestimos.Add(emprestimos);
                _db.SaveChanges();

                TempData["MensagemSucesso"] = "Cadastro realizado com sucesso!";

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

                TempData["MensagemSucesso"] = "Edição realizada com sucesso!";

                return RedirectToAction("Index");
            }

            return View(emprestimo);
        }

        [HttpPost]
        public IActionResult Excluir(EmprestimoModels emprestimo)
        {
            if (emprestimo == null) { return NotFound(); }

            _db.Emprestimos.Remove(emprestimo);
            _db.SaveChanges();

            TempData["MensagemSucesso"] = "Exclusão realizada com sucesso!";

            return RedirectToAction("Index");
        }
    }
}
