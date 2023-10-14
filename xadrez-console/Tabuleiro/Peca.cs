namespace xadrez_console.Tabuleiro
{
    internal class Peca
    {
        public Posicao Posicao { get; set; }
        public Cor Cor { get; protected set; }
        public int QteMovimentos { get; protected set; }
        public TabuleiroJogo Tabuleiro { get; protected set; }

        public Peca(Cor cor, TabuleiroJogo tabuleiro)
        {
            Posicao = null;
            Cor = cor;
            QteMovimentos = 0;
            Tabuleiro = tabuleiro;
        }

        public void IncrementarQteMovimentos()
        {
            QteMovimentos++;
        }
    }
}
