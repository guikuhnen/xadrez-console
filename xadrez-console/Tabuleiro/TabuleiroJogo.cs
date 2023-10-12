namespace xadrez_console.Tabuleiro
{
    internal class TabuleiroJogo
    {
        public int Linhas { get; private set; }
        public int Colunas { get; private set; }
        public Peca[,] Pecas { get; private set; }

        public TabuleiroJogo(int linhas, int colunas)
        {
            Linhas = linhas;
            Colunas = colunas;
            Pecas = new Peca[linhas,colunas];
        }
    }
}
