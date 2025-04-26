using JornadaMilhasV1.Modelos;

namespace JornadaMilhas.Test
{
	public class OfertaViagemTest
	{
		[Fact]
		public void Oferta_EhValida()
		{
			Rota rota = new Rota("Origem", "Destino");
			Periodo periodo = new Periodo(new DateTime(2025, 4, 1), new DateTime(2025, 4, 10));
			double preco = 100.0;

			var validation = true;

			OfertaViagem oferta = new OfertaViagem(rota, periodo, preco);

			Assert.Equal(validation, oferta.EhValido);
		}
	}
}