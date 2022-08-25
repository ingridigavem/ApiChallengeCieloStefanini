using ApiChallenge.Domain.DTOs;
using ApiChallenge.Domain.Entities;
using ApiChallenge.Domain.Interfaces.Repositories;
using Dapper;
using System.Data;

namespace ApiChallenge.Data.Repositories {
    public class MainRepository : IMainRepository {
        private readonly IDbConnection _connection;

        public MainRepository(IDbConnection connection) {
            _connection = connection;
        }

        public decimal FindTax(Transaction transaction) {

            string sql = @$"SELECT taxa FROM table_taxes
                            WHERE bandeira = '{transaction.Bandeira}'
                                AND adquirente = '{transaction.Adquirente}'
                                AND tipo = '{transaction.Tipo}' ";

            try {
                return _connection.QueryFirstOrDefault<decimal>(sql);

            } catch (Exception) {

                throw;
            }
        }

        public IEnumerable<MerchantDiscountRatesDTO> GetAllMerchantDiscountRates() {
            string sql = @"SELECT * FROM table_taxes; ";

            try {
                IEnumerable<MerchantDiscountRates> dados = _connection.Query<MerchantDiscountRates>(sql);

                List<MerchantDiscountRatesDTO> listagemResult = new List<MerchantDiscountRatesDTO>();

                var adquirentes = dados.Select(a => a.Adquirente).Distinct();
                
                foreach (var a in adquirentes) {
                    var bandeiras = dados.Where(b => b.Adquirente == a).Select(b => b.Bandeira).Distinct();

                    var objAdquirente = new MerchantDiscountRatesDTO {
                        Adquirente = a,
                        Taxas = new List<Tax>()
                    };

                    foreach (var b in bandeiras) {
                        var taxas_bandeira = dados.Where(d => d.Adquirente == a && d.Bandeira == b);

                        var objTaxas = new Tax {
                            Bandeira = b,
                            Debito = taxas_bandeira.FirstOrDefault(t => t.Tipo == "debito")?.Taxa,
                            Credito = taxas_bandeira.FirstOrDefault(t => t.Tipo == "credito")?.Taxa
                        };

                        objAdquirente.Taxas.Add(objTaxas);
                    }

                    listagemResult.Add(objAdquirente);
                }

                return listagemResult;

            } catch (Exception) {

                throw;
            }
        }

    }
}