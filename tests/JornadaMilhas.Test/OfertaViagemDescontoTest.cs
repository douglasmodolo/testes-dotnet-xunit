using JornadaMilhasV1.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JornadaMilhas.Test
{
    public class OfertaViagemDescontoTest
    {
        [Fact]
        public void OfertaViagem_DescontoValido_DeveRetornarDescontoCorreto()
		{
			// Arrange
			Rota rota = new Rota("Origem", "Destino");
			Periodo periodo = new Periodo(DateTime.Now, DateTime.Now.AddDays(10));
			double preco = 100.0;
			double desconto = 10.0;
			double precoComDesconto = preco - desconto;
			OfertaViagem oferta = new OfertaViagem(rota, periodo, preco);

			// Act
			oferta.Desconto = desconto;
			
			// Assert
			Assert.Equal(precoComDesconto, oferta.Preco);
		}

		[Theory]
		[InlineData(100, 0)]
		[InlineData(100, -10)]
		public void OfertaViagem_DescontoZeroOuNegativo_DeveRetornarPrecoOriginal(double preco, double desconto)
		{
			// Arrange
			Rota rota = new Rota("Origem", "Destino");
			Periodo periodo = new Periodo(DateTime.Now, DateTime.Now.AddDays(10));			
			OfertaViagem oferta = new OfertaViagem(rota, periodo, preco);

			// Act
			oferta.Desconto = desconto;

			// Assert
			Assert.Equal(preco, oferta.Preco);
		}

		[Theory]
		[InlineData(100, 100)]
		[InlineData(100, 150)]
		public void OfertaViagem_DescontoMaiorOuIgualAoPreco_DeveRetornarPrecoComDescontoMaximo(double preco, double desconto)
		{
			// Arrange
			const double DESCONTO_MAXIMO = 0.7;
			Rota rota = new Rota("Origem", "Destino");
			Periodo periodo = new Periodo(DateTime.Now, DateTime.Now.AddDays(10));
			double precoComDesconto = preco - (preco * DESCONTO_MAXIMO);
			
			OfertaViagem oferta = new OfertaViagem(rota, periodo, preco);

			// Act
			oferta.Desconto = desconto;

			// Assert
			Assert.Equal(precoComDesconto, oferta.Preco, 0.001);
		}
	}
}
