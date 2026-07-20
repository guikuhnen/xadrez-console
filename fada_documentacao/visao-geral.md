# Documentação - xadrez-console

## 1. O que é este sistema

Este sistema é um jogo de xadrez completo desenvolvido para execução em console (terminal). Implementa todas as regras tradicionais do xadrez, incluindo movimentações específicas de cada peça, detecção de xeque e xequemate, além de jogadas especiais como roque, en passant e promoção de peão.

O objetivo principal é permitir que dois jogadores disputem uma partida de xadrez através da interface de linha de comando, com alternância de turnos, validação de movimentos e exibição visual do tabuleiro. O sistema resolve o problema de proporcionar uma experiência funcional de xadrez sem necessidade de interface gráfica ou dependências externas.

A aplicação gerencia todo o estado da partida, incluindo controle de peças capturadas, histórico de movimentos e validação de regras complexas como a impossibilidade de colocar o próprio rei em xeque.

## 2. Arquitetura e módulos

**Program** — ponto de entrada da aplicação, gerencia o loop principal do jogo e tratamento de exceções

**Tela** — responsável pela renderização do tabuleiro no console e interação com o usuário via entrada/saída

**PartidaXadrez** — controla o fluxo completo da partida, turnos, validações e condições de vitória

**TabuleiroJogo** — representa a estrutura do tabuleiro 8x8 e gerencia posicionamento de peças

**Peca (abstrata)** — classe base para todas as peças, define movimentação e comportamento comum

**Posicao** — representa coordenadas no tabuleiro usando índices de matriz (linha, coluna)

**PosicaoXadrez** — converte notação algébrica do xadrez (ex: e4) para posições internas

**Peças específicas** — Torre, Cavalo, Bispo, Dama, Rei e Peao implementam suas regras de movimentação

**Cor** — enumeração que define as cores das peças (Branca/Preta)

**TabuleiroException** — exceção customizada para erros relacionados ao jogo

## 3. Tecnologias

| Tecnologia | Finalidade |
|------------|-----------|
| C# | Linguagem de programação principal |
| .NET | Framework de execução e biblioteca padrão |
| Console API | Interface de entrada/saída para terminal |
| HashSet | Estrutura para gerenciar peças em jogo e capturadas |
| Orientação a Objetos | Arquitetura baseada em classes, herança e polimorfismo |

## 4. Como executar

```bash
# Abrir o arquivo de solução
xadrez-console.sln

# Executar no Visual Studio
Ctrl + F5

# Ou via linha de comando
dotnet run --project xadrez-console
```

**Entrada durante o jogo:**
- Origem: Digite a posição no formato `e2`
- Destino: Digite a posição no formato `e4`

Sem configurações ou bibliotecas extras necessárias.