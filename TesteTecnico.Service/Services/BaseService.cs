using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using TesteTecnico.Domain.Entities;
using TesteTecnico.Domain.Interfaces;
using TesteTecnico.Infra.Data.Repository;

namespace TesteTecnico.Service.Services
{
    public class BaseService<T> : IService<T> where T : BaseEntity
    {

        private readonly IRepository<T> _repository;

        public BaseService(
            IRepository<T> baseRepository
            )
        {
            _repository = baseRepository;
        }

        public T Post<V>(T obj) where V : AbstractValidator<T>
        {
            Validate(obj, Activator.CreateInstance<V>());

            _repository.Insert(obj);
            return obj;
        }

        public T Put<V>(T obj) where V : AbstractValidator<T>
        {
            Validate(obj, Activator.CreateInstance<V>());

            _repository.Update(obj);
            return obj;
        }

        public void Delete(int id)
        {
            if (id == 0)
                throw new ArgumentException("The id can't be zero.");

            _repository.Remove(id);
        }

        public IList<T> Get() => _repository.SelectAll();

        public T Get(int id)
        {
            if (id == 0)
                throw new ArgumentException("The id can't be zero.");

            return _repository.Select(id);
        }

        private void Validate(T obj, AbstractValidator<T> validator)
        {
            if (obj == null)
                throw new Exception("Registros não detectados!");

            validator.ValidateAndThrow(obj);
        }
    }
}
