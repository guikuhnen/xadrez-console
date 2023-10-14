using xadrez_console.Tabuleiro;
using xadrez_console.Xadrez;

namespace xadrez_console
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                // Tabuleiro de Xadrez
                PartidaXadrez partidaXadrez = new PartidaXadrez();

                while (!partidaXadrez.Terminada)
                {
                    try
                    {
                        Console.Clear();

                        Tela.ImprimirTabuleiro(partidaXadrez.TabuleiroJogo);

                        Console.WriteLine();
                        Console.WriteLine("Turno: " + partidaXadrez.Turno);
                        Console.WriteLine("Aguardando jogada: " + partidaXadrez.JogadorAtual);

                        Console.WriteLine();
                        Console.Write("Origem: ");
                        Posicao origem = Tela.LerPosicaoXadrez().ToPosicao();
                        partidaXadrez.ValidarPosicaoOrigem(origem);

                        Console.Clear();
                        // Limpa o tabuleiro para apresentar as posições possíveis de mover a peça antes de pedir o Destino
                        bool[,] posicoesPossiveis = partidaXadrez.TabuleiroJogo.ObterPeca(origem).MovimentosPossiveis();
                        Tela.ImprimirTabuleiro(partidaXadrez.TabuleiroJogo, posicoesPossiveis);

                        Console.WriteLine();
                        Console.Write("Destino: ");
                        Posicao destino = Tela.LerPosicaoXadrez().ToPosicao();
                        partidaXadrez.ValidarPosicaoDestino(origem, destino);

                        partidaXadrez.RealizaJogada(origem, destino);
                    }
                    catch (TabuleiroException tex)
                    {
                        Console.WriteLine(tex.Message);
                        Console.ReadLine();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}