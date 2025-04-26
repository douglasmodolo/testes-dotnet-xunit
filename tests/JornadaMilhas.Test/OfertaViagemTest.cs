using JornadaMilhasV1.Modelos;

namespace JornadaMilhas.Test
{
	public class OfertaViagemTest
	{
		[Fact]
		public void OfertaViagem_RotaPeriodoPrecoValidos_DeveSerValida()
		{
			// Arrange
			Rota rota = new Rota("Origem", "Destino");
			Periodo periodo = new Periodo(new DateTime(2025, 4, 1), new DateTime(2025, 4, 10));
			double preco = 100.0;

			var validation = true;

			// Act
			OfertaViagem oferta = new OfertaViagem(rota, periodo, preco);

			// Assert
			Assert.Equal(validation, oferta.EhValido);
		}

		[Fact]
		public void OfertaViagem_RotaInvalida_DeveRetornarErro()
		{
			// Arrange
			Rota rota = null;
			Periodo periodo = new Periodo(new DateTime(2025, 4, 1), new DateTime(2025, 4, 10));
			double preco = 100.0;

			// Act
			OfertaViagem oferta = new OfertaViagem(rota, periodo, preco);

			// Assert
			Assert.Contains("A oferta de viagem não possui rota ou período válidos.", oferta.Erros.Sumario);
			Assert.False(oferta.EhValido);
		}

		[Fact]
		public void OfertaViagem_PrecoNegativo_DeveRetornarErro()
		{
			// Arrange
			Rota rota = new Rota("Origem", "Destino");
			Periodo periodo = new Periodo(new DateTime(2025, 4, 1), new DateTime(2025, 4, 10));
			double preco = -100.0;
			
			// Act
			OfertaViagem oferta = new OfertaViagem(rota, periodo, preco);
			
			// Assert
			Assert.Contains("O preço da oferta de viagem deve ser maior que zero.", oferta.Erros.Sumario);
			Assert.False(oferta.EhValido);
		}
	}
}