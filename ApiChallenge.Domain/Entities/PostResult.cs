namespace ApiChallenge.Domain.Entities {
    public class PostResult {
        public decimal ValorLiquido { get; set; }

        public PostResult(decimal valorLiquido) {
            ValorLiquido = valorLiquido;
        }
    }
}
