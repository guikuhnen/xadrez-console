namespace xadrez_console.Tabuleiro
{
    internal class Posicao
    {
        public int Linha { get; private set; }
        public int Coluna { get; private set; }

        public Posicao(int linha, int coluna) 
        {
            Linha = linha;
            Coluna = coluna;
        }
    }
}
