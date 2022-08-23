using ApiChallenge.Repositories.Models;
using Dapper;
using MySql.Data.MySqlClient;
using Repositories.Interfaces;
using System.Data;


namespace Repositories {
    public class MainRepository : IMainRepository {

        public decimal FindTax(string sql, string strConnection) {
           
            decimal tax;

            using (IDbConnection connection = new MySqlConnection(strConnection)) {
                tax = connection.QueryFirstOrDefault<decimal>(sql);
            }
            return tax;
        }

        public IEnumerable<MerchantDiscountRatesDTO> GetAllMerchantDiscountRates(string strConnection) {
            IEnumerable<MerchantDiscountRates> dados;
            using (IDbConnection connection = new MySqlConnection(strConnection)) {
                 dados = connection.Query<MerchantDiscountRates>("SELECT * FROM table_taxes GROUP BY adquirente, bandeira, tipo; ");
            }
            
            var adquirentes = dados.Select(a => a.Adquirente).Distinct();
            List<MerchantDiscountRatesDTO> listagem = new List<MerchantDiscountRatesDTO>();
            foreach (var a in adquirentes) {
                var obj = new MerchantDiscountRatesDTO {
                    Adquirente = a,
                    Taxas = dados.Where(b => b.Adquirente == a)
                                .Select(d => new Tax { 
                                    Bandeira = d.Bandeira, 
                                    Debito = d.Tipo == "debito" ? d.Taxa : null, 
                                    Credito = d.Tipo == "credito" ? d.Taxa : null 
                                }).ToList()                    
                };
                listagem.Add(obj);
            }

            return listagem;
        }

    }
}