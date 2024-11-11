class CustomChessSolver
{
    // enum de tipos de pieza
    enum PieceType
    {
        Queen
    }

    //este struct representa la posicion de la pieza y su tipo
    struct Piece
    {
        public int Row;
        public int Column;
        public PieceType Type;

        public Piece(int row, int col, PieceType type)
        {
            Row = row;
            Column = col;
            Type = type;
        }
    }

    static void Main(string[] args)
    {
        int n = 64;

        int numQueens = 8;

        Piece[] pieces = new Piece[numQueens];
        if (SolvePuzzle(n, pieces, 0, numQueens))
        {
            PrintSolution(pieces, n);
        }
    }

    // * uso *backtracking* para solucionar el problema
    static bool SolvePuzzle(int boardSize, Piece[] pieces, int currentPiece, int numQueens)
    {

        if (currentPiece >= pieces.Length)
            return true;

        PieceType currentType = PieceType.Queen;

        //pruebo cada posicion posible en el tablero
        for (int row = 0; row < boardSize; row++)
        {
            for (int col = 0; col < boardSize; col++)
            {
                if (IsSafe(pieces, currentPiece, row, col, currentType, boardSize))
                {
                    //pongo la pieza
                    pieces[currentPiece] = new Piece(row, col, currentType);

                    //pongo el resto de las piezas de forma recursiva
                    if (SolvePuzzle(boardSize, pieces, currentPiece + 1, numQueens))
                        return true;
                }
            }
        }

        return false;
    }

    // * me fijo si es seguro poner la pieza en la posicion n
    static bool IsSafe(Piece[] pieces, int currentPiece, int row, int col, PieceType type, int boardSize)
    {
        // me fijo en todas las piezas de ajedrez puestas anteriormente
        for (int i = 0; i < currentPiece; i++)
        {
            int pieceRow = pieces[i].Row;
            int pieceCol = pieces[i].Column;
            PieceType pieceType = pieces[i].Type;

            // me fijo si la posicion actual choca con la posicion de la pieza en i
            if (IsUnderAttack(row, col, pieceRow, pieceCol, pieceType) ||
                IsUnderAttack(pieceRow, pieceCol, row, col, type))
            {
                return false;
            }
        }
        return true;
    }

    // me fijo si la posicion (targetRow, targetCol) esta bajo ataque de una pieza en posicion
    // (pieceRow, pieceCol)
    static bool IsUnderAttack(int targetRow, int targetCol, int pieceRow, int pieceCol, PieceType pieceType)
    {
        switch (pieceType)
        {
            case PieceType.Queen:
                // movimiento de peon
                if (targetRow == pieceRow || targetCol == pieceCol)
                    return true;

                //movimiento de alfil
                if (Math.Abs(targetRow - pieceRow) == Math.Abs(targetCol - pieceCol))
                    return true;

                break;
        }
        return false;
    }

    static void PrintSolution(Piece[] pieces, int boardSize)
    {
        char[,] board = new char[boardSize, boardSize];

        // tablero vacio
        for (int i = 0; i < boardSize; i++)
            for (int j = 0; j < boardSize; j++)
                board[i, j] = '.';

        // pongo fichas en el tablero
        foreach (var piece in pieces)
        {
            board[piece.Row, piece.Column] = piece.Type == PieceType.Queen ? 'Q' : 'C';
        }

        Console.WriteLine("\nSolution found:");
        // imprimo la posicion de las reinas
        Console.WriteLine("(row, column):");
        for (int i = 0; i < pieces.Length; i++)
        {
            Console.WriteLine($"{pieces[i].Type}: ({pieces[i].Row}, {pieces[i].Column})");
        }
    }
}