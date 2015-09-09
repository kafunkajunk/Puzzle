namespace a1{
  
  ///<summary>
  /// Main Class. Composed of algorithm and ...
  ///</summary>
  public class Puzzle {
    
    // Implementation of search algorithm. Could be uninformed, BFS, DFS, etc.
    public Algorithm aObj;

    public Puzzle( Algorithm obj){
      
      aObj = obj;
      
    }
    public void Begin(){

      aObj.Execute();

    }
  }
}
