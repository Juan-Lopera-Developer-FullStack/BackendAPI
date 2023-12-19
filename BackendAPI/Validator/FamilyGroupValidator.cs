using BackendAPI.Models.Entities;
using FluentValidation;

namespace BackendAPI.Validator
{
    public class FamilyGroupValidator : AbstractValidator<FamilyGroup>
    {
        public FamilyGroupValidator() 
        {
            RuleFor(u => u.Usuario).NotNull().WithMessage("No puede ir vacio el Usuario");
            RuleFor(u => u.Cedula).NotNull().WithMessage("No puede ir vacia la Cedula");
            RuleFor(u => u.Nombres).NotNull().WithMessage("No puede ir vacio el campo Nombres");
            RuleFor(u => u.Apellidos).NotNull().WithMessage("No puede ir vacio el campo Apellidos");
            RuleFor(u => u.Edad).NotNull().GreaterThan(0).WithMessage("La Edad no es valida");
        }
    }
}
