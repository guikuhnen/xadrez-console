using xadrez_console.Tabuleiro;

namespace xadrez_console.Xadrez
{
    internal class PartidaXadrez
    {
        public TabuleiroJogo TabuleiroJogo { get; private set; }
        private int Turno;
        private Cor JogadorAtual;
        public bool Terminada {  get; private set; }

        public PartidaXadrez()
        {
            TabuleiroJogo = new TabuleiroJogo(8, 8);
            Turno = 1;
            JogadorAtual = Cor.Branca;
            Terminada = false;
            ColocarPecas();
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

        public void ExecutaMovimento(Posicao origem, Posicao destino)
        {
            Peca peca = TabuleiroJogo.RetirarPeca(origem);

            peca.IncrementarQteMovimentos();

            Peca pecaCapturada = TabuleiroJogo.RetirarPeca(destino);

            TabuleiroJogo.ColocarPeca(peca, destino);
        }
    }
}
