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

		[Fact]
		public void OfertaViagem_DescontoMaiorQuePreco_DeveRetornarDescontoMaximo()
		{
			// Arrange
			Rota rota = new Rota("Origem", "Destino");
			Periodo periodo = new Periodo(DateTime.Now, DateTime.Now.AddDays(10));
			double preco = 100.0;
			double desconto = 150.0; // Desconto maior que o preço original
			double precoComDesconto = 30.0; // Preço com desconto máximo (70%)
			OfertaViagem oferta = new OfertaViagem(rota, periodo, preco);
			
			// Act
			oferta.Desconto = desconto;
			
			// Assert
			Assert.Equal(precoComDesconto, oferta.Preco, 0.001);
		}

		[Fact]
		public void OfertaViagem_DescontoZero_DeveRetornarPrecoOriginal()
		{
			// Arrange
			Rota rota = new Rota("Origem", "Destino");
			Periodo periodo = new Periodo(DateTime.Now, DateTime.Now.AddDays(10));
			double preco = 100.0;
			double desconto = 0.0; // Desconto zero
			OfertaViagem oferta = new OfertaViagem(rota, periodo, preco);
			
			// Act
			oferta.Desconto = desconto;
			
			// Assert
			Assert.Equal(preco, oferta.Preco);
		}

		[Fact]
		public void OfertaViagem_DescontoNegativo_DeveRetornarPrecoOriginal()
		{
			// Arrange
			Rota rota = new Rota("Origem", "Destino");
			Periodo periodo = new Periodo(DateTime.Now, DateTime.Now.AddDays(10));
			double preco = 100.0;
			double desconto = -10.0; // Desconto negativo
			OfertaViagem oferta = new OfertaViagem(rota, periodo, preco);
			
			// Act
			oferta.Desconto = desconto;
			
			// Assert
			Assert.Equal(preco, oferta.Preco);
		}

	}
}
