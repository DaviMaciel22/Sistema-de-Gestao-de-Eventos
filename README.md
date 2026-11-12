# Sistema de Gestão de Eventos

Aplicação desktop em C# e WPF para quem quer participar de eventos: encontrar conferências e workshops, garantir o ingresso, acompanhar os ingressos e manter o próprio perfil.

Trabalho do 2º bimestre da disciplina **Programação Orientada a Objetos II**, curso de Engenharia de Software da UVV (2026).

> **Status:** em desenvolvimento. Entrega e apresentação em 18/11/2026.

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
| **Meus ingressos** | Vê a lista dos próprios ingressos, filtra por status (confirmado, cancelado, utilizado), consulta os detalhes do evento e das palestras e cancela um ingresso. |
| **Perfil** | Edita nome e e-mail, altera a senha e exclui a conta. |

## Tecnologias

- C# com .NET 10 (`net10.0-windows`)
- WPF para a interface gráfica
- Entity Framework Core com Code First
- SQL Server LocalDB como banco relacional

## Arquitetura (MVC)

O projeto segue o padrão MVC de forma explícita. Os arquivos de code-behind das telas só repassam os eventos para o controller, e nenhuma regra de negócio fica na View.

```
WPFGestaoDeEventos/
├── Model/         Entidades do domínio (Evento, Inscricao, Participante, Organizador...)
├── View/          MainWindow (barra de navegação) e as telas Home, Meus ingressos e Perfil
├── Controller/    Recebem as ações das telas e aplicam as regras (a criar)
├── Data/          AppDbContext e Migrations do EF Core (a criar)
└── Repository/    Acesso ao banco, incluindo a consulta escrita em SQL (a criar)
```

## Modelo de dados

O banco cobre os relacionamentos pedidos no trabalho:

| Relacionamento | Onde aparece |
|---|---|
| Herança | `Evento` → `Conferencia` e `Workshop`; `Organizador` → `Empresa` e `PessoaFisica` |
| 1xN | Um `Organizador` cria vários `Evento` |
| NxN | `Participante` e `Evento`, ligados por `Inscricao` |
| 1x1 | `Inscricao` e `Certificado` |
| Composição | `Evento` e `Palestra` (as palestras são excluídas junto com o evento) |
| Agregação | `Evento` e `Local` (o local continua existindo sem o evento) |
| Funcionalidade via SQL | Consulta escrita à mão no repositório, além das consultas do EF (em definição) |

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
