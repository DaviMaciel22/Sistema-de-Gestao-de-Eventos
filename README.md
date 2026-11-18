# Sistema de Gestão de Eventos

Aplicação desktop em C# e WPF para quem quer participar de eventos: encontrar conferências e workshops, garantir o ingresso, acompanhar os ingressos e manter o próprio perfil.

Trabalho do 2º bimestre da disciplina **Programação Orientada a Objetos II**, curso de Engenharia de Software da UVV (2026).

> **Status:** versão final, entregue e apresentada em 18/11/2026.

## Integrantes

| Nome | GitHub / Trello |
|---|---|
| Alan Lopes | alanlopes44 |
| Davi Maciel | DaviMaciel22 |
| Ian Passos Peternucci | ianpeternucci |
| Miguel Lyra | miguellyra |

## Funcionalidades

O sistema tem três telas, definidas com o professor:

| Tela | O que o usuário faz |
|---|---|
| **Home** | Vê os eventos disponíveis, filtra por nome, data, cidade e tipo (conferência ou workshop) e garante o ingresso. Evento sem vagas aparece como esgotado. |
| **Meus ingressos** | Vê a lista dos próprios ingressos, filtra por nome e por status (confirmado, cancelado, utilizado), consulta os detalhes do evento e das palestras, cancela um ingresso (a vaga volta para o evento) e vê o certificado de um ingresso utilizado. |
| **Perfil** | Edita nome e e-mail (o nome na barra de cima muda na hora), altera a senha (mínimo de 6 caracteres) e exclui a conta (recusado se houver ingresso confirmado). |

O sistema não tem tela de login: ele usa o participante de teste criado pelo seed (Id 1, "Ana Souza", senha `123456`).

## Tecnologias

- C# com .NET 10 (`net10.0-windows`)
- WPF para a interface gráfica
- Entity Framework Core com Code First
- SQL Server LocalDB como banco relacional

## Arquitetura (MVC)

O projeto segue o padrão MVC de forma explícita. Os arquivos de code-behind das telas só repassam os eventos para o controller, e nenhuma regra de negócio fica na View.

```
WPFGestaoDeEventos/
├── Model/         Entidades do domínio (Evento, Inscricao, Participante, Organizador...),
│                  constantes de status e o WpfGestaoDeEventosDbContext (EF Core)
├── View/          MainWindow (barra de navegação) e as telas TelaInicial, MeusIngressos e Perfil
├── Controller/    EventoController, InscricaoController e ParticipanteController
├── Repository/    Interfaces (IEventoRepository, IInscricaoRepository, IParticipanteRepository)
│                  e as classes que acessam o banco, incluindo a busca de eventos escrita em SQL
└── Migrations/    Migrations do EF Core (Code First), com os dados iniciais (seed)
```

O caminho de uma ação é sempre o mesmo: a **View** lê os campos e chama o **Controller**, o Controller valida as regras e usa o **Repository**, e o Repository é o único que conversa com o banco.

| Tela | Controller | Ações |
|---|---|---|
| Home (`TelaInicial`) | `EventoController`, `InscricaoController`, `ParticipanteController` | `ListarDisponiveis()`, `ListarCidades()`, `Buscar(nome, data, cidade, tipo)`, `ObterAtual()`, `Inscrever(participanteId, eventoId)` |
| Meus ingressos (`MeusIngressos`) | `InscricaoController`, `ParticipanteController` | `ObterAtual()`, `ListarPorParticipante(participanteId, nome, status)`, `ListarPalestras(eventoId)`, `Cancelar(inscricaoId)`, `ObterCertificado(inscricaoId)` |
| Perfil (`Perfil`) | `ParticipanteController` | `ObterAtual()`, `AtualizarDados(nome, email)`, `AlterarSenha(atual, nova, confirmacao)`, `ExcluirConta()` |

## Onde aplicamos SOLID e Clean Code

Os pontos também estão marcados no código com comentários `// SOLID (...)` e `// Clean Code`.

| Princípio | Onde | Como |
|---|---|---|
| SOLID — SRP (responsabilidade única) | `Controller/*Controller.cs`, `Repository/*Repository.cs` | Cada controller cuida das regras de um assunto (eventos, inscrições, participante) e cada repositório só acessa o banco para esse assunto. |
| SOLID — DIP (inversão de dependência) | `Repository/IEventoRepository.cs`, `IInscricaoRepository.cs`, `IParticipanteRepository.cs` e os construtores dos controllers | O controller depende da interface, não da classe que acessa o banco. Dá para trocar o repositório sem mexer na regra. |
| Clean Code — sem texto repetido | `Model/StatusInscricao.cs`, `Model/StatusEvento.cs` | Os status ("Confirmado", "Cancelado", "Utilizado", "Disponível") ficam escritos em um lugar só. |
| Clean Code — nomes claros | Todo o projeto | Métodos dizem o que fazem (`Inscrever`, `Cancelar`, `AlterarSenha`, `ExcluirConta`) e as constantes têm nome (`TamanhoMinimoSenha`). |
| Clean Code — mensagens e validação | `Controller/*Controller.cs` | Toda regra quebrada gera uma exceção com mensagem em português, que a tela mostra ao usuário. |
| Clean Code — sem código morto | `Model/` | Métodos vazios e de teste foram removidos. |
| Segurança | `Model/Participante.cs`, `Repository/EventoRepository.cs` | A senha é guardada só como hash SHA-256; a busca em SQL usa parâmetros e nunca junta o texto digitado no comando. |

## Modelo de dados

O banco cobre os relacionamentos pedidos no trabalho:

| Relacionamento | Onde aparece |
|---|---|
| Herança | `Evento` → `Conferencia` e `Workshop`; `Organizador` → `Empresa` e `PessoaFisica` |
| 1xN | Um `Organizador` cria vários `Evento` |
| NxN | `Participante` e `Evento`, ligados por `Inscricao` |
| 1x1 | `Inscricao` e `Certificado` |
| Composição | `Conferencia` e `Palestra` (as palestras são excluídas junto com a conferência, regra `Cascade` no banco) |
| Agregação | `Evento` e `Local` (o local continua existindo sem o evento) |
| Funcionalidade via SQL | Busca de eventos da Home (nome, data, cidade e tipo), feita com uma consulta SQL parametrizada no repositório |

## Como executar

### Requisitos

- Windows
- Visual Studio 2026 com a carga de trabalho **Desenvolvimento para desktop com .NET**
- SQL Server LocalDB (vem com o Visual Studio ao marcar o componente de armazenamento de dados)

### Passos

1. Clone o repositório:
   ```bash
   git clone https://github.com/DaviMaciel22/Sistema-de-Gestao-de-Eventos.git
   ```
2. Abra `WPFGestaoDeEventos/WPFGestaoDeEventos.slnx` no Visual Studio.
3. Crie o banco a partir das migrations. No Console do Gerenciador de Pacotes:
   ```powershell
   Update-Database
   ```
4. Rode o projeto com `F5`.

## Como o grupo trabalha

- Cada integrante trabalha na própria branch e abre um pull request para a `main`. O merge só acontece depois de testar.
- Só o responsável pelo banco cria migrations, para evitar conflito entre elas.
- Todos usam a mesma versão do .NET e dos pacotes do EF Core.
- O planejamento e a divisão de tarefas ficam no [Trello](https://trello.com/b/BAZRaTny/poo-ii-2-bimestre).

## Uso de inteligência artificial

O trabalho usa IA até o nível IA-2. A IA auxiliou na geração inicial de código com revisão humana completa. As ferramentas, os prompts, os resultados e a comparação de um trecho feito com e sem IA estão documentados na entrega do trabalho.
