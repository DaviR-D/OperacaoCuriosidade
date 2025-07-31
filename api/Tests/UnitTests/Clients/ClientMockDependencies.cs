using Api.Modules.Clients.Application;
using Api.Modules.Clients.Domain;

namespace Api.Tests.UnitTests.Clients
{
    public class ClientMockDependencies
    {
        public List<Client> ClientsMock { get; set; } =
            [
                new(
                    Guid.NewGuid(),
                    name: "Fernando Lima",
                    email: "fernando.lima@example.com",
                    status: "Active",
                    pending: false,
                    date: DateTime.Now,
                    age: 40,
                    address: "Rua F, 101",
                    other: "Gerente de projetos com experiência em tecnologia.",
                    interests: "Tecnologia, Viagens",
                    feelings: "Satisfeito",
                    values: "Inovação, Colaboração"
                ),
                new(
                    Guid.NewGuid(),
                    name: "Gabriela Rocha",
                    email: "gabriela.rocha@example.com",
                    status: "Inactive",
                    pending: true,
                    date: DateTime.Now.AddDays(-15),
                    age: 29,
                    address: "Avenida G, 202",
                    other: "Designer gráfico apaixonada por arte digital.",
                    interests: "Arte, Fotografia",
                    feelings: "Criativa",
                    values: "Estética, Originalidade"
                ),
                new(
                    Guid.NewGuid(),
                    name: "Hugo Almeida",
                    email: "hugo.almeida@example.com",
                    status: "Active",
                    pending: false,
                    date: DateTime.Now.AddDays(-3),
                    age: 33,
                    address: "Praça H, 303",
                    other: "Engenheiro civil com projetos em andamento.",
                    interests: "Construção, Sustentabilidade",
                    feelings: "Empolgado",
                    values: "Qualidade, Segurança"
                ),
                new(
                    Guid.NewGuid(),
                    name: "Isabela Martins",
                    email: "isabela.martins@example.com",
                    status: "Pending",
                    pending: true,
                    date: DateTime.Now.AddDays(-7),
                    age: 26,
                    address: "Rua I, 404",
                    other: "Professora de matemática com foco em educação inclusiva.",
                    interests: "Educação, Voluntariado",
                    feelings: "Esperançosa",
                    values: "Inclusão, Conhecimento"
                ),
                new(
                    Guid.NewGuid(),
                    name: "João Pedro",
                    email: "joao.pedro@example.com",
                    status: "Active",
                    pending: false,
                    date: DateTime.Now.AddDays(-1),
                    age: 31,
                    address: "Avenida J, 505",
                    other: "Músico e compositor com várias apresentações.",
                    interests: "Música, Composição",
                    feelings: "Inspirado",
                    values: "Criatividade, Expressão"
                ),
                new(
                    Guid.NewGuid(),
                    name: "Karina Souza",
                    email: "karina.souza@example.com",
                    status: "Inactive",
                    pending: true,
                    date: DateTime.Now.AddDays(-12),
                    age: 27,
                    address: "Rua K, 606",
                    other: "Nutricionista com foco em saúde e bem-estar.",
                    interests: "Saúde, Culinária",
                    feelings: "Motivada",
                    values: "Saúde, Equilíbrio"
                ),
                new(
                    Guid.NewGuid(),
                    name: "Leonardo Ferreira",
                    email: "leonardo.ferreira@example.com",
                    status: "Active",
                    pending: false,
                    date: DateTime.Now.AddDays(-4),
                    age: 38,
                    address: "Praça L, 707",
                    other: "Analista de sistemas com experiência em desenvolvimento ágil.",
                    interests: "Tecnologia, Programação",
                    feelings: "Focado",
                    values: "Eficiência, Inovação"
                ),
                new(
                    Guid.NewGuid(),
                    name: "Mariana Costa",
                    email: "mariana.costa@example.com",
                    status: "Pending",
                    pending: true,
                    date: DateTime.Now.AddDays(-8),
                    age: 24,
                    address: "Avenida M, 808",
                    other: "Estudante de psicologia com interesse em terapia.",
                    interests: "Psicologia, Leitura",
                    feelings: "Curiosa",
                    values: "Empatia, Compreensão"
                ),
                new(
                    Guid.NewGuid(),
                    name: "Nicolas Silva",
                    email: "nicolas.silva@example.com",
                    status: "Active",
                    pending: false,
                    date: DateTime.Now.AddDays(-2),
                    age: 29,
                    address: "Rua N, 909",
                    other: "Fotógrafo freelance com portfólio diversificado.",
                    interests: "Fotografia, Viagens",
                    feelings: "Aventureiro",
                    values: "Liberdade, Criatividade"
                ),
        ];
    }
}
