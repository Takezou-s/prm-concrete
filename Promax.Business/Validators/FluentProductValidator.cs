using FluentValidation;
using Promax.Core;
using Promax.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Promax.Business
{
    public class FluentProductValidator : AbstractValidator<Product>, IBeeValidator<Product>
    {
        public FluentProductValidator()
        {
            RuleFor(x => x.GateNum).GreaterThan(0).WithMessage("Kapak seçimi boş bırakılamaz!").WithErrorCode("Gate not selected");
            RuleFor(x => x.MixerServiceId).GreaterThan(0).WithMessage("Servis seçimi boş bırakılamaz!").When(x => x.ServiceCatNum == 1 || x.ServiceCatNum == 2).WithErrorCode("Mixer service not selected");
            RuleFor(x => x.PumpServiceId).GreaterThan(0).WithMessage("Servis seçimi boş bırakılamaz!").When(x => x.ServiceCatNum == 2).WithErrorCode("Pump service not selected");
            RuleFor(x => x.SelfServiceId).GreaterThan(0).WithMessage("Servis seçimi boş bırakılamaz!").When(x => x.ServiceCatNum == 3).WithErrorCode("Self service not selected");
            RuleFor(x => x.Target).GreaterThan(0).WithMessage("Miktar boş bırakılamaz!").WithErrorCode("Target is not valid");
            //RuleFor(x=>x.RealTarget).LessThanOrEqualTo(x=>x.Service.Capacity).WithMessage("Kapasite aşıldı!").When(x => x.Service != null && (x.ServiceCatNum == 1 || x.ServiceCatNum == 2)).WithErrorCode("Capacity is not valid");
        }
    }
}
