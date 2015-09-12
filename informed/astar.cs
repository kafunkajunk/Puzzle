using System.Collections.Generic;
using System;
using System.Diagnostics;
namespace a1{
  using queue = Priority_Queue.HeapPriorityQueue<PriorityQueueState>;

  public class astar : Algorithm {

    public queue openset = new queue(362880); //9! = 362880 I wouldn't think we'd ever have an open set bigger than this.
    //public HashSet<string> closed_set = new HashSet<string>();
    public Dictionary<string,string> parents = new Dictionary<string,string>();
    public static Dictionary<string,int> cost_so_far = new Dictionary<string,int>();


    public astar(int[] arr){

      CurrentState = new State(arr);
    }

    public override void Execute(){

      openset.Enqueue(new PriorityQueueState(CurrentState),0);
      cost_so_far.Add(CurrentState.Key, 0);
      watch = new Stopwatch();
      watch.Start();
      while(openset.Count > 0){

	var currentnode = openset.Dequeue();
	CurrentState = currentnode.NodeState;
	if(CurrentState.IsEqualToGoal()){
	  Console.WriteLine("found solution");
	  return;

	} else { 

	  var children = CurrentState.BuildChildren();

	  foreach(var child in children){
	    var new_cost = cost_so_far[CurrentState.Key] + 1; //1 refers to the distance between the current node and its childrens 
	    if(!cost_so_far.ContainsKey(child.Key) || new_cost < cost_so_far[child.Key]){

	      cost_so_far[child.Key] = new_cost;
	      int priority = new_cost + ManVal(child.StateArray);
	      openset.Enqueue(new PriorityQueueState(child),(double)priority);

	      if(!parents.ContainsKey(child.Key)) parents[child.Key] = CurrentState.Key;

	    }
	  }
	}

      }

    }


    public static int Hueristics(State state){
      // f(node) = g(node) + h(node)
      //1. compute the amount to reach current state, done by looking at costSoFar map. ( function g )
      //2. compute from current to goal based on how many nodes in the state are out of place from the goal. ( function h )


      return astar.cost_so_far[state.Key] + compute_h_func(state);

    }

    public static int compute_h_func(State state){
      var val = 0;
      for(int i = 0; i <  state.StateArray.Length; i++){
	var citem = state.StateArray[i];
	var gitem = GlobalVar.GOAL[i];

	if(citem != gitem) val++;
      }
      return val;
    }
    public static int ManVal(int[] arr){

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
  }
  public class PriorityQueueState : Priority_Queue.PriorityQueueNode {
    public State NodeState { get; private set;}
    public PriorityQueueState(State state){
      NodeState = state;
    }
  }

}
