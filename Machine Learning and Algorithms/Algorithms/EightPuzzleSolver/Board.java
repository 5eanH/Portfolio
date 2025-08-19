import java.util.ArrayList;
import java.util.Arrays;
import java.util.List;

public class Board {
	
	private final int[][] tiles;
	private int n;
	private int blankRow;
	private int blankCol;
	
    // create a board from an n-by-n array of tiles,
    // where tiles[row][col] = tile at (row, col)
    public Board(int[][] tiles){
		
		this.n = tiles.length;
		this.tiles = new int[n][n];
		
		// Copy the tiles and find blank tile position
		for(int row = 0; row < n; row++)
		{
			
			for(int col = 0; col < n; col++)
			{	
				this.tiles[row][col] = tiles[row][col];
				
				// Check if tile is the blank tile
				if(tiles[row][col] == 0)
				{
					blankRow = row;
					blankCol = col;
				}
			}
		}
    }

    // string representation of this board
    public String toString(){
		
		StringBuilder sb = new StringBuilder();
        sb.append(n).append("\n");
		
		// Append tile to string representation
        for (int row = 0; row < n; row++) 
		{
            for (int col = 0; col < n; col++) 
			{
				
                sb.append(tiles[row][col]).append(" ");
				
            }
			
            sb.append("\n");
			
        }
        return sb.toString();
		
    }

    // tile at (row, col) or 0 if blank
    public int tileAt(int row, int col){
		
        return tiles[row][col];
		
    }

    // board size n
    public int n(){
        return n;
		
    }

    // number of tiles out of place
    public int hamming(){
		
        int count = 0;
        int expectedValue = 1;
		
		// Check tiles and count no. of tiles out of place
        for (int row = 0; row < n; row++) 
		{
			
            for (int col = 0; col < n; col++) 
			{
				
                if (tiles[row][col] != expectedValue && tiles[row][col] != 0) 
				{
					
                    count++;
					
                }
                expectedValue++;
				
            }
        }
        return count;
		
    }

    // sum of Manhattan distances between tiles and goal
    public int manhattan(){
		
        int distance = 0;
		
        for (int row = 0; row < n; row++) 
		{
			
			// Calculate the Manhattan distance for each tile
            for (int col = 0; col < n; col++) 
			{
				
                int tileValue = tiles[row][col];
				
                if (tileValue != 0) 
				{
					
                    int targetRow = (tileValue - 1) / n;
                    int targetCol = (tileValue - 1) % n;
                    distance += Math.abs(row - targetRow) + Math.abs(col - targetCol);
					
                }
            }
        }
        return distance;
		
    }

	// is this board the goal board?
    public boolean isGoal(){
		
        int expectedValue = 1;
        
		// Check each tile and compare with the expected value
		// Last tile should be the blank tile
		for (int row = 0; row < n; row++) 
		{
			
            for (int col = 0; col < n; col++) 
			{
				
                if (tiles[row][col] != expectedValue && !(row == n - 1 && col == n - 1)) 
				{
                    
					return false;
                }
				
                expectedValue++;
            }
        }
        return true;
		
    }

    // does this board equal y?
    public boolean equals(Object y){
		
        if (this == y) return true;
        if (y == null || getClass() != y.getClass()) return false;

        Board other = (Board) y;
		
		// Use deepEquals to compare the tile arrays
        return Arrays.deepEquals(tiles, other.tiles);
		
    }

    // all neighboring boards
    public Iterable<Board> neighbors(){
		
		List<Board> neighbors = new ArrayList<>();
		
		// Generate neighboring boards by swapping the blank tile with its neighbors
		
        if (isIndexValid(blankRow - 1, blankCol)) 
		{
            neighbors.add(exchangeTiles(blankRow, blankCol, blankRow - 1, blankCol));
        }
        if (isIndexValid(blankRow + 1, blankCol)) 
		{
            neighbors.add(exchangeTiles(blankRow, blankCol, blankRow + 1, blankCol));
        }
        if (isIndexValid(blankRow, blankCol - 1)) 
		{
            neighbors.add(exchangeTiles(blankRow, blankCol, blankRow, blankCol - 1));
        }
        if (isIndexValid(blankRow, blankCol + 1)) 
		{
            neighbors.add(exchangeTiles(blankRow, blankCol, blankRow, blankCol + 1));
        }

        return neighbors;
		
    }
	
	// Check if an index is valid
	private boolean isIndexValid(int row, int col) 
	{
		
        return row >= 0 && row < n && col >= 0 && col < n;
		
    }
	// Exchange tiles and create a new board
	private Board exchangeTiles(int row1, int col1, int row2, int col2) 
	{
	
        int[][] newTiles = copyTiles();
        int temp = newTiles[row1][col1];
		
        newTiles[row1][col1] = newTiles[row2][col2];
        newTiles[row2][col2] = temp;

        return new Board(newTiles);
		
    }
	
	// Create copy of the tile array
	private int[][] copyTiles() 
	{
		
        int[][] newTiles = new int[n][n];
		
        for (int row = 0; row < n; row++) 
		{
			
            System.arraycopy(tiles[row], 0, newTiles[row], 0, n);
			
        }
		
        return newTiles;
		
    }
	
	
	
    // unit testing (required)
    public static void main(String[] args){
		
        int[][] tiles = { {0, 1, 3}, {4, 2, 5}, {7, 8, 6} }; // puzzle04
        Board board = new Board(tiles);
		
        // Test each Board method
		
		System.out.println("");
		System.out.println("Initial Board: ");
        System.out.println(board);

        System.out.println("Tile at (0, 0): " + board.tileAt(0, 0));
        System.out.println("Board size: " + board.n());
        System.out.println("Hamming dist: " + board.hamming());
        System.out.println("Manhattan dist: " + board.manhattan());
        System.out.println("Is goal: " + board.isGoal());
        System.out.println("Equals board: " + board.equals(new Board(tiles)));

		System.out.println("");

        System.out.println("Neighboring boards: ");
		
		System.out.println("");
		
        for (Board neighbor : board.neighbors()) 
		{
            System.out.println(neighbor);
        }
		
    }
}