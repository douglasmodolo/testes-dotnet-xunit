using Bogus;
using JornadaMilhasV1.Gerencidor;
using JornadaMilhasV1.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JornadaMilhas.Test
{
	public class GerenciadorOfertasRecuperaMaiorDescontoTest
	{
		[Fact]
		public void GerenciadorOfertas_ListaVazia_DeveRetornarOfertaNula()
		{
			// Arrange
			List<OfertaViagem> ofertas = new List<OfertaViagem>();
			GerenciadorDeOfertas gerenciador = new GerenciadorDeOfertas(ofertas);
			Func<OfertaViagem, bool> filtro = o => o.Rota.Destino.Equals("São Paulo");

			// Act
			OfertaViagem resultado = gerenciador.RecuperaMaiorDesconto(filtro);

			// Assert
			Assert.Null(resultado);
		}

		[Fact]
		public void GerenciadorOfertas_ListaComUmaOferta_DeveRetornarEssaOferta()
		{
			// Arrange
			List<OfertaViagem> ofertas = new List<OfertaViagem>
			{
				new OfertaViagem(new Rota("Rio de Janeiro", "São Paulo"), new Periodo(DateTime.Now, DateTime.Now.AddDays(5)), 1000)
			};
			GerenciadorDeOfertas gerenciador = new GerenciadorDeOfertas(ofertas);
			Func<OfertaViagem, bool> filtro = o => o.Rota.Destino.Equals("São Paulo");
	
			// Act
			OfertaViagem resultado = gerenciador.RecuperaMaiorDesconto(filtro);
			
			// Assert
			Assert.NotNull(resultado);
			Assert.Equal("São Paulo", resultado.Rota.Destino);
		}

		[Fact]
		public void GerenciadorOfertas_DestinoSaoPauloEDesconto40_DeveRetornarEssaOferta()
		{
			// Arrange
			var fakerPeriodo = new Faker<Periodo>()
				.CustomInstantiator(f =>
				{
					DateTime dataInicial = f.Date.Soon();
					return new Periodo(dataInicial, dataInicial.AddDays(30));
				});

			var rota = new Rota("Curitiba", "São Paulo");
			
			var fakerOferta = new Faker<OfertaViagem>()
				.CustomInstantiator(f =>
				{
					return new OfertaViagem(rota, fakerPeriodo.Generate(), 100 * f.Random.Int(1, 100));
				})
				.RuleFor(o => o.Desconto, f => 40)
				.RuleFor(o => o.Ativa, f => true);

			var ofertaEscolhida = new OfertaViagem(rota, fakerPeriodo.Generate(), 80)
			{
				Desconto = 40,
				Ativa = true
			};

			var ofertaInativa = new OfertaViagem(rota, fakerPeriodo.Generate(), 70)
			{
				Desconto = 40,
				Ativa = false
			};

			var ofertas = fakerOferta.Generate(200);
			ofertas.Add(ofertaEscolhida);
			ofertas.Add(ofertaInativa);

			GerenciadorDeOfertas gerenciador = new GerenciadorDeOfertas(ofertas);
			Func<OfertaViagem, bool> filtro = o => o.Rota.Destino.Equals("São Paulo");
			var precoEsperado = 40;

			// Act
			OfertaViagem resultado = gerenciador.RecuperaMaiorDesconto(filtro);

			// Assert
			Assert.NotNull(resultado);
			Assert.Equal(precoEsperado, resultado.Preco, 0.0001);
		}
	}
}
