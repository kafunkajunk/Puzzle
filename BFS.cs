using System.Diagnostics;
using System.Collections.Generic;
using System;
namespace a1{

  public class BreadthFirst : Algorithm {

    public Queue<State> OpenSet = new Queue<State>();
    public Dictionary<string,State> ClosedSet = new Dictionary<string,State>();

    public override void Execute(){
#if DEBUG

      Console.WriteLine("Beginning Algorithm");
      Console.WriteLine("Enqueuing initial state: {0}",CurrentState.Key);
#endif
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

	    Console.WriteLine("Move list: ");
	    var move = moves.First;
	    for(var i = 0; i < 100;i++){

	      Console.WriteLine( move.Value );

	      move = move.Next;

	    }
	    if(moves.Count > 100) Console.WriteLine("More than 100 ...");

	    Console.WriteLine("Goal: ");
	    new State(GlobalVar.GOAL).Format();
#if DEBUG
	    Console.WriteLine("Goal found");
#endif
	    break; 

	  } else {

	    //if not goal state, find each state accessible from current state (children)
	    //check if they are already present in the closed set if so ignore them 
	    var index = CurrentState.GetIndexOfHole();

#if DEBUG
	    Console.WriteLine("index of hole: {0}", index);
#endif
	    var list = BuildChildren(index);

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
	      moves.AddLast(item);
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

    ///<summary>
    ///Builds child nodes of the given hole index.
    ///</summary>

    public List<State> BuildChildren(int index){


      var rv = new List<State>();
      /* so we're going to get all of the children nodes from the current node
	 each one of these is a state that the current state transition to. The number of children change based on the current index of the hole.
	 */
      switch(index){

	case 0: // if index is 0, we can move down or left from the perspective of the hole.
	  /*
	     x,1,2
	     3,4,5
	     6,7,8
	     */
	  rv.Add(new State(CurrentState.Swap(0,3)));
	  rv.Add(new State(CurrentState.Swap(0,1)));
	  break;
	case 1: // if index is 1, three different positions are available, left, right, and down.
	  /*
	     1,x,2
	     3,4,5
	     6,7,8
	     */
	  rv.Add(new State(CurrentState.Swap(0,1))); //move the hole left
	  rv.Add(new State(CurrentState.Swap(1,2))); // move the hole right 
	  rv.Add(new State(CurrentState.Swap(1,4))); // move the hole down
	  break;
	case 2: // if index is 2, the hole is in the right corner, two moves available.
	  /*
	     1,2,x
	     3,4,5
	     6,7,8
	     */

	  rv.Add(new State(CurrentState.Swap(2,1))); //move the hole left
	  rv.Add(new State(CurrentState.Swap(2,5))); // move the hole right 
	  break;

	case 3: //if index is 3, the hole is in the second row, leftmost position.
	  /*
	     1,2,3
	     x,4,5
	     6,7,8
	     */

	  rv.Add(new State(CurrentState.Swap(3,0)));//move hole up
	  rv.Add(new State(CurrentState.Swap(3,4)));//move hole right
	  rv.Add(new State(CurrentState.Swap(3,6)));//move hole down
	  break;

	case 4:
	  /* 1,2,3
	     4,x,5
	     6,7,8
	     */
	  rv.Add(new State(CurrentState.Swap(4,1)));//move hole up
	  rv.Add(new State(CurrentState.Swap(4,3)));//move hole left
	  rv.Add(new State(CurrentState.Swap(4,5)));//move hole right
	  rv.Add(new State(CurrentState.Swap(4,7)));//move hole down

	  break;
	case 5:
	  /* 1,2,3
	     4,5,x
	     6,7,8
	     */
	  rv.Add(new State(CurrentState.Swap(5,2)));//move hole up
	  rv.Add(new State(CurrentState.Swap(5,4)));//move hole left
	  rv.Add(new State(CurrentState.Swap(5,8)));//move hole down
	  break;
	case 6:
	  /* 1,2,3
	     4,5,6
	     x,7,8
	     */
	  rv.Add(new State(CurrentState.Swap(6,3)));//move hole up
	  rv.Add(new State(CurrentState.Swap(6,7)));//move hole right
	  break;
	case 7:
	  /* 1,2,3
	     4,5,6
	     7,x,8
	     */
	  rv.Add(new State(CurrentState.Swap(7,4)));//move hole up
	  rv.Add(new State(CurrentState.Swap(7,6)));//move hole left
	  rv.Add(new State(CurrentState.Swap(7,8)));//move hole right
	  break;
	case 8:
	  /* 1,2,3
	     4,5,6
	     7,8,x
	     */
	  rv.Add(new State(CurrentState.Swap(8,5)));//move hole up
	  rv.Add(new State(CurrentState.Swap(8,7)));//move hole left
	  break;
	default:
	  throw new Exception(string.Format("index was out of range {0}", index));
      }
      return rv;
    }
  }
}
