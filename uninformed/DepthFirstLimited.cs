using System.Collections.Generic;
using System.Diagnostics;
using System;
namespace a1{

  public class DepthFirstLimited : Algorithm {
    public const int MAX_DEPTH = 100;
    public Dictionary<string,State> parents = new Dictionary<string,State>();
    public Dictionary<string, State> ClosedSet = new Dictionary<string, State>();

    /* from wikipedia
       DLS(node, goal, depth) {
       if ( depth >= 0 ) {
       if ( node == goal )
       x=goal if ( goal=depth ) 
       return node

       for each child in expand(node)
       DLS(child, goal, depth-1)
       }
       }
       */
    public DepthFirstLimited(int[] arr){
      CurrentState = new State(arr);
    }
    public override void Execute(){

      watch = new Stopwatch();
      watch.Start();

      Console.WriteLine("Beginning Depth Limited Search");
      Console.WriteLine("Inital State: ");
      CurrentState.Format();

      var result = Recursive_Depth(CurrentState,MAX_DEPTH);
      if(result == false){
	Console.WriteLine("No solution found.");
      }

      Console.WriteLine("Elapsed time: {0} ms", watch.Elapsed.Milliseconds);

    }
    private bool Recursive_Depth(State state, int depth){
      if( depth > -1 ){

	if(!ClosedSet.ContainsKey(state.Key)) ClosedSet.Add(state.Key,state); 

	if( state.IsEqualToGoal() ){
	  SolutionFound(state);
	  return true;

	} else {

	  var children = state.BuildChildren();

	  foreach(var child in children){
	    if(!ClosedSet.ContainsKey(child.Key)){

	      if(!parents.ContainsKey(child.Key)) parents.Add(child.Key,state);

	      var result = Recursive_Depth(child, depth - 1);

	      if(result) return result;
	    }
	  }
	}
	depth += 1;
	return false;
      }else{
	depth+=1;
	return false;
      }
    }
    public void SolutionFound(State FinalState){
      var temp = FinalState;

      while(true){

	if(!parents.ContainsKey(temp.Key)) break;

	moves.AddFirst(parents[temp.Key]);
	temp = parents[temp.Key];
      }

      Console.WriteLine("Move list: ");
      var move = moves.First;
      int count = 0;
      while(move != null) {

	Console.WriteLine( move.Value );
	move = move.Next;

	if(count++ > 100) break;

      }
      if(moves.Count > 100) Console.WriteLine("More than 100 ...");
      Console.WriteLine("Goal: ");
      new State(GlobalVar.GOAL).Format();
    }
  }
}

