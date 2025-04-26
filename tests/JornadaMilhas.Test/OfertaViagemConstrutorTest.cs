using JornadaMilhasV1.Modelos;
using System.Globalization;
using System.Linq;
using System.Text;

namespace JornadaMilhas.Test
{
	public class OfertaViagemConstrutorTest
	{
		public static string NormalizeString(string input)
		{
			return input.Normalize(NormalizationForm.FormD)
						.Where(c => CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
						.Aggregate(new StringBuilder(), (sb, c) => sb.Append(c))
						.ToString()
						.Normalize(NormalizationForm.FormC);
		}

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
			Assert.Contains("A oferta de viagem n�o possui rota ou per�odo v�lidos.".Normalize(NormalizationForm.FormC),
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
			Assert.Contains("O pre�o da oferta de viagem deve ser maior que zero.".Normalize(NormalizationForm.FormC),
							oferta.Erros.Sumario.Normalize(NormalizationForm.FormC));
			Assert.False(oferta.EhValido);
		}
	}
}
