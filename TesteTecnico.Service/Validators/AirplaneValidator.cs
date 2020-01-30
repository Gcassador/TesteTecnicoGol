using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using TesteTecnico.Domain.Entities;

namespace TesteTecnico.Service.Validators
{
    public class AirplaneValidator : AbstractValidator<Airplane>
    {
        public AirplaneValidator()
        {
            RuleFor(x => x)
                .NotNull()
                .OnAnyFailure(x =>
                {
                    throw new ArgumentNullException("Não foi possivel encontrar o Avião");
                });

            RuleFor(x => x.Model)
                .NotNull().WithMessage("Necessário informar o Modelo")
                .NotEmpty().WithMessage("Necessário informar o Modelo");

            RuleFor(x => x.NumberOfPassengers)
                .NotNull().WithMessage("Necessário informar o Numero de Passageiros")
                .NotNull().WithMessage("Necessário informar o Numero de Passageiros");
        }
    }
}
