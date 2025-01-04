using BibliotecaUtad.Models;
using Humanizer.Inflections;
using Microsoft.AspNetCore.Routing;
using Microsoft.CodeAnalysis.Elfie.Diagnostics;
using Microsoft.CodeAnalysis.Elfie.Model.Structures;
using Microsoft.CodeAnalysis.Elfie.Serialization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TextTemplating;
using System;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text.RegularExpressions;

namespace BibliotecaUtad.Data
{
    public static class DbInitializer
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new BibliotecaUtadContext(
                serviceProvider.GetRequiredService<DbContextOptions<BibliotecaUtadContext>>()))
            {
                if (context.Librabry.Any())
                {
                    return;   // A base de dados já tem dados
                }

                var libraryInfo = new Librabry[]
                {
                    new Librabry { Name = "Biblioteca da UTAD",
                                   LocAddress = " Quinta de Prados",
                                   LocPostalCode = "5000-801",
                                   LocCity = "Vila Real",
                                   LocCountry = "Portugal",
                                   Email = "biblioteca@utad.pt",
                                   Phone = "+351 259 350 000",
                                   OpeningHours = "Segunda a Sexta: 9h00 - 19h00" } 
                };

                context.Librabry.AddRange(libraryInfo);
                context.SaveChanges();

                // Verifica se a tabela de gêneros já tem dados
                if (context.Gender.Any())
                {
                    return;   // A base de dados já tem dados
                }

                var genders = new Gender[]
                {
                    new Gender { GenderName = "Arte" },
                    new Gender { GenderName = "Banda Desenhada" },
                    new Gender { GenderName = "Ciências Exatas e Naturais" },
                    new Gender { GenderName = "Ciências Sociais e Humanas" },
                    new Gender { GenderName = "Desenvolvimento Pessoal e Espiritual" },
                    new Gender { GenderName = "Desporto e Lazer" },
                    new Gender { GenderName = "Dicionários e Enciclopédias" },
                    new Gender { GenderName = "Direito" },
                    new Gender { GenderName = "Economia, Finanças e Contabilidade" },
                    new Gender { GenderName = "Engenharia" },
                    new Gender { GenderName = "Ensino e Educação" },
                    new Gender { GenderName = "Gastronomia e Vinhos" },
                    new Gender { GenderName = "Gestão" },
                    new Gender { GenderName = "Guias Turísticos, Mapas e Atlas" },
                    new Gender { GenderName = "História" },
                    new Gender { GenderName = "Infantis e Juvenis" },
                    new Gender { GenderName = "Informática" },
                    new Gender { GenderName = "Literatura" },
                    new Gender { GenderName = "Medicina" },
                    new Gender { GenderName = "Plano Nacional de Leitura" },
                    new Gender { GenderName = "Política" },
                    new Gender { GenderName = "Religião e Moral" },
                    new Gender { GenderName = "Saúde e Bem-Estar" },
                    new Gender { GenderName = "Vida Prática" },
                    new Gender { GenderName = "Outros" }
                };

                context.Gender.AddRange(genders);
                context.SaveChanges();

                var subgenders = new Subgender[]
                {
                    // Arte
                    new Subgender { SubGenderName = "Arquitetura", GenderId = 1 },
                    new Subgender { SubGenderName = "Artes de Palco", GenderId = 1 },
                    new Subgender { SubGenderName = "Artes em Geral", GenderId = 1 },
                    new Subgender { SubGenderName = "Cerâmica", GenderId = 1 },
                    new Subgender { SubGenderName = "Cinema", GenderId = 1 },
                    new Subgender { SubGenderName = "Design e Ilustração", GenderId = 1 },
                    new Subgender { SubGenderName = "Escultura", GenderId = 1 },
                    new Subgender { SubGenderName = "Estética", GenderId = 1 },
                    new Subgender { SubGenderName = "Estilos e Influências", GenderId = 1 },
                    new Subgender { SubGenderName = "Fotografia", GenderId = 1 },
                    new Subgender { SubGenderName = "História da Arte", GenderId = 1 },
                    new Subgender { SubGenderName = "Mobiliário e Decoração", GenderId = 1 },
                    new Subgender { SubGenderName = "Moda", GenderId = 1 },
                    new Subgender { SubGenderName = "Música", GenderId = 1 },
                    new Subgender { SubGenderName = "Ourivesaria e Joalharia", GenderId = 1 },
                    new Subgender { SubGenderName = "Pintura", GenderId = 1 },
                    new Subgender { SubGenderName = "Revistas de Arte", GenderId = 1 },
                    new Subgender { SubGenderName = "Outros", GenderId = 1 },

                    // Banda Desenhada
                    new Subgender { SubGenderName = "Aventura", GenderId = 2 },
                    new Subgender { SubGenderName = "BD Erótica", GenderId = 2 },
                    new Subgender { SubGenderName = "Fantasia Heroica", GenderId = 2 },
                    new Subgender { SubGenderName = "Ficção Científica", GenderId = 2 },
                    new Subgender { SubGenderName = "Histórica", GenderId = 2 },
                    new Subgender { SubGenderName = "Humor", GenderId = 2 },
                    new Subgender { SubGenderName = "Infantil", GenderId = 2 },
                    new Subgender { SubGenderName = "Manga", GenderId = 2 },
                    new Subgender { SubGenderName = "Policial", GenderId = 2 },
                    new Subgender { SubGenderName = "Novela Gráfica", GenderId = 2 },
                    new Subgender { SubGenderName = "Super Heróis", GenderId = 2 },
                    new Subgender { SubGenderName = "Outros", GenderId = 2 },

                    // Ciências Exatas e Naturais
                    new Subgender { SubGenderName = "Agropecuária", GenderId = 3 },
                    new Subgender { SubGenderName = "Astronomia", GenderId = 3 },
                    new Subgender { SubGenderName = "Biologia", GenderId = 3 },
                    new Subgender { SubGenderName = "Bioquímica", GenderId = 3 },
                    new Subgender { SubGenderName = "Botânica", GenderId = 3 },
                    new Subgender { SubGenderName = "Ecologia", GenderId = 3 },
                    new Subgender { SubGenderName = "Física", GenderId = 3 },
                    new Subgender { SubGenderName = "Geologia", GenderId = 3 },
                    new Subgender { SubGenderName = "Matemática", GenderId = 3 },
                    new Subgender { SubGenderName = "Meteorologia", GenderId = 3 },
                    new Subgender { SubGenderName = "Paleontologia", GenderId = 3 },
                    new Subgender { SubGenderName = "Química", GenderId = 3 },
                    new Subgender { SubGenderName = "Zoologia", GenderId = 3 },
                    new Subgender { SubGenderName = "Outros", GenderId = 3 },

                    // Ciências Sociais e Humanas
                    new Subgender { SubGenderName = "Antropologia", GenderId = 4 },
                    new Subgender { SubGenderName = "Comunicação e Jornalismo", GenderId = 4 },
                    new Subgender { SubGenderName = "Estatísticas e Demografia", GenderId = 4 },
                    new Subgender { SubGenderName = "Filosofia", GenderId = 4 },
                    new Subgender { SubGenderName = "Geografia", GenderId = 4 },
                    new Subgender { SubGenderName = "História e Metodologia Científica", GenderId = 4 },
                    new Subgender { SubGenderName = "Psicanálise", GenderId = 4 },
                    new Subgender { SubGenderName = "Psicologia", GenderId = 4 },
                    new Subgender { SubGenderName = "Serviço Social", GenderId = 4 },
                    new Subgender { SubGenderName = "Sociologia", GenderId = 4 },
                    new Subgender { SubGenderName = "Outros", GenderId = 4 },

                    // Desenvolvimento Pessoal e Espiritual
                    new Subgender { SubGenderName = "Artes Divinatórias", GenderId = 5 },
                    new Subgender { SubGenderName = "Astrologia", GenderId = 5 },
                    new Subgender { SubGenderName = "Autoajuda", GenderId = 5 },
                    new Subgender { SubGenderName = "Esoterismo", GenderId = 5 },
                    new Subgender { SubGenderName = "Espiritualidades", GenderId = 5 },
                    new Subgender { SubGenderName = "Feng Shui e Reiki", GenderId = 5 },
                    new Subgender { SubGenderName = "Meditação e Ioga", GenderId = 5 },
                    new Subgender { SubGenderName = "Ocultismo", GenderId = 5 },
                    new Subgender { SubGenderName = "Parapsicologia", GenderId = 5 },
                    new Subgender { SubGenderName = "Outros", GenderId = 5 },

                    // Desporto e Lazer
                    new Subgender { SubGenderName = "Andebol", GenderId = 6 },
                    new Subgender { SubGenderName = "Atletismo", GenderId = 6 },
                    new Subgender { SubGenderName = "Artes Marciais e Lutas", GenderId = 6 },
                    new Subgender { SubGenderName = "Basquetebol", GenderId = 6 },
                    new Subgender { SubGenderName = "Caça", GenderId = 6 },
                    new Subgender { SubGenderName = "Ciclismo", GenderId = 6 },
                    new Subgender { SubGenderName = "Ciência Desportiva", GenderId = 6 },
                    new Subgender { SubGenderName = "Defesa Pessoal", GenderId = 6 },
                    new Subgender { SubGenderName = "Desporto e Saúde", GenderId = 6 },
                    new Subgender { SubGenderName = "Desporto Infantil", GenderId = 6 },
                    new Subgender { SubGenderName = "Desportos Aquáticos", GenderId = 6 },
                    new Subgender { SubGenderName = "Desportos com Raquete", GenderId = 6 },
                    new Subgender { SubGenderName = "Desportos de Inverno", GenderId = 6 },
                    new Subgender { SubGenderName = "Desportos Motorizados", GenderId = 6 },
                    new Subgender { SubGenderName = "Equitação", GenderId = 6 },
                    new Subgender { SubGenderName = "Futebol", GenderId = 6 },
                    new Subgender { SubGenderName = "Ginástica", GenderId = 6 },
                    new Subgender { SubGenderName = "Golfe", GenderId = 6 },
                    new Subgender { SubGenderName = "História do Desporto", GenderId = 6 },
                    new Subgender { SubGenderName = "Jogos de Mesa", GenderId = 6 },
                    new Subgender { SubGenderName = "Jogos e Passatempos", GenderId = 6 },
                    new Subgender { SubGenderName = "Magia", GenderId = 6 },
                    new Subgender { SubGenderName = "Modelismo", GenderId = 6 },
                    new Subgender { SubGenderName = "Musculação e Fitness", GenderId = 6 },
                    new Subgender { SubGenderName = "Olímpicos", GenderId = 6 },
                    new Subgender { SubGenderName = "Pesca", GenderId = 6 },
                    new Subgender { SubGenderName = "Voleibol", GenderId = 6 },
                    new Subgender { SubGenderName = "Outros", GenderId = 6 },

                    // Dicionários e Enciclopédias
                    new Subgender { SubGenderName = "Alemão", GenderId = 7 },
                    new Subgender { SubGenderName = "Enciclopédias", GenderId = 7 },
                    new Subgender { SubGenderName = "Espanhol", GenderId = 7 },
                    new Subgender { SubGenderName = "Francês", GenderId = 7 },
                    new Subgender { SubGenderName = "Gramáticas", GenderId = 7 },
                    new Subgender { SubGenderName = "Inglês", GenderId = 7 },
                    new Subgender { SubGenderName = "Língua Portuguesa", GenderId = 7 },
                    new Subgender { SubGenderName = "Prontuários", GenderId = 7 },
                    new Subgender { SubGenderName = "Sinônimos", GenderId = 7 },
                    new Subgender { SubGenderName = "Técnicos", GenderId = 7 },
                    new Subgender { SubGenderName = "Verbos", GenderId = 7 },
                    new Subgender { SubGenderName = "Vocabulários", GenderId = 7 },
                    new Subgender { SubGenderName = "Outras Línguas", GenderId = 7 },

                    // Direito
                    new Subgender { SubGenderName = "Direito Administrativo", GenderId = 8 },
                    new Subgender { SubGenderName = "Direito Civil", GenderId = 8 },
                    new Subgender { SubGenderName = "Direito Comercial", GenderId = 8 },
                    new Subgender { SubGenderName = "Direito Comunitário", GenderId = 8 },
                    new Subgender { SubGenderName = "Direito Constitucional", GenderId = 8 },
                    new Subgender { SubGenderName = "Direito da Família", GenderId = 8 },
                    new Subgender { SubGenderName = "Direito da Publicidade", GenderId = 8 },
                    new Subgender { SubGenderName = "Direito das Obrigações", GenderId = 8 },
                    new Subgender { SubGenderName = "Direito das Sucessões", GenderId = 8 },
                    new Subgender { SubGenderName = "Direito do Ambiente", GenderId = 8 },
                    new Subgender { SubGenderName = "Direito do Consumo", GenderId = 8 },
                    new Subgender { SubGenderName = "Direito do Trabalho", GenderId = 8 },
                    new Subgender { SubGenderName = "Direito Econômico", GenderId = 8 },
                    new Subgender { SubGenderName = "Direito Fiscal", GenderId = 8 },
                    new Subgender { SubGenderName = "Direito Informático", GenderId = 8 },
                    new Subgender { SubGenderName = "Direito Internacional", GenderId = 8 },
                    new Subgender { SubGenderName = "Direito Penal", GenderId = 8 },
                    new Subgender { SubGenderName = "Direitos Reais", GenderId = 8 },
                    new Subgender { SubGenderName = "Direito Geral", GenderId = 8 },
                    new Subgender { SubGenderName = "História e Estudos do Direito", GenderId = 8 },
                    new Subgender { SubGenderName = "Propriedade Intelectual", GenderId = 8 },
                    new Subgender { SubGenderName = "Registos e Notariado", GenderId = 8 },
                    new Subgender { SubGenderName = "Outros", GenderId = 8 },

                    //Economia, Finanças e Contabilidade
                    new Subgender { SubGenderName = "Assuntos Europeus", GenderId = 9 },
                    new Subgender { SubGenderName = "Contabilidade", GenderId = 9 },
                    new Subgender { SubGenderName = "Economia", GenderId = 9 },
                    new Subgender { SubGenderName = "Finanças", GenderId = 9 },
                    new Subgender { SubGenderName = "Outros", GenderId = 9 },

                    // Engenharia
                    new Subgender { SubGenderName = "Aeronáutica", GenderId = 10 },
                    new Subgender { SubGenderName = "Eletricidade e Energia", GenderId = 10 },
                    new Subgender { SubGenderName = "Engenharia Civil", GenderId = 10 },
                    new Subgender { SubGenderName = "Engenharia dos Transportes", GenderId = 10 },
                    new Subgender { SubGenderName = "Engenharia Eletrotécnica", GenderId = 10 },
                    new Subgender { SubGenderName = "Engenharia Geral", GenderId = 10 },
                    new Subgender { SubGenderName = "Engenharia Hidráulica", GenderId = 10 },
                    new Subgender { SubGenderName = "Engenharia Mecânica", GenderId = 10 },
                    new Subgender { SubGenderName = "Engenharia Militar e Naval", GenderId = 10 },
                    new Subgender { SubGenderName = "Engenharia Sanitária", GenderId = 10 },
                    new Subgender { SubGenderName = "Mecânica dos Sólidos", GenderId = 10 },
                    new Subgender { SubGenderName = "Outros", GenderId = 10 },

                    // Ensino e Educação
                    new Subgender { SubGenderName = "Animação Social e Educação de Adultos", GenderId = 11 },
                    new Subgender { SubGenderName = "Português Língua Não Materna (PLNM)", GenderId = 11 },
                    new Subgender { SubGenderName = "Ensino Especial", GenderId = 11 },
                    new Subgender { SubGenderName = "Ensino Técnico", GenderId = 11 },
                    new Subgender { SubGenderName = "Pais e Educadores", GenderId = 11 },
                    new Subgender { SubGenderName = "Políticas Educacionais e Administração Escolar", GenderId = 11 },
                    new Subgender { SubGenderName = "Psicologia Educacional", GenderId = 11 },
                    new Subgender { SubGenderName = "Sociologia Educacional", GenderId = 11 },
                    new Subgender { SubGenderName = "Teorias Educacionais e Currículo", GenderId = 11 },
                    new Subgender { SubGenderName = "Ensino de Português no Estrangeiro (EPE)", GenderId = 11 },
                    new Subgender { SubGenderName = "Português Língua Estrangeira (PLE)", GenderId = 11 },
                    new Subgender { SubGenderName = "Outros", GenderId = 11 },

                    // Gastronomia e Vinhos
                    new Subgender { SubGenderName = "Cozinha Vegetariana", GenderId = 12 },
                    new Subgender { SubGenderName = "Culinária", GenderId = 12 },
                    new Subgender { SubGenderName = "Enologia", GenderId = 12 },
                    new Subgender { SubGenderName = "Epicurismo e Charutos", GenderId = 12 },
                    new Subgender { SubGenderName = "Guias de Vinhos e Bebidas", GenderId = 12 },
                    new Subgender { SubGenderName = "Roteiros Gastronômicos", GenderId = 12 },
                    new Subgender { SubGenderName = "Outros", GenderId = 12 },

                    // Gestão
                    new Subgender { SubGenderName = "Comércio", GenderId = 13 },
                    new Subgender { SubGenderName = "Gestão e Organização", GenderId = 13 },
                    new Subgender { SubGenderName = "Hotelaria e Turismo", GenderId = 13 },
                    new Subgender { SubGenderName = "Marketing", GenderId = 13 },
                    new Subgender { SubGenderName = "Organização de Empresas", GenderId = 13 },
                    new Subgender { SubGenderName = "Publicidade", GenderId = 13 },
                    new Subgender { SubGenderName = "Recursos Humanos", GenderId = 13 },
                    new Subgender { SubGenderName = "Relações Públicas", GenderId = 13 },
                    new Subgender { SubGenderName = "Outros", GenderId = 13 },

                    // Guias Turísticos, Mapas e Atlas
                    new Subgender { SubGenderName = "África", GenderId = 14 },
                    new Subgender { SubGenderName = "América Central e Caraíbas", GenderId = 14 },
                    new Subgender { SubGenderName = "América do Norte", GenderId = 14 },
                    new Subgender { SubGenderName = "América do Sul", GenderId = 14 },
                    new Subgender { SubGenderName = "Ásia Pacífico e Oceania", GenderId = 14 },
                    new Subgender { SubGenderName = "Atlas", GenderId = 14 },
                    new Subgender { SubGenderName = "Cartas Militares", GenderId = 14 },
                    new Subgender { SubGenderName = "Espanha", GenderId = 14 },
                    new Subgender { SubGenderName = "Europa", GenderId = 14 },
                    new Subgender { SubGenderName = "Guias de Conservação", GenderId = 14 },
                    new Subgender { SubGenderName = "Mapas da Europa", GenderId = 14 },
                    new Subgender { SubGenderName = "Mapas de Estrada", GenderId = 14 },
                    new Subgender { SubGenderName = "Mapas de Portugal", GenderId = 14 },
                    new Subgender { SubGenderName = "Mapas Mundo", GenderId = 14 },
                    new Subgender { SubGenderName = "Orientação e Topografia", GenderId = 14 },
                    new Subgender { SubGenderName = "Patrimônio", GenderId = 14 },
                    new Subgender { SubGenderName = "Portugal", GenderId = 14 },
                    new Subgender { SubGenderName = "Outros Guias", GenderId = 14 },
                    new Subgender { SubGenderName = "Outros Mapas", GenderId = 14 },

                    // História  
                    new Subgender { SubGenderName = "Arqueologia", GenderId = 15 },
                    new Subgender { SubGenderName = "Etnografia", GenderId = 15 },
                    new Subgender { SubGenderName = "Genealogia e Heráldica", GenderId = 15 },
                    new Subgender { SubGenderName = "História Antiga", GenderId = 15 },
                    new Subgender { SubGenderName = "História de África", GenderId = 15 },
                    new Subgender { SubGenderName = "História da América", GenderId = 15 },
                    new Subgender { SubGenderName = "História da Ásia", GenderId = 15 },
                    new Subgender { SubGenderName = "História da Europa", GenderId = 15 },
                    new Subgender { SubGenderName = "História da Idade Média", GenderId = 15 },
                    new Subgender { SubGenderName = "História de Portugal", GenderId = 15 },
                    new Subgender { SubGenderName = "História de Timor", GenderId = 15 },
                    new Subgender { SubGenderName = "História do Brasil", GenderId = 15 },
                    new Subgender { SubGenderName = "História em Geral", GenderId = 15 },
                    new Subgender { SubGenderName = "História Militar", GenderId = 15 },
                    new Subgender { SubGenderName = "História Moderna e Contemporânea", GenderId = 15 },
                    new Subgender { SubGenderName = "Museografia e Museologia", GenderId = 15 },
                    new Subgender { SubGenderName = "Outros", GenderId = 15 },

                    // Infantis e Juvenis  
                    new Subgender { SubGenderName = "Contos, Fábulas e Narrativas", GenderId = 16 },
                    new Subgender { SubGenderName = "Literatura Juvenil", GenderId = 16 },
                    new Subgender { SubGenderName = "Livros de Atividades", GenderId = 16 },
                    new Subgender { SubGenderName = "Livros de Aventuras", GenderId = 16 },
                    new Subgender { SubGenderName = "Livros de Pintar", GenderId = 16 },
                    new Subgender { SubGenderName = "Livros de Referência", GenderId = 16 },
                    new Subgender { SubGenderName = "Livros Infantis de Ficção", GenderId = 16 },
                    new Subgender { SubGenderName = "Livros Ludodidáticos", GenderId = 16 },
                    new Subgender { SubGenderName = "Livros Puzzle", GenderId = 16 },
                    new Subgender { SubGenderName = "Poemas e Rimas", GenderId = 16 },
                    new Subgender { SubGenderName = "Outros", GenderId = 16 },

                    // Informática  
                    new Subgender { SubGenderName = "Bases de Dados", GenderId = 17 },
                    new Subgender { SubGenderName = "CAD", GenderId = 17 },
                    new Subgender { SubGenderName = "Edição de Imagem", GenderId = 17 },
                    new Subgender { SubGenderName = "Folhas de Cálculo", GenderId = 17 },
                    new Subgender { SubGenderName = "Hardware", GenderId = 17 },
                    new Subgender { SubGenderName = "Iniciação à Informática", GenderId = 17 },
                    new Subgender { SubGenderName = "Inteligência Artificial", GenderId = 17 },
                    new Subgender { SubGenderName = "Internet", GenderId = 17 },
                    new Subgender { SubGenderName = "Processamento de Texto", GenderId = 17 },
                    new Subgender { SubGenderName = "Programação", GenderId = 17 },
                    new Subgender { SubGenderName = "Segurança Informática", GenderId = 17 },
                    new Subgender { SubGenderName = "Sistemas Operativos e Redes", GenderId = 17 },
                    new Subgender { SubGenderName = "Outros", GenderId = 17 },

                    // Literatura  
                    new Subgender { SubGenderName = "Biografias", GenderId = 18 },
                    new Subgender { SubGenderName = "Contos", GenderId = 18 },
                    new Subgender { SubGenderName = "Crônicas", GenderId = 18 },
                    new Subgender { SubGenderName = "Ensaios", GenderId = 18 },
                    new Subgender { SubGenderName = "Epístolas e Cartas", GenderId = 18 },
                    new Subgender { SubGenderName = "Estória", GenderId = 18 },
                    new Subgender { SubGenderName = "Ficção Científica e Fantasia", GenderId = 18 },
                    new Subgender { SubGenderName = "História da Literatura", GenderId = 18 },
                    new Subgender { SubGenderName = "Humor", GenderId = 18 },
                    new Subgender { SubGenderName = "Jovem Adulto", GenderId = 18 },
                    new Subgender { SubGenderName = "Leituras Orientadas", GenderId = 18 },
                    new Subgender { SubGenderName = "Linguística e Filologia", GenderId = 18 },
                    new Subgender { SubGenderName = "Literatura Erótica", GenderId = 18 },
                    new Subgender { SubGenderName = "Literatura Fantástica", GenderId = 18 },
                    new Subgender { SubGenderName = "Literatura de Viagem", GenderId = 18 },
                    new Subgender { SubGenderName = "Memórias e Testemunhos", GenderId = 18 },
                    new Subgender { SubGenderName = "Monografias", GenderId = 18 },
                    new Subgender { SubGenderName = "Poesia", GenderId = 18 },
                    new Subgender { SubGenderName = "Policial e Thriller", GenderId = 18 },
                    new Subgender { SubGenderName = "Revistas Literárias", GenderId = 18 },
                    new Subgender { SubGenderName = "Romance", GenderId = 18 },
                    new Subgender { SubGenderName = "Teatro", GenderId = 18 },
                    new Subgender { SubGenderName = "Outros", GenderId = 18 },

                    // Medicina  
                    new Subgender { SubGenderName = "Anatomia", GenderId = 19 },
                    new Subgender { SubGenderName = "Cardiologia", GenderId = 19 },
                    new Subgender { SubGenderName = "Cirurgia Geral", GenderId = 19 },
                    new Subgender { SubGenderName = "Enfermagem", GenderId = 19 },
                    new Subgender { SubGenderName = "Estomatologia e Medicina Dentária", GenderId = 19 },
                    new Subgender { SubGenderName = "Farmacologia", GenderId = 19 },
                    new Subgender { SubGenderName = "Fisiologia", GenderId = 19 },
                    new Subgender { SubGenderName = "Geriatria", GenderId = 19 },
                    new Subgender { SubGenderName = "Ginecologia e Obstetrícia", GenderId = 19 },
                    new Subgender { SubGenderName = "Medicina Desportiva", GenderId = 19 },
                    new Subgender { SubGenderName = "Medicina Geral", GenderId = 19 },
                    new Subgender { SubGenderName = "Medicina Interna", GenderId = 19 },
                    new Subgender { SubGenderName = "Medicina Legal", GenderId = 19 },
                    new Subgender { SubGenderName = "Medicina Veterinária", GenderId = 19 },
                    new Subgender { SubGenderName = "Medicinas Alternativas", GenderId = 19 },
                    new Subgender { SubGenderName = "Neurologia", GenderId = 19 },
                    new Subgender { SubGenderName = "Nutrição", GenderId = 19 },
                    new Subgender { SubGenderName = "Oftalmologia", GenderId = 19 },
                    new Subgender { SubGenderName = "Oncologia", GenderId = 19 },
                    new Subgender { SubGenderName = "Ortopedia", GenderId = 19 },
                    new Subgender { SubGenderName = "Patologia", GenderId = 19 },
                    new Subgender { SubGenderName = "Pediatria", GenderId = 19 },
                    new Subgender { SubGenderName = "Psiquiatria", GenderId = 19 },
                    new Subgender { SubGenderName = "Socorrismo", GenderId = 19 },
                    new Subgender { SubGenderName = "Outros", GenderId = 19 },

                    // Plano Nacional de Leitura
                    new Subgender { SubGenderName = "0 - 2 Anos", GenderId = 20 },
                    new Subgender { SubGenderName = "3 - 5 Anos", GenderId = 20 },
                    new Subgender { SubGenderName = "6 - 8 Anos", GenderId = 20 },
                    new Subgender { SubGenderName = "9 - 11 Anos", GenderId = 20 },
                    new Subgender { SubGenderName = "12 - 14 Anos", GenderId = 20 },
                    new Subgender { SubGenderName = "15 - 18 Anos", GenderId = 20 },
                    new Subgender { SubGenderName = "Mais de 18 Anos", GenderId = 20 },

                    // Política
                    new Subgender { SubGenderName = "Administração Pública", GenderId = 21 },
                    new Subgender { SubGenderName = "Ciência Militar e Estratégica", GenderId = 21 },
                    new Subgender { SubGenderName = "Política em Geral", GenderId = 21 },
                    new Subgender { SubGenderName = "Política Europeia", GenderId = 21 },
                    new Subgender { SubGenderName = "Política Internacional", GenderId = 21 },
                    new Subgender { SubGenderName = "Outros", GenderId = 21 },

                    // Religião e Moral
                    new Subgender { SubGenderName = "Budismo e Xintoísmo", GenderId = 22 },
                    new Subgender { SubGenderName = "Catolicismo", GenderId = 22 },
                    new Subgender { SubGenderName = "Ciência e História das Religiões", GenderId = 22 },
                    new Subgender { SubGenderName = "Estudos Bíblicos", GenderId = 22 },
                    new Subgender { SubGenderName = "Estudos Teológicos", GenderId = 22 },
                    new Subgender { SubGenderName = "Islamismo e Maometismo", GenderId = 22 },
                    new Subgender { SubGenderName = "Mitologias", GenderId = 22 },
                    new Subgender { SubGenderName = "Moral e Ética", GenderId = 22 },
                    new Subgender { SubGenderName = "Religião Judaica", GenderId = 22 },
                    new Subgender { SubGenderName = "Outros", GenderId = 22 },

                    // Saúde e Bem-Estar
                    new Subgender { SubGenderName = "Dietas", GenderId = 23 },
                    new Subgender { SubGenderName = "Planejamento Familiar", GenderId = 23 },
                    new Subgender { SubGenderName = "Puericultura", GenderId = 23 },
                    new Subgender { SubGenderName = "Vida Saudável", GenderId = 23 },
                    new Subgender { SubGenderName = "Vida Sexual", GenderId = 23 },
                    new Subgender { SubGenderName = "Outros", GenderId = 23 },

                    // Vida Prática
                    new Subgender { SubGenderName = "Agendas e Diários", GenderId = 24 },
                    new Subgender { SubGenderName = "Animais de Estimação", GenderId = 24 },
                    new Subgender { SubGenderName = "Artesanato e Trabalhos Manuais", GenderId = 24 },
                    new Subgender { SubGenderName = "Bricolage", GenderId = 24 },
                    new Subgender { SubGenderName = "Calendários, Postais e Pôsteres", GenderId = 24 },
                    new Subgender { SubGenderName = "Colecionismo", GenderId = 24 },
                    new Subgender { SubGenderName = "Cosmética e Beleza", GenderId = 24 },
                    new Subgender { SubGenderName = "Gestão Doméstica", GenderId = 24 },
                    new Subgender { SubGenderName = "Guias do Consumidor", GenderId = 24 },
                    new Subgender { SubGenderName = "Jardinagem e Floricultura", GenderId = 24 },
                    new Subgender { SubGenderName = "Moda e Vestuário", GenderId = 24 },
                    new Subgender { SubGenderName = "Vida Prática em Geral", GenderId = 24 },

                    // Outros
                    new Subgender { SubGenderName = "Outros", GenderId = 25 }
                    };

                context.Subgender.AddRange(subgenders);
                context.SaveChanges();
            }
        }
    }
}