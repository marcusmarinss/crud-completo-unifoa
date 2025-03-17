using Academico.Models;
using Microsoft.AspNetCore.Mvc;

namespace Academico.Controllers
{
    public class AlunoController : Controller
    {

        private static List<Aluno> alunos = new List<Aluno>();
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Create([Bind("Nome", "Email", "Telefone", "Endereco", "Complemento", "Bairro", "Municipio", "Uf", "Cep")] Aluno aluno)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    aluno.AlunoID = alunos.Count + 1;
                    alunos.Add(aluno);
                    return RedirectToAction("Index");
                }
                return View(aluno);
            }
            catch(Exception ex) 
            {
                ViewData["Erro"] = ex.Message;
                return View(aluno);
            }
        }
        public IActionResult Index()
        {
            return View(alunos);
        }

        public IActionResult Edit(int id)
        {
            var aluno = alunos.FirstOrDefault(a => a.AlunoID == id);
            if (aluno == null)
            {
                return NotFound();
            }
            return View(aluno);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Edit(Aluno aluno)
        {
            alunos.Remove(alunos.Where(i => i.AlunoID == aluno.AlunoID).First());
            alunos.Add(aluno);
            return RedirectToAction("Index");
        }

        public IActionResult Details(int id)
        {
            var aluno = alunos.FirstOrDefault(a => a.AlunoID == id);
            if (aluno == null)
            {
                return NotFound();
            }
            return View(aluno);
        }

        public IActionResult Delete(int id)
        {
            var aluno = alunos.FirstOrDefault(a => a.AlunoID == id);
            if (aluno == null)
            {
                return NotFound();
            }
            return View(aluno);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]

        public IActionResult DeleteConfirmed(int id)
        {
            try
            {
                var aluno = alunos.FirstOrDefault(a => a.AlunoID == id);
                if (aluno == null)
                {
                    return NotFound();
                }
                alunos.Remove(aluno);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Não foi possível excluir o aluno: {ex.Message}");
            }
            return View(alunos);
        }
    }
}
