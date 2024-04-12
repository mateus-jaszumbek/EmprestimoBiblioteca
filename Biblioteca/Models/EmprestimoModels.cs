﻿using System.ComponentModel.DataAnnotations;

namespace Biblioteca.Models
{
    public class EmprestimoModels
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Digite o nome do Recebedor")]
        public string Recebedor { get; set; }

        [Required(ErrorMessage = "Digite o nome do Fornecedor")]
        public string Fornecedor { get; set;}

        [Required(ErrorMessage = "Digite o nome do Livro")]
        public string LivroEmprestado { get; set; }
        public DateTime DataUltimaAtualizacao { get; set; } = DateTime.Now;
    }
}