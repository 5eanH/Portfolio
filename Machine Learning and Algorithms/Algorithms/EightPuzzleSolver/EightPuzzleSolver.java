import java.util.Scanner;
import java.io.File;
import java.io.FileNotFoundException;
import java.util.ArrayList;
import java.util.Arrays;
import java.util.List;
import java.util.PriorityQueue;

public class EightPuzzleSolver {
	
	private final Board initialBoard;
	private State goalState;
	
    // find a solution to the initial board (using the A* algorithm)
    public EightPuzzleSolver(Board initial){
		
		this.initialBoard = initial;
		solve();
		
    }

    // min number of moves to solve initial board
    public int moves(){
		
        if (goalState != null) 
		{
			
            return goalState.moves;
			
        }
        return -1;
		
    }

    // sequence of boards in a shortest solution
    public Iterable<Board> solution(){
		
        if (goalState != null) 
		{
			
            List<Board> solutionPath = new ArrayList<>();
            State currentState = goalState;
			
            while (currentState != null) 
			{
				
                solutionPath.add(0, currentState.board);
                currentState = currentState.previous;
				
            }
            return solutionPath;
			
        }
        return null;
		
    }
	
	// Solve the puzzle using A* algorithm
    private void solve() 
	{
		
        PriorityQueue<State> pq = new PriorityQueue<>();
        pq.offer(new State(initialBoard, null, 0));

        while (!pq.isEmpty()) 
		{
			
            State currentState = pq.poll();
            Board currentBoard = currentState.board;

            if (currentBoard.isGoal()) 
			{
				
                goalState = currentState;
                return;
				
            }

            int currentMoves = currentState.moves;
            Board previousBoard = (currentState.previous != null) ? currentState.previous.board : null;

            for (Board neighbor : currentBoard.neighbors()) 
			{
				
                if (neighbor.equals(previousBoard)) 
				{
                    continue;
                }
				
                pq.offer(new State(neighbor, currentState, currentMoves + 1));
            
			}
        }
    }
	

    // test client (see below)
    public static void main(String[] args) {
		
        int n = 0;
        int[][] tiles = null;

        File file = new File("C:/Users/seanp/OneDrive - Limerick Institute Of Technology/Year 2/Semester 2/algorithm/Ca2/EightPuzzle/puzzle04.txt");
        //File file = new File(args[0]);

        try {
            Scanner sc = new Scanner(file);

            n = sc.nextInt();
            tiles = new int[n][n];

            for (int i = 0; i < n; i++) {
                for (int j = 0; j < n; j++) {
                    tiles[i][j] = sc.nextInt();
                }
            }
        } catch (FileNotFoundException e) {
            e.printStackTrace();
        }

/*        System.out.println(n);
        for (int i = 0; i < n; i++) {
            for (int j = 0; j < n; j++) {
                System.out.print(tiles[i][j]);
            }
            System.out.println();
        }*/

        Board initial = new Board(tiles);
        EightPuzzleSolver solver = new EightPuzzleSolver(initial);

		System.out.println("");

        System.out.println("Minimum No. of moves: " + solver.moves());

		System.out.println("");

		System.out.println("Sequence of boards for Minimum moves:");
        for(Board board : solver.solution())
		{
			
            System.out.println(board);
			
        }
		
		
    }

    public static final class State implements Comparable<State>{
        Board board;        // The Board for this state
        State previous;     // The previous (parent) state
        int priority;           // The priority (heuristic) function
        int moves;          // Moves taken to get to the state

        private State (Board board, State previous, int moves){
            // YOUR CODE HERE
			this.board = board;
			this.previous = previous;
			this.moves = moves;
			// Calculate the priority using the Manhattan distance and no. of moves
			this.priority = moves + board.manhattan();
			
        }

        @Override
        public int compareTo(State other) {
            // YOUR CODE HERE
			
			// Compare states based on their priorities
			return Integer.compare(this.priority, other.priority);
			
        }
    }
}
