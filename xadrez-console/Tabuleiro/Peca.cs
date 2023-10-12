namespace xadrez_console.Tabuleiro
{
    internal class Peca
    {
        public Posicao Posicao { get; private set; }
        public Cor Cor { get; protected set; }
        public int QteMovimentos { get; protected set; }
        public TabuleiroJogo Tabuleiro { get; protected set; }

        public Peca(Posicao posicao, Cor cor, TabuleiroJogo tabuleiro)
        {
            Posicao = posicao;
            Cor = cor;
            QteMovimentos = 0;
            Tabuleiro = tabuleiro;
        }
    }
}
