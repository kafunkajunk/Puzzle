using System.Diagnostics;
using System.Collections.Generic;
namespace a1{
  
  public class BreadthFirst : Algorithm {
    
    public Queue<State> OpenSet = new Queue<State>();
    public HashSet<State> ClosedSet = new HashSet<State>();
    public override void Execute(){
      
      watch = new Stopwatch();
      watch.Start();

      OpenSet.Enqueue(CurrentState);
      while(OpenSet.Count > 0){


	var index = CurrentState.GetIndexOfHole();

	//so we're going to get all of the children nodes from the current node
	/*
	  o
	   ⃓
	 / ⃓\
	o o o -> each one of these is a state that the current state transition to. The number of children change based on the current index of the hole.
	*/

        switch(index){

	  case 0: // if index is 0, we can move down or left from the perspective of the hole.
	    OpenSet.Enqueue(new State(CurrentState.Swap(0,3)));
	    OpenSet.Enqueue(new State(CurrentState.Swap(0,1)));
	    break;
	  case 1: // if index is 1, three different positions are available, left, right, and down.
	    OpenSet.Enqueue(new State(CurrentState.Swap(0,1))); //move the hole left
	    OpenSet.Enqueue(new State(CurrentState.Swap(1,2))); // move the hole right 
	    OpenSet.Enqueue(new State(CurrentState.Swap(1,4))); // move the hole down
	    break;
	  case 2: // if index is 2, the hole is in the right corner, two moves available.

	    OpenSet.Enqueue(new State(CurrentState.Swap(2,1))); //move the hole left
	    OpenSet.Enqueue(new State(CurrentState.Swap(2,5))); // move the hole right 
	    break;

	  case 3:
	    
	    break;
	}

	break;

	  //to move down, swap index 0 with index 4;
	  
	}
      

      watch.Stop();

      System.Console.WriteLine("Elapsed time: {0}",watch.Elapsed.Seconds);
    }
  }
}
