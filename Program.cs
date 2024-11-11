class CustomChessSolver
{
    // enum de tipos de pieza
    enum PieceType
    {
        Queen,
        CustomChessPiece
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

        int numPieces = 8;
        Console.Write("Enter the name of the custom chess piece: ");
        string customPieceName = Console.ReadLine();


        Console.Write("Choose piece type (1 for Queen, 2 for Custom Piece): ");
        int pieceChoice = int.Parse(Console.ReadLine());
        PieceType selectedType = pieceChoice == 1 ? PieceType.Queen : PieceType.CustomChessPiece;

        Piece[] pieces = new Piece[numPieces];
        if (SolvePuzzle(n, pieces, 0, numPieces, selectedType))
        {
            PrintSolution(pieces, n, customPieceName);
        }
        else
        {
            Console.WriteLine("No solution found!");
        }
    }

    // * uso *backtracking* para solucionar el problema

    static bool SolvePuzzle(int boardSize, Piece[] pieces, int currentPiece, int numPieces, PieceType selectedType)
    {
        if (currentPiece >= pieces.Length)
            return true;

        for (int row = 0; row < boardSize; row++)
        {
            for (int col = 0; col < boardSize; col++)
            {
                if (IsSafe(pieces, currentPiece, row, col, selectedType, boardSize))
                {
                    pieces[currentPiece] = new Piece(row, col, selectedType);

                    if (SolvePuzzle(boardSize, pieces, currentPiece + 1, numPieces, selectedType))
                        return true;
                }
            }
        }
        return false;
    }

    // * me fijo si es seguro poner la pieza en la posicion n
    static bool IsSafe(Piece[] pieces, int currentPiece, int row, int col, PieceType type, int boardSize)
    {
        for (int i = 0; i < currentPiece; i++)
        {
            if (IsUnderAttack(row, col, pieces[i].Row, pieces[i].Column, type) ||
                IsUnderAttack(pieces[i].Row, pieces[i].Column, row, col, type))
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
            case PieceType.CustomChessPiece:
                // movimiento de peon
                if (targetRow == pieceRow || targetCol == pieceCol)
                    return true;

                //movimiento de alfil
                if (Math.Abs(targetRow - pieceRow) == Math.Abs(targetCol - pieceCol))
                    return true;

                // movimiento en L de caballo
                if (pieceType == PieceType.CustomChessPiece && ((Math.Abs(targetRow - pieceRow) == 2 && Math.Abs(targetCol - pieceCol) == 1) || (Math.Abs(targetRow - pieceRow) == 1 && Math.Abs(targetCol - pieceCol) == 2)))
                    return true;
                break;
        }
        return false;
    }

    static void PrintSolution(Piece[] pieces, int boardSize, string customPieceName)
    {
        Console.WriteLine("\nSolution found:");

        //* diferencio el custom de la reina
        var queens = pieces.Where(p => p.Type == PieceType.Queen).ToList();
        var customPieces = pieces.Where(p => p.Type == PieceType.CustomChessPiece).ToList();

        if (queens.Any())
        {
            Console.WriteLine("\nQueens:");
            foreach (var queen in queens)
            {
                Console.WriteLine($"({queen.Row}, {queen.Column})");
            }
        }

        if (customPieces.Any())
        {
            Console.WriteLine($"\n{customPieceName}s:");
            foreach (var customPiece in customPieces)
            {
                Console.WriteLine($"({customPiece.Row}, {customPiece.Column})");
            }
        }
    }
}