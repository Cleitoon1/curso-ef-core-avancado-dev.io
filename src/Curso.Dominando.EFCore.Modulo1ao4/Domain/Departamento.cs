using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;

namespace Curso.Dominando.EFCore.Domain
{
    public class Departamento
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public bool Ativo { get; set; }
        public virtual IList<Funcionario> Funcionarios { get; set; }


        //Implementação do LazyLoad sem utilizar os proxies (lembrar de retirar a parte .UseLazyLoadingProxies() no contexto
        //public Departamento()
        //{

        //}
        //private ILazyLoader _lazyLoader { get; set; }

        //private Departamento(ILazyLoader lazyLoader)
        //{
        //    _lazyLoader = lazyLoader;
        //}

        //private IList<Funcionario> _funcionarios;
        //public IList<Funcionario> Funcionarios { 
        //    get => _lazyLoader.Load(this, ref _funcionarios);
        //    set => _funcionarios = value; 
        //}

        //Implementação do LazyLoad utilizando Actions
        //public Departamento()
        //{

        //}
        //private Action<object, string> _lazyLoader { get; set; }

        //private Departamento(Action<object, string> lazyLoader)
        //{
        //    _lazyLoader = lazyLoader;
        //}

        //private IList<Funcionario> _funcionarios;
        //public IList<Funcionario> Funcionarios
        //{
        //    get
        //    {
        //        _lazyLoader?.Invoke(this, nameof(Funcionarios));
        //        return _funcionarios;

        //    }
        //    set => _funcionarios = value;
        //}



        public override string ToString()
        {
            return $"[Id={Id}, Descricao={Descricao}, Ativo={Ativo}, Funcionarios=[{(Funcionarios != null ? string.Join(", ", Funcionarios) : "")}]]";
        }
    }
}
