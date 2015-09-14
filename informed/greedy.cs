using System.Collections.Generic;
using System;
using System.Diagnostics;
namespace a1{
  using queue = Priority_Queue.HeapPriorityQueue<PriorityQueueState>;

  public class greedy : Algorithm {

    public queue openset = new queue(362880); //9! = 362880 I wouldn't think we'd ever have an open set bigger than this.
    public Dictionary<string,State> parents = new Dictionary<string,State>();

    public greedy(int[] arr){
      CurrentState = new State(arr);
      parents[CurrentState.Key] = null;
    }
    public override void Execute(){

      openset.Enqueue(new PriorityQueueState(CurrentState),0);
      Console.WriteLine("Initial State:");

      CurrentState.Format();

      watch = new Stopwatch();
      watch.Start();
      while(openset.Count > 0){

	var currentnode = openset.Dequeue();
	CurrentState = currentnode.NodeState;
	if(CurrentState.IsEqualToGoal()){
	  watch.Stop();

	  SolutionFound(CurrentState);

	  Console.WriteLine("Elapsed time: {0} ms", watch.Elapsed.Milliseconds);
	  return;

	} else { 

	  var children = CurrentState.BuildChildren();

	  foreach(var child in children){
	    

	    if(!parents.ContainsKey(child.Key)){

	      int priority = ManVal(child.StateArray);
	      openset.Enqueue(new PriorityQueueState(child),(double)priority);
	      parents[child.Key] = CurrentState;

	    }
	  }
	}
      }
    }
    public int ManVal(int[] arr){

      //since we represent our data in an 1D array, the way we compute the manhattan distance can look a little wonky.
      // the index var increments each time through the loop allowing us to grab the appropriate value.
      // this method works by representing each position of the puzzle as x,y coordinate in euclidean space
      // the entire goal state of the puzzle looking like so:
      /* 
	 1     2     3
	 (0,0) (0,1) (0,2) - row of 0
	 4     5     6
	 (1,0) (1,1) (1,2) - row of 1
	 7     8     0
	 (2,0) (2,1) (2,2) - row of 2
	 */

      // lets suppose the puzzle looks like so:
      /*
	 1 0 2 
	 3 4 5
	 7 6 8
	 */

      //as the loop progresses, the vars x and y will represent the correct coordinate value 
      //for the first position of the puzzle, val will be zero and result in h and v being 0.
      //the second position won't be evaluated.
      //the third position will result in 1 % 3 = 1 and 1 / 3 = 0 -> Abs(0 - 0) + Abs(1 - 2) => 1. This is the total amount of distance that the 2 is out of place.
      //same for the fourth postion: 2 % 3 = 2 and 2 / 3 = 0 -> Abs(0 - 1) + Abs(2 - 0) = 3

      int manSum = 0;
      int index = -1;

      for (int y = 0; y < 3; y++) {
	for (int x = 0; x < 3; x++) {
	  index++;
	  int val = (arr[index] - 1);
	  if (val != -1) {
	    int h = val % 3;
	    int v = val / 3;

	    manSum += Math.Abs(v - (y)) + Math.Abs(h - (x));
	  }
	}
      }
      return manSum;
    }
    public void SolutionFound(State FinalState){

      var temp = FinalState;
      while(true){
	if(parents[temp.Key] == null) break;
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


      Console.WriteLine("Number of expanded nodes: {0}", moves.Count);
    }
  }
}
