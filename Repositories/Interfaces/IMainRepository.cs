using ApiChallenge.Repositories.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Interfaces {
    public interface IMainRepository {

        decimal FindTax(string sql, string strConnection);
        IEnumerable<MerchantDiscountRatesDTO> GetAllMerchantDiscountRates(string strConnection);
    }
}
