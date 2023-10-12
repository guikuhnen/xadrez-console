namespace xadrez_console.Tabuleiro
{
    internal class Peca
    {
        public Posicao Posicao { get; private set; }
        public Cor Cor { get; private set; }
        public int QteMovimentos { get; private set; }
        public TabuleiroJogo Tabuleiro { get; private set; }

        public Peca(Posicao posicao, Cor cor, TabuleiroJogo tabuleiro)
        {
            Posicao = posicao;
            Cor = cor;
            QteMovimentos = 0;
            Tabuleiro = tabuleiro;
        }
    }
}
