using ApiChallenge.Domain.DTOs;
using ApiChallenge.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiChallenge.Domain.Interfaces.Services {
    public interface IMainService {
        IEnumerable<MerchantDiscountRatesDTO> GetAllMerchantDiscountRates();
        PostResult CalculateTax(Transaction transaction);
    }
}
