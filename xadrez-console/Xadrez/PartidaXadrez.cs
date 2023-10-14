using xadrez_console.Tabuleiro;

namespace xadrez_console.Xadrez
{
    internal class PartidaXadrez
    {
        public TabuleiroJogo TabuleiroJogo { get; private set; }
        public int Turno { get; private set; }
        public Cor JogadorAtual { get; private set; }
        public bool Terminada {  get; private set; }

        public PartidaXadrez()
        {
            TabuleiroJogo = new TabuleiroJogo(8, 8);
            Turno = 1;
            JogadorAtual = Cor.Branca;
            Terminada = false;
            ColocarPecas();
        }

        public void ExecutaMovimento(Posicao origem, Posicao destino)
        {
            Peca peca = TabuleiroJogo.RetirarPeca(origem);

            peca.IncrementarQteMovimentos();

            Peca pecaCapturada = TabuleiroJogo.RetirarPeca(destino);

            TabuleiroJogo.ColocarPeca(peca, destino);
        }

        public void RealizaJogada(Posicao origem, Posicao destino)
        {
            ExecutaMovimento(origem, destino);

            Turno++;

            MudaJogador();

        }

        public void ValidarPosicaoOrigem(Posicao posicao)
        {
            Peca pecaAtual = TabuleiroJogo.ObterPeca(posicao);

            if (pecaAtual == null)
                throw new TabuleiroException("Não existe peça na posição de origem escolhida!");

            if (JogadorAtual != pecaAtual.Cor)
                throw new TabuleiroException("A peça de origem escolhida não é sua!");

            if (!pecaAtual.ExisteMovimentosPossiveis())
                throw new TabuleiroException("Não há movimentos possíveis para a peça de origem escolhida!");
        }
        public void ValidarPosicaoDestino(Posicao origem, Posicao destino)
        {
            Peca pecaAtual = TabuleiroJogo.ObterPeca(origem);

            if (!pecaAtual.PodeMoverPara(destino))
                throw new TabuleiroException("Posição de destino inválida!");
        }

        private void MudaJogador()
        {
            if (JogadorAtual == Cor.Branca)
                JogadorAtual = Cor.Preta;
            else
                JogadorAtual = Cor.Branca;
        }

        private void ColocarPecas()
        {
            TabuleiroJogo.ColocarPeca(new Torre(Cor.Branca, TabuleiroJogo), new PosicaoXadrez('c', 1).ToPosicao());
            TabuleiroJogo.ColocarPeca(new Torre(Cor.Branca, TabuleiroJogo), new PosicaoXadrez('c', 2).ToPosicao());
            TabuleiroJogo.ColocarPeca(new Torre(Cor.Branca, TabuleiroJogo), new PosicaoXadrez('d', 2).ToPosicao());
            TabuleiroJogo.ColocarPeca(new Torre(Cor.Branca, TabuleiroJogo), new PosicaoXadrez('e', 2).ToPosicao());
            TabuleiroJogo.ColocarPeca(new Torre(Cor.Branca, TabuleiroJogo), new PosicaoXadrez('e', 1).ToPosicao());
            TabuleiroJogo.ColocarPeca(new Rei(Cor.Branca, TabuleiroJogo), new PosicaoXadrez('d', 1).ToPosicao());

            TabuleiroJogo.ColocarPeca(new Torre(Cor.Preta, TabuleiroJogo), new PosicaoXadrez('c', 7).ToPosicao());
            TabuleiroJogo.ColocarPeca(new Torre(Cor.Preta, TabuleiroJogo), new PosicaoXadrez('c', 8).ToPosicao());
            TabuleiroJogo.ColocarPeca(new Torre(Cor.Preta, TabuleiroJogo), new PosicaoXadrez('d', 7).ToPosicao());
            TabuleiroJogo.ColocarPeca(new Torre(Cor.Preta, TabuleiroJogo), new PosicaoXadrez('e', 7).ToPosicao());
            TabuleiroJogo.ColocarPeca(new Torre(Cor.Preta, TabuleiroJogo), new PosicaoXadrez('e', 8).ToPosicao());
            TabuleiroJogo.ColocarPeca(new Rei(Cor.Preta, TabuleiroJogo), new PosicaoXadrez('d', 8).ToPosicao());
        }
    }
}
