namespace a1{
  
  ///<summary>
  /// Main Class. Composed of algorithm and ...
  ///</summary>
  public class Puzzle<T> where T : Algorithm, new(){
    
    // Implementation of search algorithm. Could be uninformed, BFS, DFS, etc.
    public T iObj;
    
    //starting state of puzzle.
    public State start;

    public Puzzle( int[] input ){
      
      start = new State(input);
      iObj = new T();
      iObj.CurrentState = start;
    }
    public void Begin(){

      iObj.Execute();

    }
  }
}
