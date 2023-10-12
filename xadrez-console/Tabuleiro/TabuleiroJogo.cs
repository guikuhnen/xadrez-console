namespace xadrez_console.Tabuleiro
{
    internal class TabuleiroJogo
    {
        public int Linhas { get; private set; }
        public int Colunas { get; private set; }
        private Peca[,] Pecas;

        public TabuleiroJogo(int linhas, int colunas)
        {
            Linhas = linhas;
            Colunas = colunas;
            Pecas = new Peca[linhas,colunas];
        }

        public Peca ObterPeca(int linhas, int colunas)
        {
            return Pecas[linhas,colunas];
        }
    }
}
