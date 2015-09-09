using System.Diagnostics;
using System.Collections.Generic;
using System;
namespace a1{

  public class BreadthFirst : Algorithm {

    public Queue<State> OpenSet = new Queue<State>();
    public Dictionary<string,State> ClosedSet = new Dictionary<string,State>();
    public Dictionary<string,State> parents = new Dictionary<string,State>();

    public BreadthFirst(int[] arr){
      CurrentState = new State(arr);
    }

    public override void Execute(){

      Console.WriteLine("Beginning execution for BFS");
      Console.WriteLine("Initial State:");
      CurrentState.Format();
      watch = new Stopwatch();
      watch.Start();

      OpenSet.Enqueue(CurrentState);
      try{
	while(OpenSet.Count > 0){ //while openset is not empty

	  //grab a state from the open set
	  CurrentState = OpenSet.Dequeue();

#if DEBUG
	  Console.WriteLine("Removed from queue: {0}",CurrentState);
#endif

	  //put current node into the closed set
	  if(ClosedSet.ContainsKey(CurrentState.Key) == false){
	    ClosedSet.Add(CurrentState.Key,CurrentState);
	  }

	  //check if current node is goal state
	  if(CurrentState.IsEqualToGoal()){

	    watch.Stop();

	    Console.WriteLine("Solution found");
	    Console.WriteLine("Move list: ");
	    SolutionFound(CurrentState);
	    var move = moves.First;
	    do{

	      Console.WriteLine( move.Value );
	      move = move.Next;
	    }
	    while(move != null);


	    if(moves.Count > 100) Console.WriteLine("More than 100 ...");
	    new State(GlobalVar.GOAL).Format();

	    Console.ReadLine();
	    break; 

	  } else {

	    //if not goal state, find each state accessible from current state (children)
	    //check if they are already present in the closed set if so ignore them 

	    var list = CurrentState.BuildChildren();

#if DEBUG 
	    Console.WriteLine("Children of node: {0}", CurrentState);
	    foreach(var item in list){

	      item.Format();
	      Console.WriteLine();
	    }

#endif 
	    foreach(var item in list){
	      if(ClosedSet.ContainsKey(item.Key)){ 
#if DEBUG 
		Console.WriteLine("ClosedSet already contains key {0}", item.Key);
#endif
		continue;
	      }
	      OpenSet.Enqueue(item); // if they aren't present add them to the queue. 

	      if(!parents.ContainsKey(item.Key)) parents.Add(item.Key,CurrentState);

	    }
	  }

	  //if openset is empty and we never found the goal, there is no solution.
	  if(OpenSet.Count == 0){
	    throw new Exception("no solution");
	  }

	}
      }catch(ArgumentException ex){
	Console.WriteLine(ex.Message + ex.StackTrace);
	Console.WriteLine("Count: {0}",moves.Count);
      }

      System.Console.WriteLine("Elapsed time: {0} ms", watch.Elapsed.Milliseconds);
    }

    private void SolutionFound(State FinalState){

      //Debugger.Break();
      var temp = FinalState;


      while(true){

	if(!parents.ContainsKey(temp.Key)) break;

	moves.AddFirst(parents[temp.Key]);
	temp = parents[temp.Key];


      }
    }
  }
}
