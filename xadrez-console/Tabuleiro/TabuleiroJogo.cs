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
            Pecas = new Peca[linhas, colunas];
        }

        public Peca ObterPeca(int linhas, int colunas)
        {
            return Pecas[linhas, colunas];
        }

        public Peca ObterPeca(Posicao posicao)
        {
            return Pecas[posicao.Linha, posicao.Coluna];
        }

        public void ColocarPeca(Peca peca, Posicao posicao)
        {
            if (ExistePeca(posicao))
                throw new TabuleiroException("Já existe uma peça nessa posição!");

            Pecas[posicao.Linha, posicao.Coluna] = peca;
            peca.Posicao = posicao;
        }

        public bool ExistePeca(Posicao posicao)
        {
            ValidarPosicao(posicao);

            return ObterPeca(posicao) != null;
        }

        public bool PosicaoValida(Posicao posicao)
        {
            if (posicao.Linha < 0 || posicao.Linha >= Linhas
                || posicao.Coluna < 0 || posicao.Coluna >= Colunas)
                return false;

            return true;
        }

        public void ValidarPosicao(Posicao posicao)
        {
            if (!PosicaoValida(posicao))
                throw new TabuleiroException("Posição inválida!");
        }
    }
}
