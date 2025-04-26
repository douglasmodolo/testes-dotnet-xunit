using JornadaMilhasV1.Modelos;
using System.Globalization;
using System.Linq;
using System.Text;

namespace JornadaMilhas.Test
{
	public class OfertaViagemConstrutorTest
	{
		[Theory]
		[InlineData("", null, "2025-01-01", "2025-01-02", 0, false)]
		[InlineData("Origem", "Destino", "2025-01-04", "2025-10-04", 100, true)]
		[InlineData("Origem", "Destino", "2025-01-04", "2025-10-04", -100, false)]
		[InlineData("Origem", "Destino", "2025-01-04", "2025-10-04", 0, false)]
		public void OfertaViagem_DeveValidarRotaPeriodoPrecoCorretamente(string origem, string destino, string dataIda, string dataVolta, double preco, bool validation)
		{
			// Arrange
			Rota rota = new Rota(origem, destino);
			Periodo periodo = new Periodo(DateTime.Parse(dataIda), DateTime.Parse(dataVolta));

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
			Assert.Contains("A oferta de viagem não possui rota ou período válidos.".Normalize(NormalizationForm.FormC),
							oferta.Erros.Sumario.Normalize(NormalizationForm.FormC));
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
			Assert.Contains("O preço da oferta de viagem deve ser maior que zero.".Normalize(NormalizationForm.FormC),
							oferta.Erros.Sumario.Normalize(NormalizationForm.FormC));
			Assert.False(oferta.EhValido);
		}
	}
}
